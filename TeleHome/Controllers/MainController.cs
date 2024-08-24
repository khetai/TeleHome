using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using TeleHome.Models;

namespace TeleHome.Controllers
{
    public class MainController : Controller
    {
        private readonly RmlubecoTelehomeContext _db;
        private readonly PaymentService _paymentService;

        private readonly string payriffApiBaseUrl = "https://api.payriff.com/api/v2";
        private readonly string payriffApiKey = "85A2F148C5F0499B8157C9B06F44AD8B";


        public MainController(RmlubecoTelehomeContext db)
        {
            _db = db;
            _paymentService = new PaymentService(db);
        }
        public IActionResult Home()
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var productList = _db.Products
            .Include(x => x.ProductDeepCategory.DeepCategorySubCategory)
            .Include(x => x.Baskets)
            .Include(x => x.Favorites)
            .Where(x => x.ProductIsActive == true)
            .OrderByDescending(x => x.ProductDate)
            .Take(8)
            .Select(p => new ProductWithCommentCount
            {
                IsBasket = p.Baskets.Any(x => x.BasketProductId == p.ProductId && x.BasketUserId == userId),
                IsFavorite = p.Favorites.Any(x => x.FavoriteProductId == p.ProductId && x.FavoriteUserId == userId),
                Product = p,
                CommentCount = _db.Comments.Count(c => c.CommentProductId == p.ProductId)
            })
            .ToList();

            ViewBag.UsedBrandsView = _db.UsedBrands.OrderByDescending(x => x.BrandsId).ToList();

            ViewBag.Campaign = _db.Campaigns.OrderByDescending(x => x.CampaignStartDate).ToList();
            ViewBag.TopSeller = _db.Favorites
            .GroupBy(f => f.FavoriteProductId)
            .OrderByDescending(g => g.Count())
            .Take(8)
            .Select(g => g.Key)
            .Join(_db.Products, favoriteProductId => favoriteProductId, product => product.ProductId, (favoriteProductId, product) => product)
            .Where(x => x.ProductIsActive == true)
            .Include(x => x.ProductDeepCategory.DeepCategorySubCategory)
            .Select(p => new ProductWithCommentCount
            {
                IsBasket = p.Baskets.Any(x => x.BasketProductId == p.ProductId && x.BasketUserId == userId),
                IsFavorite = p.Favorites.Any(x => x.FavoriteProductId == p.ProductId && x.FavoriteUserId == userId),
                Product = p,
                CommentCount = _db.Comments.Count(c => c.CommentProductId == p.ProductId)
            })
            .ToList();

            return View("Home", productList);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var u = _db.Users.FirstOrDefault(x => x.UserEmail == user.UserEmail);
            if (u != null)
            {
                ViewBag.Error = "Bu e-poçt ilə artıq qeydiyyatdan keçilib!";
                return View();
            }
            Subscribe subscribe = new Subscribe();
            subscribe.SubscribeEmail = user.UserEmail;
            subscribe.SubscribeDate = DateTime.Now;
            _db.Subscribes.Add(subscribe);
            user.UserStatusId = 2;
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Subscribe(Subscribe subscribe)
        {
            if (subscribe.SubscribeEmail == null)
            {
                return RedirectToAction("Home");
            }
            subscribe.SubscribeDate = DateTime.Now;
            _db.Subscribes.Add(subscribe);
            _db.SaveChanges();
            return RedirectToAction("Home");
        }
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        public IActionResult ShopForSubCategory(int id)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var a = _db.Products.Include(x => x.ProductDeepCategory).ThenInclude(x => x.DeepCategorySubCategory);

