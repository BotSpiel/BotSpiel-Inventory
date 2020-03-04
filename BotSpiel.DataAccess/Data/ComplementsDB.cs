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

    public class ComplementsDB : DbContext
    {

        public ComplementsDB(DbContextOptions<ComplementsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Complements> Complements { get; set; }
		public DbSet<ComplementsPost> ComplementsPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complements>()
                .ToTable("config_vw_Complements")
                .HasKey(c => new { c.ixComplement });
            modelBuilder.Entity<ComplementsPost>()
                .ToTable("config_vw_ComplementsPost")
                .HasKey(c => new { c.ixComplement });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ComplementsPost)).ToList())
            {
                var config_vw_complementspost = e.Entity as ComplementsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sComplement = cmd.CreateParameter();
                            sComplement.ParameterName = "p0";
                            sComplement.Value = config_vw_complementspost.sComplement;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_complementspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_complementspost.ixLanguageStyle;
                            var sComplementMade = cmd.CreateParameter();
                            sComplementMade.ParameterName = "p3";
                            sComplementMade.Value = config_vw_complementspost.sComplementMade;
                            var sComplementAccepted = cmd.CreateParameter();
                            sComplementAccepted.ParameterName = "p4";
                            sComplementAccepted.Value = config_vw_complementspost.sComplementAccepted;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p5";
                            ixResponseType.Value = config_vw_complementspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p6";
                            bActive.Value = config_vw_complementspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_complementspost.UserName;

                            var ixComplement = cmd.CreateParameter();
                            ixComplement.ParameterName = "p8";
                            ixComplement.DbType = DbType.Int64;
                            ixComplement.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateComplements ");
                            sql.Append("@sComplement = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sComplementMade = @p3, ");
                            sql.Append("@sComplementAccepted = @p4, ");
                            sql.Append("@ixResponseType = @p5, ");
                            sql.Append("@bActive = @p6, ");
                            if (config_vw_complementspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixComplement = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sComplement);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sComplementMade);
                            cmd.Parameters.Add(sComplementAccepted);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_complementspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixComplement); 
                            cmd.ExecuteNonQuery();
                            config_vw_complementspost.ixComplement = (Int64)ixComplement.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixComplement"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeComplements @ixComplement = @p0, @sComplement = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sComplementMade = @p4, @sComplementAccepted = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_complementspost.ixComplement, config_vw_complementspost.sComplement, config_vw_complementspost.ixLanguage, config_vw_complementspost.ixLanguageStyle, config_vw_complementspost.sComplementMade, config_vw_complementspost.sComplementAccepted, config_vw_complementspost.ixResponseType, config_vw_complementspost.bActive, config_vw_complementspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteComplements @ixComplement = @p0, @sComplement = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sComplementMade = @p4, @sComplementAccepted = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_complementspost.ixComplement, config_vw_complementspost.sComplement, config_vw_complementspost.ixLanguage, config_vw_complementspost.ixLanguageStyle, config_vw_complementspost.sComplementMade, config_vw_complementspost.sComplementAccepted, config_vw_complementspost.ixResponseType, config_vw_complementspost.bActive, config_vw_complementspost.UserName);
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
  

