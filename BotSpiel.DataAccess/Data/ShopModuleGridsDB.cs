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

    public class ShopModuleGridsDB : DbContext
    {

        public ShopModuleGridsDB(DbContextOptions<ShopModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<ShopModuleGrids> ShopModuleGrids { get; set; }
		public DbSet<ShopModuleGridsconfig> ShopModuleGridsconfig { get; set; }
		public DbSet<ShopModuleGridsmd> ShopModuleGridsmd { get; set; }
		public DbSet<ShopModuleGridstx> ShopModuleGridstx { get; set; }
		public DbSet<ShopModuleGridsanalytics> ShopModuleGridsanalytics { get; set; }
		public DbSet<ShopModuleGridsPost> ShopModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopModuleGrids>()
                .ToTable("config_vw_ShopModuleGrids")
                .HasKey(c => new { c.ixShopModuleGrid });
            modelBuilder.Entity<ShopModuleGridsPost>()
                .ToTable("config_vw_ShopModuleGridsPost")
                .HasKey(c => new { c.ixShopModuleGrid });
            modelBuilder.Entity<ShopModuleGridsconfig>()
                .ToTable("config_vw_ShopModuleGridsconfig")
                .HasKey(c => new { c.ixShopModuleGrid });
            modelBuilder.Entity<ShopModuleGridsmd>()
                .ToTable("config_vw_ShopModuleGridsmd")
                .HasKey(c => new { c.ixShopModuleGrid });
            modelBuilder.Entity<ShopModuleGridstx>()
                .ToTable("config_vw_ShopModuleGridstx")
                .HasKey(c => new { c.ixShopModuleGrid });
            modelBuilder.Entity<ShopModuleGridsanalytics>()
                .ToTable("config_vw_ShopModuleGridsanalytics")
                .HasKey(c => new { c.ixShopModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ShopModuleGridsPost)).ToList())
            {
                var config_vw_shopmodulegridspost = e.Entity as ShopModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sShopModuleGrid = cmd.CreateParameter();
                            sShopModuleGrid.ParameterName = "p0";
                            sShopModuleGrid.Value = config_vw_shopmodulegridspost.sShopModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_shopmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_shopmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_shopmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_shopmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_shopmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_shopmodulegridspost.UserName;

                            var ixShopModuleGrid = cmd.CreateParameter();
                            ixShopModuleGrid.ParameterName = "p7";
                            ixShopModuleGrid.DbType = DbType.Int64;
                            ixShopModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateShopModuleGrids ");
                            sql.Append("@sShopModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_shopmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixShopModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sShopModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_shopmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixShopModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_shopmodulegridspost.ixShopModuleGrid = (Int64)ixShopModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixShopModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeShopModuleGrids @ixShopModuleGrid = @p0, @sShopModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_shopmodulegridspost.ixShopModuleGrid, config_vw_shopmodulegridspost.sShopModuleGrid, config_vw_shopmodulegridspost.sShortDescription, config_vw_shopmodulegridspost.sDataEntityType, config_vw_shopmodulegridspost.bCanCreate, config_vw_shopmodulegridspost.bCanEdit, config_vw_shopmodulegridspost.bCanDelete, config_vw_shopmodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteShopModuleGrids @ixShopModuleGrid = @p0, @sShopModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_shopmodulegridspost.ixShopModuleGrid, config_vw_shopmodulegridspost.sShopModuleGrid, config_vw_shopmodulegridspost.sShortDescription, config_vw_shopmodulegridspost.sDataEntityType, config_vw_shopmodulegridspost.bCanCreate, config_vw_shopmodulegridspost.bCanEdit, config_vw_shopmodulegridspost.bCanDelete, config_vw_shopmodulegridspost.UserName);
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
  

