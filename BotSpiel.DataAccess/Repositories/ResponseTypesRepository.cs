using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ResponseTypesRepository : IResponseTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ResponseTypesDB _context;
       private readonly AccusationsDB _contextAccusations;
        private readonly ComplementsDB _contextComplements;
        private readonly FarewellsDB _contextFarewells;
        private readonly GreetingsDB _contextGreetings;
        private readonly InvitationsOffersDB _contextInvitationsOffers;
        private readonly QuestionsDB _contextQuestions;
        private readonly RequestsForInformationDB _contextRequestsForInformation;
  
        public ResponseTypesRepository(ResponseTypesDB context, AccusationsDB contextAccusations, ComplementsDB contextComplements, FarewellsDB contextFarewells, GreetingsDB contextGreetings, InvitationsOffersDB contextInvitationsOffers, QuestionsDB contextQuestions, RequestsForInformationDB contextRequestsForInformation)
        {
            _context = context;
           _contextAccusations = contextAccusations;
            _contextComplements = contextComplements;
            _contextFarewells = contextFarewells;
            _contextGreetings = contextGreetings;
            _contextInvitationsOffers = contextInvitationsOffers;
            _contextQuestions = contextQuestions;
            _contextRequestsForInformation = contextRequestsForInformation;
  
        }

        public ResponseTypesPost GetPost(Int64 ixResponseType) => _context.ResponseTypesPost.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).First();
         
		public ResponseTypes Get(Int64 ixResponseType)
        {
            ResponseTypes responsetypes = _context.ResponseTypes.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).First();
            return responsetypes;
        }

        public IQueryable<ResponseTypes> Index()
        {
            var responsetypes = _context.ResponseTypes.AsNoTracking(); 
            return responsetypes;
        }

        public IQueryable<ResponseTypes> IndexDb()
        {
            var responsetypes = _context.ResponseTypes.AsNoTracking(); 
            return responsetypes;
        }
        public List<string> VerifyResponseTypeDeleteOK(Int64 ixResponseType, string sResponseType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextAccusations.Accusations.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("Accusations");
            if (_contextComplements.Complements.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("Complements");
            if (_contextFarewells.Farewells.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("Farewells");
            if (_contextGreetings.Greetings.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("Greetings");
            if (_contextInvitationsOffers.InvitationsOffers.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("InvitationsOffers");
            if (_contextQuestions.Questions.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("Questions");
            if (_contextRequestsForInformation.RequestsForInformation.AsNoTracking().Where(x => x.ixResponseType == ixResponseType).Any()) existInEntities.Add("RequestsForInformation");

            return existInEntities;
        }


        public void RegisterCreate(ResponseTypesPost responsetypesPost)
		{
            _context.ResponseTypesPost.Add(responsetypesPost); 
        }

        public void RegisterEdit(ResponseTypesPost responsetypesPost)
        {
            _context.Entry(responsetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ResponseTypesPost responsetypesPost)
        {
            _context.ResponseTypesPost.Remove(responsetypesPost);
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
  

