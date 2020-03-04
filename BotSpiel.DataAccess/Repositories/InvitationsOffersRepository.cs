using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InvitationsOffersRepository : IInvitationsOffersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InvitationsOffersDB _context;
  
        public InvitationsOffersRepository(InvitationsOffersDB context)
        {
            _context = context;
  
        }

        public InvitationsOffersPost GetPost(Int64 ixInvitationOffer) => _context.InvitationsOffersPost.AsNoTracking().Where(x => x.ixInvitationOffer == ixInvitationOffer).First();
         
		public InvitationsOffers Get(Int64 ixInvitationOffer)
        {
            InvitationsOffers invitationsoffers = _context.InvitationsOffers.AsNoTracking().Where(x => x.ixInvitationOffer == ixInvitationOffer).First();
            invitationsoffers.Languages = _context.Languages.Find(invitationsoffers.ixLanguage);
            invitationsoffers.LanguageStyles = _context.LanguageStyles.Find(invitationsoffers.ixLanguageStyle);
            invitationsoffers.ResponseTypes = _context.ResponseTypes.Find(invitationsoffers.ixResponseType);

            return invitationsoffers;
        }

        public IQueryable<InvitationsOffers> Index()
        {
            var invitationsoffers = _context.InvitationsOffers.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return invitationsoffers;
        }

        public IQueryable<InvitationsOffers> IndexDb()
        {
            var invitationsoffers = _context.InvitationsOffers.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return invitationsoffers;
        }
       public IQueryable<Languages> selectLanguages()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> selectLanguageStyles()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> selectResponseTypes()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
       public IQueryable<Languages> LanguagesDb()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> LanguageStylesDb()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> ResponseTypesDb()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
        public List<string> VerifyInvitationOfferDeleteOK(Int64 ixInvitationOffer, string sInvitationOffer)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InvitationsOffersPost invitationsoffersPost)
		{
            _context.InvitationsOffersPost.Add(invitationsoffersPost); 
        }

        public void RegisterEdit(InvitationsOffersPost invitationsoffersPost)
        {
            _context.Entry(invitationsoffersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InvitationsOffersPost invitationsoffersPost)
        {
            _context.InvitationsOffersPost.Remove(invitationsoffersPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

