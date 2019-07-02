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

    public class MeasurementSystemsDB : DbContext
    {

        public MeasurementSystemsDB(DbContextOptions<MeasurementSystemsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementSystems>()
                .ToTable("md_vw_MeasurementSystems")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementSystemsPost>()
                .ToTable("md_vw_MeasurementSystemsPost")
                .HasKey(c => new { c.ixMeasurementSystem });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MeasurementSystemsPost)).ToList())
            {
                var md_vw_measurementsystemspost = e.Entity as MeasurementSystemsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMeasurementSystem = cmd.CreateParameter();
                            sMeasurementSystem.ParameterName = "p0";
                            sMeasurementSystem.Value = md_vw_measurementsystemspost.sMeasurementSystem;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_measurementsystemspost.UserName;

                            var ixMeasurementSystem = cmd.CreateParameter();
                            ixMeasurementSystem.ParameterName = "p2";
                            ixMeasurementSystem.DbType = DbType.Int64;
                            ixMeasurementSystem.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateMeasurementSystems ");
                            sql.Append("@sMeasurementSystem = @p0, ");
                            if (md_vw_measurementsystemspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixMeasurementSystem = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMeasurementSystem);
                            if (md_vw_measurementsystemspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMeasurementSystem); 
                            cmd.ExecuteNonQuery();
                            md_vw_measurementsystemspost.ixMeasurementSystem = (Int64)ixMeasurementSystem.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMeasurementSystem"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeMeasurementSystems @ixMeasurementSystem = @p0, @sMeasurementSystem = @p1, @UserName = @p2", md_vw_measurementsystemspost.ixMeasurementSystem, md_vw_measurementsystemspost.sMeasurementSystem, md_vw_measurementsystemspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteMeasurementSystems @ixMeasurementSystem = @p0, @sMeasurementSystem = @p1, @UserName = @p2", md_vw_measurementsystemspost.ixMeasurementSystem, md_vw_measurementsystemspost.sMeasurementSystem, md_vw_measurementsystemspost.UserName);
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
  

