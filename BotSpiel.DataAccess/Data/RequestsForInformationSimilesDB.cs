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

    public class RequestsForInformationSimilesDB : DbContext
    {

        public RequestsForInformationSimilesDB(DbContextOptions<RequestsForInformationSimilesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<RequestsForInformationSimiles> RequestsForInformationSimiles { get; set; }
		public DbSet<RequestsForInformationSimilesPost> RequestsForInformationSimilesPost { get; set; }
		public DbSet<RequestsForInformation> RequestsForInformation { get; set; }
		public DbSet<RequestsForInformationPost> RequestsForInformationPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<Topics> Topics { get; set; }
		public DbSet<TopicsPost> TopicsPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestsForInformationSimiles>()
                .ToTable("config_vw_RequestsForInformationSimiles")
                .HasKey(c => new { c.ixRequestsForInformationSimile });
            modelBuilder.Entity<RequestsForInformationSimilesPost>()
                .ToTable("config_vw_RequestsForInformationSimilesPost")
                .HasKey(c => new { c.ixRequestsForInformationSimile });
            modelBuilder.Entity<RequestsForInformation>()
                .ToTable("config_vw_RequestsForInformation")
                .HasKey(c => new { c.ixRequestForInformation });
            modelBuilder.Entity<RequestsForInformationPost>()
                .ToTable("config_vw_RequestsForInformationPost")
                .HasKey(c => new { c.ixRequestForInformation });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<Topics>()
                .ToTable("config_vw_Topics")
                .HasKey(c => new { c.ixTopic });
            modelBuilder.Entity<TopicsPost>()
                .ToTable("config_vw_TopicsPost")
                .HasKey(c => new { c.ixTopic });
            modelBuilder.Entity<LanguageStyles>()
                .ToTable("config_vw_LanguageStyles")
                .HasKey(c => new { c.ixLanguageStyle });
            modelBuilder.Entity<LanguageStylesPost>()
                .ToTable("config_vw_LanguageStylesPost")
                .HasKey(c => new { c.ixLanguageStyle });
            modelBuilder.Entity<ResponseTypes>()
                .ToTable("config_vw_ResponseTypes")
                .HasKey(c => new { c.ixResponseType });
            modelBuilder.Entity<ResponseTypesPost>()
                .ToTable("config_vw_ResponseTypesPost")
                .HasKey(c => new { c.ixResponseType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is RequestsForInformationSimilesPost)).ToList())
            {
                var config_vw_requestsforinformationsimilespost = e.Entity as RequestsForInformationSimilesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sRequestsForInformationSimile = cmd.CreateParameter();
                            sRequestsForInformationSimile.ParameterName = "p0";
                            sRequestsForInformationSimile.Value = config_vw_requestsforinformationsimilespost.sRequestsForInformationSimile;
                            var ixRequestForInformation = cmd.CreateParameter();
                            ixRequestForInformation.ParameterName = "p1";
                            ixRequestForInformation.Value = config_vw_requestsforinformationsimilespost.ixRequestForInformation;
                            var sRequestsForInformationSimileText = cmd.CreateParameter();
                            sRequestsForInformationSimileText.ParameterName = "p2";
                            sRequestsForInformationSimileText.Value = config_vw_requestsforinformationsimilespost.sRequestsForInformationSimileText;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = config_vw_requestsforinformationsimilespost.UserName;

                            var ixRequestsForInformationSimile = cmd.CreateParameter();
                            ixRequestsForInformationSimile.ParameterName = "p4";
                            ixRequestsForInformationSimile.DbType = DbType.Int64;
                            ixRequestsForInformationSimile.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateRequestsForInformationSimiles ");
                            sql.Append("@sRequestsForInformationSimile = @p0, ");
                            sql.Append("@ixRequestForInformation = @p1, ");
                            sql.Append("@sRequestsForInformationSimileText = @p2, ");
                            if (config_vw_requestsforinformationsimilespost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixRequestsForInformationSimile = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sRequestsForInformationSimile);
                            cmd.Parameters.Add(ixRequestForInformation);
                            cmd.Parameters.Add(sRequestsForInformationSimileText);
                            if (config_vw_requestsforinformationsimilespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixRequestsForInformationSimile); 
                            cmd.ExecuteNonQuery();
                            config_vw_requestsforinformationsimilespost.ixRequestsForInformationSimile = (Int64)ixRequestsForInformationSimile.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixRequestsForInformationSimile"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeRequestsForInformationSimiles @ixRequestsForInformationSimile = @p0, @sRequestsForInformationSimile = @p1, @ixRequestForInformation = @p2, @sRequestsForInformationSimileText = @p3, @UserName = @p4", config_vw_requestsforinformationsimilespost.ixRequestsForInformationSimile, config_vw_requestsforinformationsimilespost.sRequestsForInformationSimile, config_vw_requestsforinformationsimilespost.ixRequestForInformation, config_vw_requestsforinformationsimilespost.sRequestsForInformationSimileText, config_vw_requestsforinformationsimilespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteRequestsForInformationSimiles @ixRequestsForInformationSimile = @p0, @sRequestsForInformationSimile = @p1, @ixRequestForInformation = @p2, @sRequestsForInformationSimileText = @p3, @UserName = @p4", config_vw_requestsforinformationsimilespost.ixRequestsForInformationSimile, config_vw_requestsforinformationsimilespost.sRequestsForInformationSimile, config_vw_requestsforinformationsimilespost.ixRequestForInformation, config_vw_requestsforinformationsimilespost.sRequestsForInformationSimileText, config_vw_requestsforinformationsimilespost.UserName);
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
  

