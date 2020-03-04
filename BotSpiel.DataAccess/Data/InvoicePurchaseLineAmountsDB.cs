using System;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Web;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Data
{

    public class InvoicePurchaseLineAmountsDB : DbContext
    {

        public InvoicePurchaseLineAmountsDB(DbContextOptions<InvoicePurchaseLineAmountsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InvoicePurchaseLineAmounts> InvoicePurchaseLineAmounts { get; set; }
		public DbSet<InvoicePurchaseLineAmountsPost> InvoicePurchaseLineAmountsPost { get; set; }
		public DbSet<Currencies> Currencies { get; set; }
		public DbSet<CurrenciesPost> CurrenciesPost { get; set; }
		public DbSet<Invoices> Invoices { get; set; }
		public DbSet<InvoicesPost> InvoicesPost { get; set; }
		public DbSet<PurchaseLines> PurchaseLines { get; set; }
		public DbSet<PurchaseLinesPost> PurchaseLinesPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<Purchases> Purchases { get; set; }
		public DbSet<PurchasesPost> PurchasesPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoicePurchaseLineAmounts>()
                .ToTable("tx_vw_InvoicePurchaseLineAmounts")
                .HasKey(c => new { c.ixInvoicePurchaseLineAmount });
            modelBuilder.Entity<InvoicePurchaseLineAmountsPost>()
                .ToTable("tx_vw_InvoicePurchaseLineAmountsPost")
                .HasKey(c => new { c.ixInvoicePurchaseLineAmount });
            modelBuilder.Entity<Currencies>()
                .ToTable("md_vw_Currencies")
                .HasKey(c => new { c.ixCurrency });
            modelBuilder.Entity<CurrenciesPost>()
                .ToTable("md_vw_CurrenciesPost")
                .HasKey(c => new { c.ixCurrency });
            modelBuilder.Entity<Invoices>()
                .ToTable("tx_vw_Invoices")
                .HasKey(c => new { c.ixInvoice });
            modelBuilder.Entity<InvoicesPost>()
                .ToTable("tx_vw_InvoicesPost")
                .HasKey(c => new { c.ixInvoice });
            modelBuilder.Entity<PurchaseLines>()
                .ToTable("tx_vw_PurchaseLines")
                .HasKey(c => new { c.ixPurchaseLine });
            modelBuilder.Entity<PurchaseLinesPost>()
                .ToTable("tx_vw_PurchaseLinesPost")
                .HasKey(c => new { c.ixPurchaseLine });
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<Purchases>()
                .ToTable("tx_vw_Purchases")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<PurchasesPost>()
                .ToTable("tx_vw_PurchasesPost")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<People>()
                .ToTable("md_vw_People")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<PeoplePost>()
                .ToTable("md_vw_PeoplePost")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<Companies>()
                .ToTable("md_vw_Companies")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<CompaniesPost>()
                .ToTable("md_vw_CompaniesPost")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<MeasurementSystems>()
                .ToTable("md_vw_MeasurementSystems")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementSystemsPost>()
                .ToTable("md_vw_MeasurementSystemsPost")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementUnitsOf>()
                .ToTable("config_vw_MeasurementUnitsOf")
                .HasKey(c => new { c.ixMeasurementUnitOf });
            modelBuilder.Entity<MeasurementUnitsOfPost>()
                .ToTable("config_vw_MeasurementUnitsOfPost")
                .HasKey(c => new { c.ixMeasurementUnitOf });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InvoicePurchaseLineAmountsPost)).ToList())
            {
                var tx_vw_invoicepurchaselineamountspost = e.Entity as InvoicePurchaseLineAmountsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixInvoice = cmd.CreateParameter();
                            ixInvoice.ParameterName = "p0";
                            ixInvoice.Value = tx_vw_invoicepurchaselineamountspost.ixInvoice;
                            var ixPurchaseLine = cmd.CreateParameter();
                            ixPurchaseLine.ParameterName = "p1";
                            ixPurchaseLine.Value = tx_vw_invoicepurchaselineamountspost.ixPurchaseLine;
                            var mAmount = cmd.CreateParameter();
                            mAmount.ParameterName = "p2";
                            mAmount.Value = tx_vw_invoicepurchaselineamountspost.mAmount;
                            var ixCurrency = cmd.CreateParameter();
                            ixCurrency.ParameterName = "p3";
                            ixCurrency.Value = tx_vw_invoicepurchaselineamountspost.ixCurrency;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = tx_vw_invoicepurchaselineamountspost.UserName;

                            var ixInvoicePurchaseLineAmount = cmd.CreateParameter();
                            ixInvoicePurchaseLineAmount.ParameterName = "p5";
                            ixInvoicePurchaseLineAmount.DbType = DbType.Int64;
                            ixInvoicePurchaseLineAmount.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInvoicePurchaseLineAmounts ");
                            sql.Append("@ixInvoice = @p0, ");
                            sql.Append("@ixPurchaseLine = @p1, ");
                            sql.Append("@mAmount = @p2, ");
                            sql.Append("@ixCurrency = @p3, ");
                            if (tx_vw_invoicepurchaselineamountspost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixInvoicePurchaseLineAmount = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixInvoice);
                            cmd.Parameters.Add(ixPurchaseLine);
                            cmd.Parameters.Add(mAmount);
                            cmd.Parameters.Add(ixCurrency);
                            if (tx_vw_invoicepurchaselineamountspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInvoicePurchaseLineAmount); 
                            cmd.ExecuteNonQuery();
                            tx_vw_invoicepurchaselineamountspost.ixInvoicePurchaseLineAmount = (Int64)ixInvoicePurchaseLineAmount.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInvoicePurchaseLineAmount"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInvoicePurchaseLineAmounts @ixInvoicePurchaseLineAmount = @p0, @ixInvoice = @p1, @ixPurchaseLine = @p2, @mAmount = @p3, @ixCurrency = @p4, @UserName = @p5", tx_vw_invoicepurchaselineamountspost.ixInvoicePurchaseLineAmount, tx_vw_invoicepurchaselineamountspost.ixInvoice, tx_vw_invoicepurchaselineamountspost.ixPurchaseLine, tx_vw_invoicepurchaselineamountspost.mAmount, tx_vw_invoicepurchaselineamountspost.ixCurrency, tx_vw_invoicepurchaselineamountspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInvoicePurchaseLineAmounts @ixInvoicePurchaseLineAmount = @p0, @ixInvoice = @p1, @ixPurchaseLine = @p2, @mAmount = @p3, @ixCurrency = @p4, @UserName = @p5", tx_vw_invoicepurchaselineamountspost.ixInvoicePurchaseLineAmount, tx_vw_invoicepurchaselineamountspost.ixInvoice, tx_vw_invoicepurchaselineamountspost.ixPurchaseLine, tx_vw_invoicepurchaselineamountspost.mAmount, tx_vw_invoicepurchaselineamountspost.ixCurrency, tx_vw_invoicepurchaselineamountspost.UserName);
                        e.State = EntityState.Detached;                           
						break;
                }

                ++changes;
            }



            return changes; 
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x =>
            {
                x.State = EntityState.Detached;
            });
        }

    }
}
  

