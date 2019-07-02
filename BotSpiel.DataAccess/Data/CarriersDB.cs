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

    public class CarriersDB : DbContext
    {

        public CarriersDB(DbContextOptions<CarriersDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Carriers> Carriers { get; set; }
		public DbSet<CarriersPost> CarriersPost { get; set; }
		public DbSet<CarrierTypes> CarrierTypes { get; set; }
		public DbSet<CarrierTypesPost> CarrierTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carriers>()
                .ToTable("md_vw_Carriers")
                .HasKey(c => new { c.ixCarrier });
            modelBuilder.Entity<CarriersPost>()
                .ToTable("md_vw_CarriersPost")
                .HasKey(c => new { c.ixCarrier });
            modelBuilder.Entity<CarrierTypes>()
                .ToTable("config_vw_CarrierTypes")
                .HasKey(c => new { c.ixCarrierType });
            modelBuilder.Entity<CarrierTypesPost>()
                .ToTable("config_vw_CarrierTypesPost")
                .HasKey(c => new { c.ixCarrierType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CarriersPost)).ToList())
            {
                var md_vw_carrierspost = e.Entity as CarriersPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCarrier = cmd.CreateParameter();
                            sCarrier.ParameterName = "p0";
                            sCarrier.Value = md_vw_carrierspost.sCarrier;
                            var ixCarrierType = cmd.CreateParameter();
                            ixCarrierType.ParameterName = "p1";
                            ixCarrierType.Value = md_vw_carrierspost.ixCarrierType;
                            var sStandardCarrierAlphaCode = cmd.CreateParameter();
                            sStandardCarrierAlphaCode.ParameterName = "p2";
                            sStandardCarrierAlphaCode.Value = md_vw_carrierspost.sStandardCarrierAlphaCode;
                            var sCarrierConsignmentNumberPrefix = cmd.CreateParameter();
                            sCarrierConsignmentNumberPrefix.ParameterName = "p3";
                            sCarrierConsignmentNumberPrefix.Value = md_vw_carrierspost.sCarrierConsignmentNumberPrefix;
                            var nCarrierConsignmentNumberStart = cmd.CreateParameter();
                            nCarrierConsignmentNumberStart.ParameterName = "p4";
                            nCarrierConsignmentNumberStart.Value = md_vw_carrierspost.nCarrierConsignmentNumberStart;
                            var nCarrierConsignmentNumberLastUsed = cmd.CreateParameter();
                            nCarrierConsignmentNumberLastUsed.ParameterName = "p5";
                            nCarrierConsignmentNumberLastUsed.Value = md_vw_carrierspost.nCarrierConsignmentNumberLastUsed;
                            var dtScheduledPickupTime = cmd.CreateParameter();
                            dtScheduledPickupTime.ParameterName = "p6";
                            dtScheduledPickupTime.Value = md_vw_carrierspost.dtScheduledPickupTime;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = md_vw_carrierspost.UserName;

                            var ixCarrier = cmd.CreateParameter();
                            ixCarrier.ParameterName = "p8";
                            ixCarrier.DbType = DbType.Int64;
                            ixCarrier.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCarriers ");
                            sql.Append("@sCarrier = @p0, ");
                            sql.Append("@ixCarrierType = @p1, ");
                            if (md_vw_carrierspost.sStandardCarrierAlphaCode != null) { sql.Append("@sStandardCarrierAlphaCode = @p2, "); }  
                            sql.Append("@sCarrierConsignmentNumberPrefix = @p3, ");
                            sql.Append("@nCarrierConsignmentNumberStart = @p4, ");
                            if (md_vw_carrierspost.nCarrierConsignmentNumberLastUsed != null) { sql.Append("@nCarrierConsignmentNumberLastUsed = @p5, "); }  
                            if (md_vw_carrierspost.dtScheduledPickupTime != null) { sql.Append("@dtScheduledPickupTime = @p6, "); }  
                            if (md_vw_carrierspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixCarrier = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCarrier);
                            cmd.Parameters.Add(ixCarrierType);
                            if (md_vw_carrierspost.sStandardCarrierAlphaCode != null) { cmd.Parameters.Add(sStandardCarrierAlphaCode); }
                            cmd.Parameters.Add(sCarrierConsignmentNumberPrefix);
                            cmd.Parameters.Add(nCarrierConsignmentNumberStart);
                            if (md_vw_carrierspost.nCarrierConsignmentNumberLastUsed != null) { cmd.Parameters.Add(nCarrierConsignmentNumberLastUsed); }
                            if (md_vw_carrierspost.dtScheduledPickupTime != null) { cmd.Parameters.Add(dtScheduledPickupTime); }
                            if (md_vw_carrierspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCarrier); 
                            cmd.ExecuteNonQuery();
                            md_vw_carrierspost.ixCarrier = (Int64)ixCarrier.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCarrier"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCarriers @ixCarrier = @p0, @sCarrier = @p1, @ixCarrierType = @p2, @sStandardCarrierAlphaCode = @p3, @sCarrierConsignmentNumberPrefix = @p4, @nCarrierConsignmentNumberStart = @p5, @nCarrierConsignmentNumberLastUsed = @p6, @dtScheduledPickupTime = @p7, @UserName = @p8", md_vw_carrierspost.ixCarrier, md_vw_carrierspost.sCarrier, md_vw_carrierspost.ixCarrierType, md_vw_carrierspost.sStandardCarrierAlphaCode, md_vw_carrierspost.sCarrierConsignmentNumberPrefix, md_vw_carrierspost.nCarrierConsignmentNumberStart, md_vw_carrierspost.nCarrierConsignmentNumberLastUsed, md_vw_carrierspost.dtScheduledPickupTime, md_vw_carrierspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCarriers @ixCarrier = @p0, @sCarrier = @p1, @ixCarrierType = @p2, @sStandardCarrierAlphaCode = @p3, @sCarrierConsignmentNumberPrefix = @p4, @nCarrierConsignmentNumberStart = @p5, @nCarrierConsignmentNumberLastUsed = @p6, @dtScheduledPickupTime = @p7, @UserName = @p8", md_vw_carrierspost.ixCarrier, md_vw_carrierspost.sCarrier, md_vw_carrierspost.ixCarrierType, md_vw_carrierspost.sStandardCarrierAlphaCode, md_vw_carrierspost.sCarrierConsignmentNumberPrefix, md_vw_carrierspost.nCarrierConsignmentNumberStart, md_vw_carrierspost.nCarrierConsignmentNumberLastUsed, md_vw_carrierspost.dtScheduledPickupTime, md_vw_carrierspost.UserName);
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
  

