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

    public class ContactFunctionsDB : DbContext
    {

        public ContactFunctionsDB(DbContextOptions<ContactFunctionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<ContactFunctions> ContactFunctions { get; set; }
		public DbSet<ContactFunctionsPost> ContactFunctionsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactFunctions>()
                .ToTable("config_vw_ContactFunctions")
                .HasKey(c => new { c.ixContactFunction });
            modelBuilder.Entity<ContactFunctionsPost>()
                .ToTable("config_vw_ContactFunctionsPost")
                .HasKey(c => new { c.ixContactFunction });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ContactFunctionsPost)).ToList())
            {
                var config_vw_contactfunctionspost = e.Entity as ContactFunctionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sContactFunction = cmd.CreateParameter();
                            sContactFunction.ParameterName = "p0";
                            sContactFunction.Value = config_vw_contactfunctionspost.sContactFunction;
                            var sContactFunctionCode = cmd.CreateParameter();
                            sContactFunctionCode.ParameterName = "p1";
                            sContactFunctionCode.Value = config_vw_contactfunctionspost.sContactFunctionCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_contactfunctionspost.UserName;

                            var ixContactFunction = cmd.CreateParameter();
                            ixContactFunction.ParameterName = "p3";
                            ixContactFunction.DbType = DbType.Int64;
                            ixContactFunction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateContactFunctions ");
                            sql.Append("@sContactFunction = @p0, ");
                            sql.Append("@sContactFunctionCode = @p1, ");
                            if (config_vw_contactfunctionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixContactFunction = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sContactFunction);
                            cmd.Parameters.Add(sContactFunctionCode);
                            if (config_vw_contactfunctionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixContactFunction); 
                            cmd.ExecuteNonQuery();
                            config_vw_contactfunctionspost.ixContactFunction = (Int64)ixContactFunction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixContactFunction"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeContactFunctions @ixContactFunction = @p0, @sContactFunction = @p1, @sContactFunctionCode = @p2, @UserName = @p3", config_vw_contactfunctionspost.ixContactFunction, config_vw_contactfunctionspost.sContactFunction, config_vw_contactfunctionspost.sContactFunctionCode, config_vw_contactfunctionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteContactFunctions @ixContactFunction = @p0, @sContactFunction = @p1, @sContactFunctionCode = @p2, @UserName = @p3", config_vw_contactfunctionspost.ixContactFunction, config_vw_contactfunctionspost.sContactFunction, config_vw_contactfunctionspost.sContactFunctionCode, config_vw_contactfunctionspost.UserName);
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
  

