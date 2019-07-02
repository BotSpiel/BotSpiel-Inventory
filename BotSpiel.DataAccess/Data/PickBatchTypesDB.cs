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

    public class PickBatchTypesDB : DbContext
    {

        public PickBatchTypesDB(DbContextOptions<PickBatchTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PickBatchTypes> PickBatchTypes { get; set; }
		public DbSet<PickBatchTypesPost> PickBatchTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PickBatchTypes>()
                .ToTable("config_vw_PickBatchTypes")
                .HasKey(c => new { c.ixPickBatchType });
            modelBuilder.Entity<PickBatchTypesPost>()
                .ToTable("config_vw_PickBatchTypesPost")
                .HasKey(c => new { c.ixPickBatchType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is PickBatchTypesPost)).ToList())
            {
                var config_vw_pickbatchtypespost = e.Entity as PickBatchTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPickBatchType = cmd.CreateParameter();
                            sPickBatchType.ParameterName = "p0";
                            sPickBatchType.Value = config_vw_pickbatchtypespost.sPickBatchType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_pickbatchtypespost.UserName;

                            var ixPickBatchType = cmd.CreateParameter();
                            ixPickBatchType.ParameterName = "p2";
                            ixPickBatchType.DbType = DbType.Int64;
                            ixPickBatchType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreatePickBatchTypes ");
                            sql.Append("@sPickBatchType = @p0, ");
                            if (config_vw_pickbatchtypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixPickBatchType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPickBatchType);
                            if (config_vw_pickbatchtypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPickBatchType); 
                            cmd.ExecuteNonQuery();
                            config_vw_pickbatchtypespost.ixPickBatchType = (Int64)ixPickBatchType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPickBatchType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangePickBatchTypes @ixPickBatchType = @p0, @sPickBatchType = @p1, @UserName = @p2", config_vw_pickbatchtypespost.ixPickBatchType, config_vw_pickbatchtypespost.sPickBatchType, config_vw_pickbatchtypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeletePickBatchTypes @ixPickBatchType = @p0, @sPickBatchType = @p1, @UserName = @p2", config_vw_pickbatchtypespost.ixPickBatchType, config_vw_pickbatchtypespost.sPickBatchType, config_vw_pickbatchtypespost.UserName);
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
  

