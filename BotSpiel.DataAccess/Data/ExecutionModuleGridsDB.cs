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

    public class ExecutionModuleGridsDB : DbContext
    {

        public ExecutionModuleGridsDB(DbContextOptions<ExecutionModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<ExecutionModuleGrids> ExecutionModuleGrids { get; set; }
		public DbSet<ExecutionModuleGridsconfig> ExecutionModuleGridsconfig { get; set; }
		public DbSet<ExecutionModuleGridsmd> ExecutionModuleGridsmd { get; set; }
		public DbSet<ExecutionModuleGridstx> ExecutionModuleGridstx { get; set; }
		public DbSet<ExecutionModuleGridsanalytics> ExecutionModuleGridsanalytics { get; set; }
		public DbSet<ExecutionModuleGridsPost> ExecutionModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExecutionModuleGrids>()
                .ToTable("config_vw_ExecutionModuleGrids")
                .HasKey(c => new { c.ixExecutionModuleGrid });
            modelBuilder.Entity<ExecutionModuleGridsPost>()
                .ToTable("config_vw_ExecutionModuleGridsPost")
                .HasKey(c => new { c.ixExecutionModuleGrid });
            modelBuilder.Entity<ExecutionModuleGridsconfig>()
                .ToTable("config_vw_ExecutionModuleGridsconfig")
                .HasKey(c => new { c.ixExecutionModuleGrid });
            modelBuilder.Entity<ExecutionModuleGridsmd>()
                .ToTable("config_vw_ExecutionModuleGridsmd")
                .HasKey(c => new { c.ixExecutionModuleGrid });
            modelBuilder.Entity<ExecutionModuleGridstx>()
                .ToTable("config_vw_ExecutionModuleGridstx")
                .HasKey(c => new { c.ixExecutionModuleGrid });
            modelBuilder.Entity<ExecutionModuleGridsanalytics>()
                .ToTable("config_vw_ExecutionModuleGridsanalytics")
                .HasKey(c => new { c.ixExecutionModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ExecutionModuleGridsPost)).ToList())
            {
                var config_vw_executionmodulegridspost = e.Entity as ExecutionModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sExecutionModuleGrid = cmd.CreateParameter();
                            sExecutionModuleGrid.ParameterName = "p0";
                            sExecutionModuleGrid.Value = config_vw_executionmodulegridspost.sExecutionModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_executionmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_executionmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_executionmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_executionmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_executionmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_executionmodulegridspost.UserName;

                            var ixExecutionModuleGrid = cmd.CreateParameter();
                            ixExecutionModuleGrid.ParameterName = "p7";
                            ixExecutionModuleGrid.DbType = DbType.Int64;
                            ixExecutionModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateExecutionModuleGrids ");
                            sql.Append("@sExecutionModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_executionmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixExecutionModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sExecutionModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_executionmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixExecutionModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_executionmodulegridspost.ixExecutionModuleGrid = (Int64)ixExecutionModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixExecutionModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeExecutionModuleGrids @ixExecutionModuleGrid = @p0, @sExecutionModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_executionmodulegridspost.ixExecutionModuleGrid, config_vw_executionmodulegridspost.sExecutionModuleGrid, config_vw_executionmodulegridspost.sShortDescription, config_vw_executionmodulegridspost.sDataEntityType, config_vw_executionmodulegridspost.bCanCreate, config_vw_executionmodulegridspost.bCanEdit, config_vw_executionmodulegridspost.bCanDelete, config_vw_executionmodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteExecutionModuleGrids @ixExecutionModuleGrid = @p0, @sExecutionModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_executionmodulegridspost.ixExecutionModuleGrid, config_vw_executionmodulegridspost.sExecutionModuleGrid, config_vw_executionmodulegridspost.sShortDescription, config_vw_executionmodulegridspost.sDataEntityType, config_vw_executionmodulegridspost.bCanCreate, config_vw_executionmodulegridspost.bCanEdit, config_vw_executionmodulegridspost.bCanDelete, config_vw_executionmodulegridspost.UserName);
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
  

