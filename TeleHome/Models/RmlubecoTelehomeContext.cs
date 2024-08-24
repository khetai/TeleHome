using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeleHome.Models;

public partial class RmlubecoTelehomeContext : DbContext
{
    public RmlubecoTelehomeContext()
    {
    }

    public RmlubecoTelehomeContext(DbContextOptions<RmlubecoTelehomeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<BigCampaign> BigCampaigns { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ColorsPr> ColorsPrs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<DeepCategory> DeepCategories { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Icon> Icons { get; set; }

    public virtual DbSet<ImagesPh> ImagesPhs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderBasket> OrderBaskets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Subscribe> Subscribes { get; set; }

    public virtual DbSet<TechCharacteristic> TechCharacteristics { get; set; }

    public virtual DbSet<TechCharacteristicsContent> TechCharacteristicsContents { get; set; }

    public virtual DbSet<UsedBrand> UsedBrands { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=p1420.use1.mysecurecloudhost.com;Database=rmlubeco_telehome;User Id=rmlubeco_telehome;Password=TeleHome@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("rmlubeco_telehome");

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.BasketId).HasName("PK__Baskets__8FDA77B5E509F07A");

            entity.HasOne(d => d.BasketProduct).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.BasketProductId)
                .HasConstraintName("FK__Baskets__BasketP__5070F446");

            entity.HasOne(d => d.BasketUser).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.BasketUserId)
                .HasConstraintName("FK__Baskets__BasketU__5165187F");
        });

        modelBuilder.Entity<BigCampaign>(entity =>
        {
            entity.HasKey(e => e.BigCampaignId).HasName("PK__BigCampa__4790B78AA6C1A516");

            entity.ToTable("BigCampaign");
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__Campaign__3F5E8A991BF963A4");

            entity.Property(e => e.CampaignEndDate).HasColumnType("datetime");
            entity.Property(e => e.CampaignImage).HasMaxLength(300);
            entity.Property(e => e.CampaignName).HasMaxLength(250);
            entity.Property(e => e.CampaignStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CampaignDeepCategory).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.CampaignDeepCategoryId)
                .HasConstraintName("FK__Campaigns__Campa__59063A47");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BB0A92309");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryIcon).HasMaxLength(50);
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<ColorsPr>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("PK__Colors__8DA7674D0523C47D");

            entity.ToTable("ColorsPr");

            entity.Property(e => e.ColorHash).HasMaxLength(10);
            entity.Property(e => e.ColorTitle).HasMaxLength(30);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA46B5E9B3");

            entity.Property(e => e.CommentContent).HasMaxLength(500);
            entity.Property(e => e.CommentDate).HasColumnType("datetime");

            entity.HasOne(d => d.CommentProduct).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentProductId)
                .HasConstraintName("FK__Comments__Commen__3B75D760");

            entity.HasOne(d => d.CommentUser).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentUserId)
                .HasConstraintName("FK__Comments__Commen__3A81B327");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact__5C66259BFED4C44F");

            entity.ToTable("Contact");

            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<DeepCategory>(entity =>
        {
            entity.HasKey(e => e.DeepCategoryId).HasName("PK__DeepCate__C35D770EFBA71E8D");

            entity.ToTable("DeepCategory");

            entity.Property(e => e.DeepCategoryName).HasMaxLength(50);

            entity.HasOne(d => d.DeepCategorySubCategory).WithMany(p => p.DeepCategories)
                .HasForeignKey(d => d.DeepCategorySubCategoryId)
                .HasConstraintName("FK__DeepCateg__DeepC__2E1BDC42");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAD590D6B88B");

            entity.HasOne(d => d.FavoriteProduct).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FavoriteProductId)
                .HasConstraintName("FK__Favorites__Favor__37A5467C");

            entity.HasOne(d => d.FavoriteUser).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FavoriteUserId)
                .HasConstraintName("FK__Favorites__Favor__36B12243");
        });

        modelBuilder.Entity<Icon>(entity =>
        {
            entity.HasKey(e => e.IconId).HasName("PK__Icons__43C7AD0F6BAC91EB");

            entity.Property(e => e.IconClassname).HasMaxLength(100);
            entity.Property(e => e.IconUniCode).HasMaxLength(100);
        });

        modelBuilder.Entity<ImagesPh>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F70C8992AA08");

            entity.ToTable("ImagesPh");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

            entity.HasOne(d => d.ImageProduct).WithMany(p => p.ImagesPhs)
                .HasForeignKey(d => d.ImageProductId)
                .HasConstraintName("FK__Images__ImagePro__33D4B598");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF4B58474B");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderUser).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderUserId)
                .HasConstraintName("FK__Orders__OrderUse__44FF419A");
        });

        modelBuilder.Entity<OrderBasket>(entity =>
        {
            entity.HasKey(e => e.OrderBasketId).HasName("PK__OrderBas__F40E6313E2925700");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderBasketOrder).WithMany(p => p.OrderBaskets)
                .HasForeignKey(d => d.OrderBasketOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderBask__Order__14270015");

            entity.HasOne(d => d.OrderBasketProduct).WithMany(p => p.OrderBaskets)
                .HasForeignKey(d => d.OrderBasketProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderBask__Order__151B244E");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD1767FD26");

            entity.Property(e => e.ProductDate).HasColumnType("date");
            entity.Property(e => e.ProductFirstPhotoUrl)
                .HasMaxLength(200)
                .HasColumnName("ProductFirstPhotoURL");
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.ProductDeepCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductDeepCategoryId)
                .HasConstraintName("FK__Products__Produc__30F848ED");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.ProductColorId).HasName("PK__ProductC__C5DB683EC8522A6D");

            entity.ToTable("ProductColor");

            entity.HasOne(d => d.ProductColorColor).WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.ProductColorColorId)
                .HasConstraintName("FK__ProductCo__Produ__4BAC3F29");

            entity.HasOne(d => d.ProductColorProduct).WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.ProductColorProductId)
                .HasConstraintName("FK__ProductCo__Produ__4AB81AF0");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Statuses__C8EE20637837E3B4");

            entity.Property(e => e.StatusName).HasMaxLength(30);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK__SubCateg__26BE5B1918AE7B74");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryName).HasMaxLength(50);
            entity.Property(e => e.SubCategoryPhotoUrl)
                .HasMaxLength(100)
                .HasColumnName("SubCategoryPhotoURL");

            entity.HasOne(d => d.SubCategoryCategory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.SubCategoryCategoryId)
                .HasConstraintName("FK__SubCatego__SubCa__2B3F6F97");
        });

        modelBuilder.Entity<Subscribe>(entity =>
        {
            entity.HasKey(e => e.SubscribeId).HasName("PK__Subscrib__CAEA295229F45B13");

            entity.ToTable("Subscribe");

            entity.Property(e => e.SubscribeDate).HasColumnType("datetime");
            entity.Property(e => e.SubscribeEmail).HasMaxLength(100);
        });

        modelBuilder.Entity<TechCharacteristic>(entity =>
        {
            entity.HasKey(e => e.TechCharacteristicId).HasName("PK__TechChar__918D20C8F0ABF014");

            entity.Property(e => e.TechCharacteristicName).HasMaxLength(200);

            entity.HasOne(d => d.TechCharacteristicSubCategory).WithMany(p => p.TechCharacteristics)
                .HasForeignKey(d => d.TechCharacteristicSubCategoryId)
                .HasConstraintName("FK__TechChara__TechC__4D94879B");
        });

        modelBuilder.Entity<TechCharacteristicsContent>(entity =>
        {
            entity.HasKey(e => e.TechCharacteristicsContentId).HasName("PK__TechChar__1A6E0AF93BBFE9F6");

            entity.ToTable("TechCharacteristicsContent");

            entity.Property(e => e.TechCharacteristicsContentTitle).HasMaxLength(300);

            entity.HasOne(d => d.TechCharacteristicsContentProduct).WithMany(p => p.TechCharacteristicsContents)
                .HasForeignKey(d => d.TechCharacteristicsContentProductId)
                .HasConstraintName("FK__TechChara__TechC__412EB0B6");

            entity.HasOne(d => d.TechCharacteristicsContentTechCharacteristics).WithMany(p => p.TechCharacteristicsContents)
                .HasForeignKey(d => d.TechCharacteristicsContentTechCharacteristicsId)
                .HasConstraintName("FK__TechChara__TechC__4222D4EF");
        });

        modelBuilder.Entity<UsedBrand>(entity =>
        {
            entity.HasKey(e => e.BrandsId).HasName("PK__UsedBran__E48B2B41B8FB3C0C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC41C4DD4");

            entity.Property(e => e.UserAddress).HasMaxLength(500);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
            entity.Property(e => e.UserExpandDate).HasColumnType("datetime");
            entity.Property(e => e.UserForgotToken).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPassword).HasMaxLength(50);
            entity.Property(e => e.UserPhone).HasMaxLength(30);
            entity.Property(e => e.UserPhoto).HasMaxLength(500);
            entity.Property(e => e.UserSurname).HasMaxLength(50);

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStatusId)
                .HasConstraintName("FK__Users__UserStatu__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
