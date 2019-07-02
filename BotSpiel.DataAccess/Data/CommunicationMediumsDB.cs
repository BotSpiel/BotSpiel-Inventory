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

    public class CommunicationMediumsDB : DbContext
    {

        public CommunicationMediumsDB(DbContextOptions<CommunicationMediumsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<CommunicationMediums> CommunicationMediums { get; set; }
		public DbSet<CommunicationMediumsPost> CommunicationMediumsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommunicationMediums>()
                .ToTable("config_vw_CommunicationMediums")
                .HasKey(c => new { c.ixCommunicationMedium });
            modelBuilder.Entity<CommunicationMediumsPost>()
                .ToTable("config_vw_CommunicationMediumsPost")
                .HasKey(c => new { c.ixCommunicationMedium });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CommunicationMediumsPost)).ToList())
            {
                var config_vw_communicationmediumspost = e.Entity as CommunicationMediumsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCommunicationMedium = cmd.CreateParameter();
                            sCommunicationMedium.ParameterName = "p0";
                            sCommunicationMedium.Value = config_vw_communicationmediumspost.sCommunicationMedium;
                            var sCommunicationMediumCode = cmd.CreateParameter();
                            sCommunicationMediumCode.ParameterName = "p1";
                            sCommunicationMediumCode.Value = config_vw_communicationmediumspost.sCommunicationMediumCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_communicationmediumspost.UserName;

                            var ixCommunicationMedium = cmd.CreateParameter();
                            ixCommunicationMedium.ParameterName = "p3";
                            ixCommunicationMedium.DbType = DbType.Int64;
                            ixCommunicationMedium.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateCommunicationMediums ");
                            sql.Append("@sCommunicationMedium = @p0, ");
                            sql.Append("@sCommunicationMediumCode = @p1, ");
                            if (config_vw_communicationmediumspost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixCommunicationMedium = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCommunicationMedium);
                            cmd.Parameters.Add(sCommunicationMediumCode);
                            if (config_vw_communicationmediumspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCommunicationMedium); 
                            cmd.ExecuteNonQuery();
                            config_vw_communicationmediumspost.ixCommunicationMedium = (Int64)ixCommunicationMedium.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCommunicationMedium"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeCommunicationMediums @ixCommunicationMedium = @p0, @sCommunicationMedium = @p1, @sCommunicationMediumCode = @p2, @UserName = @p3", config_vw_communicationmediumspost.ixCommunicationMedium, config_vw_communicationmediumspost.sCommunicationMedium, config_vw_communicationmediumspost.sCommunicationMediumCode, config_vw_communicationmediumspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteCommunicationMediums @ixCommunicationMedium = @p0, @sCommunicationMedium = @p1, @sCommunicationMediumCode = @p2, @UserName = @p3", config_vw_communicationmediumspost.ixCommunicationMedium, config_vw_communicationmediumspost.sCommunicationMedium, config_vw_communicationmediumspost.sCommunicationMediumCode, config_vw_communicationmediumspost.UserName);
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
  

