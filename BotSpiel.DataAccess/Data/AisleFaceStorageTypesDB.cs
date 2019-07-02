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

    public class AisleFaceStorageTypesDB : DbContext
    {

        public AisleFaceStorageTypesDB(DbContextOptions<AisleFaceStorageTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<AisleFaceStorageTypes> AisleFaceStorageTypes { get; set; }
		public DbSet<AisleFaceStorageTypesPost> AisleFaceStorageTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AisleFaceStorageTypes>()
                .ToTable("config_vw_AisleFaceStorageTypes")
                .HasKey(c => new { c.ixAisleFaceStorageType });
            modelBuilder.Entity<AisleFaceStorageTypesPost>()
                .ToTable("config_vw_AisleFaceStorageTypesPost")
                .HasKey(c => new { c.ixAisleFaceStorageType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is AisleFaceStorageTypesPost)).ToList())
            {
                var config_vw_aislefacestoragetypespost = e.Entity as AisleFaceStorageTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sAisleFaceStorageType = cmd.CreateParameter();
                            sAisleFaceStorageType.ParameterName = "p0";
                            sAisleFaceStorageType.Value = config_vw_aislefacestoragetypespost.sAisleFaceStorageType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_aislefacestoragetypespost.UserName;

                            var ixAisleFaceStorageType = cmd.CreateParameter();
                            ixAisleFaceStorageType.ParameterName = "p2";
                            ixAisleFaceStorageType.DbType = DbType.Int64;
                            ixAisleFaceStorageType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateAisleFaceStorageTypes ");
                            sql.Append("@sAisleFaceStorageType = @p0, ");
                            if (config_vw_aislefacestoragetypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixAisleFaceStorageType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sAisleFaceStorageType);
                            if (config_vw_aislefacestoragetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixAisleFaceStorageType); 
                            cmd.ExecuteNonQuery();
                            config_vw_aislefacestoragetypespost.ixAisleFaceStorageType = (Int64)ixAisleFaceStorageType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixAisleFaceStorageType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeAisleFaceStorageTypes @ixAisleFaceStorageType = @p0, @sAisleFaceStorageType = @p1, @UserName = @p2", config_vw_aislefacestoragetypespost.ixAisleFaceStorageType, config_vw_aislefacestoragetypespost.sAisleFaceStorageType, config_vw_aislefacestoragetypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteAisleFaceStorageTypes @ixAisleFaceStorageType = @p0, @sAisleFaceStorageType = @p1, @UserName = @p2", config_vw_aislefacestoragetypespost.ixAisleFaceStorageType, config_vw_aislefacestoragetypespost.sAisleFaceStorageType, config_vw_aislefacestoragetypespost.UserName);
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
  

