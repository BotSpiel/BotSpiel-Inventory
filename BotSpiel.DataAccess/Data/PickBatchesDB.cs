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

    public class PickBatchesDB : DbContext
    {

        public PickBatchesDB(DbContextOptions<PickBatchesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PickBatches> PickBatches { get; set; }
		public DbSet<PickBatchesPost> PickBatchesPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<PickBatchTypes> PickBatchTypes { get; set; }
		public DbSet<PickBatchTypesPost> PickBatchTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PickBatches>()
                .ToTable("tx_vw_PickBatches")
                .HasKey(c => new { c.ixPickBatch });
            modelBuilder.Entity<PickBatchesPost>()
                .ToTable("tx_vw_PickBatchesPost")
                .HasKey(c => new { c.ixPickBatch });
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<PickBatchTypes>()
                .ToTable("config_vw_PickBatchTypes")
                .HasKey(c => new { c.ixPickBatchType });
            modelBuilder.Entity<PickBatchTypesPost>()
                .ToTable("config_vw_PickBatchTypesPost")
                .HasKey(c => new { c.ixPickBatchType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is PickBatchesPost)).ToList())
            {
                var tx_vw_pickbatchespost = e.Entity as PickBatchesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPickBatchType = cmd.CreateParameter();
                            ixPickBatchType.ParameterName = "p0";
                            ixPickBatchType.Value = tx_vw_pickbatchespost.ixPickBatchType;
                            var bMultiResource = cmd.CreateParameter();
                            bMultiResource.ParameterName = "p1";
                            bMultiResource.Value = tx_vw_pickbatchespost.bMultiResource;
                            var dtStartBy = cmd.CreateParameter();
                            dtStartBy.ParameterName = "p2";
                            dtStartBy.Value = tx_vw_pickbatchespost.dtStartBy;
                            var dtCompleteBy = cmd.CreateParameter();
                            dtCompleteBy.ParameterName = "p3";
                            dtCompleteBy.Value = tx_vw_pickbatchespost.dtCompleteBy;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p4";
                            ixStatus.Value = tx_vw_pickbatchespost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p5";
                            UserName.Value = tx_vw_pickbatchespost.UserName;

                            var ixPickBatch = cmd.CreateParameter();
                            ixPickBatch.ParameterName = "p6";
                            ixPickBatch.DbType = DbType.Int64;
                            ixPickBatch.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePickBatches ");
                            sql.Append("@ixPickBatchType = @p0, ");
                            sql.Append("@bMultiResource = @p1, ");
                            sql.Append("@dtStartBy = @p2, ");
                            sql.Append("@dtCompleteBy = @p3, ");
                            sql.Append("@ixStatus = @p4, ");
                            if (tx_vw_pickbatchespost.UserName != null) { sql.Append("@UserName = @p5, "); }  
                            sql.Append("@ixPickBatch = @p6 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPickBatchType);
                            cmd.Parameters.Add(bMultiResource);
                            cmd.Parameters.Add(dtStartBy);
                            cmd.Parameters.Add(dtCompleteBy);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_pickbatchespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPickBatch); 
                            cmd.ExecuteNonQuery();
                            tx_vw_pickbatchespost.ixPickBatch = (Int64)ixPickBatch.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPickBatch"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePickBatches @ixPickBatch = @p0, @ixPickBatchType = @p1, @bMultiResource = @p2, @dtStartBy = @p3, @dtCompleteBy = @p4, @ixStatus = @p5, @UserName = @p6", tx_vw_pickbatchespost.ixPickBatch, tx_vw_pickbatchespost.ixPickBatchType, tx_vw_pickbatchespost.bMultiResource, tx_vw_pickbatchespost.dtStartBy, tx_vw_pickbatchespost.dtCompleteBy, tx_vw_pickbatchespost.ixStatus, tx_vw_pickbatchespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePickBatches @ixPickBatch = @p0, @ixPickBatchType = @p1, @bMultiResource = @p2, @dtStartBy = @p3, @dtCompleteBy = @p4, @ixStatus = @p5, @UserName = @p6", tx_vw_pickbatchespost.ixPickBatch, tx_vw_pickbatchespost.ixPickBatchType, tx_vw_pickbatchespost.bMultiResource, tx_vw_pickbatchespost.dtStartBy, tx_vw_pickbatchespost.dtCompleteBy, tx_vw_pickbatchespost.ixStatus, tx_vw_pickbatchespost.UserName);
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
  

