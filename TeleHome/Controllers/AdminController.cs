using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Data;
using System.Security.Claims;
//using System.Drawing;
using System.Text.Json;
using TeleHome.Models;
//using MozJpegSharp.Encoder;

namespace TeleHome.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RmlubecoTelehomeContext _db;
        private readonly IMediator _mediator;
        public AdminController(RmlubecoTelehomeContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }
        public async Task<string> OptimizeImage(IFormFile file)
        {
            string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
            string filePath = Path.Combine("wwwroot/sekiller", filename);

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                var encoder = new JpegEncoder { Quality = 65 };
                image.Mutate(x => x.Resize(new ResizeOptions { Size = new Size(800, 600), Mode = ResizeMode.Max }));
                image.Save(filePath);
            }

            return filename;
        }
        public string SaveImg(IFormFile file)
        {
            string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
            string filePath = Path.Combine("wwwroot/sekiller", filename);
            using (var image = Image.Load(file.OpenReadStream()))
            {
                image.Save(filePath, new JpegEncoder { Quality = 65 });
            }
            return filename;
        }
        public string SaveImgSS(IFormFile file)
        {
            string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
            using (Stream stream = new FileStream("wwwroot/sekiller/" + filename, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
        public IActionResult Home(int page = 0)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Products.Count() / 10);
            var productList = _db.Products.Include(x => x.ProductDeepCategory).Skip(page * 10).Take(10).OrderByDescending(x => x.ProductDate).ToList();
            ViewBag.ProductList = productList;
            return View();
        }
        public class SearchRequest
        {
            public string Search { get; set; }
            public int Page { get; set; }
        }
        [HttpPost]
        public IActionResult Search([FromBody] SearchRequest searchRequest)
        {
            List<ProductInfo> productInfos = new List<ProductInfo>();

            int totalCount = _db.Products
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Count(x => x.ProductDeepCategory.DeepCategoryName.Contains(searchRequest.Search) ||
                            (string.IsNullOrEmpty(searchRequest.Search) ||
                             x.ProductName.Contains(searchRequest.Search) ||
                             x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryName.Contains(searchRequest.Search) ||
                             x.ProductPrice.ToString().Contains(searchRequest.Search)));

            int pageSize = 3;
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            int skipCount = searchRequest.Page * pageSize;

            productInfos = _db.Products
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Where(x => x.ProductDeepCategory.DeepCategoryName.Contains(searchRequest.Search) ||
                            (string.IsNullOrEmpty(searchRequest.Search) ||
                             x.ProductName.Contains(searchRequest.Search) ||
                             x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryName.Contains(searchRequest.Search) ||
                             x.ProductPrice.ToString().Contains(searchRequest.Search)))
                .Skip(skipCount)
                .Take(pageSize)
                .OrderByDescending(x => x.ProductDate)
                .Select(x => new ProductInfo
                {
                    ProductName = x.ProductName,
                    ProductFirstURL = x.ProductFirstPhotoUrl,
                    ProductId = x.ProductId,
                    ProductDate = x.ProductDate,
                    ProductIsSctock = x.ProductIsSctock,
                    ProductIsActive = x.ProductIsActive,
                    ProductPrice = x.ProductPrice,
                    DeepCategoryName = x.ProductDeepCategory.DeepCategoryName,
                    SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId
                })
                .ToList();

            var response = new
            {
                ProductList = productInfos,
                PageCount = pageCount
            };

            return Ok(response);
        }
        public IActionResult CreateCampaign()
        {
            return View(_db.DeepCategories.ToList());
        }
        public IActionResult Campaigns()
        {
            return View(_db.Campaigns.Include(x => x.CampaignDeepCategory).ToList());
        }
        public IActionResult Contacts()
        {
            return View(_db.Contacts.ToList());
        }
        public IActionResult Categories()
        {
            return View(_db.Categories.ToList());
        }

        public void DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.SingleOrDefault(x => x.CategoryId == id));
            _db.SaveChanges();
        }
        public void DeleteContact(int id)
        {
            _db.Contacts.Remove(_db.Contacts.SingleOrDefault(x => x.ContactId == id));
            _db.SaveChanges();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category, IFormFile filephoto)
        {
            _db.Categories.Add(category);
            category.CategoryIcon = SaveImgSS(filephoto);
            _db.SaveChanges();
            return RedirectToAction("Categories");
        }
        public IActionResult EditCategory(int id)
        {
            return View(_db.Categories.SingleOrDefault(x => x.CategoryId == id));
        }
        [HttpPost]
        public IActionResult EditCategory(int id, Category category, IFormFile filephoto)
        {
            var old = _db.Categories.SingleOrDefault(x => x.CategoryId == id);
            old.CategoryName = category.CategoryName;
            if (filephoto != null)
            {
                old.CategoryIcon = SaveImgSS(filephoto);
            }
            _db.SaveChanges();
            return RedirectToAction("Categories");
        }
        public IActionResult EditCampaign(int id)
        {
            ViewBag.DeepCategory = new SelectList(_db.DeepCategories.ToList(), "DeepCategoryId", "DeepCategoryName");
            return View(_db.Campaigns.SingleOrDefault(x => x.CampaignId == id));
        }
        [HttpPost]
        public async Task<IActionResult> EditCampaign(int id, Campaign campaign, IFormFile formFile)
        {
            var oldcamp = _db.Campaigns.SingleOrDefault(x => x.CampaignId == id);
            oldcamp.CampaignDescription = campaign.CampaignDescription;
            oldcamp.CampaignStartDate = campaign.CampaignStartDate;
            oldcamp.CampaignEndDate = campaign.CampaignEndDate;
            oldcamp.CampaignName = campaign.CampaignName;
            oldcamp.CampaignDeepCategoryId = campaign.CampaignDeepCategoryId;
            if (formFile != null)
            {
                oldcamp.CampaignImage = await OptimizeImage(formFile);
            }
            _db.SaveChanges();
            return RedirectToAction("Campaigns");
        }
        public void DeleteCampaign(int id)
        {
            _db.Campaigns.Remove(_db.Campaigns.SingleOrDefault(x => x.CampaignId == id));
            _db.SaveChanges();
        }
        [HttpPost]
        public IActionResult CreateCampaign(Campaign campaign, IFormFile formFile)
        {
            _db.Campaigns.Add(campaign);
            campaign.CampaignImage = SaveImg(formFile);
            _db.SaveChanges();
            _mediator.Publish(new CampaignCreatedEvent { Campaign = campaign });
            return RedirectToAction("Campaigns");
        }

        public IActionResult EditProduct(int id)
        {
            var product = _db.Products.Include(x => x.ProductDeepCategory).ThenInclude(x => x.DeepCategorySubCategory)
                             .Include(p => p.ImagesPhs)
                             .Include(p => p.TechCharacteristicsContents)
                             .ThenInclude(x => x.TechCharacteristicsContentTechCharacteristics)
                             .Include(p => p.ProductColors)
                             .ThenInclude(pc => pc.ProductColorColor)
                             .FirstOrDefault(p => p.ProductId == id);

            ViewBag.colors = _db.ColorsPrs.ToList();
            ViewBag.sub = _db.SubCategories.ToList();
            ViewBag.tech = _db.TechCharacteristics.ToList();
            ViewBag.Deeb = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == product.ProductDeepCategory.DeepCategorySubCategoryId).ToList();
            var viewModel = new EditProductViewModel
            {
                Products = product,
                DeepCategory = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == product.ProductDeepCategory.DeepCategorySubCategoryId).ToList(),
                Image = product.ImagesPhs.ToList(),
                TechCharacteristicsContent = product.TechCharacteristicsContents.ToList()
            };
            return View(product);
        }
        public IActionResult ChooseSubCategory()
        {
            return View(_db.SubCategories.ToList());
        }
        public IActionResult AddTech()
        {
            return View(_db.SubCategories.ToList());
        }
        [HttpPost]
        public IActionResult AddTech(TechCharacteristic techCharacteristic)
        {
            _db.TechCharacteristics.Add(techCharacteristic);
            _db.SaveChanges();
            return RedirectToAction("AddTech");
        }
        public IActionResult Techs()
        {
            return View(_db.TechCharacteristics.ToList());
        }
        public IActionResult TechEdit(int id)
        {
            ViewBag.sub = _db.SubCategories.ToList();
            return View(_db.TechCharacteristics.SingleOrDefault(x => x.TechCharacteristicId == id));
        }
        [HttpPost]
        public IActionResult TechEdit(TechCharacteristic techCharacteristic)
        {
            TechCharacteristic t = _db.TechCharacteristics.SingleOrDefault(x => x.TechCharacteristicId == techCharacteristic.TechCharacteristicId);
            t.TechCharacteristicName = techCharacteristic.TechCharacteristicName;
            t.TechCharacteristicSubCategory = techCharacteristic.TechCharacteristicSubCategory;

            _db.TechCharacteristics.Update(t);
            _db.SaveChanges();
            return RedirectToAction("Techs");
        }

        public IActionResult TechDelete(int id)
        {
            var techCharacteristic = _db.TechCharacteristics.SingleOrDefault(x => x.TechCharacteristicId == id);

            if (techCharacteristic == null)
            {
                return NotFound();
            }

            var dependentContents = _db.TechCharacteristicsContents
     .Where(tcc => tcc.TechCharacteristicsContentTechCharacteristicsId == id)
     .ToList();

            _db.TechCharacteristicsContents.RemoveRange(dependentContents);

            // Now delete the parent TechCharacteristic
            _db.TechCharacteristics.Remove(techCharacteristic);
            _db.SaveChanges();
            return RedirectToAction("Techs");
        }
        public IActionResult BigCampaigns()
        {
            var sl = _db.BigCampaigns.ToList();

            return View(sl);
        }
        public IActionResult AddBigCampaign()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBigCampaign(BigCampaign bigCampaign, IFormFile BigCampaignPicture)
        {
            string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(BigCampaignPicture.FileName);
            if (BigCampaignPicture != null)
            {
                using (Stream stream = new FileStream("wwwroot/BigCompany/" + filename, FileMode.Create))
                {
                    BigCampaignPicture.CopyTo(stream);
                }
                bigCampaign.BigCampaignPicture = filename;
            }
            BigCampaign b = new BigCampaign();
            b.BigCampaignPicture = filename;

            _db.BigCampaigns.Add(b);
            _db.SaveChanges();
            return View();
        }

        public async Task<IActionResult> DeleteBigCampaign(int? id)
        {
            if (id is null) return BadRequest();
            var bigCampaign = await _db.BigCampaigns.FirstOrDefaultAsync(x => x.BigCampaignId == id);
            if (bigCampaign is null) return NotFound();

            _db.BigCampaigns.Remove(bigCampaign);
            await _db.SaveChangesAsync();

            if (!string.IsNullOrEmpty(bigCampaign.BigCampaignPicture))
            {
                string imagePath = Path.Combine("wwwroot/BigCompany/", bigCampaign.BigCampaignPicture);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            return RedirectToAction(nameof(BigCampaigns));
        }

        public IActionResult AddDeepCategory()
        {
            return View(_db.SubCategories.ToList());
        }
        [HttpPost]
        public IActionResult AddDeepCategory(DeepCategory deepCategory)
        {
            _db.DeepCategories.Add(deepCategory);
            _db.SaveChanges();
            return RedirectToAction("AddDeepCategory");
        }
        public IActionResult AddSubCategory()
        {
            return View(_db.Categories.ToList());
        }
        public IActionResult EditSubCategory(int id)
        {
            ViewBag.Kateqoriyalar = _db.Categories.ToList();
            return View(_db.SubCategories.SingleOrDefault(x => x.SubCategoryId == id));
        }
        [HttpPost]
        public async Task<IActionResult> EditSubCategory(int id, SubCategory subCategory, IFormFile formFile)
        {
            var oldsub = _db.SubCategories.SingleOrDefault(x => x.SubCategoryId == id);
            if (formFile != null)
            {
                oldsub.SubCategoryPhotoUrl = await OptimizeImage(formFile);
            }
            oldsub.SubCategoryName = subCategory.SubCategoryName;
            oldsub.SubCategoryCategoryId = subCategory.SubCategoryCategoryId;
            _db.SaveChanges();
            return RedirectToAction("ChooseSubCategory");
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategory subCategory, IFormFile formFile)
        {
            _db.SubCategories.Add(subCategory);
            subCategory.SubCategoryPhotoUrl = await OptimizeImage(formFile);
            subCategory.SubCategoryIsActive = false;
            _db.SaveChanges();
            return RedirectToAction("ChooseSubCategory");
        }
        public void DeleteProduct(int productId)
        {
            Product p = _db.Products.SingleOrDefault(x => x.ProductId == productId);
            p.ProductIsActive = !p.ProductIsActive;
            _db.SaveChanges();
        }
        public void DeleteTchContent(int tchid)
        {
            var t = _db.TechCharacteristicsContents.SingleOrDefault(x => x.TechCharacteristicsContentId == tchid);
            _db.TechCharacteristicsContents.Remove(t);
            _db.SaveChanges();
        }
        public IActionResult DeletePhoto(int id)
        {
            var hphoto = _db.ImagesPhs.SingleOrDefault(x => x.ImageId == id);
            _db.ImagesPhs.Remove(hphoto);
            _db.SaveChanges();
            return Ok(id);
        }
        public void RecyleProduct(int productId)
        {
            Product p = _db.Products.SingleOrDefault(x => x.ProductId == productId);
            p.ProductIsSctock = !p.ProductIsSctock;
            _db.SaveChanges();
        }
        public void IsCategoryActive(int productId)
        {
            SubCategory p = _db.SubCategories.SingleOrDefault(x => x.SubCategoryId == productId);
            p.SubCategoryIsActive = !p.SubCategoryIsActive;
            _db.SaveChanges();
        }
        public ActionResult<IEnumerable<DeepCategory>> GetCateogry(int subCategoryId)
        {
            var data = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == subCategoryId).ToList();
            return Json(data);
        }
        public ActionResult<IEnumerable<TechCharacteristic>> GetTech(int subCategoryId)
        {
            var data = _db.TechCharacteristics.Where(x => x.TechCharacteristicSubCategoryId == subCategoryId).ToList();
            return Json(data);
        }
        [HttpGet]
        public IActionResult NewProduct()
        {
            ViewBag.Color = _db.ColorsPrs.ToList();
            ViewBag.tech = _db.TechCharacteristics.ToList();
            var sub = _db.SubCategories.FirstOrDefault().SubCategoryId;
            ViewBag.sub = _db.SubCategories.ToList();
            ViewBag.deep = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == sub).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewProduct(Product product, IFormFile firstFile, IFormFile[] nextFile, string[] fileName, string[] tch, string[] tchcontent, int[] clrs)
        {
            if (product.ProductDeepCategoryId == null)
            {
                ViewBag.Color = _db.ColorsPrs.ToList();
                ViewBag.tech = _db.TechCharacteristics.ToList();
                ViewBag.sub = _db.SubCategories.ToList();
                ViewBag.deep = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == 1).ToList();
                ModelState.AddModelError("ProductDeepCategoryId", "Şəkil mütləqdir...");
                return View("NewProduct", product);
            }
            if (firstFile == null)
            {
                ViewBag.Color = _db.ColorsPrs.ToList();
                ViewBag.tech = _db.TechCharacteristics.ToList();
                ViewBag.sub = _db.SubCategories.ToList();
                ViewBag.deep = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == 1).ToList();
                ModelState.AddModelError("ProductFirstPhotoUrl", "Şəkil mütləqdir...");
                return View("NewProduct", product);
            }
            product.ProductIsActive = true;
            product.ProductIsSctock = true;
            product.ProductFirstPhotoUrl = await OptimizeImage(firstFile);
            product.ProductDate = DateTime.Now;
            _db.Products.Add(product);
            _db.SaveChanges();
            foreach (var item in clrs)
            {
                ProductColor productColor = new ProductColor();
                productColor.ProductColorProductId = product.ProductId;
                productColor.ProductColorColorId = item;
                _db.ProductColors.Add(productColor);
                _db.SaveChanges();
            }

            foreach (var item in tch.Select((value, index) => new { Value = value, Index = index }))
            {
                TechCharacteristicsContent techCharacteristicsContent = new TechCharacteristicsContent();
                techCharacteristicsContent.TechCharacteristicsContentProductId = product.ProductId;
                techCharacteristicsContent.TechCharacteristicsContentTechCharacteristicsId = int.Parse(item.Value);
                techCharacteristicsContent.TechCharacteristicsContentTitle = tchcontent[item.Index];
                _db.TechCharacteristicsContents.Add(techCharacteristicsContent);
                _db.SaveChanges();
            }

            var files = fileName.Distinct().ToList();
            foreach (IFormFile file in nextFile)
            {
                if (files.Contains(file.FileName))
                {
                    ImagesPh image = new ImagesPh();
                    image.ImageProductId = product.ProductId;
                    image.ImageUrl = await OptimizeImage(file);
                    _db.ImagesPhs.Add(image);
                    _db.SaveChanges();
                    files.Remove(file.FileName);
                }
            }
            return RedirectToAction("Home");
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, int id, IFormFile firstFile, IFormFile[] nextFile, string[] fileName, string[] tch, string[] tchcontent, int[] clrs)
        {
            Product p = _db.Products.Include(x => x.ImagesPhs).SingleOrDefault(x => x.ProductId == id);
            if (firstFile != null)
            {
                p.ProductFirstPhotoUrl = await OptimizeImage(firstFile);
            }
            p.ProductPrice = product.ProductPrice;
            p.ProductWithoutPrice = product.ProductWithoutPrice;
            p.ProductDescription = product.ProductDescription;
            p.ProductName = product.ProductName;
            p.ProductDeepCategoryId = product.ProductDeepCategoryId;
            _db.SaveChanges();

            var removeColor = _db.ProductColors.Where(x => x.ProductColorProductId == id);
            _db.ProductColors.RemoveRange(removeColor);
            _db.SaveChanges();

            foreach (var item in clrs)
            {
                ProductColor productColor = new ProductColor();
                productColor.ProductColorProductId = id;
                productColor.ProductColorColorId = item;
                _db.ProductColors.Add(productColor);
                _db.SaveChanges();
            }
            var removeid = _db.TechCharacteristicsContents.Where(x => x.TechCharacteristicsContentProductId == id);
            if (removeid != null)
            {
                _db.TechCharacteristicsContents.RemoveRange(removeid);
                _db.SaveChanges();
            }

            foreach (var item in tch.Select((value, index) => new { Value = value, Index = index }))
            {
                TechCharacteristicsContent techCharacteristicsContent = new TechCharacteristicsContent();
                techCharacteristicsContent.TechCharacteristicsContentProductId = id;
                techCharacteristicsContent.TechCharacteristicsContentTechCharacteristicsId = int.Parse(item.Value);
                techCharacteristicsContent.TechCharacteristicsContentTitle = tchcontent[item.Index];
                _db.TechCharacteristicsContents.Add(techCharacteristicsContent);
                _db.SaveChanges();
            }

            var files = fileName.Distinct().ToList();
            foreach (IFormFile file in nextFile)
            {
                if (files.Contains(file.FileName))
                {
                    ImagesPh image = new ImagesPh();
                    image.ImageProductId = id;
                    image.ImageUrl = await OptimizeImage(file);
                    _db.ImagesPhs.Add(image);
                    _db.SaveChanges();
                    files.Remove(file.FileName);
                }
            }
            return RedirectToAction("Home");
        }
        //    public IActionResult FilterRedirect(int offset, int limit = 3, string search = null)
        //    {
        //        int? userId = null;
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        //        }
        //        var query = _db.Products
        //            .Include(x => x.Favorites)
        //            .Include(x => x.Baskets)
        //            .Include(x => x.ProductDeepCategory)
        //            .ThenInclude(x => x.DeepCategorySubCategory)
        //            .Where(x => x.ProductIsActive == true &&
        //                        (string.IsNullOrEmpty(search) ||
        //                         x.ProductName.Contains(search) ||
        //                         x.ProductDeepCategory.DeepCategoryName.Contains(search) ||
        //                         x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryName.Contains(search)
        //                         ));
        //        var count = query.Count();
        //        var result = query
        //            .Take(limit)
        //.Skip(offset * 3)
        //            .Select(x => new ProductInfo
        //            {
        //                ProductId = x.ProductId,
        //                ProductFirstURL = x.ProductFirstPhotoUrl,
        //                ProductIsSctock = x.ProductIsSctock,
        //                ProductPrice = x.ProductPrice,
        //                ProductName = x.ProductName,
        //                SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategoryId,
        //                IsInBasket = x.Baskets.Any(b => b.BasketUserId == userId),
        //                IsInFavorite = x.Favorites.Any(b => b.FavoriteUserId == userId),
        //                Cap = count
        //            })
        //            .AsNoTracking()
        //            .ToList();

        //        return View(result);
        //    }
        public class ProductInfo
        {
            public int ProductId { get; set; }
            public string ProductFirstURL { get; set; }
            public bool? ProductIsSctock { get; set; }
            public double? ProductPrice { get; set; }
            public string ProductName { get; set; }
            public DateTime? ProductDate { get; set; }
            public bool? ProductIsActive { get; set; }

            public string DeepCategoryName { get; set; }
            public int? SubCategoryId { get; set; }
            public int Cap { get; set; }
            //public List<TechCharacteristicsContent> TechCharacteristicsContents { get; set; }
        }

        public IActionResult OrderList()
        {
            return View();
        }



        public IActionResult AddBrands()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBrands(UsedBrand brands, IFormFile BrandsPhoto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (BrandsPhoto != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(BrandsPhoto.FileName);
                using (Stream stream = new FileStream("wwwroot/img/" + filename, FileMode.Create))
                {
                    BrandsPhoto.CopyTo(stream);
                }
                brands.BrandsPhoto = filename;
            }
            _db.UsedBrands.Add(brands);
            _db.SaveChanges();
            return View();
        }


        [HttpGet]
        public IActionResult Orders(int page = 0)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_db.OrderBaskets.Count() / 10);
            List<Order> order = _db.Orders
                              .Where(x => x.OrderBaskets.Any())  // Ensure there is at least one OrderBasket
                             .OrderByDescending(x => x.OrderDate)
                             .Skip(page * 10)
                             .Take(10)
                             .Select(x => new Order
                             {
                                 OrderId =x.OrderId,
                                 OrderUserId =  x.OrderUserId,
                                 OrderMoney= x.OrderMoney,
                                 OrderDate =  x.OrderDate,
                                 OrderStatus = x.OrderStatus,
                                 OrderBaskets = x.OrderBaskets.Select(ob => new OrderBasket
                                 {
                                    
                                    OrderBasketId=ob.OrderBasketId,
                                    OrderBasketCount=ob.OrderBasketCount,
                                    OrderBasketMoney    =ob.OrderBasketMoney,
                                    OrderBasketOrderId =ob.OrderBasketOrderId,
                                    OrderBasketProductId=   ob.OrderBasketProductId,
                                    OrderBasketProduct =ob.OrderBasketProduct,                                     
                                     // Add other properties as needed
                                 }).ToList(),
                                 OrderUser =x.OrderUser
                             })
                             .ToList();


            return View(order);


        }
        [HttpGet]
        public IActionResult OrderDetails(int Id)
        {
            Order? order = _db.Orders
                            .Where(x => x.OrderId == Id)
                            .Include(x => x.OrderBaskets)
                                .ThenInclude(x => x.OrderBasketProduct)
                            .Select(x => new Order
                            {
                                OrderId = x.OrderId,
                                OrderUserId = x.OrderUserId,
                                OrderMoney = x.OrderMoney,
                                OrderDate = x.OrderDate,
                                OrderStatus = x.OrderStatus,
                                OrderBaskets = x.OrderBaskets.Select(ob => new OrderBasket
                                {
                                    OrderBasketId = ob.OrderBasketId,
                                    OrderBasketCount = ob.OrderBasketCount,
                                    OrderBasketMoney = ob.OrderBasketMoney,
                                    OrderBasketOrderId = ob.OrderBasketOrderId,
                                    OrderBasketProductId = ob.OrderBasketProductId,
                                    OrderBasketProduct = ob.OrderBasketProduct,
                                    // Add other properties as needed
                                }).ToList(),
                                OrderUser = x.OrderUser,
                            })
                            .FirstOrDefault();
            return View(order);
                            
        }
    }
}
