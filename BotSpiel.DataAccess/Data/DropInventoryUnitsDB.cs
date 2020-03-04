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

    public class DropInventoryUnitsDB : DbContext
    {

        public DropInventoryUnitsDB(DbContextOptions<DropInventoryUnitsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<DropInventoryUnits> DropInventoryUnits { get; set; }
		public DbSet<DropInventoryUnitsPost> DropInventoryUnitsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DropInventoryUnits>()
                .ToTable("tx_vw_DropInventoryUnits")
                .HasKey(c => new { c.ixDropInventoryUnit });
            modelBuilder.Entity<DropInventoryUnitsPost>()
                .ToTable("tx_vw_DropInventoryUnitsPost")
                .HasKey(c => new { c.ixDropInventoryUnit });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is DropInventoryUnitsPost)).ToList())
            {
                var tx_vw_dropinventoryunitspost = e.Entity as DropInventoryUnitsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sDropInventoryUnit = cmd.CreateParameter();
                            sDropInventoryUnit.ParameterName = "p0";
                            sDropInventoryUnit.Value = tx_vw_dropinventoryunitspost.sDropInventoryUnit;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = tx_vw_dropinventoryunitspost.UserName;

                            var ixDropInventoryUnit = cmd.CreateParameter();
                            ixDropInventoryUnit.ParameterName = "p2";
                            ixDropInventoryUnit.DbType = DbType.Int64;
                            ixDropInventoryUnit.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateDropInventoryUnits ");
                            sql.Append("@sDropInventoryUnit = @p0, ");
                            if (tx_vw_dropinventoryunitspost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixDropInventoryUnit = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sDropInventoryUnit);
                            if (tx_vw_dropinventoryunitspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixDropInventoryUnit); 
                            cmd.ExecuteNonQuery();
                            tx_vw_dropinventoryunitspost.ixDropInventoryUnit = (Int64)ixDropInventoryUnit.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixDropInventoryUnit"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeDropInventoryUnits @ixDropInventoryUnit = @p0, @sDropInventoryUnit = @p1, @UserName = @p2", tx_vw_dropinventoryunitspost.ixDropInventoryUnit, tx_vw_dropinventoryunitspost.sDropInventoryUnit, tx_vw_dropinventoryunitspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteDropInventoryUnits @ixDropInventoryUnit = @p0, @sDropInventoryUnit = @p1, @UserName = @p2", tx_vw_dropinventoryunitspost.ixDropInventoryUnit, tx_vw_dropinventoryunitspost.sDropInventoryUnit, tx_vw_dropinventoryunitspost.UserName);
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
  

