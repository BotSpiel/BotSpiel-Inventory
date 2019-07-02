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

    public class CarrierServicesDB : DbContext
    {

        public CarrierServicesDB(DbContextOptions<CarrierServicesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<CarrierServices> CarrierServices { get; set; }
		public DbSet<CarrierServicesPost> CarrierServicesPost { get; set; }
		public DbSet<Carriers> Carriers { get; set; }
		public DbSet<CarriersPost> CarriersPost { get; set; }
		public DbSet<CarrierTypes> CarrierTypes { get; set; }
		public DbSet<CarrierTypesPost> CarrierTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrierServices>()
                .ToTable("md_vw_CarrierServices")
                .HasKey(c => new { c.ixCarrierService });
            modelBuilder.Entity<CarrierServicesPost>()
                .ToTable("md_vw_CarrierServicesPost")
                .HasKey(c => new { c.ixCarrierService });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CarrierServicesPost)).ToList())
            {
                var md_vw_carrierservicespost = e.Entity as CarrierServicesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCarrierService = cmd.CreateParameter();
                            sCarrierService.ParameterName = "p0";
                            sCarrierService.Value = md_vw_carrierservicespost.sCarrierService;
                            var ixCarrier = cmd.CreateParameter();
                            ixCarrier.ParameterName = "p1";
                            ixCarrier.Value = md_vw_carrierservicespost.ixCarrier;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_carrierservicespost.UserName;

                            var ixCarrierService = cmd.CreateParameter();
                            ixCarrierService.ParameterName = "p3";
                            ixCarrierService.DbType = DbType.Int64;
                            ixCarrierService.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCarrierServices ");
                            sql.Append("@sCarrierService = @p0, ");
                            sql.Append("@ixCarrier = @p1, ");
                            if (md_vw_carrierservicespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixCarrierService = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCarrierService);
                            cmd.Parameters.Add(ixCarrier);
                            if (md_vw_carrierservicespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCarrierService); 
                            cmd.ExecuteNonQuery();
                            md_vw_carrierservicespost.ixCarrierService = (Int64)ixCarrierService.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCarrierService"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCarrierServices @ixCarrierService = @p0, @sCarrierService = @p1, @ixCarrier = @p2, @UserName = @p3", md_vw_carrierservicespost.ixCarrierService, md_vw_carrierservicespost.sCarrierService, md_vw_carrierservicespost.ixCarrier, md_vw_carrierservicespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCarrierServices @ixCarrierService = @p0, @sCarrierService = @p1, @ixCarrier = @p2, @UserName = @p3", md_vw_carrierservicespost.ixCarrierService, md_vw_carrierservicespost.sCarrierService, md_vw_carrierservicespost.ixCarrier, md_vw_carrierservicespost.UserName);
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
  

