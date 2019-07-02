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

    public class MoveQueueContextsDB : DbContext
    {

        public MoveQueueContextsDB(DbContextOptions<MoveQueueContextsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MoveQueueContexts> MoveQueueContexts { get; set; }
		public DbSet<MoveQueueContextsPost> MoveQueueContextsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveQueueContexts>()
                .ToTable("config_vw_MoveQueueContexts")
                .HasKey(c => new { c.ixMoveQueueContext });
            modelBuilder.Entity<MoveQueueContextsPost>()
                .ToTable("config_vw_MoveQueueContextsPost")
                .HasKey(c => new { c.ixMoveQueueContext });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MoveQueueContextsPost)).ToList())
            {
                var config_vw_movequeuecontextspost = e.Entity as MoveQueueContextsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMoveQueueContext = cmd.CreateParameter();
                            sMoveQueueContext.ParameterName = "p0";
                            sMoveQueueContext.Value = config_vw_movequeuecontextspost.sMoveQueueContext;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_movequeuecontextspost.UserName;

                            var ixMoveQueueContext = cmd.CreateParameter();
                            ixMoveQueueContext.ParameterName = "p2";
                            ixMoveQueueContext.DbType = DbType.Int64;
                            ixMoveQueueContext.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMoveQueueContexts ");
                            sql.Append("@sMoveQueueContext = @p0, ");
                            if (config_vw_movequeuecontextspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixMoveQueueContext = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMoveQueueContext);
                            if (config_vw_movequeuecontextspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMoveQueueContext); 
                            cmd.ExecuteNonQuery();
                            config_vw_movequeuecontextspost.ixMoveQueueContext = (Int64)ixMoveQueueContext.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMoveQueueContext"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMoveQueueContexts @ixMoveQueueContext = @p0, @sMoveQueueContext = @p1, @UserName = @p2", config_vw_movequeuecontextspost.ixMoveQueueContext, config_vw_movequeuecontextspost.sMoveQueueContext, config_vw_movequeuecontextspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMoveQueueContexts @ixMoveQueueContext = @p0, @sMoveQueueContext = @p1, @UserName = @p2", config_vw_movequeuecontextspost.ixMoveQueueContext, config_vw_movequeuecontextspost.sMoveQueueContext, config_vw_movequeuecontextspost.UserName);
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
  

