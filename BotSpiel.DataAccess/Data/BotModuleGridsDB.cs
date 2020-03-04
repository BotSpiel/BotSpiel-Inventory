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

    public class BotModuleGridsDB : DbContext
    {

        public BotModuleGridsDB(DbContextOptions<BotModuleGridsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<BotModuleGrids> BotModuleGrids { get; set; }
		public DbSet<BotModuleGridsconfig> BotModuleGridsconfig { get; set; }
		public DbSet<BotModuleGridsmd> BotModuleGridsmd { get; set; }
		public DbSet<BotModuleGridstx> BotModuleGridstx { get; set; }
		public DbSet<BotModuleGridsanalytics> BotModuleGridsanalytics { get; set; }
		public DbSet<BotModuleGridsPost> BotModuleGridsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotModuleGrids>()
                .ToTable("config_vw_BotModuleGrids")
                .HasKey(c => new { c.ixBotModuleGrid });
            modelBuilder.Entity<BotModuleGridsPost>()
                .ToTable("config_vw_BotModuleGridsPost")
                .HasKey(c => new { c.ixBotModuleGrid });
            modelBuilder.Entity<BotModuleGridsconfig>()
                .ToTable("config_vw_BotModuleGridsconfig")
                .HasKey(c => new { c.ixBotModuleGrid });
            modelBuilder.Entity<BotModuleGridsmd>()
                .ToTable("config_vw_BotModuleGridsmd")
                .HasKey(c => new { c.ixBotModuleGrid });
            modelBuilder.Entity<BotModuleGridstx>()
                .ToTable("config_vw_BotModuleGridstx")
                .HasKey(c => new { c.ixBotModuleGrid });
            modelBuilder.Entity<BotModuleGridsanalytics>()
                .ToTable("config_vw_BotModuleGridsanalytics")
                .HasKey(c => new { c.ixBotModuleGrid });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is BotModuleGridsPost)).ToList())
            {
                var config_vw_botmodulegridspost = e.Entity as BotModuleGridsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sBotModuleGrid = cmd.CreateParameter();
                            sBotModuleGrid.ParameterName = "p0";
                            sBotModuleGrid.Value = config_vw_botmodulegridspost.sBotModuleGrid;
                            var sShortDescription = cmd.CreateParameter();
                            sShortDescription.ParameterName = "p1";
                            sShortDescription.Value = config_vw_botmodulegridspost.sShortDescription;
                            var sDataEntityType = cmd.CreateParameter();
                            sDataEntityType.ParameterName = "p2";
                            sDataEntityType.Value = config_vw_botmodulegridspost.sDataEntityType;
                            var bCanCreate = cmd.CreateParameter();
                            bCanCreate.ParameterName = "p3";
                            bCanCreate.Value = config_vw_botmodulegridspost.bCanCreate;
                            var bCanEdit = cmd.CreateParameter();
                            bCanEdit.ParameterName = "p4";
                            bCanEdit.Value = config_vw_botmodulegridspost.bCanEdit;
                            var bCanDelete = cmd.CreateParameter();
                            bCanDelete.ParameterName = "p5";
                            bCanDelete.Value = config_vw_botmodulegridspost.bCanDelete;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = config_vw_botmodulegridspost.UserName;

                            var ixBotModuleGrid = cmd.CreateParameter();
                            ixBotModuleGrid.ParameterName = "p7";
                            ixBotModuleGrid.DbType = DbType.Int64;
                            ixBotModuleGrid.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateBotModuleGrids ");
                            sql.Append("@sBotModuleGrid = @p0, ");
                            sql.Append("@sShortDescription = @p1, ");
                            sql.Append("@sDataEntityType = @p2, ");
                            sql.Append("@bCanCreate = @p3, ");
                            sql.Append("@bCanEdit = @p4, ");
                            sql.Append("@bCanDelete = @p5, ");
                            if (config_vw_botmodulegridspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixBotModuleGrid = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sBotModuleGrid);
                            cmd.Parameters.Add(sShortDescription);
                            cmd.Parameters.Add(sDataEntityType);
                            cmd.Parameters.Add(bCanCreate);
                            cmd.Parameters.Add(bCanEdit);
                            cmd.Parameters.Add(bCanDelete);
                            if (config_vw_botmodulegridspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixBotModuleGrid); 
                            cmd.ExecuteNonQuery();
                            config_vw_botmodulegridspost.ixBotModuleGrid = (Int64)ixBotModuleGrid.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixBotModuleGrid"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeBotModuleGrids @ixBotModuleGrid = @p0, @sBotModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_botmodulegridspost.ixBotModuleGrid, config_vw_botmodulegridspost.sBotModuleGrid, config_vw_botmodulegridspost.sShortDescription, config_vw_botmodulegridspost.sDataEntityType, config_vw_botmodulegridspost.bCanCreate, config_vw_botmodulegridspost.bCanEdit, config_vw_botmodulegridspost.bCanDelete, config_vw_botmodulegridspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteBotModuleGrids @ixBotModuleGrid = @p0, @sBotModuleGrid = @p1, @sShortDescription = @p2, @sDataEntityType = @p3, @bCanCreate = @p4, @bCanEdit = @p5, @bCanDelete = @p6, @UserName = @p7", config_vw_botmodulegridspost.ixBotModuleGrid, config_vw_botmodulegridspost.sBotModuleGrid, config_vw_botmodulegridspost.sShortDescription, config_vw_botmodulegridspost.sDataEntityType, config_vw_botmodulegridspost.bCanCreate, config_vw_botmodulegridspost.bCanEdit, config_vw_botmodulegridspost.bCanDelete, config_vw_botmodulegridspost.UserName);
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
  

