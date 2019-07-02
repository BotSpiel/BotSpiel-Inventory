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

    public class LanguagesDB : DbContext
    {

        public LanguagesDB(DbContextOptions<LanguagesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is LanguagesPost)).ToList())
            {
                var md_vw_languagespost = e.Entity as LanguagesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sLanguage = cmd.CreateParameter();
                            sLanguage.ParameterName = "p0";
                            sLanguage.Value = md_vw_languagespost.sLanguage;
                            var sLanguageCode = cmd.CreateParameter();
                            sLanguageCode.ParameterName = "p1";
                            sLanguageCode.Value = md_vw_languagespost.sLanguageCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_languagespost.UserName;

                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p3";
                            ixLanguage.DbType = DbType.Int64;
                            ixLanguage.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateLanguages ");
                            sql.Append("@sLanguage = @p0, ");
                            sql.Append("@sLanguageCode = @p1, ");
                            if (md_vw_languagespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixLanguage = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sLanguage);
                            cmd.Parameters.Add(sLanguageCode);
                            if (md_vw_languagespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixLanguage); 
                            cmd.ExecuteNonQuery();
                            md_vw_languagespost.ixLanguage = (Int64)ixLanguage.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixLanguage"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeLanguages @ixLanguage = @p0, @sLanguage = @p1, @sLanguageCode = @p2, @UserName = @p3", md_vw_languagespost.ixLanguage, md_vw_languagespost.sLanguage, md_vw_languagespost.sLanguageCode, md_vw_languagespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteLanguages @ixLanguage = @p0, @sLanguage = @p1, @sLanguageCode = @p2, @UserName = @p3", md_vw_languagespost.ixLanguage, md_vw_languagespost.sLanguage, md_vw_languagespost.sLanguageCode, md_vw_languagespost.UserName);
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
  

