using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_Gateway.data.Entites
{
    public partial class Loyalty_Robinsons_ProdContext : DbContext
    {
        public Loyalty_Robinsons_ProdContext()
        {
        }

        public Loyalty_Robinsons_ProdContext(DbContextOptions<Loyalty_Robinsons_ProdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrmAccumulatedAccount> CrmAccumulatedAccounts { get; set; } = null!;
        public virtual DbSet<CrmBuyingUnit> CrmBuyingUnits { get; set; } = null!;
        public virtual DbSet<CrmBuyingUnitAccountsActivity> CrmBuyingUnitAccountsActivities { get; set; } = null!;
        public virtual DbSet<CrmBuyingUnitHistory> CrmBuyingUnitHistories { get; set; } = null!;
        public virtual DbSet<CrmBuyingUnitHistoryEvent> CrmBuyingUnitHistoryEvents { get; set; } = null!;
        public virtual DbSet<CrmBuyingUnitSegment> CrmBuyingUnitSegments { get; set; } = null!;
        public virtual DbSet<CrmClubcard> CrmClubcards { get; set; } = null!;
        public virtual DbSet<CrmLoyaltyDocument> CrmLoyaltyDocuments { get; set; } = null!;
        public virtual DbSet<CrmMember> CrmMembers { get; set; } = null!;
        public virtual DbSet<CrmMemberAccountsActivity> CrmMemberAccountsActivities { get; set; } = null!;
        public virtual DbSet<CrmMemberAttributeValue> CrmMemberAttributeValues { get; set; } = null!;
        public virtual DbSet<CrmMemberLookup> CrmMemberLookups { get; set; } = null!;
        public virtual DbSet<CrmMemberStoreAssign> CrmMemberStoreAssigns { get; set; } = null!;
        public virtual DbSet<CrmPosaccountsActivity> CrmPosaccountsActivities { get; set; } = null!;
        public virtual DbSet<CrmPosloyaltyDocumentsActivity> CrmPosloyaltyDocumentsActivities { get; set; } = null!;
        public virtual DbSet<CrmPostran> CrmPostrans { get; set; } = null!;
        public virtual DbSet<CrmRestrictionType> CrmRestrictionTypes { get; set; } = null!;
        public virtual DbSet<CrmSegment> CrmSegments { get; set; } = null!;
        public virtual DbSet<StgCrmMemberAccountsActivity> StgCrmMemberAccountsActivities { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreCode> StoreCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=azsshqltydv01.database.windows.net; User Id=hqdb_reader; Password=s36IapseIQIy; Database=Loyalty_Robinsons_Prod; Trusted_Connection=False;Encrypt=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrmAccumulatedAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_AccumulatedAccounts");

                entity.Property(e => e.AccountInternalKey).ValueGeneratedOnAdd();

                entity.Property(e => e.AccumulationRatio).HasColumnType("money");

                entity.Property(e => e.ArchiveReferenceYear)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ConversionReferenceYear)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ConvertedResultIntervals).HasColumnType("money");

                entity.Property(e => e.ConvertedResultLimitFrom).HasColumnType("money");

                entity.Property(e => e.ConvertedResultLimitTo).HasColumnType("money");

                entity.Property(e => e.DisplayPointsInEj).HasColumnName("DisplayPointsInEJ");

                entity.Property(e => e.EnablePointsTransferBetweenHh).HasColumnName("EnablePointsTransferBetweenHH");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ExpirationReferenceYear)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.InheritFromMa).HasColumnName("InheritFromMA");

                entity.Property(e => e.InitialAccountValue).HasColumnType("money");

                entity.Property(e => e.IsInheritSourceGui).HasColumnName("IsInheritSourceGUI");

                entity.Property(e => e.IsInheritSourcePos).HasColumnName("IsInheritSourcePOS");

                entity.Property(e => e.IsInheritSourceWs).HasColumnName("IsInheritSourceWS");

                entity.Property(e => e.IsManualUpdateMs).HasColumnName("IsManualUpdateMS");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PosupdateMaximumAccumulationPerTransaction)
                    .HasColumnType("money")
                    .HasColumnName("POSUpdate_MaximumAccumulationPerTransaction");

                entity.Property(e => e.PosupdateMaximumBalance)
                    .HasColumnType("money")
                    .HasColumnName("POSUpdate_MaximumBalance");

                entity.Property(e => e.PrintingEndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.PrintingStartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.PublicationRowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.RedemptionRatio).HasColumnType("money");

                entity.Property(e => e.ResetAccountValueOn).HasMaxLength(100);

                entity.Property(e => e.ResetReferenceYear)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.TransferPointsFromMa).HasColumnName("TransferPointsFromMA");

                entity.Property(e => e.TransferPointsTargetHh).HasColumnName("TransferPointsTargetHH");

                entity.Property(e => e.TresholdValue).HasColumnType("money");

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<CrmBuyingUnit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_BuyingUnit");

                entity.Property(e => e.AddressKana).HasMaxLength(400);

                entity.Property(e => e.BuyingUnitInternalKey).ValueGeneratedOnAdd();

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.County).HasMaxLength(40);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("EMailAddress");

                entity.Property(e => e.Extension).HasMaxLength(30);

                entity.Property(e => e.ExternalBuyingUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalSource).HasMaxLength(50);

                entity.Property(e => e.HomePhone).HasMaxLength(20);

                entity.Property(e => e.HouseName).HasMaxLength(40);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhonePrefix).HasMaxLength(10);

                entity.Property(e => e.Pobox)
                    .HasMaxLength(10)
                    .HasColumnName("POBox");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Remarks).HasMaxLength(120);

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.State).HasMaxLength(20);

                entity.Property(e => e.Street1).HasMaxLength(40);

                entity.Property(e => e.Street2).HasMaxLength(400);

                entity.Property(e => e.StreetNum).HasMaxLength(20);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CrmBuyingUnitAccountsActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_BuyingUnitAccountsActivity");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.HousekeepingBalance).HasColumnType("money");

                entity.Property(e => e.HousekeepingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.HousekeepingTotalAccumulated).HasColumnType("money");

                entity.Property(e => e.HousekeepingTotalRedeemed).HasColumnType("money");

                entity.Property(e => e.LastBurnTranId).HasColumnName("LastBurnTranID");

                entity.Property(e => e.LastEarnTranId).HasColumnName("LastEarnTranID");

                entity.Property(e => e.LastUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.TotalAccumulated).HasColumnType("money");

                entity.Property(e => e.TotalRedeemed).HasColumnType("money");
            });

            modelBuilder.Entity<CrmBuyingUnitHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_BuyingUnitHistory");

                entity.Property(e => e.BuyingUnitHistoryId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.SourceClubCardId).HasMaxLength(100);

                entity.Property(e => e.TargetClubCardId).HasMaxLength(100);
            });

            modelBuilder.Entity<CrmBuyingUnitHistoryEvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_BuyingUnitHistoryEvents");

                entity.Property(e => e.EventEntryId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmBuyingUnitSegment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_BuyingUnitSegment");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<CrmClubcard>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_Clubcard");

                entity.Property(e => e.ClubCardId).HasMaxLength(100);

                entity.Property(e => e.EffectiveDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("smalldatetime");

                entity.Property(e => e.IssueDate).HasColumnType("smalldatetime");

                entity.Property(e => e.LastUpdateStatusDate).HasColumnType("smalldatetime");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<CrmLoyaltyDocument>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_LoyaltyDocuments");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.Barcode).HasMaxLength(128);

                entity.Property(e => e.BusinessId)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDateTime).HasColumnType("datetime");

                entity.Property(e => e.DocumentInternalKey).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.HousekeepingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.IssuedQty).HasColumnType("money");

                entity.Property(e => e.RedeemedValue).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<CrmMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_Member");

                entity.Property(e => e.AdditionalFirstName).HasMaxLength(50);

                entity.Property(e => e.AdditionalLastName).HasMaxLength(50);

                entity.Property(e => e.AdressNormalizationUpdate).HasMaxLength(1);

                entity.Property(e => e.BirthDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CommercialDriversLicense).HasMaxLength(20);

                entity.Property(e => e.CompanyName).HasMaxLength(30);

                entity.Property(e => e.DriversLicense).HasMaxLength(20);

                entity.Property(e => e.EffectiveDate).HasColumnType("smalldatetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("EMailAddress");

                entity.Property(e => e.ExternalMemberKey)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LastUpdatedDateForExport).HasColumnType("datetime");

                entity.Property(e => e.MemberInternalKey).ValueGeneratedOnAdd();

                entity.Property(e => e.MiddleInitial).HasMaxLength(1);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(20);

                entity.Property(e => e.NationalInsuranceNumber).HasMaxLength(18);

                entity.Property(e => e.Passport).HasMaxLength(25);

                entity.Property(e => e.Remarks).HasMaxLength(256);

                entity.Property(e => e.SpouseLastName).HasMaxLength(30);

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.WorkPhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<CrmMemberAccountsActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_MemberAccountsActivity");

                entity.Property(e => e.TotalAccumulated).HasColumnType("money");

                entity.Property(e => e.TotalRedeemed).HasColumnType("money");
            });

            modelBuilder.Entity<CrmMemberAttributeValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_MemberAttributeValue");

                entity.Property(e => e.DateValue).HasColumnType("smalldatetime");

                entity.Property(e => e.LongStringValue).HasMaxLength(1024);

                entity.Property(e => e.MoneyValue).HasColumnType("money");

                entity.Property(e => e.StringValue).HasMaxLength(60);

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<CrmMemberLookup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_MemberLookup");

                entity.HasIndex(e => e.MobilePhoneNumber, "IX_CRM_Member_Lookup");

                entity.Property(e => e.ExternalMemberKey)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(20);

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<CrmMemberStoreAssign>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_MemberStoreAssign");

                entity.Property(e => e.IsHomeStore).HasColumnName("isHomeStore");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CrmPosaccountsActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_POSAccountsActivity");

                entity.Property(e => e.EarnValue).HasColumnType("money");

                entity.Property(e => e.ExpirationDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ExternalReferenceId).HasMaxLength(50);

                entity.Property(e => e.InitialValue).HasColumnType("money");

                entity.Property(e => e.PosDateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.ProcessDate).HasColumnType("smalldatetime");

                entity.Property(e => e.RedeemValue).HasColumnType("money");

                entity.Property(e => e.Remarks).HasMaxLength(256);
            });

            modelBuilder.Entity<CrmPosloyaltyDocumentsActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_POSLoyaltyDocumentsActivity");

                entity.Property(e => e.PosDateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.PostranInternalKey).HasColumnName("POSTranInternalKey");
            });

            modelBuilder.Entity<CrmPostran>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_POSTran");

                entity.Property(e => e.AmountDiff).HasColumnType("money");

                entity.Property(e => e.ClubCardId).HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.ExternalTranId).HasMaxLength(25);

                entity.Property(e => e.MessageSignature).HasMaxLength(128);

                entity.Property(e => e.MessageSource).HasMaxLength(24);

                entity.Property(e => e.OriginalClubCardId)
                    .HasMaxLength(100)
                    .HasColumnName("Original_ClubCardID");

                entity.Property(e => e.PosDateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.PostranInternalKey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("POSTranInternalKey");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SalesAmount).HasColumnType("money");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CrmRestrictionType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_RestrictionType");

                entity.Property(e => e.IsDefaultinMs).HasColumnName("IsDefaultinMS");

                entity.Property(e => e.RestrictionDescription).HasMaxLength(45);
            });

            modelBuilder.Entity<CrmSegment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CRM_Segment");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.PublicationRowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SegmentDescription).HasMaxLength(50);

                entity.Property(e => e.SegmentInternalKey).ValueGeneratedOnAdd();

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SyncToMsg12).HasColumnName("SyncToMSG12");

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<StgCrmMemberAccountsActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stg_CRM_MemberAccountsActivity");

                entity.Property(e => e.TotalAccumulated).HasColumnType("money");

                entity.Property(e => e.TotalRedeemed).HasColumnType("money");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Store");

                entity.Property(e => e.AdditionalName).HasMaxLength(40);

                entity.Property(e => e.CdsfilesPath)
                    .HasMaxLength(200)
                    .HasColumnName("CDSFilesPath");

                entity.Property(e => e.CommPointPassword).HasMaxLength(50);

                entity.Property(e => e.CommunicationFilesPath).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.MacroDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("smalldatetime");

                entity.Property(e => e.OutLookPassword).HasMaxLength(50);

                entity.Property(e => e.OutLookUserName).HasMaxLength(50);

                entity.Property(e => e.Postype).HasColumnName("POSType");

                entity.Property(e => e.Posversion)
                    .HasMaxLength(40)
                    .HasColumnName("POSVersion");

                entity.Property(e => e.PublicationRowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.StoreEmailAddress).HasMaxLength(50);

                entity.Property(e => e.StoreName).HasMaxLength(40);

                entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.UploadFilesPath).HasMaxLength(200);
            });

            modelBuilder.Entity<StoreCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StoreCode");

                entity.Property(e => e.StoreId).HasMaxLength(24);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
