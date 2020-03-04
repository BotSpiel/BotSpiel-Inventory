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

    public class HandlingUnitTypesDB : DbContext
    {

        public HandlingUnitTypesDB(DbContextOptions<HandlingUnitTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<HandlingUnitTypes> HandlingUnitTypes { get; set; }
		public DbSet<HandlingUnitTypesPost> HandlingUnitTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HandlingUnitTypes>()
                .ToTable("config_vw_HandlingUnitTypes")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<HandlingUnitTypesPost>()
                .ToTable("config_vw_HandlingUnitTypesPost")
                .HasKey(c => new { c.ixHandlingUnitType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is HandlingUnitTypesPost)).ToList())
            {
                var config_vw_handlingunittypespost = e.Entity as HandlingUnitTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sHandlingUnitType = cmd.CreateParameter();
                            sHandlingUnitType.ParameterName = "p0";
                            sHandlingUnitType.Value = config_vw_handlingunittypespost.sHandlingUnitType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_handlingunittypespost.UserName;

                            var ixHandlingUnitType = cmd.CreateParameter();
                            ixHandlingUnitType.ParameterName = "p2";
                            ixHandlingUnitType.DbType = DbType.Int64;
                            ixHandlingUnitType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateHandlingUnitTypes ");
                            sql.Append("@sHandlingUnitType = @p0, ");
                            if (config_vw_handlingunittypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixHandlingUnitType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sHandlingUnitType);
                            if (config_vw_handlingunittypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixHandlingUnitType); 
                            cmd.ExecuteNonQuery();
                            config_vw_handlingunittypespost.ixHandlingUnitType = (Int64)ixHandlingUnitType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixHandlingUnitType"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeHandlingUnitTypes @ixHandlingUnitType = @p0, @sHandlingUnitType = @p1, @UserName = @p2", config_vw_handlingunittypespost.ixHandlingUnitType, config_vw_handlingunittypespost.sHandlingUnitType, config_vw_handlingunittypespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteHandlingUnitTypes @ixHandlingUnitType = @p0, @sHandlingUnitType = @p1, @UserName = @p2", config_vw_handlingunittypespost.ixHandlingUnitType, config_vw_handlingunittypespost.sHandlingUnitType, config_vw_handlingunittypespost.UserName);
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
  

