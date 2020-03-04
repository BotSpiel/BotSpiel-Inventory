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

    public class SendEmailsDB : DbContext
    {

        public SendEmailsDB(DbContextOptions<SendEmailsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<SendEmails> SendEmails { get; set; }
		public DbSet<SendEmailsPost> SendEmailsPost { get; set; }
		public DbSet<People> People { get; set; }
		public DbSet<PeoplePost> PeoplePost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SendEmails>()
                .ToTable("tx_vw_SendEmails")
                .HasKey(c => new { c.ixSendEmail });
            modelBuilder.Entity<SendEmailsPost>()
                .ToTable("tx_vw_SendEmailsPost")
                .HasKey(c => new { c.ixSendEmail });
            modelBuilder.Entity<People>()
                .ToTable("md_vw_People")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<PeoplePost>()
                .ToTable("md_vw_PeoplePost")
                .HasKey(c => new { c.ixPerson });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is SendEmailsPost)).ToList())
            {
                var tx_vw_sendemailspost = e.Entity as SendEmailsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixPerson = cmd.CreateParameter();
                            ixPerson.ParameterName = "p0";
                            ixPerson.Value = tx_vw_sendemailspost.ixPerson;
                            var sSubject = cmd.CreateParameter();
                            sSubject.ParameterName = "p1";
                            sSubject.Value = tx_vw_sendemailspost.sSubject;
                            var sContent = cmd.CreateParameter();
                            sContent.ParameterName = "p2";
                            sContent.Value = tx_vw_sendemailspost.sContent;
                            var sAttachment = cmd.CreateParameter();
                            sAttachment.ParameterName = "p3";
                            sAttachment.Value = tx_vw_sendemailspost.sAttachment;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = tx_vw_sendemailspost.UserName;

                            var ixSendEmail = cmd.CreateParameter();
                            ixSendEmail.ParameterName = "p5";
                            ixSendEmail.DbType = DbType.Int64;
                            ixSendEmail.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateSendEmails ");
                            sql.Append("@ixPerson = @p0, ");
                            sql.Append("@sSubject = @p1, ");
                            sql.Append("@sContent = @p2, ");
                            if (tx_vw_sendemailspost.sAttachment != null) { sql.Append("@sAttachment = @p3, "); }  
                            if (tx_vw_sendemailspost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixSendEmail = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixPerson);
                            cmd.Parameters.Add(sSubject);
                            cmd.Parameters.Add(sContent);
                            if (tx_vw_sendemailspost.sAttachment != null) { cmd.Parameters.Add(sAttachment); }
                            if (tx_vw_sendemailspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixSendEmail); 
                            cmd.ExecuteNonQuery();
                            tx_vw_sendemailspost.ixSendEmail = (Int64)ixSendEmail.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixSendEmail"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeSendEmails @ixSendEmail = @p0, @ixPerson = @p1, @sSubject = @p2, @sContent = @p3, @sAttachment = @p4, @UserName = @p5", tx_vw_sendemailspost.ixSendEmail, tx_vw_sendemailspost.ixPerson, tx_vw_sendemailspost.sSubject, tx_vw_sendemailspost.sContent, tx_vw_sendemailspost.sAttachment, tx_vw_sendemailspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteSendEmails @ixSendEmail = @p0, @ixPerson = @p1, @sSubject = @p2, @sContent = @p3, @sAttachment = @p4, @UserName = @p5", tx_vw_sendemailspost.ixSendEmail, tx_vw_sendemailspost.ixPerson, tx_vw_sendemailspost.sSubject, tx_vw_sendemailspost.sContent, tx_vw_sendemailspost.sAttachment, tx_vw_sendemailspost.UserName);
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
  

