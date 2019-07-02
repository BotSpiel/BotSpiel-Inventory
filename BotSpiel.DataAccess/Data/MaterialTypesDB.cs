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

    public class MaterialTypesDB : DbContext
    {

        public MaterialTypesDB(DbContextOptions<MaterialTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MaterialTypesPost)).ToList())
            {
                var config_vw_materialtypespost = e.Entity as MaterialTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMaterialType = cmd.CreateParameter();
                            sMaterialType.ParameterName = "p0";
                            sMaterialType.Value = config_vw_materialtypespost.sMaterialType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_materialtypespost.UserName;

                            var ixMaterialType = cmd.CreateParameter();
                            ixMaterialType.ParameterName = "p2";
                            ixMaterialType.DbType = DbType.Int64;
                            ixMaterialType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMaterialTypes ");
                            sql.Append("@sMaterialType = @p0, ");
                            if (config_vw_materialtypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixMaterialType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMaterialType);
                            if (config_vw_materialtypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMaterialType); 
                            cmd.ExecuteNonQuery();
                            config_vw_materialtypespost.ixMaterialType = (Int64)ixMaterialType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMaterialType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMaterialTypes @ixMaterialType = @p0, @sMaterialType = @p1, @UserName = @p2", config_vw_materialtypespost.ixMaterialType, config_vw_materialtypespost.sMaterialType, config_vw_materialtypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMaterialTypes @ixMaterialType = @p0, @sMaterialType = @p1, @UserName = @p2", config_vw_materialtypespost.ixMaterialType, config_vw_materialtypespost.sMaterialType, config_vw_materialtypespost.UserName);
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
  

