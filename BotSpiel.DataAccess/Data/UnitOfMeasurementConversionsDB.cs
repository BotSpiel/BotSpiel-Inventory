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

    public class UnitOfMeasurementConversionsDB : DbContext
    {

        public UnitOfMeasurementConversionsDB(DbContextOptions<UnitOfMeasurementConversionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<UnitOfMeasurementConversions> UnitOfMeasurementConversions { get; set; }
		public DbSet<UnitOfMeasurementConversionsPost> UnitOfMeasurementConversionsPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitOfMeasurementConversions>()
                .ToTable("config_vw_UnitOfMeasurementConversions")
                .HasKey(c => new { c.ixUnitOfMeasurementConversion });
            modelBuilder.Entity<UnitOfMeasurementConversionsPost>()
                .ToTable("config_vw_UnitOfMeasurementConversionsPost")
                .HasKey(c => new { c.ixUnitOfMeasurementConversion });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is UnitOfMeasurementConversionsPost)).ToList())
            {
                var config_vw_unitofmeasurementconversionspost = e.Entity as UnitOfMeasurementConversionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixUnitOfMeasurementFrom = cmd.CreateParameter();
                            ixUnitOfMeasurementFrom.ParameterName = "p0";
                            ixUnitOfMeasurementFrom.Value = config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementFrom;
                            var ixUnitOfMeasurementTo = cmd.CreateParameter();
                            ixUnitOfMeasurementTo.ParameterName = "p1";
                            ixUnitOfMeasurementTo.Value = config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementTo;
                            var nMultiplier = cmd.CreateParameter();
                            nMultiplier.ParameterName = "p2";
                            nMultiplier.Value = config_vw_unitofmeasurementconversionspost.nMultiplier;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = config_vw_unitofmeasurementconversionspost.UserName;

                            var ixUnitOfMeasurementConversion = cmd.CreateParameter();
                            ixUnitOfMeasurementConversion.ParameterName = "p4";
                            ixUnitOfMeasurementConversion.DbType = DbType.Int64;
                            ixUnitOfMeasurementConversion.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateUnitOfMeasurementConversions ");
                            sql.Append("@ixUnitOfMeasurementFrom = @p0, ");
                            sql.Append("@ixUnitOfMeasurementTo = @p1, ");
                            sql.Append("@nMultiplier = @p2, ");
                            if (config_vw_unitofmeasurementconversionspost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixUnitOfMeasurementConversion = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixUnitOfMeasurementFrom);
                            cmd.Parameters.Add(ixUnitOfMeasurementTo);
                            cmd.Parameters.Add(nMultiplier);
                            if (config_vw_unitofmeasurementconversionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixUnitOfMeasurementConversion); 
                            cmd.ExecuteNonQuery();
                            config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementConversion = (Int64)ixUnitOfMeasurementConversion.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixUnitOfMeasurementConversion"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeUnitOfMeasurementConversions @ixUnitOfMeasurementConversion = @p0, @ixUnitOfMeasurementFrom = @p1, @ixUnitOfMeasurementTo = @p2, @nMultiplier = @p3, @UserName = @p4", config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementConversion, config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementFrom, config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementTo, config_vw_unitofmeasurementconversionspost.nMultiplier, config_vw_unitofmeasurementconversionspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteUnitOfMeasurementConversions @ixUnitOfMeasurementConversion = @p0, @ixUnitOfMeasurementFrom = @p1, @ixUnitOfMeasurementTo = @p2, @nMultiplier = @p3, @UserName = @p4", config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementConversion, config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementFrom, config_vw_unitofmeasurementconversionspost.ixUnitOfMeasurementTo, config_vw_unitofmeasurementconversionspost.nMultiplier, config_vw_unitofmeasurementconversionspost.UserName);
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
  

