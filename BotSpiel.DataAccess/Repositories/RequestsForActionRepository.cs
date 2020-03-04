using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class RequestsForActionRepository : IRequestsForActionRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly RequestsForActionDB _context;
       private readonly RequestForActionSimilesDB _contextRequestForActionSimiles;
  
        public RequestsForActionRepository(RequestsForActionDB context, RequestForActionSimilesDB contextRequestForActionSimiles)
        {
            _context = context;
           _contextRequestForActionSimiles = contextRequestForActionSimiles;
  
        }

        public RequestsForActionPost GetPost(Int64 ixRequestForAction) => _context.RequestsForActionPost.AsNoTracking().Where(x => x.ixRequestForAction == ixRequestForAction).First();
         
		public RequestsForAction Get(Int64 ixRequestForAction)
        {
            RequestsForAction requestsforaction = _context.RequestsForAction.AsNoTracking().Where(x => x.ixRequestForAction == ixRequestForAction).First();
            requestsforaction.Languages = _context.Languages.Find(requestsforaction.ixLanguage);
            requestsforaction.LanguageStyles = _context.LanguageStyles.Find(requestsforaction.ixLanguageStyle);

            return requestsforaction;
        }

        public IQueryable<RequestsForAction> Index()
        {
            var requestsforaction = _context.RequestsForAction.Include(a => a.Languages).Include(a => a.LanguageStyles).AsNoTracking(); 
            return requestsforaction;
        }

        public IQueryable<RequestsForAction> IndexDb()
        {
            var requestsforaction = _context.RequestsForAction.Include(a => a.Languages).Include(a => a.LanguageStyles).AsNoTracking(); 
            return requestsforaction;
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
        public List<string> VerifyRequestForActionDeleteOK(Int64 ixRequestForAction, string sRequestForAction)
        {
            List<string> existInEntities = new List<string>();
           if (_contextRequestForActionSimiles.RequestForActionSimiles.AsNoTracking().Where(x => x.ixRequestForAction == ixRequestForAction).Any()) existInEntities.Add("RequestForActionSimiles");

            return existInEntities;
        }


        public void RegisterCreate(RequestsForActionPost requestsforactionPost)
		{
            _context.RequestsForActionPost.Add(requestsforactionPost); 
        }

        public void RegisterEdit(RequestsForActionPost requestsforactionPost)
        {
            _context.Entry(requestsforactionPost).State = EntityState.Modified;
        }

        public void RegisterDelete(RequestsForActionPost requestsforactionPost)
        {
            _context.RequestsForActionPost.Remove(requestsforactionPost);
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
  

