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

    public class RequestsForInformationDB : DbContext
    {

        public RequestsForInformationDB(DbContextOptions<RequestsForInformationDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is RequestsForInformationPost)).ToList())
            {
                var config_vw_requestsforinformationpost = e.Entity as RequestsForInformationPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sRequestForInformation = cmd.CreateParameter();
                            sRequestForInformation.ParameterName = "p0";
                            sRequestForInformation.Value = config_vw_requestsforinformationpost.sRequestForInformation;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_requestsforinformationpost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_requestsforinformationpost.ixLanguageStyle;
                            var ixTopic = cmd.CreateParameter();
                            ixTopic.ParameterName = "p3";
                            ixTopic.Value = config_vw_requestsforinformationpost.ixTopic;
                            var sInformationRequest = cmd.CreateParameter();
                            sInformationRequest.ParameterName = "p4";
                            sInformationRequest.Value = config_vw_requestsforinformationpost.sInformationRequest;
                            var sInformationRequestResponse = cmd.CreateParameter();
                            sInformationRequestResponse.ParameterName = "p5";
                            sInformationRequestResponse.Value = config_vw_requestsforinformationpost.sInformationRequestResponse;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p6";
                            ixResponseType.Value = config_vw_requestsforinformationpost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p7";
                            bActive.Value = config_vw_requestsforinformationpost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p8";
                            UserName.Value = config_vw_requestsforinformationpost.UserName;

                            var ixRequestForInformation = cmd.CreateParameter();
                            ixRequestForInformation.ParameterName = "p9";
                            ixRequestForInformation.DbType = DbType.Int64;
                            ixRequestForInformation.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateRequestsForInformation ");
                            sql.Append("@sRequestForInformation = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@ixTopic = @p3, ");
                            sql.Append("@sInformationRequest = @p4, ");
                            sql.Append("@sInformationRequestResponse = @p5, ");
                            sql.Append("@ixResponseType = @p6, ");
                            sql.Append("@bActive = @p7, ");
                            if (config_vw_requestsforinformationpost.UserName != null) { sql.Append("@UserName = @p8, "); }  
                            sql.Append("@ixRequestForInformation = @p9 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sRequestForInformation);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(ixTopic);
                            cmd.Parameters.Add(sInformationRequest);
                            cmd.Parameters.Add(sInformationRequestResponse);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_requestsforinformationpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixRequestForInformation); 
                            cmd.ExecuteNonQuery();
                            config_vw_requestsforinformationpost.ixRequestForInformation = (Int64)ixRequestForInformation.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixRequestForInformation"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeRequestsForInformation @ixRequestForInformation = @p0, @sRequestForInformation = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @ixTopic = @p4, @sInformationRequest = @p5, @sInformationRequestResponse = @p6, @ixResponseType = @p7, @bActive = @p8, @UserName = @p9", config_vw_requestsforinformationpost.ixRequestForInformation, config_vw_requestsforinformationpost.sRequestForInformation, config_vw_requestsforinformationpost.ixLanguage, config_vw_requestsforinformationpost.ixLanguageStyle, config_vw_requestsforinformationpost.ixTopic, config_vw_requestsforinformationpost.sInformationRequest, config_vw_requestsforinformationpost.sInformationRequestResponse, config_vw_requestsforinformationpost.ixResponseType, config_vw_requestsforinformationpost.bActive, config_vw_requestsforinformationpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteRequestsForInformation @ixRequestForInformation = @p0, @sRequestForInformation = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @ixTopic = @p4, @sInformationRequest = @p5, @sInformationRequestResponse = @p6, @ixResponseType = @p7, @bActive = @p8, @UserName = @p9", config_vw_requestsforinformationpost.ixRequestForInformation, config_vw_requestsforinformationpost.sRequestForInformation, config_vw_requestsforinformationpost.ixLanguage, config_vw_requestsforinformationpost.ixLanguageStyle, config_vw_requestsforinformationpost.ixTopic, config_vw_requestsforinformationpost.sInformationRequest, config_vw_requestsforinformationpost.sInformationRequestResponse, config_vw_requestsforinformationpost.ixResponseType, config_vw_requestsforinformationpost.bActive, config_vw_requestsforinformationpost.UserName);
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
  

