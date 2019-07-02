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

    public class InboundModuleGridsDB : DbContext
    {

        public InboundModuleGridsDB(DbContextOptions<InboundModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InboundModuleGrids> InboundModuleGrids { get; set; }
		public DbSet<InboundModuleGridsconfig> InboundModuleGridsconfig { get; set; }
		public DbSet<InboundModuleGridsmd> InboundModuleGridsmd { get; set; }
		public DbSet<InboundModuleGridstx> InboundModuleGridstx { get; set; }
		public DbSet<InboundModuleGridsanalytics> InboundModuleGridsanalytics { get; set; }
		public DbSet<InboundModuleGridsPost> InboundModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InboundModuleGrids>()
                .ToTable("config_vw_InboundModuleGrids")
                .HasKey(c => new { c.ixInboundModuleGrid });
            modelBuilder.Entity<InboundModuleGridsPost>()
                .ToTable("config_vw_InboundModuleGridsPost")
                .HasKey(c => new { c.ixInboundModuleGrid });
            modelBuilder.Entity<InboundModuleGridsconfig>()
                .ToTable("config_vw_InboundModuleGridsconfig")
                .HasKey(c => new { c.ixInboundModuleGrid });
            modelBuilder.Entity<InboundModuleGridsmd>()
                .ToTable("config_vw_InboundModuleGridsmd")
                .HasKey(c => new { c.ixInboundModuleGrid });
            modelBuilder.Entity<InboundModuleGridstx>()
                .ToTable("config_vw_InboundModuleGridstx")
                .HasKey(c => new { c.ixInboundModuleGrid });
            modelBuilder.Entity<InboundModuleGridsanalytics>()
                .ToTable("config_vw_InboundModuleGridsanalytics")
                .HasKey(c => new { c.ixInboundModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InboundModuleGridsPost)).ToList())
            {
                var config_vw_inboundmodulegridspost = e.Entity as InboundModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInboundModuleGrid = cmd.CreateParameter();
                            sInboundModuleGrid.ParameterName = "p0";
                            sInboundModuleGrid.Value = config_vw_inboundmodulegridspost.sInboundModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_inboundmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_inboundmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_inboundmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_inboundmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_inboundmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_inboundmodulegridspost.UserName;

                            var ixInboundModuleGrid = cmd.CreateParameter();
                            ixInboundModuleGrid.ParameterName = "p7";
                            ixInboundModuleGrid.DbType = DbType.Int64;
                            ixInboundModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInboundModuleGrids ");
                            sql.Append("@sInboundModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_inboundmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixInboundModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInboundModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_inboundmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInboundModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_inboundmodulegridspost.ixInboundModuleGrid = (Int64)ixInboundModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInboundModuleGrid"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInboundModuleGrids @ixInboundModuleGrid = @p0, @sInboundModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_inboundmodulegridspost.ixInboundModuleGrid, config_vw_inboundmodulegridspost.sInboundModuleGrid, config_vw_inboundmodulegridspost.sShortDescription, config_vw_inboundmodulegridspost.sDataEntityType, config_vw_inboundmodulegridspost.bCanCreate, config_vw_inboundmodulegridspost.bCanEdit, config_vw_inboundmodulegridspost.bCanDelete, config_vw_inboundmodulegridspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInboundModuleGrids @ixInboundModuleGrid = @p0, @sInboundModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_inboundmodulegridspost.ixInboundModuleGrid, config_vw_inboundmodulegridspost.sInboundModuleGrid, config_vw_inboundmodulegridspost.sShortDescription, config_vw_inboundmodulegridspost.sDataEntityType, config_vw_inboundmodulegridspost.bCanCreate, config_vw_inboundmodulegridspost.bCanEdit, config_vw_inboundmodulegridspost.bCanDelete, config_vw_inboundmodulegridspost.UserName);
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
  

