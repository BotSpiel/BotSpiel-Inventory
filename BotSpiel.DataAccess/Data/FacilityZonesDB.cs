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

    public class FacilityZonesDB : DbContext
    {

        public FacilityZonesDB(DbContextOptions<FacilityZonesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<FacilityZones> FacilityZones { get; set; }
		public DbSet<FacilityZonesPost> FacilityZonesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilityZones>()
                .ToTable("md_vw_FacilityZones")
                .HasKey(c => new { c.ixFacilityZone });
            modelBuilder.Entity<FacilityZonesPost>()
                .ToTable("md_vw_FacilityZonesPost")
                .HasKey(c => new { c.ixFacilityZone });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is FacilityZonesPost)).ToList())
            {
                var md_vw_facilityzonespost = e.Entity as FacilityZonesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFacilityZone = cmd.CreateParameter();
                            sFacilityZone.ParameterName = "p0";
                            sFacilityZone.Value = md_vw_facilityzonespost.sFacilityZone;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_facilityzonespost.UserName;

                            var ixFacilityZone = cmd.CreateParameter();
                            ixFacilityZone.ParameterName = "p2";
                            ixFacilityZone.DbType = DbType.Int64;
                            ixFacilityZone.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateFacilityZones ");
                            sql.Append("@sFacilityZone = @p0, ");
                            if (md_vw_facilityzonespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixFacilityZone = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFacilityZone);
                            if (md_vw_facilityzonespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFacilityZone); 
                            cmd.ExecuteNonQuery();
                            md_vw_facilityzonespost.ixFacilityZone = (Int64)ixFacilityZone.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFacilityZone"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeFacilityZones @ixFacilityZone = @p0, @sFacilityZone = @p1, @UserName = @p2", md_vw_facilityzonespost.ixFacilityZone, md_vw_facilityzonespost.sFacilityZone, md_vw_facilityzonespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteFacilityZones @ixFacilityZone = @p0, @sFacilityZone = @p1, @UserName = @p2", md_vw_facilityzonespost.ixFacilityZone, md_vw_facilityzonespost.sFacilityZone, md_vw_facilityzonespost.UserName);
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
  

