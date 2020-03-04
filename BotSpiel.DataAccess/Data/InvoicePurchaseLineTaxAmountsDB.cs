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

    public class InvoicePurchaseLineTaxAmountsDB : DbContext
    {

        public InvoicePurchaseLineTaxAmountsDB(DbContextOptions<InvoicePurchaseLineTaxAmountsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InvoicePurchaseLineTaxAmounts> InvoicePurchaseLineTaxAmounts { get; set; }
		public DbSet<InvoicePurchaseLineTaxAmountsPost> InvoicePurchaseLineTaxAmountsPost { get; set; }
		public DbSet<Taxes> Taxes { get; set; }
		public DbSet<TaxesPost> TaxesPost { get; set; }
		public DbSet<InvoicePurchaseLineAmounts> InvoicePurchaseLineAmounts { get; set; }
		public DbSet<InvoicePurchaseLineAmountsPost> InvoicePurchaseLineAmountsPost { get; set; }
		public DbSet<CountrySubDivisions> CountrySubDivisions { get; set; }
		public DbSet<CountrySubDivisionsPost> CountrySubDivisionsPost { get; set; }
		public DbSet<Currencies> Currencies { get; set; }
		public DbSet<CurrenciesPost> CurrenciesPost { get; set; }
		public DbSet<Invoices> Invoices { get; set; }
		public DbSet<InvoicesPost> InvoicesPost { get; set; }
		public DbSet<PurchaseLines> PurchaseLines { get; set; }
		public DbSet<PurchaseLinesPost> PurchaseLinesPost { get; set; }
		public DbSet<Countries> Countries { get; set; }
		public DbSet<CountriesPost> CountriesPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<Purchases> Purchases { get; set; }
		public DbSet<PurchasesPost> PurchasesPost { get; set; }
		public DbSet<PlanetSubRegions> PlanetSubRegions { get; set; }
		public DbSet<PlanetSubRegionsPost> PlanetSubRegionsPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<PlanetRegions> PlanetRegions { get; set; }
		public DbSet<PlanetRegionsPost> PlanetRegionsPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
		public DbSet<Planets> Planets { get; set; }
		public DbSet<PlanetsPost> PlanetsPost { get; set; }
		public DbSet<PlanetarySystems> PlanetarySystems { get; set; }
		public DbSet<PlanetarySystemsPost> PlanetarySystemsPost { get; set; }
		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoicePurchaseLineTaxAmounts>()
                .ToTable("tx_vw_InvoicePurchaseLineTaxAmounts")
                .HasKey(c => new { c.ixInvoicePurchaseLineTaxAmount });
            modelBuilder.Entity<InvoicePurchaseLineTaxAmountsPost>()
                .ToTable("tx_vw_InvoicePurchaseLineTaxAmountsPost")
                .HasKey(c => new { c.ixInvoicePurchaseLineTaxAmount });
            modelBuilder.Entity<Taxes>()
                .ToTable("md_vw_Taxes")
                .HasKey(c => new { c.ixTax });
            modelBuilder.Entity<TaxesPost>()
                .ToTable("md_vw_TaxesPost")
                .HasKey(c => new { c.ixTax });
            modelBuilder.Entity<InvoicePurchaseLineAmounts>()
                .ToTable("tx_vw_InvoicePurchaseLineAmounts")
                .HasKey(c => new { c.ixInvoicePurchaseLineAmount });
            modelBuilder.Entity<InvoicePurchaseLineAmountsPost>()
                .ToTable("tx_vw_InvoicePurchaseLineAmountsPost")
                .HasKey(c => new { c.ixInvoicePurchaseLineAmount });
            modelBuilder.Entity<CountrySubDivisions>()
                .ToTable("md_vw_CountrySubDivisions")
                .HasKey(c => new { c.ixCountrySubDivision });
            modelBuilder.Entity<CountrySubDivisionsPost>()
                .ToTable("md_vw_CountrySubDivisionsPost")
                .HasKey(c => new { c.ixCountrySubDivision });
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
            modelBuilder.Entity<Countries>()
                .ToTable("md_vw_Countries")
                .HasKey(c => new { c.ixCountry });
            modelBuilder.Entity<CountriesPost>()
                .ToTable("md_vw_CountriesPost")
                .HasKey(c => new { c.ixCountry });
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
            modelBuilder.Entity<PlanetSubRegions>()
                .ToTable("md_vw_PlanetSubRegions")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetSubRegionsPost>()
                .ToTable("md_vw_PlanetSubRegionsPost")
                .HasKey(c => new { c.ixPlanetSubRegion });
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
            modelBuilder.Entity<PlanetRegions>()
                .ToTable("md_vw_PlanetRegions")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<PlanetRegionsPost>()
                .ToTable("md_vw_PlanetRegionsPost")
                .HasKey(c => new { c.ixPlanetRegion });
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
            modelBuilder.Entity<Planets>()
                .ToTable("md_vw_Planets")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetsPost>()
                .ToTable("md_vw_PlanetsPost")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetarySystems>()
                .ToTable("md_vw_PlanetarySystems")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<PlanetarySystemsPost>()
                .ToTable("md_vw_PlanetarySystemsPost")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<Galaxies>()
                .ToTable("md_vw_Galaxies")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<GalaxiesPost>()
                .ToTable("md_vw_GalaxiesPost")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<Universes>()
                .ToTable("md_vw_Universes")
                .HasKey(c => new { c.ixUniverse });
            modelBuilder.Entity<UniversesPost>()
                .ToTable("md_vw_UniversesPost")
                .HasKey(c => new { c.ixUniverse });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InvoicePurchaseLineTaxAmountsPost)).ToList())
            {
                var tx_vw_invoicepurchaselinetaxamountspost = e.Entity as InvoicePurchaseLineTaxAmountsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixInvoicePurchaseLineAmount = cmd.CreateParameter();
                            ixInvoicePurchaseLineAmount.ParameterName = "p0";
                            ixInvoicePurchaseLineAmount.Value = tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineAmount;
                            var ixTax = cmd.CreateParameter();
                            ixTax.ParameterName = "p1";
                            ixTax.Value = tx_vw_invoicepurchaselinetaxamountspost.ixTax;
                            var mAmount = cmd.CreateParameter();
                            mAmount.ParameterName = "p2";
                            mAmount.Value = tx_vw_invoicepurchaselinetaxamountspost.mAmount;
                            var ixCurrency = cmd.CreateParameter();
                            ixCurrency.ParameterName = "p3";
                            ixCurrency.Value = tx_vw_invoicepurchaselinetaxamountspost.ixCurrency;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = tx_vw_invoicepurchaselinetaxamountspost.UserName;

                            var ixInvoicePurchaseLineTaxAmount = cmd.CreateParameter();
                            ixInvoicePurchaseLineTaxAmount.ParameterName = "p5";
                            ixInvoicePurchaseLineTaxAmount.DbType = DbType.Int64;
                            ixInvoicePurchaseLineTaxAmount.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInvoicePurchaseLineTaxAmounts ");
                            sql.Append("@ixInvoicePurchaseLineAmount = @p0, ");
                            sql.Append("@ixTax = @p1, ");
                            sql.Append("@mAmount = @p2, ");
                            sql.Append("@ixCurrency = @p3, ");
                            if (tx_vw_invoicepurchaselinetaxamountspost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixInvoicePurchaseLineTaxAmount = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixInvoicePurchaseLineAmount);
                            cmd.Parameters.Add(ixTax);
                            cmd.Parameters.Add(mAmount);
                            cmd.Parameters.Add(ixCurrency);
                            if (tx_vw_invoicepurchaselinetaxamountspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInvoicePurchaseLineTaxAmount); 
                            cmd.ExecuteNonQuery();
                            tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineTaxAmount = (Int64)ixInvoicePurchaseLineTaxAmount.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInvoicePurchaseLineTaxAmount"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInvoicePurchaseLineTaxAmounts @ixInvoicePurchaseLineTaxAmount = @p0, @ixInvoicePurchaseLineAmount = @p1, @ixTax = @p2, @mAmount = @p3, @ixCurrency = @p4, @UserName = @p5", tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineTaxAmount, tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineAmount, tx_vw_invoicepurchaselinetaxamountspost.ixTax, tx_vw_invoicepurchaselinetaxamountspost.mAmount, tx_vw_invoicepurchaselinetaxamountspost.ixCurrency, tx_vw_invoicepurchaselinetaxamountspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInvoicePurchaseLineTaxAmounts @ixInvoicePurchaseLineTaxAmount = @p0, @ixInvoicePurchaseLineAmount = @p1, @ixTax = @p2, @mAmount = @p3, @ixCurrency = @p4, @UserName = @p5", tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineTaxAmount, tx_vw_invoicepurchaselinetaxamountspost.ixInvoicePurchaseLineAmount, tx_vw_invoicepurchaselinetaxamountspost.ixTax, tx_vw_invoicepurchaselinetaxamountspost.mAmount, tx_vw_invoicepurchaselinetaxamountspost.ixCurrency, tx_vw_invoicepurchaselinetaxamountspost.UserName);
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
  

