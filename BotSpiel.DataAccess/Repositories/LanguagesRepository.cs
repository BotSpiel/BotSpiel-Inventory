using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class LanguagesRepository : ILanguagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly LanguagesDB _context;
       private readonly AccusationsDB _contextAccusations;
        private readonly ComplementsDB _contextComplements;
        private readonly FarewellsDB _contextFarewells;
        private readonly GreetingsDB _contextGreetings;
        private readonly InvitationsOffersDB _contextInvitationsOffers;
        private readonly PeopleDB _contextPeople;
        private readonly RequestsForActionDB _contextRequestsForAction;
        private readonly RequestsForInformationDB _contextRequestsForInformation;
  
        public LanguagesRepository(LanguagesDB context, AccusationsDB contextAccusations, ComplementsDB contextComplements, FarewellsDB contextFarewells, GreetingsDB contextGreetings, InvitationsOffersDB contextInvitationsOffers, PeopleDB contextPeople, RequestsForActionDB contextRequestsForAction, RequestsForInformationDB contextRequestsForInformation)
        {
            _context = context;
           _contextAccusations = contextAccusations;
            _contextComplements = contextComplements;
            _contextFarewells = contextFarewells;
            _contextGreetings = contextGreetings;
            _contextInvitationsOffers = contextInvitationsOffers;
            _contextPeople = contextPeople;
            _contextRequestsForAction = contextRequestsForAction;
            _contextRequestsForInformation = contextRequestsForInformation;
  
        }

        public LanguagesPost GetPost(Int64 ixLanguage) => _context.LanguagesPost.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).First();
         
		public Languages Get(Int64 ixLanguage)
        {
            Languages languages = _context.Languages.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).First();
            return languages;
        }

        public IQueryable<Languages> Index()
        {
            var languages = _context.Languages.AsNoTracking(); 
            return languages;
        }

        public IQueryable<Languages> IndexDb()
        {
            var languages = _context.Languages.AsNoTracking(); 
            return languages;
        }
        public bool VerifyLanguageUnique(Int64 ixLanguage, string sLanguage)
        {
            if (_context.Languages.AsNoTracking().Where(x => x.sLanguage == sLanguage).Any() && ixLanguage == 0L) return false;
            else if (_context.Languages.AsNoTracking().Where(x => x.sLanguage == sLanguage && x.ixLanguage != ixLanguage).Any() && ixLanguage != 0L) return false;
            else return true;
        }

        public List<string> VerifyLanguageDeleteOK(Int64 ixLanguage, string sLanguage)
        {
            List<string> existInEntities = new List<string>();
           if (_contextAccusations.Accusations.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("Accusations");
            if (_contextComplements.Complements.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("Complements");
            if (_contextFarewells.Farewells.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("Farewells");
            if (_contextGreetings.Greetings.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("Greetings");
            if (_contextInvitationsOffers.InvitationsOffers.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("InvitationsOffers");
            if (_contextPeople.People.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("People");
            if (_contextRequestsForAction.RequestsForAction.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("RequestsForAction");
            if (_contextRequestsForInformation.RequestsForInformation.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("RequestsForInformation");

            return existInEntities;
        }


        public void RegisterCreate(LanguagesPost languagesPost)
		{
            _context.LanguagesPost.Add(languagesPost); 
        }

        public void RegisterEdit(LanguagesPost languagesPost)
        {
            _context.Entry(languagesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(LanguagesPost languagesPost)
        {
            _context.LanguagesPost.Remove(languagesPost);
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
  

