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

    public class InventoryStatesDB : DbContext
    {

        public InventoryStatesDB(DbContextOptions<InventoryStatesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryStates> InventoryStates { get; set; }
		public DbSet<InventoryStatesPost> InventoryStatesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryStates>()
                .ToTable("config_vw_InventoryStates")
                .HasKey(c => new { c.ixInventoryState });
            modelBuilder.Entity<InventoryStatesPost>()
                .ToTable("config_vw_InventoryStatesPost")
                .HasKey(c => new { c.ixInventoryState });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InventoryStatesPost)).ToList())
            {
                var config_vw_inventorystatespost = e.Entity as InventoryStatesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInventoryState = cmd.CreateParameter();
                            sInventoryState.ParameterName = "p0";
                            sInventoryState.Value = config_vw_inventorystatespost.sInventoryState;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_inventorystatespost.UserName;

                            var ixInventoryState = cmd.CreateParameter();
                            ixInventoryState.ParameterName = "p2";
                            ixInventoryState.DbType = DbType.Int64;
                            ixInventoryState.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInventoryStates ");
                            sql.Append("@sInventoryState = @p0, ");
                            if (config_vw_inventorystatespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixInventoryState = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInventoryState);
                            if (config_vw_inventorystatespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryState); 
                            cmd.ExecuteNonQuery();
                            config_vw_inventorystatespost.ixInventoryState = (Int64)ixInventoryState.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryState"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInventoryStates @ixInventoryState = @p0, @sInventoryState = @p1, @UserName = @p2", config_vw_inventorystatespost.ixInventoryState, config_vw_inventorystatespost.sInventoryState, config_vw_inventorystatespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInventoryStates @ixInventoryState = @p0, @sInventoryState = @p1, @UserName = @p2", config_vw_inventorystatespost.ixInventoryState, config_vw_inventorystatespost.sInventoryState, config_vw_inventorystatespost.UserName);
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
  

