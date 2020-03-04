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

    public class FoundationModuleGridsDB : DbContext
    {

        public FoundationModuleGridsDB(DbContextOptions<FoundationModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<FoundationModuleGrids> FoundationModuleGrids { get; set; }
		public DbSet<FoundationModuleGridsconfig> FoundationModuleGridsconfig { get; set; }
		public DbSet<FoundationModuleGridsmd> FoundationModuleGridsmd { get; set; }
		public DbSet<FoundationModuleGridstx> FoundationModuleGridstx { get; set; }
		public DbSet<FoundationModuleGridsanalytics> FoundationModuleGridsanalytics { get; set; }
		public DbSet<FoundationModuleGridsPost> FoundationModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoundationModuleGrids>()
                .ToTable("config_vw_FoundationModuleGrids")
                .HasKey(c => new { c.ixFoundationModuleGrid });
            modelBuilder.Entity<FoundationModuleGridsPost>()
                .ToTable("config_vw_FoundationModuleGridsPost")
                .HasKey(c => new { c.ixFoundationModuleGrid });
            modelBuilder.Entity<FoundationModuleGridsconfig>()
                .ToTable("config_vw_FoundationModuleGridsconfig")
                .HasKey(c => new { c.ixFoundationModuleGrid });
            modelBuilder.Entity<FoundationModuleGridsmd>()
                .ToTable("config_vw_FoundationModuleGridsmd")
                .HasKey(c => new { c.ixFoundationModuleGrid });
            modelBuilder.Entity<FoundationModuleGridstx>()
                .ToTable("config_vw_FoundationModuleGridstx")
                .HasKey(c => new { c.ixFoundationModuleGrid });
            modelBuilder.Entity<FoundationModuleGridsanalytics>()
                .ToTable("config_vw_FoundationModuleGridsanalytics")
                .HasKey(c => new { c.ixFoundationModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is FoundationModuleGridsPost)).ToList())
            {
                var config_vw_foundationmodulegridspost = e.Entity as FoundationModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFoundationModuleGrid = cmd.CreateParameter();
                            sFoundationModuleGrid.ParameterName = "p0";
                            sFoundationModuleGrid.Value = config_vw_foundationmodulegridspost.sFoundationModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_foundationmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_foundationmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_foundationmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_foundationmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_foundationmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_foundationmodulegridspost.UserName;

                            var ixFoundationModuleGrid = cmd.CreateParameter();
                            ixFoundationModuleGrid.ParameterName = "p7";
                            ixFoundationModuleGrid.DbType = DbType.Int64;
                            ixFoundationModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateFoundationModuleGrids ");
                            sql.Append("@sFoundationModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_foundationmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixFoundationModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFoundationModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_foundationmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFoundationModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_foundationmodulegridspost.ixFoundationModuleGrid = (Int64)ixFoundationModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFoundationModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeFoundationModuleGrids @ixFoundationModuleGrid = @p0, @sFoundationModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_foundationmodulegridspost.ixFoundationModuleGrid, config_vw_foundationmodulegridspost.sFoundationModuleGrid, config_vw_foundationmodulegridspost.sShortDescription, config_vw_foundationmodulegridspost.sDataEntityType, config_vw_foundationmodulegridspost.bCanCreate, config_vw_foundationmodulegridspost.bCanEdit, config_vw_foundationmodulegridspost.bCanDelete, config_vw_foundationmodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteFoundationModuleGrids @ixFoundationModuleGrid = @p0, @sFoundationModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_foundationmodulegridspost.ixFoundationModuleGrid, config_vw_foundationmodulegridspost.sFoundationModuleGrid, config_vw_foundationmodulegridspost.sShortDescription, config_vw_foundationmodulegridspost.sDataEntityType, config_vw_foundationmodulegridspost.bCanCreate, config_vw_foundationmodulegridspost.bCanEdit, config_vw_foundationmodulegridspost.bCanDelete, config_vw_foundationmodulegridspost.UserName);
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
  

