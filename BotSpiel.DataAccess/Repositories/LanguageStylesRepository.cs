using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class LanguageStylesRepository : ILanguageStylesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly LanguageStylesDB _context;
       private readonly AccusationsDB _contextAccusations;
        private readonly ComplementsDB _contextComplements;
        private readonly FarewellsDB _contextFarewells;
        private readonly GreetingsDB _contextGreetings;
        private readonly InvitationsOffersDB _contextInvitationsOffers;
        private readonly QuestionsDB _contextQuestions;
        private readonly RequestsForActionDB _contextRequestsForAction;
        private readonly RequestsForInformationDB _contextRequestsForInformation;
  
        public LanguageStylesRepository(LanguageStylesDB context, AccusationsDB contextAccusations, ComplementsDB contextComplements, FarewellsDB contextFarewells, GreetingsDB contextGreetings, InvitationsOffersDB contextInvitationsOffers, QuestionsDB contextQuestions, RequestsForActionDB contextRequestsForAction, RequestsForInformationDB contextRequestsForInformation)
        {
            _context = context;
           _contextAccusations = contextAccusations;
            _contextComplements = contextComplements;
            _contextFarewells = contextFarewells;
            _contextGreetings = contextGreetings;
            _contextInvitationsOffers = contextInvitationsOffers;
            _contextQuestions = contextQuestions;
            _contextRequestsForAction = contextRequestsForAction;
            _contextRequestsForInformation = contextRequestsForInformation;
  
        }

        public LanguageStylesPost GetPost(Int64 ixLanguageStyle) => _context.LanguageStylesPost.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).First();
         
		public LanguageStyles Get(Int64 ixLanguageStyle)
        {
            LanguageStyles languagestyles = _context.LanguageStyles.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).First();
            return languagestyles;
        }

        public IQueryable<LanguageStyles> Index()
        {
            var languagestyles = _context.LanguageStyles.AsNoTracking(); 
            return languagestyles;
        }

        public IQueryable<LanguageStyles> IndexDb()
        {
            var languagestyles = _context.LanguageStyles.AsNoTracking(); 
            return languagestyles;
        }
        public List<string> VerifyLanguageStyleDeleteOK(Int64 ixLanguageStyle, string sLanguageStyle)
        {
            List<string> existInEntities = new List<string>();
           if (_contextAccusations.Accusations.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("Accusations");
            if (_contextComplements.Complements.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("Complements");
            if (_contextFarewells.Farewells.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("Farewells");
            if (_contextGreetings.Greetings.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("Greetings");
            if (_contextInvitationsOffers.InvitationsOffers.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("InvitationsOffers");
            if (_contextQuestions.Questions.AsNoTracking().Where(x => x.ixLanguage == ixLanguageStyle).Any()) existInEntities.Add("Questions");
            if (_contextQuestions.Questions.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("Questions");
            if (_contextRequestsForAction.RequestsForAction.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("RequestsForAction");
            if (_contextRequestsForInformation.RequestsForInformation.AsNoTracking().Where(x => x.ixLanguageStyle == ixLanguageStyle).Any()) existInEntities.Add("RequestsForInformation");

            return existInEntities;
        }


        public void RegisterCreate(LanguageStylesPost languagestylesPost)
		{
            _context.LanguageStylesPost.Add(languagestylesPost); 
        }

        public void RegisterEdit(LanguageStylesPost languagestylesPost)
        {
            _context.Entry(languagestylesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(LanguageStylesPost languagestylesPost)
        {
            _context.LanguageStylesPost.Remove(languagestylesPost);
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
  

