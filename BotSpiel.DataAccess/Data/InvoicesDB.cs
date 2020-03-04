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

    public class InvoicesDB : DbContext
    {

        public InvoicesDB(DbContextOptions<InvoicesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Invoices> Invoices { get; set; }
		public DbSet<InvoicesPost> InvoicesPost { get; set; }
		public DbSet<Purchases> Purchases { get; set; }
		public DbSet<PurchasesPost> PurchasesPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoices>()
                .ToTable("tx_vw_Invoices")
                .HasKey(c => new { c.ixInvoice });
            modelBuilder.Entity<InvoicesPost>()
                .ToTable("tx_vw_InvoicesPost")
                .HasKey(c => new { c.ixInvoice });
            modelBuilder.Entity<Purchases>()
                .ToTable("tx_vw_Purchases")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<PurchasesPost>()
                .ToTable("tx_vw_PurchasesPost")
                .HasKey(c => new { c.ixPurchase });
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
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InvoicesPost)).ToList())
            {
                var tx_vw_invoicespost = e.Entity as InvoicesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPurchase = cmd.CreateParameter();
                            ixPurchase.ParameterName = "p0";
                            ixPurchase.Value = tx_vw_invoicespost.ixPurchase;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = tx_vw_invoicespost.UserName;

                            var ixInvoice = cmd.CreateParameter();
                            ixInvoice.ParameterName = "p2";
                            ixInvoice.DbType = DbType.Int64;
                            ixInvoice.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInvoices ");
                            sql.Append("@ixPurchase = @p0, ");
                            if (tx_vw_invoicespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixInvoice = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPurchase);
                            if (tx_vw_invoicespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInvoice); 
                            cmd.ExecuteNonQuery();
                            tx_vw_invoicespost.ixInvoice = (Int64)ixInvoice.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInvoice"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInvoices @ixInvoice = @p0, @ixPurchase = @p1, @UserName = @p2", tx_vw_invoicespost.ixInvoice, tx_vw_invoicespost.ixPurchase, tx_vw_invoicespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInvoices @ixInvoice = @p0, @ixPurchase = @p1, @UserName = @p2", tx_vw_invoicespost.ixInvoice, tx_vw_invoicespost.ixPurchase, tx_vw_invoicespost.UserName);
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
  

