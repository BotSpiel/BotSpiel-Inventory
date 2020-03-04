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

    public class QuestionsDB : DbContext
    {

        public QuestionsDB(DbContextOptions<QuestionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is QuestionsPost)).ToList())
            {
                var config_vw_questionspost = e.Entity as QuestionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sQuestion = cmd.CreateParameter();
                            sQuestion.ParameterName = "p0";
                            sQuestion.Value = config_vw_questionspost.sQuestion;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_questionspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_questionspost.ixLanguageStyle;
                            var ixTopic = cmd.CreateParameter();
                            ixTopic.ParameterName = "p3";
                            ixTopic.Value = config_vw_questionspost.ixTopic;
                            var sAsk = cmd.CreateParameter();
                            sAsk.ParameterName = "p4";
                            sAsk.Value = config_vw_questionspost.sAsk;
                            var sAnswer = cmd.CreateParameter();
                            sAnswer.ParameterName = "p5";
                            sAnswer.Value = config_vw_questionspost.sAnswer;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p6";
                            ixResponseType.Value = config_vw_questionspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p7";
                            bActive.Value = config_vw_questionspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p8";
                            UserName.Value = config_vw_questionspost.UserName;

                            var ixQuestion = cmd.CreateParameter();
                            ixQuestion.ParameterName = "p9";
                            ixQuestion.DbType = DbType.Int64;
                            ixQuestion.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateQuestions ");
                            sql.Append("@sQuestion = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@ixTopic = @p3, ");
                            sql.Append("@sAsk = @p4, ");
                            sql.Append("@sAnswer = @p5, ");
                            sql.Append("@ixResponseType = @p6, ");
                            sql.Append("@bActive = @p7, ");
                            if (config_vw_questionspost.UserName != null) { sql.Append("@UserName = @p8, "); }  
                            sql.Append("@ixQuestion = @p9 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sQuestion);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(ixTopic);
                            cmd.Parameters.Add(sAsk);
                            cmd.Parameters.Add(sAnswer);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_questionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixQuestion); 
                            cmd.ExecuteNonQuery();
                            config_vw_questionspost.ixQuestion = (Int64)ixQuestion.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixQuestion"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeQuestions @ixQuestion = @p0, @sQuestion = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @ixTopic = @p4, @sAsk = @p5, @sAnswer = @p6, @ixResponseType = @p7, @bActive = @p8, @UserName = @p9", config_vw_questionspost.ixQuestion, config_vw_questionspost.sQuestion, config_vw_questionspost.ixLanguage, config_vw_questionspost.ixLanguageStyle, config_vw_questionspost.ixTopic, config_vw_questionspost.sAsk, config_vw_questionspost.sAnswer, config_vw_questionspost.ixResponseType, config_vw_questionspost.bActive, config_vw_questionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteQuestions @ixQuestion = @p0, @sQuestion = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @ixTopic = @p4, @sAsk = @p5, @sAnswer = @p6, @ixResponseType = @p7, @bActive = @p8, @UserName = @p9", config_vw_questionspost.ixQuestion, config_vw_questionspost.sQuestion, config_vw_questionspost.ixLanguage, config_vw_questionspost.ixLanguageStyle, config_vw_questionspost.ixTopic, config_vw_questionspost.sAsk, config_vw_questionspost.sAnswer, config_vw_questionspost.ixResponseType, config_vw_questionspost.bActive, config_vw_questionspost.UserName);
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
  

