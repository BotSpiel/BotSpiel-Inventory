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

    public class OutboundOrderLinesDB : DbContext
    {

        public OutboundOrderLinesDB(DbContextOptions<OutboundOrderLinesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<OutboundOrderLines> OutboundOrderLines { get; set; }
		public DbSet<OutboundOrderLinesPost> OutboundOrderLinesPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is OutboundOrderLinesPost)).ToList())
            {
                var tx_vw_outboundorderlinespost = e.Entity as OutboundOrderLinesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sOrderLineReference = cmd.CreateParameter();
                            sOrderLineReference.ParameterName = "p0";
                            sOrderLineReference.Value = tx_vw_outboundorderlinespost.sOrderLineReference;
                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p1";
                            ixMaterial.Value = tx_vw_outboundorderlinespost.ixMaterial;
                            var sBatchNumber = cmd.CreateParameter();
                            sBatchNumber.ParameterName = "p2";
                            sBatchNumber.Value = tx_vw_outboundorderlinespost.sBatchNumber;
                            var sSerialNumber = cmd.CreateParameter();
                            sSerialNumber.ParameterName = "p3";
                            sSerialNumber.Value = tx_vw_outboundorderlinespost.sSerialNumber;
                            var nBaseUnitQuantityOrdered = cmd.CreateParameter();
                            nBaseUnitQuantityOrdered.ParameterName = "p4";
                            nBaseUnitQuantityOrdered.Value = tx_vw_outboundorderlinespost.nBaseUnitQuantityOrdered;
                            var nBaseUnitQuantityShipped = cmd.CreateParameter();
                            nBaseUnitQuantityShipped.ParameterName = "p5";
                            nBaseUnitQuantityShipped.Value = tx_vw_outboundorderlinespost.nBaseUnitQuantityShipped;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p6";
                            ixStatus.Value = tx_vw_outboundorderlinespost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = tx_vw_outboundorderlinespost.UserName;

                            var ixOutboundOrderLine = cmd.CreateParameter();
                            ixOutboundOrderLine.ParameterName = "p8";
                            ixOutboundOrderLine.DbType = DbType.Int64;
                            ixOutboundOrderLine.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateOutboundOrderLines ");
                            sql.Append("@sOrderLineReference = @p0, ");
                            sql.Append("@ixMaterial = @p1, ");
                            if (tx_vw_outboundorderlinespost.sBatchNumber != null) { sql.Append("@sBatchNumber = @p2, "); }  
                            if (tx_vw_outboundorderlinespost.sSerialNumber != null) { sql.Append("@sSerialNumber = @p3, "); }  
                            sql.Append("@nBaseUnitQuantityOrdered = @p4, ");
                            sql.Append("@nBaseUnitQuantityShipped = @p5, ");
                            sql.Append("@ixStatus = @p6, ");
                            if (tx_vw_outboundorderlinespost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixOutboundOrderLine = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sOrderLineReference);
                            cmd.Parameters.Add(ixMaterial);
                            if (tx_vw_outboundorderlinespost.sBatchNumber != null) { cmd.Parameters.Add(sBatchNumber); }
                            if (tx_vw_outboundorderlinespost.sSerialNumber != null) { cmd.Parameters.Add(sSerialNumber); }
                            cmd.Parameters.Add(nBaseUnitQuantityOrdered);
                            cmd.Parameters.Add(nBaseUnitQuantityShipped);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_outboundorderlinespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixOutboundOrderLine); 
                            cmd.ExecuteNonQuery();
                            tx_vw_outboundorderlinespost.ixOutboundOrderLine = (Int64)ixOutboundOrderLine.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixOutboundOrderLine"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeOutboundOrderLines @ixOutboundOrderLine = @p0, @sOrderLineReference = @p1, @ixMaterial = @p2, @sBatchNumber = @p3, @sSerialNumber = @p4, @nBaseUnitQuantityOrdered = @p5, @nBaseUnitQuantityShipped = @p6, @ixStatus = @p7, @UserName = @p8", tx_vw_outboundorderlinespost.ixOutboundOrderLine, tx_vw_outboundorderlinespost.sOrderLineReference, tx_vw_outboundorderlinespost.ixMaterial, tx_vw_outboundorderlinespost.sBatchNumber, tx_vw_outboundorderlinespost.sSerialNumber, tx_vw_outboundorderlinespost.nBaseUnitQuantityOrdered, tx_vw_outboundorderlinespost.nBaseUnitQuantityShipped, tx_vw_outboundorderlinespost.ixStatus, tx_vw_outboundorderlinespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteOutboundOrderLines @ixOutboundOrderLine = @p0, @sOrderLineReference = @p1, @ixMaterial = @p2, @sBatchNumber = @p3, @sSerialNumber = @p4, @nBaseUnitQuantityOrdered = @p5, @nBaseUnitQuantityShipped = @p6, @ixStatus = @p7, @UserName = @p8", tx_vw_outboundorderlinespost.ixOutboundOrderLine, tx_vw_outboundorderlinespost.sOrderLineReference, tx_vw_outboundorderlinespost.ixMaterial, tx_vw_outboundorderlinespost.sBatchNumber, tx_vw_outboundorderlinespost.sSerialNumber, tx_vw_outboundorderlinespost.nBaseUnitQuantityOrdered, tx_vw_outboundorderlinespost.nBaseUnitQuantityShipped, tx_vw_outboundorderlinespost.ixStatus, tx_vw_outboundorderlinespost.UserName);
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
  

