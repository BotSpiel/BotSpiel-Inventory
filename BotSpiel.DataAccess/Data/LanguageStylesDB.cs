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

    public class LanguageStylesDB : DbContext
    {

        public LanguageStylesDB(DbContextOptions<LanguageStylesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is LanguageStylesPost)).ToList())
            {
                var config_vw_languagestylespost = e.Entity as LanguageStylesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sLanguageStyle = cmd.CreateParameter();
                            sLanguageStyle.ParameterName = "p0";
                            sLanguageStyle.Value = config_vw_languagestylespost.sLanguageStyle;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_languagestylespost.UserName;

                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.DbType = DbType.Int64;
                            ixLanguageStyle.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateLanguageStyles ");
                            sql.Append("@sLanguageStyle = @p0, ");
                            if (config_vw_languagestylespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixLanguageStyle = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sLanguageStyle);
                            if (config_vw_languagestylespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixLanguageStyle); 
                            cmd.ExecuteNonQuery();
                            config_vw_languagestylespost.ixLanguageStyle = (Int64)ixLanguageStyle.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixLanguageStyle"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeLanguageStyles @ixLanguageStyle = @p0, @sLanguageStyle = @p1, @UserName = @p2", config_vw_languagestylespost.ixLanguageStyle, config_vw_languagestylespost.sLanguageStyle, config_vw_languagestylespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteLanguageStyles @ixLanguageStyle = @p0, @sLanguageStyle = @p1, @UserName = @p2", config_vw_languagestylespost.ixLanguageStyle, config_vw_languagestylespost.sLanguageStyle, config_vw_languagestylespost.UserName);
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
  

