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

    public class InboundOrderTypesDB : DbContext
    {

        public InboundOrderTypesDB(DbContextOptions<InboundOrderTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InboundOrderTypes> InboundOrderTypes { get; set; }
		public DbSet<InboundOrderTypesPost> InboundOrderTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InboundOrderTypes>()
                .ToTable("config_vw_InboundOrderTypes")
                .HasKey(c => new { c.ixInboundOrderType });
            modelBuilder.Entity<InboundOrderTypesPost>()
                .ToTable("config_vw_InboundOrderTypesPost")
                .HasKey(c => new { c.ixInboundOrderType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InboundOrderTypesPost)).ToList())
            {
                var config_vw_inboundordertypespost = e.Entity as InboundOrderTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInboundOrderType = cmd.CreateParameter();
                            sInboundOrderType.ParameterName = "p0";
                            sInboundOrderType.Value = config_vw_inboundordertypespost.sInboundOrderType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_inboundordertypespost.UserName;

                            var ixInboundOrderType = cmd.CreateParameter();
                            ixInboundOrderType.ParameterName = "p2";
                            ixInboundOrderType.DbType = DbType.Int64;
                            ixInboundOrderType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInboundOrderTypes ");
                            sql.Append("@sInboundOrderType = @p0, ");
                            if (config_vw_inboundordertypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixInboundOrderType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInboundOrderType);
                            if (config_vw_inboundordertypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInboundOrderType); 
                            cmd.ExecuteNonQuery();
                            config_vw_inboundordertypespost.ixInboundOrderType = (Int64)ixInboundOrderType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInboundOrderType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInboundOrderTypes @ixInboundOrderType = @p0, @sInboundOrderType = @p1, @UserName = @p2", config_vw_inboundordertypespost.ixInboundOrderType, config_vw_inboundordertypespost.sInboundOrderType, config_vw_inboundordertypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInboundOrderTypes @ixInboundOrderType = @p0, @sInboundOrderType = @p1, @UserName = @p2", config_vw_inboundordertypespost.ixInboundOrderType, config_vw_inboundordertypespost.sInboundOrderType, config_vw_inboundordertypespost.UserName);
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
  

