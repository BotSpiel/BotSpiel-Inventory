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

    public class InvitationsOffersDB : DbContext
    {

        public InvitationsOffersDB(DbContextOptions<InvitationsOffersDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InvitationsOffers> InvitationsOffers { get; set; }
		public DbSet<InvitationsOffersPost> InvitationsOffersPost { get; set; }
		public DbSet<Languages> Languages { get; set; }
		public DbSet<LanguagesPost> LanguagesPost { get; set; }
		public DbSet<LanguageStyles> LanguageStyles { get; set; }
		public DbSet<LanguageStylesPost> LanguageStylesPost { get; set; }
		public DbSet<ResponseTypes> ResponseTypes { get; set; }
		public DbSet<ResponseTypesPost> ResponseTypesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvitationsOffers>()
                .ToTable("config_vw_InvitationsOffers")
                .HasKey(c => new { c.ixInvitationOffer });
            modelBuilder.Entity<InvitationsOffersPost>()
                .ToTable("config_vw_InvitationsOffersPost")
                .HasKey(c => new { c.ixInvitationOffer });
            modelBuilder.Entity<Languages>()
                .ToTable("md_vw_Languages")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguagesPost>()
                .ToTable("md_vw_LanguagesPost")
                .HasKey(c => new { c.ixLanguage });
            modelBuilder.Entity<LanguageStyles>()
                .ToTable("config_vw_LanguageStyles")
                .HasKey(c => new { c.ixLanguageStyle });
            modelBuilder.Entity<LanguageStylesPost>()
                .ToTable("config_vw_LanguageStylesPost")
                .HasKey(c => new { c.ixLanguageStyle });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InvitationsOffersPost)).ToList())
            {
                var config_vw_invitationsofferspost = e.Entity as InvitationsOffersPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInvitationOffer = cmd.CreateParameter();
                            sInvitationOffer.ParameterName = "p0";
                            sInvitationOffer.Value = config_vw_invitationsofferspost.sInvitationOffer;
                            var ixLanguage = cmd.CreateParameter();
                            ixLanguage.ParameterName = "p1";
                            ixLanguage.Value = config_vw_invitationsofferspost.ixLanguage;
                            var ixLanguageStyle = cmd.CreateParameter();
                            ixLanguageStyle.ParameterName = "p2";
                            ixLanguageStyle.Value = config_vw_invitationsofferspost.ixLanguageStyle;
                            var sInvitationOffered = cmd.CreateParameter();
                            sInvitationOffered.ParameterName = "p3";
                            sInvitationOffered.Value = config_vw_invitationsofferspost.sInvitationOffered;
                            var sAcceptDecline = cmd.CreateParameter();
                            sAcceptDecline.ParameterName = "p4";
                            sAcceptDecline.Value = config_vw_invitationsofferspost.sAcceptDecline;
                            var ixResponseType = cmd.CreateParameter();
                            ixResponseType.ParameterName = "p5";
                            ixResponseType.Value = config_vw_invitationsofferspost.ixResponseType;
                            var bActive = cmd.CreateParameter();
                            bActive.ParameterName = "p6";
                            bActive.Value = config_vw_invitationsofferspost.bActive;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = config_vw_invitationsofferspost.UserName;

                            var ixInvitationOffer = cmd.CreateParameter();
                            ixInvitationOffer.ParameterName = "p8";
                            ixInvitationOffer.DbType = DbType.Int64;
                            ixInvitationOffer.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.config_sp_CreateInvitationsOffers ");
                            sql.Append("@sInvitationOffer = @p0, ");
                            sql.Append("@ixLanguage = @p1, ");
                            sql.Append("@ixLanguageStyle = @p2, ");
                            sql.Append("@sInvitationOffered = @p3, ");
                            sql.Append("@sAcceptDecline = @p4, ");
                            sql.Append("@ixResponseType = @p5, ");
                            sql.Append("@bActive = @p6, ");
                            if (config_vw_invitationsofferspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixInvitationOffer = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInvitationOffer);
                            cmd.Parameters.Add(ixLanguage);
                            cmd.Parameters.Add(ixLanguageStyle);
                            cmd.Parameters.Add(sInvitationOffered);
                            cmd.Parameters.Add(sAcceptDecline);
                            cmd.Parameters.Add(ixResponseType);
                            cmd.Parameters.Add(bActive);
                            if (config_vw_invitationsofferspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInvitationOffer); 
                            cmd.ExecuteNonQuery();
                            config_vw_invitationsofferspost.ixInvitationOffer = (Int64)ixInvitationOffer.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInvitationOffer"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_ChangeInvitationsOffers @ixInvitationOffer = @p0, @sInvitationOffer = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sInvitationOffered = @p4, @sAcceptDecline = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_invitationsofferspost.ixInvitationOffer, config_vw_invitationsofferspost.sInvitationOffer, config_vw_invitationsofferspost.ixLanguage, config_vw_invitationsofferspost.ixLanguageStyle, config_vw_invitationsofferspost.sInvitationOffered, config_vw_invitationsofferspost.sAcceptDecline, config_vw_invitationsofferspost.ixResponseType, config_vw_invitationsofferspost.bActive, config_vw_invitationsofferspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.config_sp_DeleteInvitationsOffers @ixInvitationOffer = @p0, @sInvitationOffer = @p1, @ixLanguage = @p2, @ixLanguageStyle = @p3, @sInvitationOffered = @p4, @sAcceptDecline = @p5, @ixResponseType = @p6, @bActive = @p7, @UserName = @p8", config_vw_invitationsofferspost.ixInvitationOffer, config_vw_invitationsofferspost.sInvitationOffer, config_vw_invitationsofferspost.ixLanguage, config_vw_invitationsofferspost.ixLanguageStyle, config_vw_invitationsofferspost.sInvitationOffered, config_vw_invitationsofferspost.sAcceptDecline, config_vw_invitationsofferspost.ixResponseType, config_vw_invitationsofferspost.bActive, config_vw_invitationsofferspost.UserName);
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
  

