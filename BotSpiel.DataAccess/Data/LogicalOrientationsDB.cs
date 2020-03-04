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

    public class LogicalOrientationsDB : DbContext
    {

        public LogicalOrientationsDB(DbContextOptions<LogicalOrientationsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<LogicalOrientations> LogicalOrientations { get; set; }
		public DbSet<LogicalOrientationsPost> LogicalOrientationsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogicalOrientations>()
                .ToTable("config_vw_LogicalOrientations")
                .HasKey(c => new { c.ixLogicalOrientation });
            modelBuilder.Entity<LogicalOrientationsPost>()
                .ToTable("config_vw_LogicalOrientationsPost")
                .HasKey(c => new { c.ixLogicalOrientation });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is LogicalOrientationsPost)).ToList())
            {
                var config_vw_logicalorientationspost = e.Entity as LogicalOrientationsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sLogicalOrientation = cmd.CreateParameter();
                            sLogicalOrientation.ParameterName = "p0";
                            sLogicalOrientation.Value = config_vw_logicalorientationspost.sLogicalOrientation;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_logicalorientationspost.UserName;

                            var ixLogicalOrientation = cmd.CreateParameter();
                            ixLogicalOrientation.ParameterName = "p2";
                            ixLogicalOrientation.DbType = DbType.Int64;
                            ixLogicalOrientation.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateLogicalOrientations ");
                            sql.Append("@sLogicalOrientation = @p0, ");
                            if (config_vw_logicalorientationspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixLogicalOrientation = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sLogicalOrientation);
                            if (config_vw_logicalorientationspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixLogicalOrientation); 
                            cmd.ExecuteNonQuery();
                            config_vw_logicalorientationspost.ixLogicalOrientation = (Int64)ixLogicalOrientation.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixLogicalOrientation"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeLogicalOrientations @ixLogicalOrientation = @p0, @sLogicalOrientation = @p1, @UserName = @p2", config_vw_logicalorientationspost.ixLogicalOrientation, config_vw_logicalorientationspost.sLogicalOrientation, config_vw_logicalorientationspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteLogicalOrientations @ixLogicalOrientation = @p0, @sLogicalOrientation = @p1, @UserName = @p2", config_vw_logicalorientationspost.ixLogicalOrientation, config_vw_logicalorientationspost.sLogicalOrientation, config_vw_logicalorientationspost.UserName);
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
  

