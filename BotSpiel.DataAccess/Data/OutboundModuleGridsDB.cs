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

    public class OutboundModuleGridsDB : DbContext
    {

        public OutboundModuleGridsDB(DbContextOptions<OutboundModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<OutboundModuleGrids> OutboundModuleGrids { get; set; }
		public DbSet<OutboundModuleGridsconfig> OutboundModuleGridsconfig { get; set; }
		public DbSet<OutboundModuleGridsmd> OutboundModuleGridsmd { get; set; }
		public DbSet<OutboundModuleGridstx> OutboundModuleGridstx { get; set; }
		public DbSet<OutboundModuleGridsanalytics> OutboundModuleGridsanalytics { get; set; }
		public DbSet<OutboundModuleGridsPost> OutboundModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboundModuleGrids>()
                .ToTable("config_vw_OutboundModuleGrids")
                .HasKey(c => new { c.ixOutboundModuleGrid });
            modelBuilder.Entity<OutboundModuleGridsPost>()
                .ToTable("config_vw_OutboundModuleGridsPost")
                .HasKey(c => new { c.ixOutboundModuleGrid });
            modelBuilder.Entity<OutboundModuleGridsconfig>()
                .ToTable("config_vw_OutboundModuleGridsconfig")
                .HasKey(c => new { c.ixOutboundModuleGrid });
            modelBuilder.Entity<OutboundModuleGridsmd>()
                .ToTable("config_vw_OutboundModuleGridsmd")
                .HasKey(c => new { c.ixOutboundModuleGrid });
            modelBuilder.Entity<OutboundModuleGridstx>()
                .ToTable("config_vw_OutboundModuleGridstx")
                .HasKey(c => new { c.ixOutboundModuleGrid });
            modelBuilder.Entity<OutboundModuleGridsanalytics>()
                .ToTable("config_vw_OutboundModuleGridsanalytics")
                .HasKey(c => new { c.ixOutboundModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is OutboundModuleGridsPost)).ToList())
            {
                var config_vw_outboundmodulegridspost = e.Entity as OutboundModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sOutboundModuleGrid = cmd.CreateParameter();
                            sOutboundModuleGrid.ParameterName = "p0";
                            sOutboundModuleGrid.Value = config_vw_outboundmodulegridspost.sOutboundModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_outboundmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_outboundmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_outboundmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_outboundmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_outboundmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_outboundmodulegridspost.UserName;

                            var ixOutboundModuleGrid = cmd.CreateParameter();
                            ixOutboundModuleGrid.ParameterName = "p7";
                            ixOutboundModuleGrid.DbType = DbType.Int64;
                            ixOutboundModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateOutboundModuleGrids ");
                            sql.Append("@sOutboundModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_outboundmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixOutboundModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sOutboundModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_outboundmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixOutboundModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_outboundmodulegridspost.ixOutboundModuleGrid = (Int64)ixOutboundModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixOutboundModuleGrid"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeOutboundModuleGrids @ixOutboundModuleGrid = @p0, @sOutboundModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_outboundmodulegridspost.ixOutboundModuleGrid, config_vw_outboundmodulegridspost.sOutboundModuleGrid, config_vw_outboundmodulegridspost.sShortDescription, config_vw_outboundmodulegridspost.sDataEntityType, config_vw_outboundmodulegridspost.bCanCreate, config_vw_outboundmodulegridspost.bCanEdit, config_vw_outboundmodulegridspost.bCanDelete, config_vw_outboundmodulegridspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteOutboundModuleGrids @ixOutboundModuleGrid = @p0, @sOutboundModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_outboundmodulegridspost.ixOutboundModuleGrid, config_vw_outboundmodulegridspost.sOutboundModuleGrid, config_vw_outboundmodulegridspost.sShortDescription, config_vw_outboundmodulegridspost.sDataEntityType, config_vw_outboundmodulegridspost.bCanCreate, config_vw_outboundmodulegridspost.bCanEdit, config_vw_outboundmodulegridspost.bCanDelete, config_vw_outboundmodulegridspost.UserName);
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
  

