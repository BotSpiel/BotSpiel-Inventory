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

    public class UniversesDB : DbContext
    {

        public UniversesDB(DbContextOptions<UniversesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is UniversesPost)).ToList())
            {
                var md_vw_universespost = e.Entity as UniversesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sUniverse = cmd.CreateParameter();
                            sUniverse.ParameterName = "p0";
                            sUniverse.Value = md_vw_universespost.sUniverse;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_universespost.UserName;

                            var ixUniverse = cmd.CreateParameter();
                            ixUniverse.ParameterName = "p2";
                            ixUniverse.DbType = DbType.Int64;
                            ixUniverse.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateUniverses ");
                            sql.Append("@sUniverse = @p0, ");
                            if (md_vw_universespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixUniverse = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sUniverse);
                            if (md_vw_universespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixUniverse); 
                            cmd.ExecuteNonQuery();
                            md_vw_universespost.ixUniverse = (Int64)ixUniverse.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixUniverse"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeUniverses @ixUniverse = @p0, @sUniverse = @p1, @UserName = @p2", md_vw_universespost.ixUniverse, md_vw_universespost.sUniverse, md_vw_universespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteUniverses @ixUniverse = @p0, @sUniverse = @p1, @UserName = @p2", md_vw_universespost.ixUniverse, md_vw_universespost.sUniverse, md_vw_universespost.UserName);
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
  

