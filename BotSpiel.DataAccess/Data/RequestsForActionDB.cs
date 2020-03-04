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

    public class RequestsForActionDB : DbContext
    {

        public RequestsForActionDB(DbContextOptions<RequestsForActionDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<RequestsForAction> RequestsForAction { get; set; }
		public DbSet<RequestsForActionPost> RequestsForActionPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is RequestsForActionPost)).ToList())
            {
                var config_vw_requestsforactionpost = e.Entity as RequestsForActionPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sRequestForAction = cmd.CreateParameter();
                            sRequestForAction.ParameterName = "p0";
                            sRequestForAction.Value = config_vw_requestsforactionpost.sRequestForAction;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_requestsforactionpost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_requestsforactionpost.ixLanguageStyle;
                            var sActionRequest = cmd.CreateParameter();
                            sActionRequest.ParameterName = "p3";
                            sActionRequest.Value = config_vw_requestsforactionpost.sActionRequest;
                            var sModule = cmd.CreateParameter();
                            sModule.ParameterName = "p4";
                            sModule.Value = config_vw_requestsforactionpost.sModule;
                            var sEntity = cmd.CreateParameter();
                            sEntity.ParameterName = "p5";
                            sEntity.Value = config_vw_requestsforactionpost.sEntity;
                            var sEntityIntent = cmd.CreateParameter();
                            sEntityIntent.ParameterName = "p6";
                            sEntityIntent.Value = config_vw_requestsforactionpost.sEntityIntent;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_requestsforactionpost.UserName;

                            var ixRequestForAction = cmd.CreateParameter();
                            ixRequestForAction.ParameterName = "p8";
                            ixRequestForAction.DbType = DbType.Int64;
                            ixRequestForAction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateRequestsForAction ");
                            sql.Append("@sRequestForAction = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sActionRequest = @p3, ");
                            sql.Append("@sModule = @p4, ");
                            sql.Append("@sEntity = @p5, ");
                            sql.Append("@sEntityIntent = @p6, ");
                            if (config_vw_requestsforactionpost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixRequestForAction = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sRequestForAction);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sActionRequest);
                            cmd.Parameters.Add(sModule);
                            cmd.Parameters.Add(sEntity);
                            cmd.Parameters.Add(sEntityIntent);
                            if (config_vw_requestsforactionpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixRequestForAction); 
                            cmd.ExecuteNonQuery();
                            config_vw_requestsforactionpost.ixRequestForAction = (Int64)ixRequestForAction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixRequestForAction"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeRequestsForAction @ixRequestForAction = @p0, @sRequestForAction = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sActionRequest = @p4, @sModule = @p5, @sEntity = @p6, @sEntityIntent = @p7, @UserName = @p8", config_vw_requestsforactionpost.ixRequestForAction, config_vw_requestsforactionpost.sRequestForAction, config_vw_requestsforactionpost.ixLanguage, config_vw_requestsforactionpost.ixLanguageStyle, config_vw_requestsforactionpost.sActionRequest, config_vw_requestsforactionpost.sModule, config_vw_requestsforactionpost.sEntity, config_vw_requestsforactionpost.sEntityIntent, config_vw_requestsforactionpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteRequestsForAction @ixRequestForAction = @p0, @sRequestForAction = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sActionRequest = @p4, @sModule = @p5, @sEntity = @p6, @sEntityIntent = @p7, @UserName = @p8", config_vw_requestsforactionpost.ixRequestForAction, config_vw_requestsforactionpost.sRequestForAction, config_vw_requestsforactionpost.ixLanguage, config_vw_requestsforactionpost.ixLanguageStyle, config_vw_requestsforactionpost.sActionRequest, config_vw_requestsforactionpost.sModule, config_vw_requestsforactionpost.sEntity, config_vw_requestsforactionpost.sEntityIntent, config_vw_requestsforactionpost.UserName);
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
  

