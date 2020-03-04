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

    public class TopicsDB : DbContext
    {

        public TopicsDB(DbContextOptions<TopicsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Topics> Topics { get; set; }
		public DbSet<TopicsPost> TopicsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topics>()
                .ToTable("config_vw_Topics")
                .HasKey(c => new { c.ixTopic });
            modelBuilder.Entity<TopicsPost>()
                .ToTable("config_vw_TopicsPost")
                .HasKey(c => new { c.ixTopic });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is TopicsPost)).ToList())
            {
                var config_vw_topicspost = e.Entity as TopicsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sTopic = cmd.CreateParameter();
                            sTopic.ParameterName = "p0";
                            sTopic.Value = config_vw_topicspost.sTopic;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_topicspost.UserName;

                            var ixTopic = cmd.CreateParameter();
                            ixTopic.ParameterName = "p2";
                            ixTopic.DbType = DbType.Int64;
                            ixTopic.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateTopics ");
                            sql.Append("@sTopic = @p0, ");
                            if (config_vw_topicspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixTopic = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sTopic);
                            if (config_vw_topicspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixTopic); 
                            cmd.ExecuteNonQuery();
                            config_vw_topicspost.ixTopic = (Int64)ixTopic.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixTopic"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeTopics @ixTopic = @p0, @sTopic = @p1, @UserName = @p2", config_vw_topicspost.ixTopic, config_vw_topicspost.sTopic, config_vw_topicspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteTopics @ixTopic = @p0, @sTopic = @p1, @UserName = @p2", config_vw_topicspost.ixTopic, config_vw_topicspost.sTopic, config_vw_topicspost.UserName);
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
  

