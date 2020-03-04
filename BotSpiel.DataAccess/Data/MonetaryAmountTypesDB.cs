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

    public class MonetaryAmountTypesDB : DbContext
    {

        public MonetaryAmountTypesDB(DbContextOptions<MonetaryAmountTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MonetaryAmountTypes> MonetaryAmountTypes { get; set; }
		public DbSet<MonetaryAmountTypesPost> MonetaryAmountTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonetaryAmountTypes>()
                .ToTable("config_vw_MonetaryAmountTypes")
                .HasKey(c => new { c.ixMonetaryAmountType });
            modelBuilder.Entity<MonetaryAmountTypesPost>()
                .ToTable("config_vw_MonetaryAmountTypesPost")
                .HasKey(c => new { c.ixMonetaryAmountType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is MonetaryAmountTypesPost)).ToList())
            {
                var config_vw_monetaryamounttypespost = e.Entity as MonetaryAmountTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMonetaryAmountType = cmd.CreateParameter();
                            sMonetaryAmountType.ParameterName = "p0";
                            sMonetaryAmountType.Value = config_vw_monetaryamounttypespost.sMonetaryAmountType;
                            var sMonetaryAmountTypeCode = cmd.CreateParameter();
                            sMonetaryAmountTypeCode.ParameterName = "p1";
                            sMonetaryAmountTypeCode.Value = config_vw_monetaryamounttypespost.sMonetaryAmountTypeCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_monetaryamounttypespost.UserName;

                            var ixMonetaryAmountType = cmd.CreateParameter();
                            ixMonetaryAmountType.ParameterName = "p3";
                            ixMonetaryAmountType.DbType = DbType.Int64;
                            ixMonetaryAmountType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateMonetaryAmountTypes ");
                            sql.Append("@sMonetaryAmountType = @p0, ");
                            sql.Append("@sMonetaryAmountTypeCode = @p1, ");
                            if (config_vw_monetaryamounttypespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixMonetaryAmountType = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMonetaryAmountType);
                            cmd.Parameters.Add(sMonetaryAmountTypeCode);
                            if (config_vw_monetaryamounttypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMonetaryAmountType); 
                            cmd.ExecuteNonQuery();
                            config_vw_monetaryamounttypespost.ixMonetaryAmountType = (Int64)ixMonetaryAmountType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMonetaryAmountType"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeMonetaryAmountTypes @ixMonetaryAmountType = @p0, @sMonetaryAmountType = @p1, @sMonetaryAmountTypeCode = @p2, @UserName = @p3", config_vw_monetaryamounttypespost.ixMonetaryAmountType, config_vw_monetaryamounttypespost.sMonetaryAmountType, config_vw_monetaryamounttypespost.sMonetaryAmountTypeCode, config_vw_monetaryamounttypespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteMonetaryAmountTypes @ixMonetaryAmountType = @p0, @sMonetaryAmountType = @p1, @sMonetaryAmountTypeCode = @p2, @UserName = @p3", config_vw_monetaryamounttypespost.ixMonetaryAmountType, config_vw_monetaryamounttypespost.sMonetaryAmountType, config_vw_monetaryamounttypespost.sMonetaryAmountTypeCode, config_vw_monetaryamounttypespost.UserName);
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
  

