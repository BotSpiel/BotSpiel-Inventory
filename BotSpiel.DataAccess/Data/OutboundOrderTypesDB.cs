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

    public class OutboundOrderTypesDB : DbContext
    {

        public OutboundOrderTypesDB(DbContextOptions<OutboundOrderTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<OutboundOrderTypes> OutboundOrderTypes { get; set; }
		public DbSet<OutboundOrderTypesPost> OutboundOrderTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboundOrderTypes>()
                .ToTable("config_vw_OutboundOrderTypes")
                .HasKey(c => new { c.ixOutboundOrderType });
            modelBuilder.Entity<OutboundOrderTypesPost>()
                .ToTable("config_vw_OutboundOrderTypesPost")
                .HasKey(c => new { c.ixOutboundOrderType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is OutboundOrderTypesPost)).ToList())
            {
                var config_vw_outboundordertypespost = e.Entity as OutboundOrderTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sOutboundOrderType = cmd.CreateParameter();
                            sOutboundOrderType.ParameterName = "p0";
                            sOutboundOrderType.Value = config_vw_outboundordertypespost.sOutboundOrderType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_outboundordertypespost.UserName;

                            var ixOutboundOrderType = cmd.CreateParameter();
                            ixOutboundOrderType.ParameterName = "p2";
                            ixOutboundOrderType.DbType = DbType.Int64;
                            ixOutboundOrderType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateOutboundOrderTypes ");
                            sql.Append("@sOutboundOrderType = @p0, ");
                            if (config_vw_outboundordertypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixOutboundOrderType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sOutboundOrderType);
                            if (config_vw_outboundordertypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixOutboundOrderType); 
                            cmd.ExecuteNonQuery();
                            config_vw_outboundordertypespost.ixOutboundOrderType = (Int64)ixOutboundOrderType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixOutboundOrderType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeOutboundOrderTypes @ixOutboundOrderType = @p0, @sOutboundOrderType = @p1, @UserName = @p2", config_vw_outboundordertypespost.ixOutboundOrderType, config_vw_outboundordertypespost.sOutboundOrderType, config_vw_outboundordertypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteOutboundOrderTypes @ixOutboundOrderType = @p0, @sOutboundOrderType = @p1, @UserName = @p2", config_vw_outboundordertypespost.ixOutboundOrderType, config_vw_outboundordertypespost.sOutboundOrderType, config_vw_outboundordertypespost.UserName);
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
  

