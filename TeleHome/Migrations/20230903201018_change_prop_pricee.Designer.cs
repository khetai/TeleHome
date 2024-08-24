﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeleHome.Models;

#nullable disable

namespace TeleHome.Migrations
{
    [DbContext(typeof(RmlubecoTelehomeContext))]
    [Migration("20230903201018_change_prop_pricee")]
    partial class change_prop_pricee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("rmlubeco_telehome")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeleHome.Models.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasketId"));

                    b.Property<int?>("BasketProductId")
                        .HasColumnType("int");

                    b.Property<int?>("BasketQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("BasketUserId")
                        .HasColumnType("int");

                    b.HasKey("BasketId")
                        .HasName("PK__Baskets__8FDA77B5E509F07A");

                    b.HasIndex("BasketProductId");

                    b.HasIndex("BasketUserId");

                    b.ToTable("Baskets", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.BigCampaign", b =>
                {
                    b.Property<int>("BigCampaignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BigCampaignId"));

                    b.Property<string>("BigCampaignPicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BigCampaignId")
                        .HasName("PK__BigCampa__4790B78AA6C1A516");

                    b.ToTable("BigCampaign", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Campaign", b =>
                {
                    b.Property<int>("CampaignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CampaignId"));

                    b.Property<int?>("CampaignDeepCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CampaignDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CampaignEndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CampaignImage")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("CampaignName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("CampaignStartDate")
                        .HasColumnType("datetime");

                    b.HasKey("CampaignId")
                        .HasName("PK__Campaign__3F5E8A991BF963A4");

                    b.HasIndex("CampaignDeepCategoryId");

                    b.ToTable("Campaigns", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryIcon")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Category__19093A0BB0A92309");

                    b.ToTable("Category", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.ColorsPr", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<string>("ColorHash")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ColorTitle")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ColorId")
                        .HasName("PK__Colors__8DA7674D0523C47D");

                    b.ToTable("ColorsPr", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentContent")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("CommentDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CommentProductId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentUserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId")
                        .HasName("PK__Comments__C3B4DFCA46B5E9B3");

                    b.HasIndex("CommentProductId");

                    b.HasIndex("CommentUserId");

                    b.ToTable("Comments", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactTitle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ContactId")
                        .HasName("PK__Contact__5C66259BFED4C44F");

                    b.ToTable("Contact", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.DeepCategory", b =>
                {
                    b.Property<int>("DeepCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeepCategoryId"));

                    b.Property<string>("DeepCategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("DeepCategorySubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("DeepCategoryId")
                        .HasName("PK__DeepCate__C35D770EFBA71E8D");

                    b.HasIndex("DeepCategorySubCategoryId");

                    b.ToTable("DeepCategory", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteId"));

                    b.Property<int?>("FavoriteProductId")
                        .HasColumnType("int");

                    b.Property<int?>("FavoriteUserId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteId")
                        .HasName("PK__Favorite__CE74FAD590D6B88B");

                    b.HasIndex("FavoriteProductId");

                    b.HasIndex("FavoriteUserId");

                    b.ToTable("Favorites", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Icon", b =>
                {
                    b.Property<int>("IconId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IconId"));

                    b.Property<string>("IconClassname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IconUniCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IconId")
                        .HasName("PK__Icons__43C7AD0F6BAC91EB");

                    b.ToTable("Icons", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.ImagesPh", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<int?>("ImageProductId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImageURL");

                    b.HasKey("ImageId")
                        .HasName("PK__Images__7516F70C8992AA08");

                    b.HasIndex("ImageProductId");

                    b.ToTable("ImagesPh", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<double?>("OrderMoney")
                        .HasColumnType("float");

                    b.Property<int?>("OrderProductId")
                        .HasColumnType("int");

                    b.Property<bool?>("OrderStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderUserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BCF4B58474B");

                    b.HasIndex("OrderProductId");

                    b.HasIndex("OrderUserId");

                    b.ToTable("Orders", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateTime?>("ProductDate")
                        .HasColumnType("date");

                    b.Property<int?>("ProductDeepCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductFirstPhotoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("ProductFirstPhotoURL");

                    b.Property<bool?>("ProductIsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("ProductIsSctock")
                        .HasColumnType("bit");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double?>("ProductPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId")
                        .HasName("PK__Products__B40CC6CD1767FD26");

                    b.HasIndex("ProductDeepCategoryId");

                    b.ToTable("Products", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.ProductColor", b =>
                {
                    b.Property<int>("ProductColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductColorId"));

                    b.Property<int?>("ProductColorColorId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductColorProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductColorId")
                        .HasName("PK__ProductC__C5DB683EC8522A6D");

                    b.HasIndex("ProductColorColorId");

                    b.HasIndex("ProductColorProductId");

                    b.ToTable("ProductColor", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StatusId")
                        .HasName("PK__Statuses__C8EE20637837E3B4");

                    b.ToTable("Statuses", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"));

                    b.Property<int?>("SubCategoryCategoryId")
                        .HasColumnType("int");

                    b.Property<bool?>("SubCategoryIsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SubCategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SubCategoryPhotoUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("SubCategoryPhotoURL");

                    b.HasKey("SubCategoryId")
                        .HasName("PK__SubCateg__26BE5B1918AE7B74");

                    b.HasIndex("SubCategoryCategoryId");

                    b.ToTable("SubCategory", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Subscribe", b =>
                {
                    b.Property<int>("SubscribeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscribeId"));

                    b.Property<DateTime?>("SubscribeDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SubscribeEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SubscribeId")
                        .HasName("PK__Subscrib__CAEA295229F45B13");

                    b.ToTable("Subscribe", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.TechCharacteristic", b =>
                {
                    b.Property<int>("TechCharacteristicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechCharacteristicId"));

                    b.Property<string>("TechCharacteristicName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("TechCharacteristicSubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("TechCharacteristicId")
                        .HasName("PK__TechChar__918D20C8F0ABF014");

                    b.HasIndex("TechCharacteristicSubCategoryId");

                    b.ToTable("TechCharacteristics", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.TechCharacteristicsContent", b =>
                {
                    b.Property<int>("TechCharacteristicsContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechCharacteristicsContentId"));

                    b.Property<int?>("TechCharacteristicsContentProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TechCharacteristicsContentTechCharacteristicsId")
                        .HasColumnType("int");

                    b.Property<string>("TechCharacteristicsContentTitle")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("TechCharacteristicsContentId")
                        .HasName("PK__TechChar__1A6E0AF93BBFE9F6");

                    b.HasIndex("TechCharacteristicsContentProductId");

                    b.HasIndex("TechCharacteristicsContentTechCharacteristicsId");

                    b.ToTable("TechCharacteristicsContent", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserAddress")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("UserExpandDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserForgotToken")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPhone")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UserPhoto")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("UserStatusId")
                        .HasColumnType("int");

                    b.Property<string>("UserSurname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CC4CC41C4DD4");

                    b.HasIndex("UserStatusId");

                    b.ToTable("Users", "rmlubeco_telehome");
                });

            modelBuilder.Entity("TeleHome.Models.Basket", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "BasketProduct")
                        .WithMany("Baskets")
                        .HasForeignKey("BasketProductId")
                        .HasConstraintName("FK__Baskets__BasketP__5070F446");

                    b.HasOne("TeleHome.Models.User", "BasketUser")
                        .WithMany("Baskets")
                        .HasForeignKey("BasketUserId")
                        .HasConstraintName("FK__Baskets__BasketU__5165187F");

                    b.Navigation("BasketProduct");

                    b.Navigation("BasketUser");
                });

            modelBuilder.Entity("TeleHome.Models.Campaign", b =>
                {
                    b.HasOne("TeleHome.Models.DeepCategory", "CampaignDeepCategory")
                        .WithMany("Campaigns")
                        .HasForeignKey("CampaignDeepCategoryId")
                        .HasConstraintName("FK__Campaigns__Campa__59063A47");

                    b.Navigation("CampaignDeepCategory");
                });

            modelBuilder.Entity("TeleHome.Models.Comment", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "CommentProduct")
                        .WithMany("Comments")
                        .HasForeignKey("CommentProductId")
                        .HasConstraintName("FK__Comments__Commen__3B75D760");

                    b.HasOne("TeleHome.Models.User", "CommentUser")
                        .WithMany("Comments")
                        .HasForeignKey("CommentUserId")
                        .HasConstraintName("FK__Comments__Commen__3A81B327");

                    b.Navigation("CommentProduct");

                    b.Navigation("CommentUser");
                });

            modelBuilder.Entity("TeleHome.Models.DeepCategory", b =>
                {
                    b.HasOne("TeleHome.Models.SubCategory", "DeepCategorySubCategory")
                        .WithMany("DeepCategories")
                        .HasForeignKey("DeepCategorySubCategoryId")
                        .HasConstraintName("FK__DeepCateg__DeepC__2E1BDC42");

                    b.Navigation("DeepCategorySubCategory");
                });

            modelBuilder.Entity("TeleHome.Models.Favorite", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "FavoriteProduct")
                        .WithMany("Favorites")
                        .HasForeignKey("FavoriteProductId")
                        .HasConstraintName("FK__Favorites__Favor__37A5467C");

                    b.HasOne("TeleHome.Models.User", "FavoriteUser")
                        .WithMany("Favorites")
                        .HasForeignKey("FavoriteUserId")
                        .HasConstraintName("FK__Favorites__Favor__36B12243");

                    b.Navigation("FavoriteProduct");

                    b.Navigation("FavoriteUser");
                });

            modelBuilder.Entity("TeleHome.Models.ImagesPh", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "ImageProduct")
                        .WithMany("ImagesPhs")
                        .HasForeignKey("ImageProductId")
                        .HasConstraintName("FK__Images__ImagePro__33D4B598");

                    b.Navigation("ImageProduct");
                });

            modelBuilder.Entity("TeleHome.Models.Order", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "OrderProduct")
                        .WithMany("Orders")
                        .HasForeignKey("OrderProductId")
                        .HasConstraintName("FK__Orders__OrderPro__45F365D3");

                    b.HasOne("TeleHome.Models.User", "OrderUser")
                        .WithMany("Orders")
                        .HasForeignKey("OrderUserId")
                        .HasConstraintName("FK__Orders__OrderUse__44FF419A");

                    b.Navigation("OrderProduct");

                    b.Navigation("OrderUser");
                });

            modelBuilder.Entity("TeleHome.Models.Product", b =>
                {
                    b.HasOne("TeleHome.Models.DeepCategory", "ProductDeepCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductDeepCategoryId")
                        .HasConstraintName("FK__Products__Produc__30F848ED");

                    b.Navigation("ProductDeepCategory");
                });

            modelBuilder.Entity("TeleHome.Models.ProductColor", b =>
                {
                    b.HasOne("TeleHome.Models.ColorsPr", "ProductColorColor")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductColorColorId")
                        .HasConstraintName("FK__ProductCo__Produ__4BAC3F29");

                    b.HasOne("TeleHome.Models.Product", "ProductColorProduct")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductColorProductId")
                        .HasConstraintName("FK__ProductCo__Produ__4AB81AF0");

                    b.Navigation("ProductColorColor");

                    b.Navigation("ProductColorProduct");
                });

            modelBuilder.Entity("TeleHome.Models.SubCategory", b =>
                {
                    b.HasOne("TeleHome.Models.Category", "SubCategoryCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("SubCategoryCategoryId")
                        .HasConstraintName("FK__SubCatego__SubCa__2B3F6F97");

                    b.Navigation("SubCategoryCategory");
                });

            modelBuilder.Entity("TeleHome.Models.TechCharacteristic", b =>
                {
                    b.HasOne("TeleHome.Models.SubCategory", "TechCharacteristicSubCategory")
                        .WithMany("TechCharacteristics")
                        .HasForeignKey("TechCharacteristicSubCategoryId")
                        .HasConstraintName("FK__TechChara__TechC__4D94879B");

                    b.Navigation("TechCharacteristicSubCategory");
                });

            modelBuilder.Entity("TeleHome.Models.TechCharacteristicsContent", b =>
                {
                    b.HasOne("TeleHome.Models.Product", "TechCharacteristicsContentProduct")
                        .WithMany("TechCharacteristicsContents")
                        .HasForeignKey("TechCharacteristicsContentProductId")
                        .HasConstraintName("FK__TechChara__TechC__412EB0B6");

                    b.HasOne("TeleHome.Models.TechCharacteristic", "TechCharacteristicsContentTechCharacteristics")
                        .WithMany("TechCharacteristicsContents")
                        .HasForeignKey("TechCharacteristicsContentTechCharacteristicsId")
                        .HasConstraintName("FK__TechChara__TechC__4222D4EF");

                    b.Navigation("TechCharacteristicsContentProduct");

                    b.Navigation("TechCharacteristicsContentTechCharacteristics");
                });

            modelBuilder.Entity("TeleHome.Models.User", b =>
                {
                    b.HasOne("TeleHome.Models.Status", "UserStatus")
                        .WithMany("Users")
                        .HasForeignKey("UserStatusId")
                        .HasConstraintName("FK__Users__UserStatu__267ABA7A");

                    b.Navigation("UserStatus");
                });

            modelBuilder.Entity("TeleHome.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("TeleHome.Models.ColorsPr", b =>
                {
                    b.Navigation("ProductColors");
                });

            modelBuilder.Entity("TeleHome.Models.DeepCategory", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("TeleHome.Models.Product", b =>
                {
                    b.Navigation("Baskets");

                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("ImagesPhs");

                    b.Navigation("Orders");

                    b.Navigation("ProductColors");

                    b.Navigation("TechCharacteristicsContents");
                });

            modelBuilder.Entity("TeleHome.Models.Status", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TeleHome.Models.SubCategory", b =>
                {
                    b.Navigation("DeepCategories");

                    b.Navigation("TechCharacteristics");
                });

            modelBuilder.Entity("TeleHome.Models.TechCharacteristic", b =>
                {
                    b.Navigation("TechCharacteristicsContents");
                });

            modelBuilder.Entity("TeleHome.Models.User", b =>
                {
                    b.Navigation("Baskets");

                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
