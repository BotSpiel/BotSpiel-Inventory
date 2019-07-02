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

    public class GalaxiesDB : DbContext
    {

        public GalaxiesDB(DbContextOptions<GalaxiesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is GalaxiesPost)).ToList())
            {
                var md_vw_galaxiespost = e.Entity as GalaxiesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sGalaxy = cmd.CreateParameter();
                            sGalaxy.ParameterName = "p0";
                            sGalaxy.Value = md_vw_galaxiespost.sGalaxy;
                            var ixUniverse = cmd.CreateParameter();
                            ixUniverse.ParameterName = "p1";
                            ixUniverse.Value = md_vw_galaxiespost.ixUniverse;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = md_vw_galaxiespost.UserName;

                            var ixGalaxy = cmd.CreateParameter();
                            ixGalaxy.ParameterName = "p3";
                            ixGalaxy.DbType = DbType.Int64;
                            ixGalaxy.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateGalaxies ");
                            sql.Append("@sGalaxy = @p0, ");
                            sql.Append("@ixUniverse = @p1, ");
                            if (md_vw_galaxiespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixGalaxy = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sGalaxy);
                            cmd.Parameters.Add(ixUniverse);
                            if (md_vw_galaxiespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixGalaxy); 
                            cmd.ExecuteNonQuery();
                            md_vw_galaxiespost.ixGalaxy = (Int64)ixGalaxy.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixGalaxy"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeGalaxies @ixGalaxy = @p0, @sGalaxy = @p1, @ixUniverse = @p2, @UserName = @p3", md_vw_galaxiespost.ixGalaxy, md_vw_galaxiespost.sGalaxy, md_vw_galaxiespost.ixUniverse, md_vw_galaxiespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteGalaxies @ixGalaxy = @p0, @sGalaxy = @p1, @ixUniverse = @p2, @UserName = @p3", md_vw_galaxiespost.ixGalaxy, md_vw_galaxiespost.sGalaxy, md_vw_galaxiespost.ixUniverse, md_vw_galaxiespost.UserName);
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
  

