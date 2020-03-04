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

    public class RequestForActionSimilesDB : DbContext
    {

        public RequestForActionSimilesDB(DbContextOptions<RequestForActionSimilesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<RequestForActionSimiles> RequestForActionSimiles { get; set; }
		public DbSet<RequestForActionSimilesPost> RequestForActionSimilesPost { get; set; }
		public DbSet<RequestsForAction> RequestsForAction { get; set; }
		public DbSet<RequestsForActionPost> RequestsForActionPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestForActionSimiles>()
                .ToTable("config_vw_RequestForActionSimiles")
                .HasKey(c => new { c.ixRequestForActionSimile });
            modelBuilder.Entity<RequestForActionSimilesPost>()
                .ToTable("config_vw_RequestForActionSimilesPost")
                .HasKey(c => new { c.ixRequestForActionSimile });
            modelBuilder.Entity<RequestsForAction>()
                .ToTable("config_vw_RequestsForAction")
                .HasKey(c => new { c.ixRequestForAction });
            modelBuilder.Entity<RequestsForActionPost>()
                .ToTable("config_vw_RequestsForActionPost")
                .HasKey(c => new { c.ixRequestForAction });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguageStyles>()
                .ToTable("config_vw_LanguageStyles")
                .HasKey(c => new { c.ixLanguageStyle });
            modelBuilder.Entity<LanguageStylesPost>()
                .ToTable("config_vw_LanguageStylesPost")
                .HasKey(c => new { c.ixLanguageStyle });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is RequestForActionSimilesPost)).ToList())
            {
                var config_vw_requestforactionsimilespost = e.Entity as RequestForActionSimilesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sRequestForActionSimile = cmd.CreateParameter();
                            sRequestForActionSimile.ParameterName = "p0";
                            sRequestForActionSimile.Value = config_vw_requestforactionsimilespost.sRequestForActionSimile;
                            var ixRequestForAction = cmd.CreateParameter();
                            ixRequestForAction.ParameterName = "p1";
                            ixRequestForAction.Value = config_vw_requestforactionsimilespost.ixRequestForAction;
                            var sRequestForActionSimileText = cmd.CreateParameter();
                            sRequestForActionSimileText.ParameterName = "p2";
                            sRequestForActionSimileText.Value = config_vw_requestforactionsimilespost.sRequestForActionSimileText;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = config_vw_requestforactionsimilespost.UserName;

                            var ixRequestForActionSimile = cmd.CreateParameter();
                            ixRequestForActionSimile.ParameterName = "p4";
                            ixRequestForActionSimile.DbType = DbType.Int64;
                            ixRequestForActionSimile.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateRequestForActionSimiles ");
                            sql.Append("@sRequestForActionSimile = @p0, ");
                            sql.Append("@ixRequestForAction = @p1, ");
                            sql.Append("@sRequestForActionSimileText = @p2, ");
                            if (config_vw_requestforactionsimilespost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixRequestForActionSimile = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sRequestForActionSimile);
                            cmd.Parameters.Add(ixRequestForAction);
                            cmd.Parameters.Add(sRequestForActionSimileText);
                            if (config_vw_requestforactionsimilespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixRequestForActionSimile); 
                            cmd.ExecuteNonQuery();
                            config_vw_requestforactionsimilespost.ixRequestForActionSimile = (Int64)ixRequestForActionSimile.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixRequestForActionSimile"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeRequestForActionSimiles @ixRequestForActionSimile = @p0, @sRequestForActionSimile = @p1, @ixRequestForAction = @p2, @sRequestForActionSimileText = @p3, @UserName = @p4", config_vw_requestforactionsimilespost.ixRequestForActionSimile, config_vw_requestforactionsimilespost.sRequestForActionSimile, config_vw_requestforactionsimilespost.ixRequestForAction, config_vw_requestforactionsimilespost.sRequestForActionSimileText, config_vw_requestforactionsimilespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteRequestForActionSimiles @ixRequestForActionSimile = @p0, @sRequestForActionSimile = @p1, @ixRequestForAction = @p2, @sRequestForActionSimileText = @p3, @UserName = @p4", config_vw_requestforactionsimilespost.ixRequestForActionSimile, config_vw_requestforactionsimilespost.sRequestForActionSimile, config_vw_requestforactionsimilespost.ixRequestForAction, config_vw_requestforactionsimilespost.sRequestForActionSimileText, config_vw_requestforactionsimilespost.UserName);
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
  

