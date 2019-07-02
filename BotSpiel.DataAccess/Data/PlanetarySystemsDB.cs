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

    public class PlanetarySystemsDB : DbContext
    {

        public PlanetarySystemsDB(DbContextOptions<PlanetarySystemsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PlanetarySystems> PlanetarySystems { get; set; }
		public DbSet<PlanetarySystemsPost> PlanetarySystemsPost { get; set; }
		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is PlanetarySystemsPost)).ToList())
            {
                var md_vw_planetarysystemspost = e.Entity as PlanetarySystemsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPlanetarySystem = cmd.CreateParameter();
                            sPlanetarySystem.ParameterName = "p0";
                            sPlanetarySystem.Value = md_vw_planetarysystemspost.sPlanetarySystem;
                            var ixGalaxy = cmd.CreateParameter();
                            ixGalaxy.ParameterName = "p1";
                            ixGalaxy.Value = md_vw_planetarysystemspost.ixGalaxy;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_planetarysystemspost.UserName;

                            var ixPlanetarySystem = cmd.CreateParameter();
                            ixPlanetarySystem.ParameterName = "p3";
                            ixPlanetarySystem.DbType = DbType.Int64;
                            ixPlanetarySystem.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreatePlanetarySystems ");
                            sql.Append("@sPlanetarySystem = @p0, ");
                            sql.Append("@ixGalaxy = @p1, ");
                            if (md_vw_planetarysystemspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixPlanetarySystem = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPlanetarySystem);
                            cmd.Parameters.Add(ixGalaxy);
                            if (md_vw_planetarysystemspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPlanetarySystem); 
                            cmd.ExecuteNonQuery();
                            md_vw_planetarysystemspost.ixPlanetarySystem = (Int64)ixPlanetarySystem.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPlanetarySystem"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangePlanetarySystems @ixPlanetarySystem = @p0, @sPlanetarySystem = @p1, @ixGalaxy = @p2, @UserName = @p3", md_vw_planetarysystemspost.ixPlanetarySystem, md_vw_planetarysystemspost.sPlanetarySystem, md_vw_planetarysystemspost.ixGalaxy, md_vw_planetarysystemspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeletePlanetarySystems @ixPlanetarySystem = @p0, @sPlanetarySystem = @p1, @ixGalaxy = @p2, @UserName = @p3", md_vw_planetarysystemspost.ixPlanetarySystem, md_vw_planetarysystemspost.sPlanetarySystem, md_vw_planetarysystemspost.ixGalaxy, md_vw_planetarysystemspost.UserName);
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
  

