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

    public class PurchaseEmailsDB : DbContext
    {

        public PurchaseEmailsDB(DbContextOptions<PurchaseEmailsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PurchaseEmails> PurchaseEmails { get; set; }
		public DbSet<PurchaseEmailsPost> PurchaseEmailsPost { get; set; }
		public DbSet<Purchases> Purchases { get; set; }
		public DbSet<PurchasesPost> PurchasesPost { get; set; }
		public DbSet<SendEmails> SendEmails { get; set; }
		public DbSet<SendEmailsPost> SendEmailsPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseEmails>()
                .ToTable("tx_vw_PurchaseEmails")
                .HasKey(c => new { c.ixPurchaseEmail });
            modelBuilder.Entity<PurchaseEmailsPost>()
                .ToTable("tx_vw_PurchaseEmailsPost")
                .HasKey(c => new { c.ixPurchaseEmail });
            modelBuilder.Entity<Purchases>()
                .ToTable("tx_vw_Purchases")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<PurchasesPost>()
                .ToTable("tx_vw_PurchasesPost")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<SendEmails>()
                .ToTable("tx_vw_SendEmails")
                .HasKey(c => new { c.ixSendEmail });
            modelBuilder.Entity<SendEmailsPost>()
                .ToTable("tx_vw_SendEmailsPost")
                .HasKey(c => new { c.ixSendEmail });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PurchaseEmailsPost)).ToList())
            {
                var tx_vw_purchaseemailspost = e.Entity as PurchaseEmailsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPurchase = cmd.CreateParameter();
                            ixPurchase.ParameterName = "p0";
                            ixPurchase.Value = tx_vw_purchaseemailspost.ixPurchase;
                            var ixSendEmail = cmd.CreateParameter();
                            ixSendEmail.ParameterName = "p1";
                            ixSendEmail.Value = tx_vw_purchaseemailspost.ixSendEmail;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_purchaseemailspost.UserName;

                            var ixPurchaseEmail = cmd.CreateParameter();
                            ixPurchaseEmail.ParameterName = "p3";
                            ixPurchaseEmail.DbType = DbType.Int64;
                            ixPurchaseEmail.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePurchaseEmails ");
                            sql.Append("@ixPurchase = @p0, ");
                            sql.Append("@ixSendEmail = @p1, ");
                            if (tx_vw_purchaseemailspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPurchaseEmail = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPurchase);
                            cmd.Parameters.Add(ixSendEmail);
                            if (tx_vw_purchaseemailspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPurchaseEmail); 
                            cmd.ExecuteNonQuery();
                            tx_vw_purchaseemailspost.ixPurchaseEmail = (Int64)ixPurchaseEmail.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPurchaseEmail"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePurchaseEmails @ixPurchaseEmail = @p0, @ixPurchase = @p1, @ixSendEmail = @p2, @UserName = @p3", tx_vw_purchaseemailspost.ixPurchaseEmail, tx_vw_purchaseemailspost.ixPurchase, tx_vw_purchaseemailspost.ixSendEmail, tx_vw_purchaseemailspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePurchaseEmails @ixPurchaseEmail = @p0, @ixPurchase = @p1, @ixSendEmail = @p2, @UserName = @p3", tx_vw_purchaseemailspost.ixPurchaseEmail, tx_vw_purchaseemailspost.ixPurchase, tx_vw_purchaseemailspost.ixSendEmail, tx_vw_purchaseemailspost.UserName);
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
  

