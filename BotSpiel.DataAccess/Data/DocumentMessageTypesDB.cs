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

    public class DocumentMessageTypesDB : DbContext
    {

        public DocumentMessageTypesDB(DbContextOptions<DocumentMessageTypesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<DocumentMessageTypes> DocumentMessageTypes { get; set; }
		public DbSet<DocumentMessageTypesPost> DocumentMessageTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentMessageTypes>()
                .ToTable("config_vw_DocumentMessageTypes")
                .HasKey(c => new { c.ixDocumentMessageType });
            modelBuilder.Entity<DocumentMessageTypesPost>()
                .ToTable("config_vw_DocumentMessageTypesPost")
                .HasKey(c => new { c.ixDocumentMessageType });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is DocumentMessageTypesPost)).ToList())
            {
                var config_vw_documentmessagetypespost = e.Entity as DocumentMessageTypesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sDocumentMessageType = cmd.CreateParameter();
                            sDocumentMessageType.ParameterName = "p0";
                            sDocumentMessageType.Value = config_vw_documentmessagetypespost.sDocumentMessageType;
                            var sDocumentMessageTypeCode = cmd.CreateParameter();
                            sDocumentMessageTypeCode.ParameterName = "p1";
                            sDocumentMessageTypeCode.Value = config_vw_documentmessagetypespost.sDocumentMessageTypeCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p2";
                            UserName.Value = config_vw_documentmessagetypespost.UserName;

                            var ixDocumentMessageType = cmd.CreateParameter();
                            ixDocumentMessageType.ParameterName = "p3";
                            ixDocumentMessageType.DbType = DbType.Int64;
                            ixDocumentMessageType.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateDocumentMessageTypes ");
                            sql.Append("@sDocumentMessageType = @p0, ");
                            sql.Append("@sDocumentMessageTypeCode = @p1, ");
                            if (config_vw_documentmessagetypespost.UserName != null) { sql.Append("@UserName = @p2, "); }  
                            sql.Append("@ixDocumentMessageType = @p3 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sDocumentMessageType);
                            cmd.Parameters.Add(sDocumentMessageTypeCode);
                            if (config_vw_documentmessagetypespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixDocumentMessageType); 
                            cmd.ExecuteNonQuery();
                            config_vw_documentmessagetypespost.ixDocumentMessageType = (Int64)ixDocumentMessageType.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixDocumentMessageType"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeDocumentMessageTypes @ixDocumentMessageType = @p0, @sDocumentMessageType = @p1, @sDocumentMessageTypeCode = @p2, @UserName = @p3", config_vw_documentmessagetypespost.ixDocumentMessageType, config_vw_documentmessagetypespost.sDocumentMessageType, config_vw_documentmessagetypespost.sDocumentMessageTypeCode, config_vw_documentmessagetypespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteDocumentMessageTypes @ixDocumentMessageType = @p0, @sDocumentMessageType = @p1, @sDocumentMessageTypeCode = @p2, @UserName = @p3", config_vw_documentmessagetypespost.ixDocumentMessageType, config_vw_documentmessagetypespost.sDocumentMessageType, config_vw_documentmessagetypespost.sDocumentMessageTypeCode, config_vw_documentmessagetypespost.UserName);
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
  

