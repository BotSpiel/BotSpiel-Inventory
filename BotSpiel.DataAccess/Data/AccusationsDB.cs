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

    public class AccusationsDB : DbContext
    {

        public AccusationsDB(DbContextOptions<AccusationsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Accusations> Accusations { get; set; }
		public DbSet<AccusationsPost> AccusationsPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accusations>()
                .ToTable("config_vw_Accusations")
                .HasKey(c => new { c.ixAccusation });
            modelBuilder.Entity<AccusationsPost>()
                .ToTable("config_vw_AccusationsPost")
                .HasKey(c => new { c.ixAccusation });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is AccusationsPost)).ToList())
            {
                var config_vw_accusationspost = e.Entity as AccusationsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sAccusation = cmd.CreateParameter();
                            sAccusation.ParameterName = "p0";
                            sAccusation.Value = config_vw_accusationspost.sAccusation;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_accusationspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_accusationspost.ixLanguageStyle;
                            var sAccusationMade = cmd.CreateParameter();
                            sAccusationMade.ParameterName = "p3";
                            sAccusationMade.Value = config_vw_accusationspost.sAccusationMade;
                            var sAdmissionDenial = cmd.CreateParameter();
                            sAdmissionDenial.ParameterName = "p4";
                            sAdmissionDenial.Value = config_vw_accusationspost.sAdmissionDenial;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p5";
                            ixResponseType.Value = config_vw_accusationspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p6";
                            bActive.Value = config_vw_accusationspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_accusationspost.UserName;

                            var ixAccusation = cmd.CreateParameter();
                            ixAccusation.ParameterName = "p8";
                            ixAccusation.DbType = DbType.Int64;
                            ixAccusation.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateAccusations ");
                            sql.Append("@sAccusation = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sAccusationMade = @p3, ");
                            sql.Append("@sAdmissionDenial = @p4, ");
                            sql.Append("@ixResponseType = @p5, ");
                            sql.Append("@bActive = @p6, ");
                            if (config_vw_accusationspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixAccusation = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sAccusation);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sAccusationMade);
                            cmd.Parameters.Add(sAdmissionDenial);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_accusationspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixAccusation); 
                            cmd.ExecuteNonQuery();
                            config_vw_accusationspost.ixAccusation = (Int64)ixAccusation.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixAccusation"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeAccusations @ixAccusation = @p0, @sAccusation = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sAccusationMade = @p4, @sAdmissionDenial = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_accusationspost.ixAccusation, config_vw_accusationspost.sAccusation, config_vw_accusationspost.ixLanguage, config_vw_accusationspost.ixLanguageStyle, config_vw_accusationspost.sAccusationMade, config_vw_accusationspost.sAdmissionDenial, config_vw_accusationspost.ixResponseType, config_vw_accusationspost.bActive, config_vw_accusationspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteAccusations @ixAccusation = @p0, @sAccusation = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sAccusationMade = @p4, @sAdmissionDenial = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_accusationspost.ixAccusation, config_vw_accusationspost.sAccusation, config_vw_accusationspost.ixLanguage, config_vw_accusationspost.ixLanguageStyle, config_vw_accusationspost.sAccusationMade, config_vw_accusationspost.sAdmissionDenial, config_vw_accusationspost.ixResponseType, config_vw_accusationspost.bActive, config_vw_accusationspost.UserName);
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
  

