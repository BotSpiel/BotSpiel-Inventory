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

    public class CurrenciesDB : DbContext
    {

        public CurrenciesDB(DbContextOptions<CurrenciesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Currencies> Currencies { get; set; }
		public DbSet<CurrenciesPost> CurrenciesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currencies>()
                .ToTable("md_vw_Currencies")
                .HasKey(c => new { c.ixCurrency });
            modelBuilder.Entity<CurrenciesPost>()
                .ToTable("md_vw_CurrenciesPost")
                .HasKey(c => new { c.ixCurrency });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is CurrenciesPost)).ToList())
            {
                var md_vw_currenciespost = e.Entity as CurrenciesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCurrency = cmd.CreateParameter();
                            sCurrency.ParameterName = "p0";
                            sCurrency.Value = md_vw_currenciespost.sCurrency;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = md_vw_currenciespost.UserName;

                            var ixCurrency = cmd.CreateParameter();
                            ixCurrency.ParameterName = "p2";
                            ixCurrency.DbType = DbType.Int64;
                            ixCurrency.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCurrencies ");
                            sql.Append("@sCurrency = @p0, ");
                            if (md_vw_currenciespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixCurrency = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCurrency);
                            if (md_vw_currenciespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCurrency); 
                            cmd.ExecuteNonQuery();
                            md_vw_currenciespost.ixCurrency = (Int64)ixCurrency.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCurrency"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCurrencies @ixCurrency = @p0, @sCurrency = @p1, @UserName = @p2", md_vw_currenciespost.ixCurrency, md_vw_currenciespost.sCurrency, md_vw_currenciespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCurrencies @ixCurrency = @p0, @sCurrency = @p1, @UserName = @p2", md_vw_currenciespost.ixCurrency, md_vw_currenciespost.sCurrency, md_vw_currenciespost.UserName);
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
  

