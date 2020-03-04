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

    public class PlanetsDB : DbContext
    {

        public PlanetsDB(DbContextOptions<PlanetsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PlanetsPost)).ToList())
            {
                var md_vw_planetspost = e.Entity as PlanetsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPlanet = cmd.CreateParameter();
                            sPlanet.ParameterName = "p0";
                            sPlanet.Value = md_vw_planetspost.sPlanet;
                            var ixPlanetarySystem = cmd.CreateParameter();
                            ixPlanetarySystem.ParameterName = "p1";
                            ixPlanetarySystem.Value = md_vw_planetspost.ixPlanetarySystem;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_planetspost.UserName;

                            var ixPlanet = cmd.CreateParameter();
                            ixPlanet.ParameterName = "p3";
                            ixPlanet.DbType = DbType.Int64;
                            ixPlanet.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreatePlanets ");
                            sql.Append("@sPlanet = @p0, ");
                            sql.Append("@ixPlanetarySystem = @p1, ");
                            if (md_vw_planetspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPlanet = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPlanet);
                            cmd.Parameters.Add(ixPlanetarySystem);
                            if (md_vw_planetspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPlanet); 
                            cmd.ExecuteNonQuery();
                            md_vw_planetspost.ixPlanet = (Int64)ixPlanet.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPlanet"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangePlanets @ixPlanet = @p0, @sPlanet = @p1, @ixPlanetarySystem = @p2, @UserName = @p3", md_vw_planetspost.ixPlanet, md_vw_planetspost.sPlanet, md_vw_planetspost.ixPlanetarySystem, md_vw_planetspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeletePlanets @ixPlanet = @p0, @sPlanet = @p1, @ixPlanetarySystem = @p2, @UserName = @p3", md_vw_planetspost.ixPlanet, md_vw_planetspost.sPlanet, md_vw_planetspost.ixPlanetarySystem, md_vw_planetspost.UserName);
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
  

