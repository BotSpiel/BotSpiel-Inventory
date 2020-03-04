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

    public class GreetingsDB : DbContext
    {

        public GreetingsDB(DbContextOptions<GreetingsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Greetings> Greetings { get; set; }
		public DbSet<GreetingsPost> GreetingsPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Greetings>()
                .ToTable("config_vw_Greetings")
                .HasKey(c => new { c.ixGreeting });
            modelBuilder.Entity<GreetingsPost>()
                .ToTable("config_vw_GreetingsPost")
                .HasKey(c => new { c.ixGreeting });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is GreetingsPost)).ToList())
            {
                var config_vw_greetingspost = e.Entity as GreetingsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sGreeting = cmd.CreateParameter();
                            sGreeting.ParameterName = "p0";
                            sGreeting.Value = config_vw_greetingspost.sGreeting;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_greetingspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_greetingspost.ixLanguageStyle;
                            var sGreetingOffered = cmd.CreateParameter();
                            sGreetingOffered.ParameterName = "p3";
                            sGreetingOffered.Value = config_vw_greetingspost.sGreetingOffered;
                            var sGreetingResponse = cmd.CreateParameter();
                            sGreetingResponse.ParameterName = "p4";
                            sGreetingResponse.Value = config_vw_greetingspost.sGreetingResponse;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p5";
                            ixResponseType.Value = config_vw_greetingspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p6";
                            bActive.Value = config_vw_greetingspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_greetingspost.UserName;

                            var ixGreeting = cmd.CreateParameter();
                            ixGreeting.ParameterName = "p8";
                            ixGreeting.DbType = DbType.Int64;
                            ixGreeting.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateGreetings ");
                            sql.Append("@sGreeting = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sGreetingOffered = @p3, ");
                            sql.Append("@sGreetingResponse = @p4, ");
                            sql.Append("@ixResponseType = @p5, ");
                            sql.Append("@bActive = @p6, ");
                            if (config_vw_greetingspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixGreeting = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sGreeting);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sGreetingOffered);
                            cmd.Parameters.Add(sGreetingResponse);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_greetingspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixGreeting); 
                            cmd.ExecuteNonQuery();
                            config_vw_greetingspost.ixGreeting = (Int64)ixGreeting.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixGreeting"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeGreetings @ixGreeting = @p0, @sGreeting = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sGreetingOffered = @p4, @sGreetingResponse = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_greetingspost.ixGreeting, config_vw_greetingspost.sGreeting, config_vw_greetingspost.ixLanguage, config_vw_greetingspost.ixLanguageStyle, config_vw_greetingspost.sGreetingOffered, config_vw_greetingspost.sGreetingResponse, config_vw_greetingspost.ixResponseType, config_vw_greetingspost.bActive, config_vw_greetingspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteGreetings @ixGreeting = @p0, @sGreeting = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sGreetingOffered = @p4, @sGreetingResponse = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_greetingspost.ixGreeting, config_vw_greetingspost.sGreeting, config_vw_greetingspost.ixLanguage, config_vw_greetingspost.ixLanguageStyle, config_vw_greetingspost.sGreetingOffered, config_vw_greetingspost.sGreetingResponse, config_vw_greetingspost.ixResponseType, config_vw_greetingspost.bActive, config_vw_greetingspost.UserName);
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
  

