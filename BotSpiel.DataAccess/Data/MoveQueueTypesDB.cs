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

    public class MoveQueueTypesDB : DbContext
    {

        public MoveQueueTypesDB(DbContextOptions<MoveQueueTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MoveQueueTypes> MoveQueueTypes { get; set; }
		public DbSet<MoveQueueTypesPost> MoveQueueTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveQueueTypes>()
                .ToTable("config_vw_MoveQueueTypes")
                .HasKey(c => new { c.ixMoveQueueType });
            modelBuilder.Entity<MoveQueueTypesPost>()
                .ToTable("config_vw_MoveQueueTypesPost")
                .HasKey(c => new { c.ixMoveQueueType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MoveQueueTypesPost)).ToList())
            {
                var config_vw_movequeuetypespost = e.Entity as MoveQueueTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMoveQueueType = cmd.CreateParameter();
                            sMoveQueueType.ParameterName = "p0";
                            sMoveQueueType.Value = config_vw_movequeuetypespost.sMoveQueueType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_movequeuetypespost.UserName;

                            var ixMoveQueueType = cmd.CreateParameter();
                            ixMoveQueueType.ParameterName = "p2";
                            ixMoveQueueType.DbType = DbType.Int64;
                            ixMoveQueueType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMoveQueueTypes ");
                            sql.Append("@sMoveQueueType = @p0, ");
                            if (config_vw_movequeuetypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixMoveQueueType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMoveQueueType);
                            if (config_vw_movequeuetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMoveQueueType); 
                            cmd.ExecuteNonQuery();
                            config_vw_movequeuetypespost.ixMoveQueueType = (Int64)ixMoveQueueType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMoveQueueType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMoveQueueTypes @ixMoveQueueType = @p0, @sMoveQueueType = @p1, @UserName = @p2", config_vw_movequeuetypespost.ixMoveQueueType, config_vw_movequeuetypespost.sMoveQueueType, config_vw_movequeuetypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMoveQueueTypes @ixMoveQueueType = @p0, @sMoveQueueType = @p1, @UserName = @p2", config_vw_movequeuetypespost.ixMoveQueueType, config_vw_movequeuetypespost.sMoveQueueType, config_vw_movequeuetypespost.UserName);
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
  

