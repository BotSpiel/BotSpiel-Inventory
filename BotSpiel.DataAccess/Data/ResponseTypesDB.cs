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

    public class ResponseTypesDB : DbContext
    {

        public ResponseTypesDB(DbContextOptions<ResponseTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResponseTypes>()
                .ToTable("config_vw_ResponseTypes")
                .HasKey(c => new { c.ixResponseType });
            modelBuilder.Entity<ResponseTypesPost>()
                .ToTable("config_vw_ResponseTypesPost")
                .HasKey(c => new { c.ixResponseType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ResponseTypesPost)).ToList())
            {
                var config_vw_responsetypespost = e.Entity as ResponseTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sResponseType = cmd.CreateParameter();
                            sResponseType.ParameterName = "p0";
                            sResponseType.Value = config_vw_responsetypespost.sResponseType;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p1";
                            UserName.Value = config_vw_responsetypespost.UserName;

                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p2";
                            ixResponseType.DbType = DbType.Int64;
                            ixResponseType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateResponseTypes ");
                            sql.Append("@sResponseType = @p0, ");
                            if (config_vw_responsetypespost.UserName != null) { sql.Append("@UserName = @p1, "); }  
                            sql.Append("@ixResponseType = @p2 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sResponseType);
                            if (config_vw_responsetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixResponseType); 
                            cmd.ExecuteNonQuery();
                            config_vw_responsetypespost.ixResponseType = (Int64)ixResponseType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixResponseType"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeResponseTypes @ixResponseType = @p0, @sResponseType = @p1, @UserName = @p2", config_vw_responsetypespost.ixResponseType, config_vw_responsetypespost.sResponseType, config_vw_responsetypespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteResponseTypes @ixResponseType = @p0, @sResponseType = @p1, @UserName = @p2", config_vw_responsetypespost.ixResponseType, config_vw_responsetypespost.sResponseType, config_vw_responsetypespost.UserName);
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
  

