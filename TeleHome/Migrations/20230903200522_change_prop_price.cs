using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleHome.Migrations
{
    /// <inheritdoc />
    public partial class change_prop_price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rmlubeco_telehome");

            migrationBuilder.CreateTable(
                name: "BigCampaign",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    BigCampaignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BigCampaignPicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BigCampa__4790B78AA6C1A516", x => x.BigCampaignId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryIcon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A0BB0A92309", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ColorsPr",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorHash = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ColorTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Colors__8DA7674D0523C47D", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contact__5C66259BFED4C44F", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    IconId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconClassname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IconUniCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Icons__43C7AD0F6BAC91EB", x => x.IconId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Statuses__C8EE20637837E3B4", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Subscribe",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    SubscribeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscribeEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubscribeDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrib__CAEA295229F45B13", x => x.SubscribeId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryCategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubCategoryPhotoURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubCategoryIsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCateg__26BE5B1918AE7B74", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK__SubCatego__SubCa__2B3F6F97",
                        column: x => x.SubCategoryCategoryId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserStatusId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserForgotToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserExpandDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CC41C4DD4", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Users__UserStatu__267ABA7A",
                        column: x => x.UserStatusId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Statuses",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "DeepCategory",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    DeepCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeepCategorySubCategoryId = table.Column<int>(type: "int", nullable: true),
                    DeepCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DeepCate__C35D770EFBA71E8D", x => x.DeepCategoryId);
                    table.ForeignKey(
                        name: "FK__DeepCateg__DeepC__2E1BDC42",
                        column: x => x.DeepCategorySubCategoryId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "TechCharacteristics",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    TechCharacteristicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechCharacteristicName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TechCharacteristicSubCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TechChar__918D20C8F0ABF014", x => x.TechCharacteristicId);
                    table.ForeignKey(
                        name: "FK__TechChara__TechC__4D94879B",
                        column: x => x.TechCharacteristicSubCategoryId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignImage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CampaignName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CampaignDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CampaignEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CampaignDeepCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Campaign__3F5E8A991BF963A4", x => x.CampaignId);
                    table.ForeignKey(
                        name: "FK__Campaigns__Campa__59063A47",
                        column: x => x.CampaignDeepCategoryId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "DeepCategory",
                        principalColumn: "DeepCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDeepCategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductPrice = table.Column<double>(type: "float", nullable: true),
                    ProductDiscountPrice = table.Column<double>(type: "float", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductIsSctock = table.Column<bool>(type: "bit", nullable: true),
                    ProductIsActive = table.Column<bool>(type: "bit", nullable: true),
                    ProductDate = table.Column<DateTime>(type: "date", nullable: true),
                    ProductFirstPhotoURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6CD1767FD26", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK__Products__Produc__30F848ED",
                        column: x => x.ProductDeepCategoryId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "DeepCategory",
                        principalColumn: "DeepCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    BasketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketProductId = table.Column<int>(type: "int", nullable: true),
                    BasketUserId = table.Column<int>(type: "int", nullable: true),
                    BasketQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Baskets__8FDA77B5E509F07A", x => x.BasketId);
                    table.ForeignKey(
                        name: "FK__Baskets__BasketP__5070F446",
                        column: x => x.BasketProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK__Baskets__BasketU__5165187F",
                        column: x => x.BasketUserId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentUserId = table.Column<int>(type: "int", nullable: true),
                    CommentProductId = table.Column<int>(type: "int", nullable: true),
                    CommentContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__C3B4DFCA46B5E9B3", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK__Comments__Commen__3A81B327",
                        column: x => x.CommentUserId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Comments__Commen__3B75D760",
                        column: x => x.CommentProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoriteUserId = table.Column<int>(type: "int", nullable: true),
                    FavoriteProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__CE74FAD590D6B88B", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK__Favorites__Favor__36B12243",
                        column: x => x.FavoriteUserId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Favorites__Favor__37A5467C",
                        column: x => x.FavoriteProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ImagesPh",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageProductId = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Images__7516F70C8992AA08", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK__Images__ImagePro__33D4B598",
                        column: x => x.ImageProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderUserId = table.Column<int>(type: "int", nullable: true),
                    OrderProductId = table.Column<int>(type: "int", nullable: true),
                    OrderMoney = table.Column<double>(type: "float", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BCF4B58474B", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__Orders__OrderPro__45F365D3",
                        column: x => x.OrderProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK__Orders__OrderUse__44FF419A",
                        column: x => x.OrderUserId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductColor",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    ProductColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductColorProductId = table.Column<int>(type: "int", nullable: true),
                    ProductColorColorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductC__C5DB683EC8522A6D", x => x.ProductColorId);
                    table.ForeignKey(
                        name: "FK__ProductCo__Produ__4AB81AF0",
                        column: x => x.ProductColorProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK__ProductCo__Produ__4BAC3F29",
                        column: x => x.ProductColorColorId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "ColorsPr",
                        principalColumn: "ColorId");
                });

            migrationBuilder.CreateTable(
                name: "TechCharacteristicsContent",
                schema: "rmlubeco_telehome",
                columns: table => new
                {
                    TechCharacteristicsContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechCharacteristicsContentProductId = table.Column<int>(type: "int", nullable: true),
                    TechCharacteristicsContentTechCharacteristicsId = table.Column<int>(type: "int", nullable: true),
                    TechCharacteristicsContentTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TechChar__1A6E0AF93BBFE9F6", x => x.TechCharacteristicsContentId);
                    table.ForeignKey(
                        name: "FK__TechChara__TechC__412EB0B6",
                        column: x => x.TechCharacteristicsContentProductId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK__TechChara__TechC__4222D4EF",
                        column: x => x.TechCharacteristicsContentTechCharacteristicsId,
                        principalSchema: "rmlubeco_telehome",
                        principalTable: "TechCharacteristics",
                        principalColumn: "TechCharacteristicId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BasketProductId",
                schema: "rmlubeco_telehome",
                table: "Baskets",
                column: "BasketProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BasketUserId",
                schema: "rmlubeco_telehome",
                table: "Baskets",
                column: "BasketUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CampaignDeepCategoryId",
                schema: "rmlubeco_telehome",
                table: "Campaigns",
                column: "CampaignDeepCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentProductId",
                schema: "rmlubeco_telehome",
                table: "Comments",
                column: "CommentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentUserId",
                schema: "rmlubeco_telehome",
                table: "Comments",
                column: "CommentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeepCategory_DeepCategorySubCategoryId",
                schema: "rmlubeco_telehome",
                table: "DeepCategory",
                column: "DeepCategorySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FavoriteProductId",
                schema: "rmlubeco_telehome",
                table: "Favorites",
                column: "FavoriteProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FavoriteUserId",
                schema: "rmlubeco_telehome",
                table: "Favorites",
                column: "FavoriteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesPh_ImageProductId",
                schema: "rmlubeco_telehome",
                table: "ImagesPh",
                column: "ImageProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderProductId",
                schema: "rmlubeco_telehome",
                table: "Orders",
                column: "OrderProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderUserId",
                schema: "rmlubeco_telehome",
                table: "Orders",
                column: "OrderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductColorColorId",
                schema: "rmlubeco_telehome",
                table: "ProductColor",
                column: "ProductColorColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductColorProductId",
                schema: "rmlubeco_telehome",
                table: "ProductColor",
                column: "ProductColorProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDeepCategoryId",
                schema: "rmlubeco_telehome",
                table: "Products",
                column: "ProductDeepCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_SubCategoryCategoryId",
                schema: "rmlubeco_telehome",
                table: "SubCategory",
                column: "SubCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechCharacteristics_TechCharacteristicSubCategoryId",
                schema: "rmlubeco_telehome",
                table: "TechCharacteristics",
                column: "TechCharacteristicSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechCharacteristicsContent_TechCharacteristicsContentProductId",
                schema: "rmlubeco_telehome",
                table: "TechCharacteristicsContent",
                column: "TechCharacteristicsContentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TechCharacteristicsContent_TechCharacteristicsContentTechCharacteristicsId",
                schema: "rmlubeco_telehome",
                table: "TechCharacteristicsContent",
                column: "TechCharacteristicsContentTechCharacteristicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserStatusId",
                schema: "rmlubeco_telehome",
                table: "Users",
                column: "UserStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baskets",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "BigCampaign",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Campaigns",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Favorites",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Icons",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "ImagesPh",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "ProductColor",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Subscribe",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "TechCharacteristicsContent",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "ColorsPr",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "TechCharacteristics",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "DeepCategory",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "SubCategory",
                schema: "rmlubeco_telehome");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "rmlubeco_telehome");
        }
    }
}
