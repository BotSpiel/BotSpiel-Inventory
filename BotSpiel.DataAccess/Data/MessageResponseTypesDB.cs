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

    public class MessageResponseTypesDB : DbContext
    {

        public MessageResponseTypesDB(DbContextOptions<MessageResponseTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MessageResponseTypes> MessageResponseTypes { get; set; }
		public DbSet<MessageResponseTypesPost> MessageResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageResponseTypes>()
                .ToTable("config_vw_MessageResponseTypes")
                .HasKey(c => new { c.ixMessageResponseType });
            modelBuilder.Entity<MessageResponseTypesPost>()
                .ToTable("config_vw_MessageResponseTypesPost")
                .HasKey(c => new { c.ixMessageResponseType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MessageResponseTypesPost)).ToList())
            {
                var config_vw_messageresponsetypespost = e.Entity as MessageResponseTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMessageResponseType = cmd.CreateParameter();
                            sMessageResponseType.ParameterName = "p0";
                            sMessageResponseType.Value = config_vw_messageresponsetypespost.sMessageResponseType;
                            var sMessageResponseTypeCode = cmd.CreateParameter();
                            sMessageResponseTypeCode.ParameterName = "p1";
                            sMessageResponseTypeCode.Value = config_vw_messageresponsetypespost.sMessageResponseTypeCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_messageresponsetypespost.UserName;

                            var ixMessageResponseType = cmd.CreateParameter();
                            ixMessageResponseType.ParameterName = "p3";
                            ixMessageResponseType.DbType = DbType.Int64;
                            ixMessageResponseType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMessageResponseTypes ");
                            sql.Append("@sMessageResponseType = @p0, ");
                            sql.Append("@sMessageResponseTypeCode = @p1, ");
                            if (config_vw_messageresponsetypespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixMessageResponseType = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMessageResponseType);
                            cmd.Parameters.Add(sMessageResponseTypeCode);
                            if (config_vw_messageresponsetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMessageResponseType); 
                            cmd.ExecuteNonQuery();
                            config_vw_messageresponsetypespost.ixMessageResponseType = (Int64)ixMessageResponseType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMessageResponseType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMessageResponseTypes @ixMessageResponseType = @p0, @sMessageResponseType = @p1, @sMessageResponseTypeCode = @p2, @UserName = @p3", config_vw_messageresponsetypespost.ixMessageResponseType, config_vw_messageresponsetypespost.sMessageResponseType, config_vw_messageresponsetypespost.sMessageResponseTypeCode, config_vw_messageresponsetypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMessageResponseTypes @ixMessageResponseType = @p0, @sMessageResponseType = @p1, @sMessageResponseTypeCode = @p2, @UserName = @p3", config_vw_messageresponsetypespost.ixMessageResponseType, config_vw_messageresponsetypespost.sMessageResponseType, config_vw_messageresponsetypespost.sMessageResponseTypeCode, config_vw_messageresponsetypespost.UserName);
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
  

