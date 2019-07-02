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

    public class PeopleDB : DbContext
    {

        public PeopleDB(DbContextOptions<PeopleDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>()
                .ToTable("md_vw_People")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<PeoplePost>()
                .ToTable("md_vw_PeoplePost")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is PeoplePost)).ToList())
            {
                var md_vw_peoplepost = e.Entity as PeoplePost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPerson = cmd.CreateParameter();
                            sPerson.ParameterName = "p0";
                            sPerson.Value = md_vw_peoplepost.sPerson;
                            var sFirstName = cmd.CreateParameter();
                            sFirstName.ParameterName = "p1";
                            sFirstName.Value = md_vw_peoplepost.sFirstName;
                            var sLastName = cmd.CreateParameter();
                            sLastName.ParameterName = "p2";
                            sLastName.Value = md_vw_peoplepost.sLastName;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p3";
                            ixLanguage.Value = md_vw_peoplepost.ixLanguage;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = md_vw_peoplepost.UserName;

                            var ixPerson = cmd.CreateParameter();
                            ixPerson.ParameterName = "p5";
                            ixPerson.DbType = DbType.Int64;
                            ixPerson.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreatePeople ");
                            sql.Append("@sPerson = @p0, ");
                            sql.Append("@sFirstName = @p1, ");
                            sql.Append("@sLastName = @p2, ");
                            sql.Append("@ixLanguage = @p3, ");
                            if (md_vw_peoplepost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixPerson = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPerson);
                            cmd.Parameters.Add(sFirstName);
                            cmd.Parameters.Add(sLastName);
                            cmd.Parameters.Add(ixLanguage);
                            if (md_vw_peoplepost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPerson); 
                            cmd.ExecuteNonQuery();
                            md_vw_peoplepost.ixPerson = (Int64)ixPerson.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPerson"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangePeople @ixPerson = @p0, @sPerson = @p1, @sFirstName = @p2, @sLastName = @p3, @ixLanguage = @p4, @UserName = @p5", md_vw_peoplepost.ixPerson, md_vw_peoplepost.sPerson, md_vw_peoplepost.sFirstName, md_vw_peoplepost.sLastName, md_vw_peoplepost.ixLanguage, md_vw_peoplepost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeletePeople @ixPerson = @p0, @sPerson = @p1, @sFirstName = @p2, @sLastName = @p3, @ixLanguage = @p4, @UserName = @p5", md_vw_peoplepost.ixPerson, md_vw_peoplepost.sPerson, md_vw_peoplepost.sFirstName, md_vw_peoplepost.sLastName, md_vw_peoplepost.ixLanguage, md_vw_peoplepost.UserName);
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
  

