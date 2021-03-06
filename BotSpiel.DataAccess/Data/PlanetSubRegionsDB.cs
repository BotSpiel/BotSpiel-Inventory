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

    public class PlanetSubRegionsDB : DbContext
    {

        public PlanetSubRegionsDB(DbContextOptions<PlanetSubRegionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PlanetSubRegions> PlanetSubRegions { get; set; }
		public DbSet<PlanetSubRegionsPost> PlanetSubRegionsPost { get; set; }
		public DbSet<PlanetRegions> PlanetRegions { get; set; }
		public DbSet<PlanetRegionsPost> PlanetRegionsPost { get; set; }
		public DbSet<Planets> Planets { get; set; }
		public DbSet<PlanetsPost> PlanetsPost { get; set; }
		public DbSet<PlanetarySystems> PlanetarySystems { get; set; }
		public DbSet<PlanetarySystemsPost> PlanetarySystemsPost { get; set; }
		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanetSubRegions>()
                .ToTable("md_vw_PlanetSubRegions")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetSubRegionsPost>()
                .ToTable("md_vw_PlanetSubRegionsPost")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetRegions>()
                .ToTable("md_vw_PlanetRegions")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<PlanetRegionsPost>()
                .ToTable("md_vw_PlanetRegionsPost")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<Planets>()
                .ToTable("md_vw_Planets")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetsPost>()
                .ToTable("md_vw_PlanetsPost")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetarySystems>()
                .ToTable("md_vw_PlanetarySystems")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<PlanetarySystemsPost>()
                .ToTable("md_vw_PlanetarySystemsPost")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<Galaxies>()
                .ToTable("md_vw_Galaxies")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<GalaxiesPost>()
                .ToTable("md_vw_GalaxiesPost")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<Universes>()
                .ToTable("md_vw_Universes")
                .HasKey(c => new { c.ixUniverse });
            modelBuilder.Entity<UniversesPost>()
                .ToTable("md_vw_UniversesPost")
                .HasKey(c => new { c.ixUniverse });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PlanetSubRegionsPost)).ToList())
            {
                var md_vw_planetsubregionspost = e.Entity as PlanetSubRegionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPlanetSubRegion = cmd.CreateParameter();
                            sPlanetSubRegion.ParameterName = "p0";
                            sPlanetSubRegion.Value = md_vw_planetsubregionspost.sPlanetSubRegion;
                            var ixPlanetRegion = cmd.CreateParameter();
                            ixPlanetRegion.ParameterName = "p1";
                            ixPlanetRegion.Value = md_vw_planetsubregionspost.ixPlanetRegion;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_planetsubregionspost.UserName;

                            var ixPlanetSubRegion = cmd.CreateParameter();
                            ixPlanetSubRegion.ParameterName = "p3";
                            ixPlanetSubRegion.DbType = DbType.Int64;
                            ixPlanetSubRegion.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreatePlanetSubRegions ");
                            sql.Append("@sPlanetSubRegion = @p0, ");
                            sql.Append("@ixPlanetRegion = @p1, ");
                            if (md_vw_planetsubregionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPlanetSubRegion = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPlanetSubRegion);
                            cmd.Parameters.Add(ixPlanetRegion);
                            if (md_vw_planetsubregionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPlanetSubRegion); 
                            cmd.ExecuteNonQuery();
                            md_vw_planetsubregionspost.ixPlanetSubRegion = (Int64)ixPlanetSubRegion.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPlanetSubRegion"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangePlanetSubRegions @ixPlanetSubRegion = @p0, @sPlanetSubRegion = @p1, @ixPlanetRegion = @p2, @UserName = @p3", md_vw_planetsubregionspost.ixPlanetSubRegion, md_vw_planetsubregionspost.sPlanetSubRegion, md_vw_planetsubregionspost.ixPlanetRegion, md_vw_planetsubregionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeletePlanetSubRegions @ixPlanetSubRegion = @p0, @sPlanetSubRegion = @p1, @ixPlanetRegion = @p2, @UserName = @p3", md_vw_planetsubregionspost.ixPlanetSubRegion, md_vw_planetsubregionspost.sPlanetSubRegion, md_vw_planetsubregionspost.ixPlanetRegion, md_vw_planetsubregionspost.UserName);
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
  

