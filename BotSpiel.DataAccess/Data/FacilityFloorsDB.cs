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

    public class FacilityFloorsDB : DbContext
    {

        public FacilityFloorsDB(DbContextOptions<FacilityFloorsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<FacilityFloors> FacilityFloors { get; set; }
		public DbSet<FacilityFloorsPost> FacilityFloorsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilityFloors>()
                .ToTable("md_vw_FacilityFloors")
                .HasKey(c => new { c.ixFacilityFloor });
            modelBuilder.Entity<FacilityFloorsPost>()
                .ToTable("md_vw_FacilityFloorsPost")
                .HasKey(c => new { c.ixFacilityFloor });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is FacilityFloorsPost)).ToList())
            {
                var md_vw_facilityfloorspost = e.Entity as FacilityFloorsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFacilityFloor = cmd.CreateParameter();
                            sFacilityFloor.ParameterName = "p0";
                            sFacilityFloor.Value = md_vw_facilityfloorspost.sFacilityFloor;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_facilityfloorspost.UserName;

                            var ixFacilityFloor = cmd.CreateParameter();
                            ixFacilityFloor.ParameterName = "p2";
                            ixFacilityFloor.DbType = DbType.Int64;
                            ixFacilityFloor.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateFacilityFloors ");
                            sql.Append("@sFacilityFloor = @p0, ");
                            if (md_vw_facilityfloorspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixFacilityFloor = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFacilityFloor);
                            if (md_vw_facilityfloorspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFacilityFloor); 
                            cmd.ExecuteNonQuery();
                            md_vw_facilityfloorspost.ixFacilityFloor = (Int64)ixFacilityFloor.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFacilityFloor"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeFacilityFloors @ixFacilityFloor = @p0, @sFacilityFloor = @p1, @UserName = @p2", md_vw_facilityfloorspost.ixFacilityFloor, md_vw_facilityfloorspost.sFacilityFloor, md_vw_facilityfloorspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteFacilityFloors @ixFacilityFloor = @p0, @sFacilityFloor = @p1, @UserName = @p2", md_vw_facilityfloorspost.ixFacilityFloor, md_vw_facilityfloorspost.sFacilityFloor, md_vw_facilityfloorspost.UserName);
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
  