            ViewBag.DeepKat = _db.DeepCategories.Where(x => x.DeepCategorySubCategoryId == id).ToList();
            var subcategory = a.Where(x => x.ProductIsActive == true && x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId == id).OrderByDescending(x => x.ProductDate)
            .Select(p => new IsFavoriteProduct
            {
                Product = p,
                IsFavorite = _db.Favorites.Any(c => c.FavoriteProductId == p.ProductId && c.FavoriteUserId == userId),
                IsBasket = _db.Baskets.Any(c => c.BasketProductId == p.ProductId && c.BasketUserId == userId)
            }).Take(6).ToList();
            ViewBag.Count = a.Where(x => x.ProductIsSctock == true && x.ProductIsActive == true && x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId == id).Count();
            ViewBag.Sub = _db.SubCategories.SingleOrDefault(x => x.SubCategoryId == id).SubCategoryName;
            return View(subcategory);
        }
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        public IActionResult ShopForDeepCategory(int id)
        {
            var subid = _db.DeepCategories.FirstOrDefault(x => x.DeepCategoryId == id)?.DeepCategorySubCategoryId;
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            var query = _db.TechCharacteristics
                .Include(x => x.TechCharacteristicsContents)
                .Include(x => x.TechCharacteristicSubCategory)
                .ThenInclude(x => x.DeepCategories)
                .Where(tc => tc.TechCharacteristicSubCategoryId == subid)
                .Select(tc => new TechCharacteristic
                {
                    TechCharacteristicId = tc.TechCharacteristicId,
                    TechCharacteristicName = tc.TechCharacteristicName,
                    TechCharacteristicsContents = tc.TechCharacteristicsContents
                        .Where(cc => cc.TechCharacteristicsContentProduct.ProductDeepCategoryId == id && tc.TechCharacteristicsContents.Any())
                        .Select(cc => new TechCharacteristicsContent
                        {
                            TechCharacteristicsContentId = cc.TechCharacteristicsContentId,
                            TechCharacteristicsContentProductId = cc.TechCharacteristicsContentProductId,
                            TechCharacteristicsContentTechCharacteristicsId = cc.TechCharacteristicsContentTechCharacteristicsId,
                            TechCharacteristicsContentTitle = cc.TechCharacteristicsContentTitle,
                            TechCharacteristicsContentProduct = cc.TechCharacteristicsContentProduct,
                            TechCharacteristicsContentTechCharacteristics = cc.TechCharacteristicsContentTechCharacteristics
                        })
                        .GroupBy(tc => tc.TechCharacteristicsContentTitle)
                        .Select(g => g.First())
                        .ToList()
                })
                .ToList();

            ViewBag.Deep = query;

            var deepcategory = _db.Products
                .Include(x => x.ProductDeepCategory)
                .Where(x => x.ProductIsActive == true && x.ProductDeepCategoryId == id)
                .OrderByDescending(x => x.ProductDate)
                .Take(6)
                .Select(p => new IsFavoriteProduct
                {
                    Product = p,
                    IsFavorite = _db.Favorites.Any(c => c.FavoriteProductId == p.ProductId && c.FavoriteUserId == userId),
                    IsBasket = _db.Baskets.Any(c => c.BasketProductId == p.ProductId && c.BasketUserId == userId)
                })
                .ToList();

            ViewBag.Count = _db.Products
                .Where(x => x.ProductIsSctock == true && x.ProductIsActive == true && x.ProductDeepCategoryId == id)
                .Count();
            ViewBag.MinPrice = _db.Products.Where(x => x.ProductDeepCategoryId == id).Min(x => x.ProductPrice);
            ViewBag.MaxPrice = _db.Products.Where(x => x.ProductDeepCategoryId == id).Max(x => x.ProductPrice);
            ViewBag.Sub = _db.DeepCategories.FirstOrDefault(x => x.DeepCategoryId == id)?.DeepCategoryName;

            return View(deepcategory);
        }
          public async Task<List<ProductInfo>> FilteredProducts(bool orderByPriceAsc, bool orderByPriceDesc, bool orderByProductNameAsc, int id, int[] techCharacteristicIds, string[] techCharacteristicsContentTitles, int offset, int limit = 6, string search = null, double? minPrice = null, double? maxPrice = null, bool? isStock = null)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var query = _db.Products
                .Include(x => x.Favorites)
                .Include(x => x.Baskets)
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Include(x => x.TechCharacteristicsContents)
                .Where(x => x.ProductIsActive == true &&
                            //x.ProductId != productId && 
                            (id == null || id == 0 || x.ProductDeepCategory.DeepCategoryId == id) &&
                            (string.IsNullOrEmpty(search) ||
                             x.ProductName.Contains(search) ||
                             x.ProductDeepCategory.DeepCategoryName.Contains(search) ||
                             x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryName.Contains(search) ||
                             x.ProductPrice.ToString().Contains(search)) &&
                            (minPrice == null || x.ProductPrice >= minPrice.Value) &&
                            (maxPrice == null || x.ProductPrice <= maxPrice.Value));

            if (techCharacteristicIds != null && techCharacteristicIds.Length > 0 && techCharacteristicsContentTitles != null && techCharacteristicsContentTitles.Length > 0)
            {
                var filteredTechCharIds = await _db.TechCharacteristicsContents
                    .Where(tcc => techCharacteristicsContentTitles.Contains(tcc.TechCharacteristicsContentTitle))
                    .Select(tcc => tcc.TechCharacteristicsContentTechCharacteristicsId)
                    .ToListAsync();

                query = query.Where(x => x.TechCharacteristicsContents
                    .Any(tcc => filteredTechCharIds.Contains(tcc.TechCharacteristicsContentTechCharacteristicsId)))
                    .Where(x => x.TechCharacteristicsContents
                        .Any(tcc => techCharacteristicsContentTitles.Contains(tcc.TechCharacteristicsContentTitle)))
                    .Where(x => x.TechCharacteristicsContents
                        .Any(tcc => techCharacteristicIds.Contains(int.Parse(tcc.TechCharacteristicsContentTechCharacteristicsId.ToString()))));
            }
            if (orderByPriceAsc)
            {
                query = query.OrderBy(x => x.ProductPrice);
            }
            else if (orderByPriceDesc)
            {
                query = query.OrderByDescending(x => x.ProductPrice);
            }
            else if (orderByProductNameAsc)
            {
                query = query.OrderBy(x => x.ProductName);
            }
            else
            {
                query = query.OrderByDescending(x => x.ProductDate);
            }

