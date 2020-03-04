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

    public class BaySequenceTypesDB : DbContext
    {

        public BaySequenceTypesDB(DbContextOptions<BaySequenceTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<BaySequenceTypes> BaySequenceTypes { get; set; }
		public DbSet<BaySequenceTypesPost> BaySequenceTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaySequenceTypes>()
                .ToTable("config_vw_BaySequenceTypes")
                .HasKey(c => new { c.ixBaySequenceType });
            modelBuilder.Entity<BaySequenceTypesPost>()
                .ToTable("config_vw_BaySequenceTypesPost")
                .HasKey(c => new { c.ixBaySequenceType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is BaySequenceTypesPost)).ToList())
            {
                var config_vw_baysequencetypespost = e.Entity as BaySequenceTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sBaySequenceType = cmd.CreateParameter();
                            sBaySequenceType.ParameterName = "p0";
                            sBaySequenceType.Value = config_vw_baysequencetypespost.sBaySequenceType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_baysequencetypespost.UserName;

                            var ixBaySequenceType = cmd.CreateParameter();
                            ixBaySequenceType.ParameterName = "p2";
                            ixBaySequenceType.DbType = DbType.Int64;
                            ixBaySequenceType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateBaySequenceTypes ");
                            sql.Append("@sBaySequenceType = @p0, ");
                            if (config_vw_baysequencetypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixBaySequenceType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sBaySequenceType);
                            if (config_vw_baysequencetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixBaySequenceType); 
                            cmd.ExecuteNonQuery();
                            config_vw_baysequencetypespost.ixBaySequenceType = (Int64)ixBaySequenceType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixBaySequenceType"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeBaySequenceTypes @ixBaySequenceType = @p0, @sBaySequenceType = @p1, @UserName = @p2", config_vw_baysequencetypespost.ixBaySequenceType, config_vw_baysequencetypespost.sBaySequenceType, config_vw_baysequencetypespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteBaySequenceTypes @ixBaySequenceType = @p0, @sBaySequenceType = @p1, @UserName = @p2", config_vw_baysequencetypespost.ixBaySequenceType, config_vw_baysequencetypespost.sBaySequenceType, config_vw_baysequencetypespost.UserName);
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
  

