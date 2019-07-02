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

    public class FacilityWorkAreasDB : DbContext
    {

        public FacilityWorkAreasDB(DbContextOptions<FacilityWorkAreasDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<FacilityWorkAreas> FacilityWorkAreas { get; set; }
		public DbSet<FacilityWorkAreasPost> FacilityWorkAreasPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilityWorkAreas>()
                .ToTable("md_vw_FacilityWorkAreas")
                .HasKey(c => new { c.ixFacilityWorkArea });
            modelBuilder.Entity<FacilityWorkAreasPost>()
                .ToTable("md_vw_FacilityWorkAreasPost")
                .HasKey(c => new { c.ixFacilityWorkArea });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is FacilityWorkAreasPost)).ToList())
            {
                var md_vw_facilityworkareaspost = e.Entity as FacilityWorkAreasPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFacilityWorkArea = cmd.CreateParameter();
                            sFacilityWorkArea.ParameterName = "p0";
                            sFacilityWorkArea.Value = md_vw_facilityworkareaspost.sFacilityWorkArea;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_facilityworkareaspost.UserName;

                            var ixFacilityWorkArea = cmd.CreateParameter();
                            ixFacilityWorkArea.ParameterName = "p2";
                            ixFacilityWorkArea.DbType = DbType.Int64;
                            ixFacilityWorkArea.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateFacilityWorkAreas ");
                            sql.Append("@sFacilityWorkArea = @p0, ");
                            if (md_vw_facilityworkareaspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixFacilityWorkArea = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFacilityWorkArea);
                            if (md_vw_facilityworkareaspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFacilityWorkArea); 
                            cmd.ExecuteNonQuery();
                            md_vw_facilityworkareaspost.ixFacilityWorkArea = (Int64)ixFacilityWorkArea.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFacilityWorkArea"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeFacilityWorkAreas @ixFacilityWorkArea = @p0, @sFacilityWorkArea = @p1, @UserName = @p2", md_vw_facilityworkareaspost.ixFacilityWorkArea, md_vw_facilityworkareaspost.sFacilityWorkArea, md_vw_facilityworkareaspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteFacilityWorkAreas @ixFacilityWorkArea = @p0, @sFacilityWorkArea = @p1, @UserName = @p2", md_vw_facilityworkareaspost.ixFacilityWorkArea, md_vw_facilityworkareaspost.sFacilityWorkArea, md_vw_facilityworkareaspost.UserName);
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
  

