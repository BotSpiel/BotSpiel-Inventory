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

    public class MeasurementUnitsOfDB : DbContext
    {

        public MeasurementUnitsOfDB(DbContextOptions<MeasurementUnitsOfDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementUnitsOf>()
                .ToTable("config_vw_MeasurementUnitsOf")
                .HasKey(c => new { c.ixMeasurementUnitOf });
            modelBuilder.Entity<MeasurementUnitsOfPost>()
                .ToTable("config_vw_MeasurementUnitsOfPost")
                .HasKey(c => new { c.ixMeasurementUnitOf });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is MeasurementUnitsOfPost)).ToList())
            {
                var config_vw_measurementunitsofpost = e.Entity as MeasurementUnitsOfPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMeasurementUnitOf = cmd.CreateParameter();
                            sMeasurementUnitOf.ParameterName = "p0";
                            sMeasurementUnitOf.Value = config_vw_measurementunitsofpost.sMeasurementUnitOf;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_measurementunitsofpost.UserName;

                            var ixMeasurementUnitOf = cmd.CreateParameter();
                            ixMeasurementUnitOf.ParameterName = "p2";
                            ixMeasurementUnitOf.DbType = DbType.Int64;
                            ixMeasurementUnitOf.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMeasurementUnitsOf ");
                            sql.Append("@sMeasurementUnitOf = @p0, ");
                            if (config_vw_measurementunitsofpost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixMeasurementUnitOf = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMeasurementUnitOf);
                            if (config_vw_measurementunitsofpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMeasurementUnitOf); 
                            cmd.ExecuteNonQuery();
                            config_vw_measurementunitsofpost.ixMeasurementUnitOf = (Int64)ixMeasurementUnitOf.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMeasurementUnitOf"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMeasurementUnitsOf @ixMeasurementUnitOf = @p0, @sMeasurementUnitOf = @p1, @UserName = @p2", config_vw_measurementunitsofpost.ixMeasurementUnitOf, config_vw_measurementunitsofpost.sMeasurementUnitOf, config_vw_measurementunitsofpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMeasurementUnitsOf @ixMeasurementUnitOf = @p0, @sMeasurementUnitOf = @p1, @UserName = @p2", config_vw_measurementunitsofpost.ixMeasurementUnitOf, config_vw_measurementunitsofpost.sMeasurementUnitOf, config_vw_measurementunitsofpost.UserName);
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
  

