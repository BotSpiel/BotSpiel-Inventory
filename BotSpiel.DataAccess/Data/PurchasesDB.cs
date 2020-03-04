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

    public class PurchasesDB : DbContext
    {

        public PurchasesDB(DbContextOptions<PurchasesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PurchasesPost)).ToList())
            {
                var tx_vw_purchasespost = e.Entity as PurchasesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPerson = cmd.CreateParameter();
                            ixPerson.ParameterName = "p0";
                            ixPerson.Value = tx_vw_purchasespost.ixPerson;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p1";
                            ixCompany.Value = tx_vw_purchasespost.ixCompany;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_purchasespost.UserName;

                            var ixPurchase = cmd.CreateParameter();
                            ixPurchase.ParameterName = "p3";
                            ixPurchase.DbType = DbType.Int64;
                            ixPurchase.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePurchases ");
                            sql.Append("@ixPerson = @p0, ");
                            if (tx_vw_purchasespost.ixCompany != null) { sql.Append("@ixCompany = @p1, "); }  
                            if (tx_vw_purchasespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPurchase = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPerson);
                            if (tx_vw_purchasespost.ixCompany != null) { cmd.Parameters.Add(ixCompany); }
                            if (tx_vw_purchasespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPurchase); 
                            cmd.ExecuteNonQuery();
                            tx_vw_purchasespost.ixPurchase = (Int64)ixPurchase.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPurchase"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePurchases @ixPurchase = @p0, @ixPerson = @p1, @ixCompany = @p2, @UserName = @p3", tx_vw_purchasespost.ixPurchase, tx_vw_purchasespost.ixPerson, tx_vw_purchasespost.ixCompany, tx_vw_purchasespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePurchases @ixPurchase = @p0, @ixPerson = @p1, @ixCompany = @p2, @UserName = @p3", tx_vw_purchasespost.ixPurchase, tx_vw_purchasespost.ixPerson, tx_vw_purchasespost.ixCompany, tx_vw_purchasespost.UserName);
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
  

