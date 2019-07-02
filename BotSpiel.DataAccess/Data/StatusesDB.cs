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

    public class StatusesDB : DbContext
    {

        public StatusesDB(DbContextOptions<StatusesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is StatusesPost)).ToList())
            {
                var config_vw_statusespost = e.Entity as StatusesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sStatus = cmd.CreateParameter();
                            sStatus.ParameterName = "p0";
                            sStatus.Value = config_vw_statusespost.sStatus;
                            var sStatusCode = cmd.CreateParameter();
                            sStatusCode.ParameterName = "p1";
                            sStatusCode.Value = config_vw_statusespost.sStatusCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_statusespost.UserName;

                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p3";
                            ixStatus.DbType = DbType.Int64;
                            ixStatus.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateStatuses ");
                            sql.Append("@sStatus = @p0, ");
                            sql.Append("@sStatusCode = @p1, ");
                            if (config_vw_statusespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixStatus = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sStatus);
                            cmd.Parameters.Add(sStatusCode);
                            if (config_vw_statusespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixStatus); 
                            cmd.ExecuteNonQuery();
                            config_vw_statusespost.ixStatus = (Int64)ixStatus.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixStatus"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeStatuses @ixStatus = @p0, @sStatus = @p1, @sStatusCode = @p2, @UserName = @p3", config_vw_statusespost.ixStatus, config_vw_statusespost.sStatus, config_vw_statusespost.sStatusCode, config_vw_statusespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteStatuses @ixStatus = @p0, @sStatus = @p1, @sStatusCode = @p2, @UserName = @p3", config_vw_statusespost.ixStatus, config_vw_statusespost.sStatus, config_vw_statusespost.sStatusCode, config_vw_statusespost.UserName);
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
  

