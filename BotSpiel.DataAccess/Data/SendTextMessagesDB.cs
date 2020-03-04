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

    public class SendTextMessagesDB : DbContext
    {

        public SendTextMessagesDB(DbContextOptions<SendTextMessagesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<SendTextMessages> SendTextMessages { get; set; }
		public DbSet<SendTextMessagesPost> SendTextMessagesPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is SendTextMessagesPost)).ToList())
            {
                var tx_vw_sendtextmessagespost = e.Entity as SendTextMessagesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPerson = cmd.CreateParameter();
                            ixPerson.ParameterName = "p0";
                            ixPerson.Value = tx_vw_sendtextmessagespost.ixPerson;
                            var sContent = cmd.CreateParameter();
                            sContent.ParameterName = "p1";
                            sContent.Value = tx_vw_sendtextmessagespost.sContent;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_sendtextmessagespost.UserName;

                            var ixSendTextMessage = cmd.CreateParameter();
                            ixSendTextMessage.ParameterName = "p3";
                            ixSendTextMessage.DbType = DbType.Int64;
                            ixSendTextMessage.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateSendTextMessages ");
                            sql.Append("@ixPerson = @p0, ");
                            sql.Append("@sContent = @p1, ");
                            if (tx_vw_sendtextmessagespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixSendTextMessage = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPerson);
                            cmd.Parameters.Add(sContent);
                            if (tx_vw_sendtextmessagespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixSendTextMessage); 
                            cmd.ExecuteNonQuery();
                            tx_vw_sendtextmessagespost.ixSendTextMessage = (Int64)ixSendTextMessage.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixSendTextMessage"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeSendTextMessages @ixSendTextMessage = @p0, @ixPerson = @p1, @sContent = @p2, @UserName = @p3", tx_vw_sendtextmessagespost.ixSendTextMessage, tx_vw_sendtextmessagespost.ixPerson, tx_vw_sendtextmessagespost.sContent, tx_vw_sendtextmessagespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteSendTextMessages @ixSendTextMessage = @p0, @ixPerson = @p1, @sContent = @p2, @UserName = @p3", tx_vw_sendtextmessagespost.ixSendTextMessage, tx_vw_sendtextmessagespost.ixPerson, tx_vw_sendtextmessagespost.sContent, tx_vw_sendtextmessagespost.UserName);
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
  

