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

    public class DocumentsDB : DbContext
    {

        public DocumentsDB(DbContextOptions<DocumentsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Documents> Documents { get; set; }
		public DbSet<DocumentsPost> DocumentsPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<DocumentMessageTypes> DocumentMessageTypes { get; set; }
		public DbSet<DocumentMessageTypesPost> DocumentMessageTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>()
                .ToTable("tx_vw_Documents")
                .HasKey(c => new { c.ixDocument });
            modelBuilder.Entity<DocumentsPost>()
                .ToTable("tx_vw_DocumentsPost")
                .HasKey(c => new { c.ixDocument });
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is DocumentsPost)).ToList())
            {
                var tx_vw_documentspost = e.Entity as DocumentsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sDocument = cmd.CreateParameter();
                            sDocument.ParameterName = "p0";
                            sDocument.Value = tx_vw_documentspost.sDocument;
                            var ixDocumentMessageType = cmd.CreateParameter();
                            ixDocumentMessageType.ParameterName = "p1";
                            ixDocumentMessageType.Value = tx_vw_documentspost.ixDocumentMessageType;
                            var sVersion = cmd.CreateParameter();
                            sVersion.ParameterName = "p2";
                            sVersion.Value = tx_vw_documentspost.sVersion;
                            var sRevision = cmd.CreateParameter();
                            sRevision.ParameterName = "p3";
                            sRevision.Value = tx_vw_documentspost.sRevision;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p4";
                            ixStatus.Value = tx_vw_documentspost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p5";
                            UserName.Value = tx_vw_documentspost.UserName;

                            var ixDocument = cmd.CreateParameter();
                            ixDocument.ParameterName = "p6";
                            ixDocument.DbType = DbType.Int64;
                            ixDocument.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateDocuments ");
                            sql.Append("@sDocument = @p0, ");
                            sql.Append("@ixDocumentMessageType = @p1, ");
                            if (tx_vw_documentspost.sVersion != null) { sql.Append("@sVersion = @p2, "); }  
                            if (tx_vw_documentspost.sRevision != null) { sql.Append("@sRevision = @p3, "); }  
                            sql.Append("@ixStatus = @p4, ");
                            if (tx_vw_documentspost.UserName != null) { sql.Append("@UserName = @p5, "); }  
                            sql.Append("@ixDocument = @p6 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sDocument);
                            cmd.Parameters.Add(ixDocumentMessageType);
                            if (tx_vw_documentspost.sVersion != null) { cmd.Parameters.Add(sVersion); }
                            if (tx_vw_documentspost.sRevision != null) { cmd.Parameters.Add(sRevision); }
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_documentspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixDocument); 
                            cmd.ExecuteNonQuery();
                            tx_vw_documentspost.ixDocument = (Int64)ixDocument.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixDocument"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeDocuments @ixDocument = @p0, @sDocument = @p1, @ixDocumentMessageType = @p2, @sVersion = @p3, @sRevision = @p4, @ixStatus = @p5, @UserName = @p6", tx_vw_documentspost.ixDocument, tx_vw_documentspost.sDocument, tx_vw_documentspost.ixDocumentMessageType, tx_vw_documentspost.sVersion, tx_vw_documentspost.sRevision, tx_vw_documentspost.ixStatus, tx_vw_documentspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteDocuments @ixDocument = @p0, @sDocument = @p1, @ixDocumentMessageType = @p2, @sVersion = @p3, @sRevision = @p4, @ixStatus = @p5, @UserName = @p6", tx_vw_documentspost.ixDocument, tx_vw_documentspost.sDocument, tx_vw_documentspost.ixDocumentMessageType, tx_vw_documentspost.sVersion, tx_vw_documentspost.sRevision, tx_vw_documentspost.ixStatus, tx_vw_documentspost.UserName);
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
  

