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

    public class PurchaseTextMessagesDB : DbContext
    {

        public PurchaseTextMessagesDB(DbContextOptions<PurchaseTextMessagesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PurchaseTextMessages> PurchaseTextMessages { get; set; }
		public DbSet<PurchaseTextMessagesPost> PurchaseTextMessagesPost { get; set; }
		public DbSet<Purchases> Purchases { get; set; }
		public DbSet<PurchasesPost> PurchasesPost { get; set; }
		public DbSet<SendTextMessages> SendTextMessages { get; set; }
		public DbSet<SendTextMessagesPost> SendTextMessagesPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseTextMessages>()
                .ToTable("tx_vw_PurchaseTextMessages")
                .HasKey(c => new { c.ixPurchaseTextMessage });
            modelBuilder.Entity<PurchaseTextMessagesPost>()
                .ToTable("tx_vw_PurchaseTextMessagesPost")
                .HasKey(c => new { c.ixPurchaseTextMessage });
            modelBuilder.Entity<Purchases>()
                .ToTable("tx_vw_Purchases")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<PurchasesPost>()
                .ToTable("tx_vw_PurchasesPost")
                .HasKey(c => new { c.ixPurchase });
            modelBuilder.Entity<SendTextMessages>()
                .ToTable("tx_vw_SendTextMessages")
                .HasKey(c => new { c.ixSendTextMessage });
            modelBuilder.Entity<SendTextMessagesPost>()
                .ToTable("tx_vw_SendTextMessagesPost")
                .HasKey(c => new { c.ixSendTextMessage });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PurchaseTextMessagesPost)).ToList())
            {
                var tx_vw_purchasetextmessagespost = e.Entity as PurchaseTextMessagesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPurchase = cmd.CreateParameter();
                            ixPurchase.ParameterName = "p0";
                            ixPurchase.Value = tx_vw_purchasetextmessagespost.ixPurchase;
                            var ixSendTextMessage = cmd.CreateParameter();
                            ixSendTextMessage.ParameterName = "p1";
                            ixSendTextMessage.Value = tx_vw_purchasetextmessagespost.ixSendTextMessage;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_purchasetextmessagespost.UserName;

                            var ixPurchaseTextMessage = cmd.CreateParameter();
                            ixPurchaseTextMessage.ParameterName = "p3";
                            ixPurchaseTextMessage.DbType = DbType.Int64;
                            ixPurchaseTextMessage.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePurchaseTextMessages ");
                            sql.Append("@ixPurchase = @p0, ");
                            sql.Append("@ixSendTextMessage = @p1, ");
                            if (tx_vw_purchasetextmessagespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPurchaseTextMessage = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPurchase);
                            cmd.Parameters.Add(ixSendTextMessage);
                            if (tx_vw_purchasetextmessagespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPurchaseTextMessage); 
                            cmd.ExecuteNonQuery();
                            tx_vw_purchasetextmessagespost.ixPurchaseTextMessage = (Int64)ixPurchaseTextMessage.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPurchaseTextMessage"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePurchaseTextMessages @ixPurchaseTextMessage = @p0, @ixPurchase = @p1, @ixSendTextMessage = @p2, @UserName = @p3", tx_vw_purchasetextmessagespost.ixPurchaseTextMessage, tx_vw_purchasetextmessagespost.ixPurchase, tx_vw_purchasetextmessagespost.ixSendTextMessage, tx_vw_purchasetextmessagespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePurchaseTextMessages @ixPurchaseTextMessage = @p0, @ixPurchase = @p1, @ixSendTextMessage = @p2, @UserName = @p3", tx_vw_purchasetextmessagespost.ixPurchaseTextMessage, tx_vw_purchasetextmessagespost.ixPurchase, tx_vw_purchasetextmessagespost.ixSendTextMessage, tx_vw_purchasetextmessagespost.UserName);
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
  

