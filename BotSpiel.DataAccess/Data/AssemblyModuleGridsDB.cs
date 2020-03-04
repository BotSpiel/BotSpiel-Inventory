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

    public class AssemblyModuleGridsDB : DbContext
    {

        public AssemblyModuleGridsDB(DbContextOptions<AssemblyModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<AssemblyModuleGrids> AssemblyModuleGrids { get; set; }
		public DbSet<AssemblyModuleGridsconfig> AssemblyModuleGridsconfig { get; set; }
		public DbSet<AssemblyModuleGridsmd> AssemblyModuleGridsmd { get; set; }
		public DbSet<AssemblyModuleGridstx> AssemblyModuleGridstx { get; set; }
		public DbSet<AssemblyModuleGridsanalytics> AssemblyModuleGridsanalytics { get; set; }
		public DbSet<AssemblyModuleGridsPost> AssemblyModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssemblyModuleGrids>()
                .ToTable("config_vw_AssemblyModuleGrids")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
            modelBuilder.Entity<AssemblyModuleGridsPost>()
                .ToTable("config_vw_AssemblyModuleGridsPost")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
            modelBuilder.Entity<AssemblyModuleGridsconfig>()
                .ToTable("config_vw_AssemblyModuleGridsconfig")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
            modelBuilder.Entity<AssemblyModuleGridsmd>()
                .ToTable("config_vw_AssemblyModuleGridsmd")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
            modelBuilder.Entity<AssemblyModuleGridstx>()
                .ToTable("config_vw_AssemblyModuleGridstx")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
            modelBuilder.Entity<AssemblyModuleGridsanalytics>()
                .ToTable("config_vw_AssemblyModuleGridsanalytics")
                .HasKey(c => new { c.ixAssemblyModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is AssemblyModuleGridsPost)).ToList())
            {
                var config_vw_assemblymodulegridspost = e.Entity as AssemblyModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sAssemblyModuleGrid = cmd.CreateParameter();
                            sAssemblyModuleGrid.ParameterName = "p0";
                            sAssemblyModuleGrid.Value = config_vw_assemblymodulegridspost.sAssemblyModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_assemblymodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_assemblymodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_assemblymodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_assemblymodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_assemblymodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_assemblymodulegridspost.UserName;

                            var ixAssemblyModuleGrid = cmd.CreateParameter();
                            ixAssemblyModuleGrid.ParameterName = "p7";
                            ixAssemblyModuleGrid.DbType = DbType.Int64;
                            ixAssemblyModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateAssemblyModuleGrids ");
                            sql.Append("@sAssemblyModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_assemblymodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixAssemblyModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sAssemblyModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_assemblymodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixAssemblyModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_assemblymodulegridspost.ixAssemblyModuleGrid = (Int64)ixAssemblyModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixAssemblyModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeAssemblyModuleGrids @ixAssemblyModuleGrid = @p0, @sAssemblyModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_assemblymodulegridspost.ixAssemblyModuleGrid, config_vw_assemblymodulegridspost.sAssemblyModuleGrid, config_vw_assemblymodulegridspost.sShortDescription, config_vw_assemblymodulegridspost.sDataEntityType, config_vw_assemblymodulegridspost.bCanCreate, config_vw_assemblymodulegridspost.bCanEdit, config_vw_assemblymodulegridspost.bCanDelete, config_vw_assemblymodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteAssemblyModuleGrids @ixAssemblyModuleGrid = @p0, @sAssemblyModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_assemblymodulegridspost.ixAssemblyModuleGrid, config_vw_assemblymodulegridspost.sAssemblyModuleGrid, config_vw_assemblymodulegridspost.sShortDescription, config_vw_assemblymodulegridspost.sDataEntityType, config_vw_assemblymodulegridspost.bCanCreate, config_vw_assemblymodulegridspost.bCanEdit, config_vw_assemblymodulegridspost.bCanDelete, config_vw_assemblymodulegridspost.UserName);
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
  

