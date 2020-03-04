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

    public class CarrierTypesDB : DbContext
    {

        public CarrierTypesDB(DbContextOptions<CarrierTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<CarrierTypes> CarrierTypes { get; set; }
		public DbSet<CarrierTypesPost> CarrierTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrierTypes>()
                .ToTable("config_vw_CarrierTypes")
                .HasKey(c => new { c.ixCarrierType });
            modelBuilder.Entity<CarrierTypesPost>()
                .ToTable("config_vw_CarrierTypesPost")
                .HasKey(c => new { c.ixCarrierType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is CarrierTypesPost)).ToList())
            {
                var config_vw_carriertypespost = e.Entity as CarrierTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCarrierType = cmd.CreateParameter();
                            sCarrierType.ParameterName = "p0";
                            sCarrierType.Value = config_vw_carriertypespost.sCarrierType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_carriertypespost.UserName;

                            var ixCarrierType = cmd.CreateParameter();
                            ixCarrierType.ParameterName = "p2";
                            ixCarrierType.DbType = DbType.Int64;
                            ixCarrierType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateCarrierTypes ");
                            sql.Append("@sCarrierType = @p0, ");
                            if (config_vw_carriertypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixCarrierType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCarrierType);
                            if (config_vw_carriertypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCarrierType); 
                            cmd.ExecuteNonQuery();
                            config_vw_carriertypespost.ixCarrierType = (Int64)ixCarrierType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCarrierType"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeCarrierTypes @ixCarrierType = @p0, @sCarrierType = @p1, @UserName = @p2", config_vw_carriertypespost.ixCarrierType, config_vw_carriertypespost.sCarrierType, config_vw_carriertypespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteCarrierTypes @ixCarrierType = @p0, @sCarrierType = @p1, @UserName = @p2", config_vw_carriertypespost.ixCarrierType, config_vw_carriertypespost.sCarrierType, config_vw_carriertypespost.UserName);
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
  

