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

    public class InventoryModuleGridsDB : DbContext
    {

        public InventoryModuleGridsDB(DbContextOptions<InventoryModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryModuleGrids> InventoryModuleGrids { get; set; }
		public DbSet<InventoryModuleGridsconfig> InventoryModuleGridsconfig { get; set; }
		public DbSet<InventoryModuleGridsmd> InventoryModuleGridsmd { get; set; }
		public DbSet<InventoryModuleGridstx> InventoryModuleGridstx { get; set; }
		public DbSet<InventoryModuleGridsanalytics> InventoryModuleGridsanalytics { get; set; }
		public DbSet<InventoryModuleGridsPost> InventoryModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryModuleGrids>()
                .ToTable("config_vw_InventoryModuleGrids")
                .HasKey(c => new { c.ixInventoryModuleGrid });
            modelBuilder.Entity<InventoryModuleGridsPost>()
                .ToTable("config_vw_InventoryModuleGridsPost")
                .HasKey(c => new { c.ixInventoryModuleGrid });
            modelBuilder.Entity<InventoryModuleGridsconfig>()
                .ToTable("config_vw_InventoryModuleGridsconfig")
                .HasKey(c => new { c.ixInventoryModuleGrid });
            modelBuilder.Entity<InventoryModuleGridsmd>()
                .ToTable("config_vw_InventoryModuleGridsmd")
                .HasKey(c => new { c.ixInventoryModuleGrid });
            modelBuilder.Entity<InventoryModuleGridstx>()
                .ToTable("config_vw_InventoryModuleGridstx")
                .HasKey(c => new { c.ixInventoryModuleGrid });
            modelBuilder.Entity<InventoryModuleGridsanalytics>()
                .ToTable("config_vw_InventoryModuleGridsanalytics")
                .HasKey(c => new { c.ixInventoryModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InventoryModuleGridsPost)).ToList())
            {
                var config_vw_inventorymodulegridspost = e.Entity as InventoryModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInventoryModuleGrid = cmd.CreateParameter();
                            sInventoryModuleGrid.ParameterName = "p0";
                            sInventoryModuleGrid.Value = config_vw_inventorymodulegridspost.sInventoryModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_inventorymodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_inventorymodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_inventorymodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_inventorymodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_inventorymodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_inventorymodulegridspost.UserName;

                            var ixInventoryModuleGrid = cmd.CreateParameter();
                            ixInventoryModuleGrid.ParameterName = "p7";
                            ixInventoryModuleGrid.DbType = DbType.Int64;
                            ixInventoryModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInventoryModuleGrids ");
                            sql.Append("@sInventoryModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_inventorymodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixInventoryModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInventoryModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_inventorymodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_inventorymodulegridspost.ixInventoryModuleGrid = (Int64)ixInventoryModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryModuleGrid"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInventoryModuleGrids @ixInventoryModuleGrid = @p0, @sInventoryModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_inventorymodulegridspost.ixInventoryModuleGrid, config_vw_inventorymodulegridspost.sInventoryModuleGrid, config_vw_inventorymodulegridspost.sShortDescription, config_vw_inventorymodulegridspost.sDataEntityType, config_vw_inventorymodulegridspost.bCanCreate, config_vw_inventorymodulegridspost.bCanEdit, config_vw_inventorymodulegridspost.bCanDelete, config_vw_inventorymodulegridspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInventoryModuleGrids @ixInventoryModuleGrid = @p0, @sInventoryModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_inventorymodulegridspost.ixInventoryModuleGrid, config_vw_inventorymodulegridspost.sInventoryModuleGrid, config_vw_inventorymodulegridspost.sShortDescription, config_vw_inventorymodulegridspost.sDataEntityType, config_vw_inventorymodulegridspost.bCanCreate, config_vw_inventorymodulegridspost.bCanEdit, config_vw_inventorymodulegridspost.bCanDelete, config_vw_inventorymodulegridspost.UserName);
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
  

