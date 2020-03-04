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

    public class QuestionSimilesDB : DbContext
    {

        public QuestionSimilesDB(DbContextOptions<QuestionSimilesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<QuestionSimiles> QuestionSimiles { get; set; }
		public DbSet<QuestionSimilesPost> QuestionSimilesPost { get; set; }
		public DbSet<Questions> Questions { get; set; }
		public DbSet<QuestionsPost> QuestionsPost { get; set; }
		public DbSet<Topics> Topics { get; set; }
		public DbSet<TopicsPost> TopicsPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionSimiles>()
                .ToTable("config_vw_QuestionSimiles")
                .HasKey(c => new { c.ixQuestionSimile });
            modelBuilder.Entity<QuestionSimilesPost>()
                .ToTable("config_vw_QuestionSimilesPost")
                .HasKey(c => new { c.ixQuestionSimile });
            modelBuilder.Entity<Questions>()
                .ToTable("config_vw_Questions")
                .HasKey(c => new { c.ixQuestion });
            modelBuilder.Entity<QuestionsPost>()
                .ToTable("config_vw_QuestionsPost")
                .HasKey(c => new { c.ixQuestion });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is QuestionSimilesPost)).ToList())
            {
                var config_vw_questionsimilespost = e.Entity as QuestionSimilesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixQuestion = cmd.CreateParameter();
                            ixQuestion.ParameterName = "p0";
                            ixQuestion.Value = config_vw_questionsimilespost.ixQuestion;
                            var sQuestionSimileText = cmd.CreateParameter();
                            sQuestionSimileText.ParameterName = "p1";
                            sQuestionSimileText.Value = config_vw_questionsimilespost.sQuestionSimileText;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_questionsimilespost.UserName;

                            var ixQuestionSimile = cmd.CreateParameter();
                            ixQuestionSimile.ParameterName = "p3";
                            ixQuestionSimile.DbType = DbType.Int64;
                            ixQuestionSimile.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateQuestionSimiles ");
                            sql.Append("@ixQuestion = @p0, ");
                            sql.Append("@sQuestionSimileText = @p1, ");
                            if (config_vw_questionsimilespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixQuestionSimile = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixQuestion);
                            cmd.Parameters.Add(sQuestionSimileText);
                            if (config_vw_questionsimilespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixQuestionSimile); 
                            cmd.ExecuteNonQuery();
                            config_vw_questionsimilespost.ixQuestionSimile = (Int64)ixQuestionSimile.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixQuestionSimile"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeQuestionSimiles @ixQuestionSimile = @p0, @ixQuestion = @p1, @sQuestionSimileText = @p2, @UserName = @p3", config_vw_questionsimilespost.ixQuestionSimile, config_vw_questionsimilespost.ixQuestion, config_vw_questionsimilespost.sQuestionSimileText, config_vw_questionsimilespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteQuestionSimiles @ixQuestionSimile = @p0, @ixQuestion = @p1, @sQuestionSimileText = @p2, @UserName = @p3", config_vw_questionsimilespost.ixQuestionSimile, config_vw_questionsimilespost.ixQuestion, config_vw_questionsimilespost.sQuestionSimileText, config_vw_questionsimilespost.UserName);
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
  

