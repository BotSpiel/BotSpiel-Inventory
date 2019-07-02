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

    public class DateTimePeriodFunctionsDB : DbContext
    {

        public DateTimePeriodFunctionsDB(DbContextOptions<DateTimePeriodFunctionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<DateTimePeriodFunctions> DateTimePeriodFunctions { get; set; }
		public DbSet<DateTimePeriodFunctionsPost> DateTimePeriodFunctionsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateTimePeriodFunctions>()
                .ToTable("config_vw_DateTimePeriodFunctions")
                .HasKey(c => new { c.ixDateTimePeriodFunction });
            modelBuilder.Entity<DateTimePeriodFunctionsPost>()
                .ToTable("config_vw_DateTimePeriodFunctionsPost")
                .HasKey(c => new { c.ixDateTimePeriodFunction });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is DateTimePeriodFunctionsPost)).ToList())
            {
                var config_vw_datetimeperiodfunctionspost = e.Entity as DateTimePeriodFunctionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sDateTimePeriodFunction = cmd.CreateParameter();
                            sDateTimePeriodFunction.ParameterName = "p0";
                            sDateTimePeriodFunction.Value = config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunction;
                            var sDateTimePeriodFunctionCode = cmd.CreateParameter();
                            sDateTimePeriodFunctionCode.ParameterName = "p1";
                            sDateTimePeriodFunctionCode.Value = config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunctionCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_datetimeperiodfunctionspost.UserName;

                            var ixDateTimePeriodFunction = cmd.CreateParameter();
                            ixDateTimePeriodFunction.ParameterName = "p3";
                            ixDateTimePeriodFunction.DbType = DbType.Int64;
                            ixDateTimePeriodFunction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateDateTimePeriodFunctions ");
                            sql.Append("@sDateTimePeriodFunction = @p0, ");
                            sql.Append("@sDateTimePeriodFunctionCode = @p1, ");
                            if (config_vw_datetimeperiodfunctionspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixDateTimePeriodFunction = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sDateTimePeriodFunction);
                            cmd.Parameters.Add(sDateTimePeriodFunctionCode);
                            if (config_vw_datetimeperiodfunctionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixDateTimePeriodFunction); 
                            cmd.ExecuteNonQuery();
                            config_vw_datetimeperiodfunctionspost.ixDateTimePeriodFunction = (Int64)ixDateTimePeriodFunction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixDateTimePeriodFunction"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeDateTimePeriodFunctions @ixDateTimePeriodFunction = @p0, @sDateTimePeriodFunction = @p1, @sDateTimePeriodFunctionCode = @p2, @UserName = @p3", config_vw_datetimeperiodfunctionspost.ixDateTimePeriodFunction, config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunction, config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunctionCode, config_vw_datetimeperiodfunctionspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteDateTimePeriodFunctions @ixDateTimePeriodFunction = @p0, @sDateTimePeriodFunction = @p1, @sDateTimePeriodFunctionCode = @p2, @UserName = @p3", config_vw_datetimeperiodfunctionspost.ixDateTimePeriodFunction, config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunction, config_vw_datetimeperiodfunctionspost.sDateTimePeriodFunctionCode, config_vw_datetimeperiodfunctionspost.UserName);
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
  

