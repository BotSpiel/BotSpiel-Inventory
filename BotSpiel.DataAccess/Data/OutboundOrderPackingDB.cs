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

    public class OutboundOrderPackingDB : DbContext
    {

        public OutboundOrderPackingDB(DbContextOptions<OutboundOrderPackingDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<OutboundOrderPacking> OutboundOrderPacking { get; set; }
		public DbSet<OutboundOrderPackingPost> OutboundOrderPackingPost { get; set; }
		public DbSet<OutboundOrderLines> OutboundOrderLines { get; set; }
		public DbSet<OutboundOrderLinesPost> OutboundOrderLinesPost { get; set; }
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
            modelBuilder.Entity<OutboundOrderPacking>()
                .ToTable("tx_vw_OutboundOrderPacking")
                .HasKey(c => new { c.ixOutboundOrderPack });
            modelBuilder.Entity<OutboundOrderPackingPost>()
                .ToTable("tx_vw_OutboundOrderPackingPost")
                .HasKey(c => new { c.ixOutboundOrderPack });
            modelBuilder.Entity<OutboundOrderLines>()
                .ToTable("tx_vw_OutboundOrderLines")
                .HasKey(c => new { c.ixOutboundOrderLine });
            modelBuilder.Entity<OutboundOrderLinesPost>()
                .ToTable("tx_vw_OutboundOrderLinesPost")
                .HasKey(c => new { c.ixOutboundOrderLine });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is OutboundOrderPackingPost)).ToList())
            {
                var tx_vw_outboundorderpackingpost = e.Entity as OutboundOrderPackingPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixOutboundOrderLine = cmd.CreateParameter();
                            ixOutboundOrderLine.ParameterName = "p0";
                            ixOutboundOrderLine.Value = tx_vw_outboundorderpackingpost.ixOutboundOrderLine;
                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p1";
                            ixHandlingUnit.Value = tx_vw_outboundorderpackingpost.ixHandlingUnit;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p2";
                            ixStatus.Value = tx_vw_outboundorderpackingpost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = tx_vw_outboundorderpackingpost.UserName;

                            var ixOutboundOrderPack = cmd.CreateParameter();
                            ixOutboundOrderPack.ParameterName = "p4";
                            ixOutboundOrderPack.DbType = DbType.Int64;
                            ixOutboundOrderPack.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateOutboundOrderPacking ");
                            sql.Append("@ixOutboundOrderLine = @p0, ");
                            sql.Append("@ixHandlingUnit = @p1, ");
                            sql.Append("@ixStatus = @p2, ");
                            if (tx_vw_outboundorderpackingpost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixOutboundOrderPack = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixOutboundOrderLine);
                            cmd.Parameters.Add(ixHandlingUnit);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_outboundorderpackingpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixOutboundOrderPack); 
                            cmd.ExecuteNonQuery();
                            tx_vw_outboundorderpackingpost.ixOutboundOrderPack = (Int64)ixOutboundOrderPack.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixOutboundOrderPack"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeOutboundOrderPacking @ixOutboundOrderPack = @p0, @ixOutboundOrderLine = @p1, @ixHandlingUnit = @p2, @ixStatus = @p3, @UserName = @p4", tx_vw_outboundorderpackingpost.ixOutboundOrderPack, tx_vw_outboundorderpackingpost.ixOutboundOrderLine, tx_vw_outboundorderpackingpost.ixHandlingUnit, tx_vw_outboundorderpackingpost.ixStatus, tx_vw_outboundorderpackingpost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteOutboundOrderPacking @ixOutboundOrderPack = @p0, @ixOutboundOrderLine = @p1, @ixHandlingUnit = @p2, @ixStatus = @p3, @UserName = @p4", tx_vw_outboundorderpackingpost.ixOutboundOrderPack, tx_vw_outboundorderpackingpost.ixOutboundOrderLine, tx_vw_outboundorderpackingpost.ixHandlingUnit, tx_vw_outboundorderpackingpost.ixStatus, tx_vw_outboundorderpackingpost.UserName);
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
  

