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

    public class InventoryUnitTransactionContextsDB : DbContext
    {

        public InventoryUnitTransactionContextsDB(DbContextOptions<InventoryUnitTransactionContextsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryUnitTransactionContexts> InventoryUnitTransactionContexts { get; set; }
		public DbSet<InventoryUnitTransactionContextsPost> InventoryUnitTransactionContextsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryUnitTransactionContexts>()
                .ToTable("config_vw_InventoryUnitTransactionContexts")
                .HasKey(c => new { c.ixInventoryUnitTransactionContext });
            modelBuilder.Entity<InventoryUnitTransactionContextsPost>()
                .ToTable("config_vw_InventoryUnitTransactionContextsPost")
                .HasKey(c => new { c.ixInventoryUnitTransactionContext });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InventoryUnitTransactionContextsPost)).ToList())
            {
                var config_vw_inventoryunittransactioncontextspost = e.Entity as InventoryUnitTransactionContextsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInventoryUnitTransactionContext = cmd.CreateParameter();
                            sInventoryUnitTransactionContext.ParameterName = "p0";
                            sInventoryUnitTransactionContext.Value = config_vw_inventoryunittransactioncontextspost.sInventoryUnitTransactionContext;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_inventoryunittransactioncontextspost.UserName;

                            var ixInventoryUnitTransactionContext = cmd.CreateParameter();
                            ixInventoryUnitTransactionContext.ParameterName = "p2";
                            ixInventoryUnitTransactionContext.DbType = DbType.Int64;
                            ixInventoryUnitTransactionContext.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInventoryUnitTransactionContexts ");
                            sql.Append("@sInventoryUnitTransactionContext = @p0, ");
                            if (config_vw_inventoryunittransactioncontextspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixInventoryUnitTransactionContext = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInventoryUnitTransactionContext);
                            if (config_vw_inventoryunittransactioncontextspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryUnitTransactionContext); 
                            cmd.ExecuteNonQuery();
                            config_vw_inventoryunittransactioncontextspost.ixInventoryUnitTransactionContext = (Int64)ixInventoryUnitTransactionContext.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryUnitTransactionContext"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInventoryUnitTransactionContexts @ixInventoryUnitTransactionContext = @p0, @sInventoryUnitTransactionContext = @p1, @UserName = @p2", config_vw_inventoryunittransactioncontextspost.ixInventoryUnitTransactionContext, config_vw_inventoryunittransactioncontextspost.sInventoryUnitTransactionContext, config_vw_inventoryunittransactioncontextspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInventoryUnitTransactionContexts @ixInventoryUnitTransactionContext = @p0, @sInventoryUnitTransactionContext = @p1, @UserName = @p2", config_vw_inventoryunittransactioncontextspost.ixInventoryUnitTransactionContext, config_vw_inventoryunittransactioncontextspost.sInventoryUnitTransactionContext, config_vw_inventoryunittransactioncontextspost.UserName);
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
  

