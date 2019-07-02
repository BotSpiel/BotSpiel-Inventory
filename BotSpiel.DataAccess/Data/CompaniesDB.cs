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

    public class CompaniesDB : DbContext
    {

        public CompaniesDB(DbContextOptions<CompaniesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>()
                .ToTable("md_vw_Companies")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<CompaniesPost>()
                .ToTable("md_vw_CompaniesPost")
                .HasKey(c => new { c.ixCompany });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CompaniesPost)).ToList())
            {
                var md_vw_companiespost = e.Entity as CompaniesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCompany = cmd.CreateParameter();
                            sCompany.ParameterName = "p0";
                            sCompany.Value = md_vw_companiespost.sCompany;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_companiespost.UserName;

                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p2";
                            ixCompany.DbType = DbType.Int64;
                            ixCompany.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCompanies ");
                            sql.Append("@sCompany = @p0, ");
                            if (md_vw_companiespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixCompany = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCompany);
                            if (md_vw_companiespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCompany); 
                            cmd.ExecuteNonQuery();
                            md_vw_companiespost.ixCompany = (Int64)ixCompany.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCompany"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCompanies @ixCompany = @p0, @sCompany = @p1, @UserName = @p2", md_vw_companiespost.ixCompany, md_vw_companiespost.sCompany, md_vw_companiespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCompanies @ixCompany = @p0, @sCompany = @p1, @UserName = @p2", md_vw_companiespost.ixCompany, md_vw_companiespost.sCompany, md_vw_companiespost.UserName);
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
  

