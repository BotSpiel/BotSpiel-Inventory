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

    public class BotspielBotMessagesDB : DbContext
    {

        public BotspielBotMessagesDB(DbContextOptions<BotspielBotMessagesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<BotspielBotMessages> BotspielBotMessages { get; set; }
		public DbSet<BotspielBotMessagesPost> BotspielBotMessagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotspielBotMessages>()
                .ToTable("tx_vw_BotspielBotMessages")
                .HasKey(c => new { c.ixBotspielBotMessage });
            modelBuilder.Entity<BotspielBotMessagesPost>()
                .ToTable("tx_vw_BotspielBotMessagesPost")
                .HasKey(c => new { c.ixBotspielBotMessage });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is BotspielBotMessagesPost)).ToList())
            {
                var tx_vw_botspielbotmessagespost = e.Entity as BotspielBotMessagesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMyMessage = cmd.CreateParameter();
                            sMyMessage.ParameterName = "p0";
                            sMyMessage.Value = tx_vw_botspielbotmessagespost.sMyMessage;
                            var sYourReply = cmd.CreateParameter();
                            sYourReply.ParameterName = "p1";
                            sYourReply.Value = tx_vw_botspielbotmessagespost.sYourReply;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_botspielbotmessagespost.UserName;

                            var ixBotspielBotMessage = cmd.CreateParameter();
                            ixBotspielBotMessage.ParameterName = "p3";
                            ixBotspielBotMessage.DbType = DbType.Int64;
                            ixBotspielBotMessage.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateBotspielBotMessages ");
                            sql.Append("@sMyMessage = @p0, ");
                            sql.Append("@sYourReply = @p1, ");
                            if (tx_vw_botspielbotmessagespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixBotspielBotMessage = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMyMessage);
                            cmd.Parameters.Add(sYourReply);
                            if (tx_vw_botspielbotmessagespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixBotspielBotMessage); 
                            cmd.ExecuteNonQuery();
                            tx_vw_botspielbotmessagespost.ixBotspielBotMessage = (Int64)ixBotspielBotMessage.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixBotspielBotMessage"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeBotspielBotMessages @ixBotspielBotMessage = @p0, @sMyMessage = @p1, @sYourReply = @p2, @UserName = @p3", tx_vw_botspielbotmessagespost.ixBotspielBotMessage, tx_vw_botspielbotmessagespost.sMyMessage, tx_vw_botspielbotmessagespost.sYourReply, tx_vw_botspielbotmessagespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteBotspielBotMessages @ixBotspielBotMessage = @p0, @sMyMessage = @p1, @sYourReply = @p2, @UserName = @p3", tx_vw_botspielbotmessagespost.ixBotspielBotMessage, tx_vw_botspielbotmessagespost.sMyMessage, tx_vw_botspielbotmessagespost.sYourReply, tx_vw_botspielbotmessagespost.UserName);
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
  

