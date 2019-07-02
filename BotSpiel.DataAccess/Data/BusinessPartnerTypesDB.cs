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

    public class BusinessPartnerTypesDB : DbContext
    {

        public BusinessPartnerTypesDB(DbContextOptions<BusinessPartnerTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<BusinessPartnerTypes> BusinessPartnerTypes { get; set; }
		public DbSet<BusinessPartnerTypesPost> BusinessPartnerTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessPartnerTypes>()
                .ToTable("config_vw_BusinessPartnerTypes")
                .HasKey(c => new { c.ixBusinessPartnerType });
            modelBuilder.Entity<BusinessPartnerTypesPost>()
                .ToTable("config_vw_BusinessPartnerTypesPost")
                .HasKey(c => new { c.ixBusinessPartnerType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is BusinessPartnerTypesPost)).ToList())
            {
                var config_vw_businesspartnertypespost = e.Entity as BusinessPartnerTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sBusinessPartnerType = cmd.CreateParameter();
                            sBusinessPartnerType.ParameterName = "p0";
                            sBusinessPartnerType.Value = config_vw_businesspartnertypespost.sBusinessPartnerType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_businesspartnertypespost.UserName;

                            var ixBusinessPartnerType = cmd.CreateParameter();
                            ixBusinessPartnerType.ParameterName = "p2";
                            ixBusinessPartnerType.DbType = DbType.Int64;
                            ixBusinessPartnerType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateBusinessPartnerTypes ");
                            sql.Append("@sBusinessPartnerType = @p0, ");
                            if (config_vw_businesspartnertypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixBusinessPartnerType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sBusinessPartnerType);
                            if (config_vw_businesspartnertypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixBusinessPartnerType); 
                            cmd.ExecuteNonQuery();
                            config_vw_businesspartnertypespost.ixBusinessPartnerType = (Int64)ixBusinessPartnerType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixBusinessPartnerType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeBusinessPartnerTypes @ixBusinessPartnerType = @p0, @sBusinessPartnerType = @p1, @UserName = @p2", config_vw_businesspartnertypespost.ixBusinessPartnerType, config_vw_businesspartnertypespost.sBusinessPartnerType, config_vw_businesspartnertypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteBusinessPartnerTypes @ixBusinessPartnerType = @p0, @sBusinessPartnerType = @p1, @UserName = @p2", config_vw_businesspartnertypespost.ixBusinessPartnerType, config_vw_businesspartnertypespost.sBusinessPartnerType, config_vw_businesspartnertypespost.UserName);
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
  

