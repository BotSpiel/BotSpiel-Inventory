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

    public class MessageFunctionsDB : DbContext
    {

        public MessageFunctionsDB(DbContextOptions<MessageFunctionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MessageFunctions> MessageFunctions { get; set; }
		public DbSet<MessageFunctionsPost> MessageFunctionsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageFunctions>()
                .ToTable("config_vw_MessageFunctions")
                .HasKey(c => new { c.ixMessageFunction });
            modelBuilder.Entity<MessageFunctionsPost>()
                .ToTable("config_vw_MessageFunctionsPost")
                .HasKey(c => new { c.ixMessageFunction });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is MessageFunctionsPost)).ToList())
            {
                var config_vw_messagefunctionspost = e.Entity as MessageFunctionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMessageFunction = cmd.CreateParameter();
                            sMessageFunction.ParameterName = "p0";
                            sMessageFunction.Value = config_vw_messagefunctionspost.sMessageFunction;
                            var sMessageFunctionCode = cmd.CreateParameter();
                            sMessageFunctionCode.ParameterName = "p1";
                            sMessageFunctionCode.Value = config_vw_messagefunctionspost.sMessageFunctionCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_messagefunctionspost.UserName;

                            var ixMessageFunction = cmd.CreateParameter();
                            ixMessageFunction.ParameterName = "p3";
                            ixMessageFunction.DbType = DbType.Int64;
                            ixMessageFunction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMessageFunctions ");
                            sql.Append("@sMessageFunction = @p0, ");
                            sql.Append("@sMessageFunctionCode = @p1, ");
                            if (config_vw_messagefunctionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixMessageFunction = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMessageFunction);
                            cmd.Parameters.Add(sMessageFunctionCode);
                            if (config_vw_messagefunctionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMessageFunction); 
                            cmd.ExecuteNonQuery();
                            config_vw_messagefunctionspost.ixMessageFunction = (Int64)ixMessageFunction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMessageFunction"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMessageFunctions @ixMessageFunction = @p0, @sMessageFunction = @p1, @sMessageFunctionCode = @p2, @UserName = @p3", config_vw_messagefunctionspost.ixMessageFunction, config_vw_messagefunctionspost.sMessageFunction, config_vw_messagefunctionspost.sMessageFunctionCode, config_vw_messagefunctionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMessageFunctions @ixMessageFunction = @p0, @sMessageFunction = @p1, @sMessageFunctionCode = @p2, @UserName = @p3", config_vw_messagefunctionspost.ixMessageFunction, config_vw_messagefunctionspost.sMessageFunction, config_vw_messagefunctionspost.sMessageFunctionCode, config_vw_messagefunctionspost.UserName);
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
  

