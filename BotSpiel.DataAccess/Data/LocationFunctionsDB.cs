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

    public class LocationFunctionsDB : DbContext
    {

        public LocationFunctionsDB(DbContextOptions<LocationFunctionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<LocationFunctions> LocationFunctions { get; set; }
		public DbSet<LocationFunctionsPost> LocationFunctionsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationFunctions>()
                .ToTable("config_vw_LocationFunctions")
                .HasKey(c => new { c.ixLocationFunction });
            modelBuilder.Entity<LocationFunctionsPost>()
                .ToTable("config_vw_LocationFunctionsPost")
                .HasKey(c => new { c.ixLocationFunction });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is LocationFunctionsPost)).ToList())
            {
                var config_vw_locationfunctionspost = e.Entity as LocationFunctionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sLocationFunction = cmd.CreateParameter();
                            sLocationFunction.ParameterName = "p0";
                            sLocationFunction.Value = config_vw_locationfunctionspost.sLocationFunction;
                            var sLocationFunctionCode = cmd.CreateParameter();
                            sLocationFunctionCode.ParameterName = "p1";
                            sLocationFunctionCode.Value = config_vw_locationfunctionspost.sLocationFunctionCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_locationfunctionspost.UserName;

                            var ixLocationFunction = cmd.CreateParameter();
                            ixLocationFunction.ParameterName = "p3";
                            ixLocationFunction.DbType = DbType.Int64;
                            ixLocationFunction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateLocationFunctions ");
                            sql.Append("@sLocationFunction = @p0, ");
                            sql.Append("@sLocationFunctionCode = @p1, ");
                            if (config_vw_locationfunctionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixLocationFunction = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sLocationFunction);
                            cmd.Parameters.Add(sLocationFunctionCode);
                            if (config_vw_locationfunctionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixLocationFunction); 
                            cmd.ExecuteNonQuery();
                            config_vw_locationfunctionspost.ixLocationFunction = (Int64)ixLocationFunction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixLocationFunction"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeLocationFunctions @ixLocationFunction = @p0, @sLocationFunction = @p1, @sLocationFunctionCode = @p2, @UserName = @p3", config_vw_locationfunctionspost.ixLocationFunction, config_vw_locationfunctionspost.sLocationFunction, config_vw_locationfunctionspost.sLocationFunctionCode, config_vw_locationfunctionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteLocationFunctions @ixLocationFunction = @p0, @sLocationFunction = @p1, @sLocationFunctionCode = @p2, @UserName = @p3", config_vw_locationfunctionspost.ixLocationFunction, config_vw_locationfunctionspost.sLocationFunction, config_vw_locationfunctionspost.sLocationFunctionCode, config_vw_locationfunctionspost.UserName);
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
  

