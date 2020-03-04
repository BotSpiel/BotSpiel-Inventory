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

    public class NetworkModuleGridsDB : DbContext
    {

        public NetworkModuleGridsDB(DbContextOptions<NetworkModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<NetworkModuleGrids> NetworkModuleGrids { get; set; }
		public DbSet<NetworkModuleGridsconfig> NetworkModuleGridsconfig { get; set; }
		public DbSet<NetworkModuleGridsmd> NetworkModuleGridsmd { get; set; }
		public DbSet<NetworkModuleGridstx> NetworkModuleGridstx { get; set; }
		public DbSet<NetworkModuleGridsanalytics> NetworkModuleGridsanalytics { get; set; }
		public DbSet<NetworkModuleGridsPost> NetworkModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NetworkModuleGrids>()
                .ToTable("config_vw_NetworkModuleGrids")
                .HasKey(c => new { c.ixNetworkModuleGrid });
            modelBuilder.Entity<NetworkModuleGridsPost>()
                .ToTable("config_vw_NetworkModuleGridsPost")
                .HasKey(c => new { c.ixNetworkModuleGrid });
            modelBuilder.Entity<NetworkModuleGridsconfig>()
                .ToTable("config_vw_NetworkModuleGridsconfig")
                .HasKey(c => new { c.ixNetworkModuleGrid });
            modelBuilder.Entity<NetworkModuleGridsmd>()
                .ToTable("config_vw_NetworkModuleGridsmd")
                .HasKey(c => new { c.ixNetworkModuleGrid });
            modelBuilder.Entity<NetworkModuleGridstx>()
                .ToTable("config_vw_NetworkModuleGridstx")
                .HasKey(c => new { c.ixNetworkModuleGrid });
            modelBuilder.Entity<NetworkModuleGridsanalytics>()
                .ToTable("config_vw_NetworkModuleGridsanalytics")
                .HasKey(c => new { c.ixNetworkModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is NetworkModuleGridsPost)).ToList())
            {
                var config_vw_networkmodulegridspost = e.Entity as NetworkModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sNetworkModuleGrid = cmd.CreateParameter();
                            sNetworkModuleGrid.ParameterName = "p0";
                            sNetworkModuleGrid.Value = config_vw_networkmodulegridspost.sNetworkModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_networkmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_networkmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_networkmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_networkmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_networkmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_networkmodulegridspost.UserName;

                            var ixNetworkModuleGrid = cmd.CreateParameter();
                            ixNetworkModuleGrid.ParameterName = "p7";
                            ixNetworkModuleGrid.DbType = DbType.Int64;
                            ixNetworkModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateNetworkModuleGrids ");
                            sql.Append("@sNetworkModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_networkmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixNetworkModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sNetworkModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_networkmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixNetworkModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_networkmodulegridspost.ixNetworkModuleGrid = (Int64)ixNetworkModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixNetworkModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeNetworkModuleGrids @ixNetworkModuleGrid = @p0, @sNetworkModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_networkmodulegridspost.ixNetworkModuleGrid, config_vw_networkmodulegridspost.sNetworkModuleGrid, config_vw_networkmodulegridspost.sShortDescription, config_vw_networkmodulegridspost.sDataEntityType, config_vw_networkmodulegridspost.bCanCreate, config_vw_networkmodulegridspost.bCanEdit, config_vw_networkmodulegridspost.bCanDelete, config_vw_networkmodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteNetworkModuleGrids @ixNetworkModuleGrid = @p0, @sNetworkModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_networkmodulegridspost.ixNetworkModuleGrid, config_vw_networkmodulegridspost.sNetworkModuleGrid, config_vw_networkmodulegridspost.sShortDescription, config_vw_networkmodulegridspost.sDataEntityType, config_vw_networkmodulegridspost.bCanCreate, config_vw_networkmodulegridspost.bCanEdit, config_vw_networkmodulegridspost.bCanDelete, config_vw_networkmodulegridspost.UserName);
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
  

