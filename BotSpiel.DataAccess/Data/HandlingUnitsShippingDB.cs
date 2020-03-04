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

    public class HandlingUnitsShippingDB : DbContext
    {

        public HandlingUnitsShippingDB(DbContextOptions<HandlingUnitsShippingDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<HandlingUnitsShipping> HandlingUnitsShipping { get; set; }
		public DbSet<HandlingUnitsShippingPost> HandlingUnitsShippingPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<HandlingUnits> HandlingUnits { get; set; }
		public DbSet<HandlingUnitsPost> HandlingUnitsPost { get; set; }
		public DbSet<HandlingUnitTypes> HandlingUnitTypes { get; set; }
		public DbSet<HandlingUnitTypesPost> HandlingUnitTypesPost { get; set; }
		public DbSet<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurations { get; set; }
		public DbSet<MaterialHandlingUnitConfigurationsPost> MaterialHandlingUnitConfigurationsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HandlingUnitsShipping>()
                .ToTable("tx_vw_HandlingUnitsShipping")
                .HasKey(c => new { c.ixHandlingUnitShipping });
            modelBuilder.Entity<HandlingUnitsShippingPost>()
                .ToTable("tx_vw_HandlingUnitsShippingPost")
                .HasKey(c => new { c.ixHandlingUnitShipping });
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
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
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<HandlingUnits>()
                .ToTable("tx_vw_HandlingUnits")
                .HasKey(c => new { c.ixHandlingUnit });
            modelBuilder.Entity<HandlingUnitsPost>()
                .ToTable("tx_vw_HandlingUnitsPost")
                .HasKey(c => new { c.ixHandlingUnit });
            modelBuilder.Entity<HandlingUnitTypes>()
                .ToTable("config_vw_HandlingUnitTypes")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<HandlingUnitTypesPost>()
                .ToTable("config_vw_HandlingUnitTypesPost")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<MaterialHandlingUnitConfigurations>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurations")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
            modelBuilder.Entity<MaterialHandlingUnitConfigurationsPost>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurationsPost")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is HandlingUnitsShippingPost)).ToList())
            {
                var tx_vw_handlingunitsshippingpost = e.Entity as HandlingUnitsShippingPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p0";
                            ixHandlingUnit.Value = tx_vw_handlingunitsshippingpost.ixHandlingUnit;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p1";
                            ixStatus.Value = tx_vw_handlingunitsshippingpost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = tx_vw_handlingunitsshippingpost.UserName;

                            var ixHandlingUnitShipping = cmd.CreateParameter();
                            ixHandlingUnitShipping.ParameterName = "p3";
                            ixHandlingUnitShipping.DbType = DbType.Int64;
                            ixHandlingUnitShipping.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateHandlingUnitsShipping ");
                            sql.Append("@ixHandlingUnit = @p0, ");
                            sql.Append("@ixStatus = @p1, ");
                            if (tx_vw_handlingunitsshippingpost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixHandlingUnitShipping = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixHandlingUnit);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_handlingunitsshippingpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixHandlingUnitShipping); 
                            cmd.ExecuteNonQuery();
                            tx_vw_handlingunitsshippingpost.ixHandlingUnitShipping = (Int64)ixHandlingUnitShipping.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixHandlingUnitShipping"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeHandlingUnitsShipping @ixHandlingUnitShipping = @p0, @ixHandlingUnit = @p1, @ixStatus = @p2, @UserName = @p3", tx_vw_handlingunitsshippingpost.ixHandlingUnitShipping, tx_vw_handlingunitsshippingpost.ixHandlingUnit, tx_vw_handlingunitsshippingpost.ixStatus, tx_vw_handlingunitsshippingpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteHandlingUnitsShipping @ixHandlingUnitShipping = @p0, @ixHandlingUnit = @p1, @ixStatus = @p2, @UserName = @p3", tx_vw_handlingunitsshippingpost.ixHandlingUnitShipping, tx_vw_handlingunitsshippingpost.ixHandlingUnit, tx_vw_handlingunitsshippingpost.ixStatus, tx_vw_handlingunitsshippingpost.UserName);
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
  

