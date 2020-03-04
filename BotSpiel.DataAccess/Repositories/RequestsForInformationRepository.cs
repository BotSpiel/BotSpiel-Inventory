using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class RequestsForInformationRepository : IRequestsForInformationRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly RequestsForInformationDB _context;
       private readonly RequestsForInformationSimilesDB _contextRequestsForInformationSimiles;
  
        public RequestsForInformationRepository(RequestsForInformationDB context, RequestsForInformationSimilesDB contextRequestsForInformationSimiles)
        {
            _context = context;
           _contextRequestsForInformationSimiles = contextRequestsForInformationSimiles;
  
        }

        public RequestsForInformationPost GetPost(Int64 ixRequestForInformation) => _context.RequestsForInformationPost.AsNoTracking().Where(x => x.ixRequestForInformation == ixRequestForInformation).First();
         
		public RequestsForInformation Get(Int64 ixRequestForInformation)
        {
            RequestsForInformation requestsforinformation = _context.RequestsForInformation.AsNoTracking().Where(x => x.ixRequestForInformation == ixRequestForInformation).First();
            requestsforinformation.Languages = _context.Languages.Find(requestsforinformation.ixLanguage);
            requestsforinformation.LanguageStyles = _context.LanguageStyles.Find(requestsforinformation.ixLanguageStyle);
            requestsforinformation.ResponseTypes = _context.ResponseTypes.Find(requestsforinformation.ixResponseType);
            requestsforinformation.Topics = _context.Topics.Find(requestsforinformation.ixTopic);

            return requestsforinformation;
        }

        public IQueryable<RequestsForInformation> Index()
        {
            var requestsforinformation = _context.RequestsForInformation.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.Topics).Include(a => a.ResponseTypes).AsNoTracking(); 
            return requestsforinformation;
        }

        public IQueryable<RequestsForInformation> IndexDb()
        {
            var requestsforinformation = _context.RequestsForInformation.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.Topics).Include(a => a.ResponseTypes).AsNoTracking(); 
            return requestsforinformation;
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
        public IQueryable<Topics> selectTopics()
        {
            List<Topics> topics = new List<Topics>();
            _context.Topics.AsNoTracking()
                .ToList()
                .ForEach(x => topics.Add(x));
            return topics.AsQueryable();
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
        public IQueryable<Topics> TopicsDb()
        {
            List<Topics> topics = new List<Topics>();
            _context.Topics.AsNoTracking()
                .ToList()
                .ForEach(x => topics.Add(x));
            return topics.AsQueryable();
        }
        public List<string> VerifyRequestForInformationDeleteOK(Int64 ixRequestForInformation, string sRequestForInformation)
        {
            List<string> existInEntities = new List<string>();
           if (_contextRequestsForInformationSimiles.RequestsForInformationSimiles.AsNoTracking().Where(x => x.ixRequestForInformation == ixRequestForInformation).Any()) existInEntities.Add("RequestsForInformationSimiles");

            return existInEntities;
        }


        public void RegisterCreate(RequestsForInformationPost requestsforinformationPost)
		{
            _context.RequestsForInformationPost.Add(requestsforinformationPost); 
        }

        public void RegisterEdit(RequestsForInformationPost requestsforinformationPost)
        {
            _context.Entry(requestsforinformationPost).State = EntityState.Modified;
        }

        public void RegisterDelete(RequestsForInformationPost requestsforinformationPost)
        {
            _context.RequestsForInformationPost.Remove(requestsforinformationPost);
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
  

