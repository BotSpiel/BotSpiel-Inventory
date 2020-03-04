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

    public class FarewellsDB : DbContext
    {

        public FarewellsDB(DbContextOptions<FarewellsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Farewells> Farewells { get; set; }
		public DbSet<FarewellsPost> FarewellsPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farewells>()
                .ToTable("config_vw_Farewells")
                .HasKey(c => new { c.ixFarewell });
            modelBuilder.Entity<FarewellsPost>()
                .ToTable("config_vw_FarewellsPost")
                .HasKey(c => new { c.ixFarewell });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is FarewellsPost)).ToList())
            {
                var config_vw_farewellspost = e.Entity as FarewellsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFarewell = cmd.CreateParameter();
                            sFarewell.ParameterName = "p0";
                            sFarewell.Value = config_vw_farewellspost.sFarewell;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_farewellspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_farewellspost.ixLanguageStyle;
                            var sFarewellOffered = cmd.CreateParameter();
                            sFarewellOffered.ParameterName = "p3";
                            sFarewellOffered.Value = config_vw_farewellspost.sFarewellOffered;
                            var sFarewellResponse = cmd.CreateParameter();
                            sFarewellResponse.ParameterName = "p4";
                            sFarewellResponse.Value = config_vw_farewellspost.sFarewellResponse;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p5";
                            ixResponseType.Value = config_vw_farewellspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p6";
                            bActive.Value = config_vw_farewellspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_farewellspost.UserName;

                            var ixFarewell = cmd.CreateParameter();
                            ixFarewell.ParameterName = "p8";
                            ixFarewell.DbType = DbType.Int64;
                            ixFarewell.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateFarewells ");
                            sql.Append("@sFarewell = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sFarewellOffered = @p3, ");
                            sql.Append("@sFarewellResponse = @p4, ");
                            sql.Append("@ixResponseType = @p5, ");
                            sql.Append("@bActive = @p6, ");
                            if (config_vw_farewellspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixFarewell = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFarewell);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sFarewellOffered);
                            cmd.Parameters.Add(sFarewellResponse);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_farewellspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFarewell); 
                            cmd.ExecuteNonQuery();
                            config_vw_farewellspost.ixFarewell = (Int64)ixFarewell.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFarewell"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeFarewells @ixFarewell = @p0, @sFarewell = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sFarewellOffered = @p4, @sFarewellResponse = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_farewellspost.ixFarewell, config_vw_farewellspost.sFarewell, config_vw_farewellspost.ixLanguage, config_vw_farewellspost.ixLanguageStyle, config_vw_farewellspost.sFarewellOffered, config_vw_farewellspost.sFarewellResponse, config_vw_farewellspost.ixResponseType, config_vw_farewellspost.bActive, config_vw_farewellspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteFarewells @ixFarewell = @p0, @sFarewell = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sFarewellOffered = @p4, @sFarewellResponse = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_farewellspost.ixFarewell, config_vw_farewellspost.sFarewell, config_vw_farewellspost.ixLanguage, config_vw_farewellspost.ixLanguageStyle, config_vw_farewellspost.sFarewellOffered, config_vw_farewellspost.sFarewellResponse, config_vw_farewellspost.ixResponseType, config_vw_farewellspost.bActive, config_vw_farewellspost.UserName);
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
  