            var result = await query
                .Skip(offset)
                .Take(limit)
                .Select(x => new ProductInfo
                {

                    ProductId = x.ProductId,
                    ProductFirstURL = x.ProductFirstPhotoUrl,
                    ProductIsSctock = x.ProductIsSctock,
                    ProductPrice = x.ProductPrice,
                    ProductName = x.ProductName,
                    SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategoryId,
                    IsInBasket = x.Baskets.Any(b => b.BasketUserId == userId),
                    IsInFavorite = x.Favorites.Any(b => b.FavoriteUserId == userId),
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        public IActionResult FilterRedirect(int limit = 6, string search = null)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            var query = _db.Products
                .Include(x => x.Favorites)
                .Include(x => x.Baskets)
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Where(x => x.ProductIsActive == true &&
                            (string.IsNullOrEmpty(search) ||
                             x.ProductName.Contains(search) ||
                             x.ProductDeepCategory.DeepCategoryName.Contains(search) ||
                             x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryName.Contains(search)
                             ))
                .OrderByDescending(x => x.ProductDate);

            var count = query.Count();
            var min = query.Min(x => x.ProductPrice);
            var max = query.Max(x => x.ProductPrice);
            var deepid = query.Select(x => x.ProductDeepCategoryId).FirstOrDefault();
            var productId = query.Select(x => x.ProductId).FirstOrDefault();

            var result = query
                .Select(x => new ProductInfo
                {
                    ProductId = x.ProductId,
                    ProductFirstURL = x.ProductFirstPhotoUrl,
                    ProductIsSctock = x.ProductIsSctock,
                    ProductPrice = x.ProductPrice,
                    ProductName = x.ProductName,
                    SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategoryId,
                    IsInBasket = x.Baskets.Any(b => b.BasketUserId == userId),
                    IsInFavorite = x.Favorites.Any(b => b.FavoriteUserId == userId),
                    Cap = count
                })
                .Take(limit)
                .AsNoTracking()
                .ToList();

            var viewModel = new ProductsViewModel
            {
                Products = result,
                ProductId = productId,
                Count = count,
                Search = search,
                MinimumPr = min,
                MaximumPr = max,
                DeepId = deepid
            };

            return View("Products", viewModel);
        }
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        public IActionResult Products(ProductsViewModel viewModel)
        {
            return View(viewModel);
        }
        public class ProductInfo
        {
            public int ProductId { get; set; }
            public string ProductFirstURL { get; set; }
            public bool? ProductIsSctock { get; set; }
            public double? ProductPrice { get; set; }
            public double? ProductWithoutPrice { get; set; }
            public string ProductName { get; set; }
            public int? SubCategoryId { get; set; }
            public bool IsInBasket { get; set; }
            public bool IsInFavorite { get; set; }
            public int Cap { get; set; }
            public List<TechCharacteristicsContent> TechCharacteristicsContents { get; set; }
        }
        [Authorize]
        public IActionResult MyProfile()
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var userList = _db.Users.Include(x => x.Baskets).Include(x => x.Favorites).FirstOrDefault(x => x.UserId == userId);
            var viewModel = new ProfileInfo
            {
                User = userList,
                FavoriteCount = userList.Favorites.Count(x => x.FavoriteUserId == userId),
                BasketCount = userList.Baskets.Count(x => x.BasketUserId == userId),
            };
            return View(viewModel);
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormFile formFile)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var u = _db.Users.FirstOrDefault(x => x.UserId == userId);
            u.UserPhoto = await OptimizeImage(formFile);
            _db.SaveChanges();
            return RedirectToAction("MyProfile");
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdatePassword(string oldpassword, string newpass, string repeatpass)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var userList = _db.Users.Include(x => x.Baskets).Include(x => x.Favorites).FirstOrDefault(x => x.UserId == userId);
            var viewModel = new ProfileInfo
            {
                User = userList,
                FavoriteCount = userList.Favorites.Count(x => x.FavoriteUserId == userId),
                BasketCount = userList.Baskets.Count(x => x.BasketUserId == userId),
            };
            if (oldpassword == null || newpass == null || repeatpass == null)
            {
                ViewBag.Error = "Məlumat boş ola bilməz";
                return RedirectToAction("MyProfile", viewModel);
            }
            var olduser = _db.Users.SingleOrDefault(x => x.UserId == userId);
            if (oldpassword == olduser.UserPassword)
            {
                if (newpass == repeatpass)
                {
                    olduser.UserPassword = newpass;
                    _db.SaveChanges();
                    return RedirectToAction("MyProfile", viewModel);
                }
                ViewBag.Error = "Yeni şifrə uyğun gəlmir";
                return RedirectToAction("MyProfile", viewModel);
            }

