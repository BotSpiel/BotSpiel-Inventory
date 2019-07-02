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

    public class UnitsOfMeasurementDB : DbContext
    {

        public UnitsOfMeasurementDB(DbContextOptions<UnitsOfMeasurementDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<MeasurementSystems>()
                .ToTable("md_vw_MeasurementSystems")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementSystemsPost>()
                .ToTable("md_vw_MeasurementSystemsPost")
                .HasKey(c => new { c.ixMeasurementSystem });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is UnitsOfMeasurementPost)).ToList())
            {
                var config_vw_unitsofmeasurementpost = e.Entity as UnitsOfMeasurementPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sUnitOfMeasurement = cmd.CreateParameter();
                            sUnitOfMeasurement.ParameterName = "p0";
                            sUnitOfMeasurement.Value = config_vw_unitsofmeasurementpost.sUnitOfMeasurement;
                            var ixMeasurementUnitOf = cmd.CreateParameter();
                            ixMeasurementUnitOf.ParameterName = "p1";
                            ixMeasurementUnitOf.Value = config_vw_unitsofmeasurementpost.ixMeasurementUnitOf;
                            var ixMeasurementSystem = cmd.CreateParameter();
                            ixMeasurementSystem.ParameterName = "p2";
                            ixMeasurementSystem.Value = config_vw_unitsofmeasurementpost.ixMeasurementSystem;
                            var sSymbol = cmd.CreateParameter();
                            sSymbol.ParameterName = "p3";
                            sSymbol.Value = config_vw_unitsofmeasurementpost.sSymbol;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = config_vw_unitsofmeasurementpost.UserName;

                            var ixUnitOfMeasurement = cmd.CreateParameter();
                            ixUnitOfMeasurement.ParameterName = "p5";
                            ixUnitOfMeasurement.DbType = DbType.Int64;
                            ixUnitOfMeasurement.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateUnitsOfMeasurement ");
                            sql.Append("@sUnitOfMeasurement = @p0, ");
                            sql.Append("@ixMeasurementUnitOf = @p1, ");
                            sql.Append("@ixMeasurementSystem = @p2, ");
                            if (config_vw_unitsofmeasurementpost.sSymbol != null) { sql.Append("@sSymbol = @p3, "); }  
                            if (config_vw_unitsofmeasurementpost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixUnitOfMeasurement = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sUnitOfMeasurement);
                            cmd.Parameters.Add(ixMeasurementUnitOf);
                            cmd.Parameters.Add(ixMeasurementSystem);
                            if (config_vw_unitsofmeasurementpost.sSymbol != null) { cmd.Parameters.Add(sSymbol); }
                            if (config_vw_unitsofmeasurementpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixUnitOfMeasurement); 
                            cmd.ExecuteNonQuery();
                            config_vw_unitsofmeasurementpost.ixUnitOfMeasurement = (Int64)ixUnitOfMeasurement.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixUnitOfMeasurement"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeUnitsOfMeasurement @ixUnitOfMeasurement = @p0, @sUnitOfMeasurement = @p1, @ixMeasurementUnitOf = @p2, @ixMeasurementSystem = @p3, @sSymbol = @p4, @UserName = @p5", config_vw_unitsofmeasurementpost.ixUnitOfMeasurement, config_vw_unitsofmeasurementpost.sUnitOfMeasurement, config_vw_unitsofmeasurementpost.ixMeasurementUnitOf, config_vw_unitsofmeasurementpost.ixMeasurementSystem, config_vw_unitsofmeasurementpost.sSymbol, config_vw_unitsofmeasurementpost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteUnitsOfMeasurement @ixUnitOfMeasurement = @p0, @sUnitOfMeasurement = @p1, @ixMeasurementUnitOf = @p2, @ixMeasurementSystem = @p3, @sSymbol = @p4, @UserName = @p5", config_vw_unitsofmeasurementpost.ixUnitOfMeasurement, config_vw_unitsofmeasurementpost.sUnitOfMeasurement, config_vw_unitsofmeasurementpost.ixMeasurementUnitOf, config_vw_unitsofmeasurementpost.ixMeasurementSystem, config_vw_unitsofmeasurementpost.sSymbol, config_vw_unitsofmeasurementpost.UserName);
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
  

