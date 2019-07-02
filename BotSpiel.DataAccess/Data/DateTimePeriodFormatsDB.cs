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

    public class DateTimePeriodFormatsDB : DbContext
    {

        public DateTimePeriodFormatsDB(DbContextOptions<DateTimePeriodFormatsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<DateTimePeriodFormats> DateTimePeriodFormats { get; set; }
		public DbSet<DateTimePeriodFormatsPost> DateTimePeriodFormatsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateTimePeriodFormats>()
                .ToTable("config_vw_DateTimePeriodFormats")
                .HasKey(c => new { c.ixDateTimePeriodFormat });
            modelBuilder.Entity<DateTimePeriodFormatsPost>()
                .ToTable("config_vw_DateTimePeriodFormatsPost")
                .HasKey(c => new { c.ixDateTimePeriodFormat });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is DateTimePeriodFormatsPost)).ToList())
            {
                var config_vw_datetimeperiodformatspost = e.Entity as DateTimePeriodFormatsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sDateTimePeriodFormat = cmd.CreateParameter();
                            sDateTimePeriodFormat.ParameterName = "p0";
                            sDateTimePeriodFormat.Value = config_vw_datetimeperiodformatspost.sDateTimePeriodFormat;
                            var sDateTimePeriodFormatCode = cmd.CreateParameter();
                            sDateTimePeriodFormatCode.ParameterName = "p1";
                            sDateTimePeriodFormatCode.Value = config_vw_datetimeperiodformatspost.sDateTimePeriodFormatCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_datetimeperiodformatspost.UserName;

                            var ixDateTimePeriodFormat = cmd.CreateParameter();
                            ixDateTimePeriodFormat.ParameterName = "p3";
                            ixDateTimePeriodFormat.DbType = DbType.Int64;
                            ixDateTimePeriodFormat.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateDateTimePeriodFormats ");
                            sql.Append("@sDateTimePeriodFormat = @p0, ");
                            sql.Append("@sDateTimePeriodFormatCode = @p1, ");
                            if (config_vw_datetimeperiodformatspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixDateTimePeriodFormat = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sDateTimePeriodFormat);
                            cmd.Parameters.Add(sDateTimePeriodFormatCode);
                            if (config_vw_datetimeperiodformatspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixDateTimePeriodFormat); 
                            cmd.ExecuteNonQuery();
                            config_vw_datetimeperiodformatspost.ixDateTimePeriodFormat = (Int64)ixDateTimePeriodFormat.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixDateTimePeriodFormat"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeDateTimePeriodFormats @ixDateTimePeriodFormat = @p0, @sDateTimePeriodFormat = @p1, @sDateTimePeriodFormatCode = @p2, @UserName = @p3", config_vw_datetimeperiodformatspost.ixDateTimePeriodFormat, config_vw_datetimeperiodformatspost.sDateTimePeriodFormat, config_vw_datetimeperiodformatspost.sDateTimePeriodFormatCode, config_vw_datetimeperiodformatspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteDateTimePeriodFormats @ixDateTimePeriodFormat = @p0, @sDateTimePeriodFormat = @p1, @sDateTimePeriodFormatCode = @p2, @UserName = @p3", config_vw_datetimeperiodformatspost.ixDateTimePeriodFormat, config_vw_datetimeperiodformatspost.sDateTimePeriodFormat, config_vw_datetimeperiodformatspost.sDateTimePeriodFormatCode, config_vw_datetimeperiodformatspost.UserName);
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
  