            ViewBag.Error = "Köhnə şifrə düzgün deyil";
            return View("MyProfile", viewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var olduser = _db.Users.SingleOrDefault(x => x.UserId == userId);
            olduser.UserName = user.UserName;
            olduser.UserEmail = user.UserEmail;
            olduser.UserPhone = user.UserPhone;
            olduser.UserAddress = user.UserAddress;
            _db.SaveChanges();
            return RedirectToAction("MyProfile");
        }
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Contacts.Add(contact);
            _db.SaveChanges();
            return RedirectToAction("Contact");
        }
        public IActionResult Search(int offset, int id)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            List<ProductInfo> productInfos = _db.Products
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Where(x => x.ProductIsActive == true && x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId == id)
                .OrderByDescending(x => x.ProductDate)
                .Select(x => new ProductInfo
                {
                    IsInBasket = x.Baskets.Any(x => x.BasketUserId == userId),
                    IsInFavorite = x.Favorites.Any(x => x.FavoriteUserId == userId),
                    ProductName = x.ProductName,
                    ProductFirstURL = x.ProductFirstPhotoUrl,
                    ProductId = x.ProductId,
                    ProductIsSctock = x.ProductIsSctock,
                    ProductPrice = x.ProductPrice,
                    ProductWithoutPrice =x.ProductWithoutPrice,
                    SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId
                })
                .Skip(offset * 6)
                .Take(6)
                .ToList();
            return Ok(productInfos);
        }
        public IActionResult SearchForDeep(int page, int id)
        {
            List<ProductInfo> productInfos = new List<ProductInfo>();

            productInfos = _db.Products
                .Include(x => x.ProductDeepCategory)
                .ThenInclude(x => x.DeepCategorySubCategory)
                .Where(x => x.ProductIsActive == true && x.ProductDeepCategoryId == id)
                .Skip(page * 6)
                .Take(6)
                .OrderByDescending(x => x.ProductDate)
                .Select(x => new ProductInfo
                {
                    ProductName = x.ProductName,
                    ProductFirstURL = x.ProductFirstPhotoUrl,
                    ProductId = x.ProductId,
                    ProductIsSctock = x.ProductIsSctock,
                    ProductPrice = x.ProductPrice,
                    SubCategoryId = x.ProductDeepCategory.DeepCategorySubCategory.SubCategoryId
                })
                .ToList();
            return Ok(productInfos);
        }
        [Authorize]
        public void PostFavorite(int productId)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            var oldfavorite = _db.Favorites.SingleOrDefault(x => x.FavoriteProductId == productId && x.FavoriteUserId == userId);
            if (oldfavorite == null)
            {
                Favorite favorite = new Favorite();
                favorite.FavoriteProductId = productId;
                favorite.FavoriteUserId = userId;
                _db.Favorites.Add(favorite);
                _db.SaveChanges();
            }
            else
            {
                _db.Favorites.Remove(oldfavorite);
                _db.SaveChanges();
            }
        }


        public class BasketItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class BasketCookieData
        {
            public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        }


        public void PostBasket(int productId, int inputElement)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var basketCookie = HttpContext.Request.Cookies["Basket"];
                BasketCookieData basketData;

                if (basketCookie != null)
                {
                    basketData = JsonConvert.DeserializeObject<BasketCookieData>(basketCookie);
                }
                else
                {
                    basketData = new BasketCookieData();
                }

                var existingProduct = basketData.BasketItems.FirstOrDefault(item => item.ProductId == productId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += inputElement == 0 ? 1 : inputElement;
                }
                else
                {
                    basketData.BasketItems.Add(new BasketItem { ProductId = productId, Quantity = inputElement == 0 ? 1 : inputElement });
                }

                var updatedCookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(3)
                };
                HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basketData), updatedCookie);
            }
            else
            {
                var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var oldbasket = _db.Baskets.SingleOrDefault(x => x.BasketProductId == productId && x.BasketUserId == userId);
                if (oldbasket == null)
                {
                    if (inputElement == 0)
                    {
                        Basket basket = new Basket();
                        basket.BasketProductId = productId;
                        basket.BasketUserId = userId;
                        basket.BasketQuantity = 1;
                        basket.BasketSelected = true;
                        _db.Baskets.Add(basket);
                        _db.SaveChanges();
                    }
                    else
                    {
                        Basket basket = new Basket();
                        basket.BasketProductId = productId;
                        basket.BasketUserId = userId;
                        basket.BasketQuantity = inputElement;
                        basket.BasketSelected = true;
                        _db.Baskets.Add(basket);
                        _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Baskets.Remove(oldbasket);
                    _db.SaveChanges();
                }
            }
        }
        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Main");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user, string returnUrl)
        {
            var u = _db.Users.Include(x => x.UserStatus).Where(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword).FirstOrDefault();
            if (u != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, u.UserSurname),
                        new Claim(ClaimTypes.Sid, u.UserId.ToString()),
                        new Claim(ClaimTypes.Role, u.UserStatus.StatusName.ToString())

                    };
                var useridentity = new ClaimsIdentity(claims, "AimTech");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                HttpContext.SignInAsync(principal);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Home", "Main");
                }
            }
            else
            {
                ViewBag.Error = "Email və ya şifrə yanlışdır";
                return View();
            }
        }
        [Authorize]
        public void DeleteFavorite(int id)
        {
            var removeid = _db.Favorites.FirstOrDefault(x => x.FavoriteId == id);
            _db.Favorites.Remove(removeid);
            _db.SaveChanges();
        }
        [AllowAnonymous]
        [HttpGet]
        public string GetFavorites()
        {
            try
            {
                //    var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var fav = _db.Favorites.Include(x => x.FavoriteProduct).Where(x => x.FavoriteUserId == 1).ToList();
                return JsonConvert.SerializeObject(fav, Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                      });
                //return Ok(fav);
            }
            catch (Exception ex)
            {
                return ("Internal Server Error");
            }
        }







        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> ProductDetails(int id)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            var product = _db.Products.Include(x => x.ProductDeepCategory).ThenInclude(x => x.DeepCategorySubCategory)
                             .Include(x => x.Baskets)
                             .Include(x => x.Favorites)
                             .Include(p => p.Comments)
                             .ThenInclude(x => x.CommentUser)
                             .Include(p => p.ImagesPhs)
                             .Include(p => p.TechCharacteristicsContents)
                             .ThenInclude(x => x.TechCharacteristicsContentTechCharacteristics)
                             .Include(p => p.ProductColors)
                             .ThenInclude(pc => pc.ProductColorColor)
                             .FirstOrDefault(p => p.ProductId == id);

            ViewBag.Similar = await _db.Products.Include(x => x.ProductDeepCategory).ThenInclude(x => x.DeepCategorySubCategory)
              .Where(p => (p.ProductDeepCategory.DeepCategorySubCategoryId == product.ProductDeepCategory.DeepCategorySubCategoryId) && p.ProductId != product.ProductId)
              .OrderByDescending(x => x.ProductDate)
              .Take(8)
              .Select(p => new ProductWithCommentCount
              {
                  IsBasket = p.Baskets.Any(x => x.BasketProductId == p.ProductId && x.BasketUserId == userId),
                  IsFavorite = p.Favorites.Any(x => x.FavoriteProductId == p.ProductId && x.FavoriteUserId == userId),
                  Product = p,
              })
              .ToListAsync();

            var colorDetailsList = product.ProductColors.Select(pc => new ColorDetailsViewModel
            {
                Color = pc.ProductColorColor,
                ProductColors = product.ProductColors.Where(p => p.ProductColorColorId == pc.ProductColorColorId).ToList()
            }).ToList();
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                IsBasket = product.Baskets.Any(x => x.BasketProductId == id && x.BasketUserId == userId),
                IsFavorite = product.Favorites.Any(x => x.FavoriteProductId == id && x.FavoriteUserId == userId),
                Quantity = (int)(product.Baskets.SingleOrDefault(x => x.BasketProductId == id && x.BasketUserId == userId)?.BasketQuantity ?? 0),
                Comment = product.Comments.ToList(),
                Colors = colorDetailsList,
                CommentCount = product.Comments.Count,
                Image = product.ImagesPhs.ToList(),
                TechCharacteristicsContent = product.TechCharacteristicsContents.ToList()
            };
            //ViewBag.SameCategoies = _db.Products
            //.Include(x => x.ProductDeepCategory.DeepCategorySubCategory)
            //.Include(x => x.Baskets)
            //.Include(x => x.Favorites)
            //.Where(x => x.ProductIsActive == true && )
            //.OrderByDescending(x => x.ProductDate)
            //.Take(4)
            //.Select(p => new ProductWithCommentCount
            //{
            //    IsBasket = p.Baskets.Any(x => x.BasketProductId == p.ProductId && x.BasketUserId == userId),
            //    IsFavorite = p.Favorites.Any(x => x.FavoriteProductId == p.ProductId && x.FavoriteUserId == userId),
            //    Product = p,
            //    CommentCount = _db.Comments.Count(c => c.CommentProductId == p.ProductId)
            //})
            //.ToList();
            return View(viewModel);
        }
        [Authorize]
        public IActionResult PostComment(string CommentContent, int productId)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            Comment c = new Comment();
            c.CommentContent = CommentContent;
            c.CommentDate = DateTime.Now;
            c.CommentUserId = userId;
            c.CommentProductId = productId;
            _db.Comments.Add(c);
            _db.SaveChanges();
            return RedirectToAction("ProductDetails", new { id = productId });
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Sifaris(int orderId)
        {

            Order? o = _db.Orders.Where(x=>x.OrderId == orderId).Include(x=>x.OrderUser).FirstOrDefault();
            return View(o);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitPayment(int orderId , string Address, int kredit = 0 , string kart = "BIRKART") 
        {

            int? sessionId = null;
            if (User.Identity.IsAuthenticated)
            {
                sessionId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }
            var us = await _db.Users.SingleOrDefaultAsync(x => x.UserId == sessionId);
            us.UserAddress = Address;
            _db.Users.Update(us);
            await _db.SaveChangesAsync();
            Order total = await _db.Orders.SingleOrDefaultAsync(x => x.OrderId == orderId);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.payriff.com/api/v2/createOrder");
            request.Headers.Add("Authorization", "85A2F148C5F0499B8157C9B06F44AD8B");

            var price = total.OrderMoney;
            // burda approveURL ("https://telehome.az/main/PaySuccess/orderId") sonunda orderId gondermelisen 
            var content = new StringContent($"{{\r\n  \"body\": {{\r\n    \"amount\": {(int)price},\r\n    \"approveURL\": \"https://khetai.github.io/PaymentSucces/{total.OrderId}\",\r\n    \"cancelURL\": \"https://telehome.az/main/PayError/{total.OrderId}\",\r\n    \"cardUuid\": \"string\",\r\n    \"currencyType\": \"AZN\",\r\n    \"declineURL\": \"{"https://telehome.az/main/PayError"}\",\r\n    \"description\": \"string\",\r\n    \"directPay\": true,\r\n    \"installmentPeriod\":{(int)kredit},\r\n    \"installmentProductType\": \"BIRKART\",\r\n    \"language\": \"AZ\",\r\n    \"senderCardUID\": \"string\"\r\n  }},\r\n  \"merchant\": \"ES1092121\"\r\n}}", Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch
            {
                return RedirectToAction("PayError");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PayriffApiResponse>(responseContent);
            return Redirect(responseObject.payload.paymentUrl);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(Order order)
        {
            int? sessionId = null;
            if (User.Identity.IsAuthenticated)
            {
                sessionId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            Order total = await _paymentService.CreateNewOrder(order, sessionId);

            return RedirectToAction("Sifaris", "main", new { orderId = total.OrderId });


            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.payriff.com/api/v2/createOrder");
            //request.Headers.Add("Authorization", "85A2F148C5F0499B8157C9B06F44AD8B");

            //var price = total.OrderMoney + 1;
            //// burda approveURL ("https://telehome.az/main/PaySuccess/orderId") sonunda orderId gondermelisen 
            //var content = new StringContent($"{{\r\n  \"body\": {{\r\n    \"amount\": {(int)price},\r\n    \"approveURL\": \"https://khetai.github.io/PaymentSucces/{total.OrderId}\",\r\n    \"cancelURL\": \"https://telehome.az/main/PayError/{total.OrderId}\",\r\n    \"cardUuid\": \"string\",\r\n    \"currencyType\": \"AZN\",\r\n    \"declineURL\": \"{"https://telehome.az/main/PayError"}\",\r\n    \"description\": \"string\",\r\n    \"directPay\": true,\r\n    \"installmentPeriod\": 0,\r\n    \"installmentProductType\": \"BIRKART\",\r\n    \"language\": \"AZ\",\r\n    \"senderCardUID\": \"string\"\r\n  }},\r\n  \"merchant\": \"ES1092121\"\r\n}}", Encoding.UTF8, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //try
            //{
            //    response.EnsureSuccessStatusCode();

            //}
            //catch
            //{
            //    return RedirectToAction("PayError");
            //}
            //var responseContent = await response.Content.ReadAsStringAsync();
            //var responseObject = JsonConvert.DeserializeObject<PayriffApiResponse>(responseContent);


            //return Redirect(responseObject.payload.paymentUrl);

        }
        public IActionResult PaymentSucces(int orderId)
        {
            // public IActionResult PaySuccess(int orderId) bele olmalidi list olarsa ise orderler onda bu sen sqlde bir dene elave table yaratmalisan ordersProduct deye cunki eyni anda 2-3 mehsul alarsa product idleri orda saxlamalisan
            // orderId her payment ucun bir dene olmalidi 
            // burad order id gelmelidi hemin id ile orderi tapirsan basede onun payrifde yoxluyursan statusu successdi yoxsa declined eger successdise orderi update edirsen status true olaraq
            //list halinda vurdugun ucun orderleri sen hemin idleri bir bir getirib yoxlamalisan 


            return View(orderId);
        }
        public IActionResult PayError(int orderId)
        {
            return View(orderId);
        }

        public class PayriffApiResponse
        {
            public string code { get; set; }
            public string internalMessage { get; set; }
            public string message { get; set; }
            public Payload payload { get; set; }

            public class Payload
            {
                public string orderId { get; set; }
                public string paymentUrl { get; set; }
                public string sessionId { get; set; }
            }
        }
        [HttpPost]
        public IActionResult CompletePayment()
        {
            // Ödeme tamamlandığında bu aksiyon tetiklenecektir
            // Burada ödeme sonucunu işleyebilir ve gerekli aksiyonları gerçekleştirebilirsiniz
            return View("PaymentCompleted"); // Ödeme tamamlandı sayfasına yönlendirme
        }





        public IActionResult Basket()
        {
            if (User.Identity.IsAuthenticated)
            {
                int? userId = null;
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var ss = _db.Baskets.ToList();
                var a = _db.Baskets.Include(x => x.BasketProduct).Where(x => x.BasketUserId == userId).ToList();
                double? total = 0;
                foreach (var item in a)
                {
                    if (item.BasketProduct.ProductWithoutPrice != null && item.BasketSelected ==true)
                    {
                        total += (float)(item.BasketProduct.ProductWithoutPrice * item.BasketQuantity);
                    }
                    else if (item.BasketSelected == true)
                    {
                        total += (item.BasketProduct.ProductPrice * item.BasketQuantity);

                    }
                }
                ViewBag.TotalPrice = total;

                return View(_db.Baskets.Include(x => x.BasketProduct).Where(x => x.BasketUserId == userId).ToList());
            }
            else
            {
                var basketDataJson = HttpContext.Request.Cookies["Basket"];
                List<BasketItem> cookieBasketItems = new List<BasketItem>();

                if (!string.IsNullOrEmpty(basketDataJson))
                {
                    var basketData = JsonConvert.DeserializeObject<BasketCookieData>(basketDataJson);
                    cookieBasketItems = basketData.BasketItems;
                }

                var productIds = cookieBasketItems.Select(item => item.ProductId).ToList();

                var dbProducts = _db.Products.Where(product => productIds.Contains(product.ProductId)).ToList();

                ViewBag.Products = dbProducts;

                ViewBag.ProductQuantities = dbProducts.ToDictionary(product => product.ProductId, product => cookieBasketItems
                    .Where(item => item.ProductId == product.ProductId)
                    .Sum(item => item.Quantity));
                 ViewBag.ProductTotalPrices = dbProducts.ToDictionary(product => product.ProductId, product =>  product.ProductPrice * cookieBasketItems
                    .Where(item => item.ProductId == product.ProductId)
                    .Sum(item => item.Quantity));

                double total = 0;
                foreach (var item in ViewBag.ProductTotalPrices)
                {
                    total += (double)item.Value;
                }

                //total = Math.Max(0, total);

                //string formattedTotal = total.ToString("N2");

                ViewBag.TotalPrice = total;

                return View();

            }
        }
        //[Authorize]
        public void QuantityChange(int basketProductId, int currentQuantity)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var basketItem = _db.Baskets.SingleOrDefault(x => x.BasketProductId == basketProductId && x.BasketUserId == userId);
                if (basketItem != null)
                {
                    basketItem.BasketQuantity = currentQuantity;
                }
                _db.SaveChanges();
            }
            else
            {
                var basketDataJson = HttpContext.Request.Cookies["Basket"];
                if (!string.IsNullOrEmpty(basketDataJson))
                {
                    var basketData = JsonConvert.DeserializeObject<BasketCookieData>(basketDataJson);
                    var basketItem = basketData.BasketItems.FirstOrDefault(item => item.ProductId == basketProductId);
                    if (basketItem != null)
                    {
                        basketItem.Quantity = currentQuantity;
                        var updatedCookie = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(3)
                        };
                        HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basketData), updatedCookie);
                    }
                }
            }
        }

        public void SelectBasket(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int? userId = null;
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var select = _db.Baskets.SingleOrDefault(x => x.BasketProductId == id && x.BasketUserId == userId);
                select.BasketSelected = !select.BasketSelected;
                _db.Baskets.Update(select);
                _db.SaveChanges();
            }
        }
        public void RemoveBasket(int basketProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                int? userId = null;
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                var removeid = _db.Baskets.SingleOrDefault(x => x.BasketProductId == basketProductId && x.BasketUserId == userId);
                _db.Baskets.Remove(removeid);
                _db.SaveChanges();
            }
            else
            {
                var basketDataJson = HttpContext.Request.Cookies["Basket"];
                if (!string.IsNullOrEmpty(basketDataJson))
                {
                    var basketData = JsonConvert.DeserializeObject<BasketCookieData>(basketDataJson);
                    var itemToRemove = basketData.BasketItems.FirstOrDefault(item => item.ProductId == basketProductId);
                    if (itemToRemove != null)
                    {
                        basketData.BasketItems.Remove(itemToRemove);
                        var updatedCookie = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(3)
                        };
                        HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basketData), updatedCookie);
                    }
                }
            }
        }
        //[Authorize]
        //public void ConfirmBasket(decimal tot, int[] id)
        //{
        //    int? userId = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        //    }
        //    var basketItems = _db.Baskets.Where(x => x.BasketUserId == userId).Include(x => x.BasketProduct).ToList();
        //    decimal? totalPrice = 0;
        //    bool istrue = false;
        //    for (int index = 0; index < basketItems.Count; index++)
        //    {
        //        var item = basketItems[index];
        //        if (item.BasketProduct != null && item.BasketQuantity != null)
        //        {
        //            float? productPrice = item.BasketProduct.ProductPrice;
        //            int itemquantity = item.BasketQuantity.Value;
        //            totalPrice += productPrice * itemquantity;
        //        }
        //        if (item.BasketProductId == id[index])
        //        {
        //            istrue = true;
        //        }
        //    }
        //    if (totalPrice == tot && istrue)
        //    {
        //        //testiqlendi
        //    }
        //    else
        //    {
        //        //redd edildi
        //    }
        //}
        public IActionResult Coupon()
        {
            return View();
        }
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
        public IActionResult CampaignDetail(int id)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            }

            var campaign = _db.Campaigns.Include(x => x.CampaignDeepCategory).SingleOrDefault(x => x.CampaignId == id);

            var productList = _db.Products
                .Include(x => x.ProductDeepCategory)
                    .ThenInclude(x => x.DeepCategorySubCategory)
                .Include(x => x.Baskets)
                .Include(x => x.Favorites)
                .Where(x => x.ProductIsActive == true && x.ProductDeepCategoryId == campaign.CampaignDeepCategoryId)
                .OrderByDescending(x => x.ProductDate)
                .Take(4)
                .Select(p => new ProductWithCommentCount
                {
                    IsBasket = p.Baskets.Any(x => x.BasketProductId == p.ProductId && x.BasketUserId == userId),
                    IsFavorite = p.Favorites.Any(x => x.FavoriteProductId == p.ProductId && x.FavoriteUserId == userId),
                    Product = p,
                    Campaigns = campaign
                });
            var viewModel = new CampaignDetailViewModel
            {
                Campaigns = campaign,
                ProductList = productList
            };
            return View(viewModel);
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public async Task<string> GeneratePasswordResetToken(string email)
        {
            var token = Guid.NewGuid().ToString();
            return token;
        }
        public async Task<bool> VerifyPasswordResetToken(string email, string token)
        {
            var a = _db.Users.FirstOrDefault(x => x.UserEmail == email);
            if (a.UserEmail == email && a.UserExpandDate > DateTime.Now && token == a.UserForgotToken)
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserEmail == model.Email);
                if (user != null)
                {
                    var token = await GeneratePasswordResetToken(model.Email);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Main",
                    new { email = model.Email, token = token }, Request.Scheme);
                    SendEmail(model.Email, "Parolun Sıfırlanması", $"Parolunuzu sıfırlamaq üçün bu linkə klikləyin: {resetPasswordUrl}");
                    user.UserForgotToken = token;
                    user.UserExpandDate = DateTime.Now.AddMinutes(15);
                    _db.SaveChanges();
                    ViewBag.Message = "Parolun sıfırlanması link e-poçt ünvanınıza göndərildi.";
                    return View();
                }
            }
            ViewBag.Error = "Belə bir e-poçt mövcüd deyil!";
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel { Email = email, Token = token });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            bool isverify = await VerifyPasswordResetToken(model.Email, model.Token);
            if (isverify == true)
            {
                if (ModelState.IsValid)
                {
                    var user = _db.Users.SingleOrDefault(x => x.UserEmail == model.Email).UserEmail;
                    if (user != null)
                    {
                        if (model.NewPassword == model.ConfirmNewPassword)
                        {
                            var result = await ResetPasswordAsync(user, model.Token, model.NewPassword);
                            if (result)
                            {
                                return RedirectToAction("Login");
                            }
                            else
                            {
                                return View(model);
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Şifrələr biri-birinə uyğun gəlmir!";
                            return View(model);
                        }
                    }
                }
            }
            else
            {
                ViewBag.Verify = "Sizə göndərilən linkin vaxtı bitmişdir! Xahiş edirik yeni bir istək göndərin";
                return View(model);
            }
            return View(model);
        }
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var dbContext = new RmlubecoTelehomeContext();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
            if (user != null)
            {
                user.UserPassword = newPassword;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public void SendEmail(string toemail, string subject, string message)
        {
            var smtpclient = new SmtpClient("plesk3005.my-hosting-panel.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("info@telehome.az", "TeleHome@123"),
                EnableSsl = true
            };
            var mailmessage = new MailMessage
            {
                From = new MailAddress("info@telehome.az"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailmessage.To.Add(toemail);
            smtpclient.Send(mailmessage);
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }
    }
}
