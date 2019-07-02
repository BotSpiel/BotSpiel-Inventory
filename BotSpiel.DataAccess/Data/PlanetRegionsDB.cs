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

    public class PlanetRegionsDB : DbContext
    {

        public PlanetRegionsDB(DbContextOptions<PlanetRegionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is PlanetRegionsPost)).ToList())
            {
                var md_vw_planetregionspost = e.Entity as PlanetRegionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPlanetRegion = cmd.CreateParameter();
                            sPlanetRegion.ParameterName = "p0";
                            sPlanetRegion.Value = md_vw_planetregionspost.sPlanetRegion;
                            var ixPlanet = cmd.CreateParameter();
                            ixPlanet.ParameterName = "p1";
                            ixPlanet.Value = md_vw_planetregionspost.ixPlanet;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_planetregionspost.UserName;

                            var ixPlanetRegion = cmd.CreateParameter();
                            ixPlanetRegion.ParameterName = "p3";
                            ixPlanetRegion.DbType = DbType.Int64;
                            ixPlanetRegion.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreatePlanetRegions ");
                            sql.Append("@sPlanetRegion = @p0, ");
                            sql.Append("@ixPlanet = @p1, ");
                            if (md_vw_planetregionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPlanetRegion = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPlanetRegion);
                            cmd.Parameters.Add(ixPlanet);
                            if (md_vw_planetregionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPlanetRegion); 
                            cmd.ExecuteNonQuery();
                            md_vw_planetregionspost.ixPlanetRegion = (Int64)ixPlanetRegion.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPlanetRegion"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangePlanetRegions @ixPlanetRegion = @p0, @sPlanetRegion = @p1, @ixPlanet = @p2, @UserName = @p3", md_vw_planetregionspost.ixPlanetRegion, md_vw_planetregionspost.sPlanetRegion, md_vw_planetregionspost.ixPlanet, md_vw_planetregionspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeletePlanetRegions @ixPlanetRegion = @p0, @sPlanetRegion = @p1, @ixPlanet = @p2, @UserName = @p3", md_vw_planetregionspost.ixPlanetRegion, md_vw_planetregionspost.sPlanetRegion, md_vw_planetregionspost.ixPlanet, md_vw_planetregionspost.UserName);
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
  

