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

    public class CurrencyTypesDB : DbContext
    {

        public CurrencyTypesDB(DbContextOptions<CurrencyTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<CurrencyTypes> CurrencyTypes { get; set; }
		public DbSet<CurrencyTypesPost> CurrencyTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyTypes>()
                .ToTable("config_vw_CurrencyTypes")
                .HasKey(c => new { c.ixCurrencyType });
            modelBuilder.Entity<CurrencyTypesPost>()
                .ToTable("config_vw_CurrencyTypesPost")
                .HasKey(c => new { c.ixCurrencyType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CurrencyTypesPost)).ToList())
            {
                var config_vw_currencytypespost = e.Entity as CurrencyTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCurrencyType = cmd.CreateParameter();
                            sCurrencyType.ParameterName = "p0";
                            sCurrencyType.Value = config_vw_currencytypespost.sCurrencyType;
                            var sCurrencyTypeCode = cmd.CreateParameter();
                            sCurrencyTypeCode.ParameterName = "p1";
                            sCurrencyTypeCode.Value = config_vw_currencytypespost.sCurrencyTypeCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_currencytypespost.UserName;

                            var ixCurrencyType = cmd.CreateParameter();
                            ixCurrencyType.ParameterName = "p3";
                            ixCurrencyType.DbType = DbType.Int64;
                            ixCurrencyType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateCurrencyTypes ");
                            sql.Append("@sCurrencyType = @p0, ");
                            sql.Append("@sCurrencyTypeCode = @p1, ");
                            if (config_vw_currencytypespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixCurrencyType = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCurrencyType);
                            cmd.Parameters.Add(sCurrencyTypeCode);
                            if (config_vw_currencytypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCurrencyType); 
                            cmd.ExecuteNonQuery();
                            config_vw_currencytypespost.ixCurrencyType = (Int64)ixCurrencyType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCurrencyType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeCurrencyTypes @ixCurrencyType = @p0, @sCurrencyType = @p1, @sCurrencyTypeCode = @p2, @UserName = @p3", config_vw_currencytypespost.ixCurrencyType, config_vw_currencytypespost.sCurrencyType, config_vw_currencytypespost.sCurrencyTypeCode, config_vw_currencytypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteCurrencyTypes @ixCurrencyType = @p0, @sCurrencyType = @p1, @sCurrencyTypeCode = @p2, @UserName = @p3", config_vw_currencytypespost.ixCurrencyType, config_vw_currencytypespost.sCurrencyType, config_vw_currencytypespost.sCurrencyTypeCode, config_vw_currencytypespost.UserName);
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
  

