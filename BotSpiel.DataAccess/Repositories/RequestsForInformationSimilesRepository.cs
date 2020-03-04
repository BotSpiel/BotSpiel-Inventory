using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class RequestsForInformationSimilesRepository : IRequestsForInformationSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly RequestsForInformationSimilesDB _context;
  
        public RequestsForInformationSimilesRepository(RequestsForInformationSimilesDB context)
        {
            _context = context;
  
        }

        public RequestsForInformationSimilesPost GetPost(Int64 ixRequestsForInformationSimile) => _context.RequestsForInformationSimilesPost.AsNoTracking().Where(x => x.ixRequestsForInformationSimile == ixRequestsForInformationSimile).First();
         
		public RequestsForInformationSimiles Get(Int64 ixRequestsForInformationSimile)
        {
            RequestsForInformationSimiles requestsforinformationsimiles = _context.RequestsForInformationSimiles.AsNoTracking().Where(x => x.ixRequestsForInformationSimile == ixRequestsForInformationSimile).First();
            requestsforinformationsimiles.RequestsForInformation = _context.RequestsForInformation.Find(requestsforinformationsimiles.ixRequestForInformation);

            return requestsforinformationsimiles;
        }

        public IQueryable<RequestsForInformationSimiles> Index()
        {
            var requestsforinformationsimiles = _context.RequestsForInformationSimiles.Include(a => a.RequestsForInformation).AsNoTracking(); 
            return requestsforinformationsimiles;
        }

        public IQueryable<RequestsForInformationSimiles> IndexDb()
        {
            var requestsforinformationsimiles = _context.RequestsForInformationSimiles.Include(a => a.RequestsForInformation).AsNoTracking(); 
            return requestsforinformationsimiles;
        }
       public IQueryable<RequestsForInformation> selectRequestsForInformation()
        {
            List<RequestsForInformation> requestsforinformation = new List<RequestsForInformation>();
            _context.RequestsForInformation.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).Include(a => a.Topics).AsNoTracking()
                .ToList()
                .ForEach(x => requestsforinformation.Add(x));
            return requestsforinformation.AsQueryable();
        }
       public IQueryable<RequestsForInformation> RequestsForInformationDb()
        {
            List<RequestsForInformation> requestsforinformation = new List<RequestsForInformation>();
            _context.RequestsForInformation.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).Include(a => a.Topics).AsNoTracking()
                .ToList()
                .ForEach(x => requestsforinformation.Add(x));
            return requestsforinformation.AsQueryable();
        }
        public List<string> VerifyRequestsForInformationSimileDeleteOK(Int64 ixRequestsForInformationSimile, string sRequestsForInformationSimile)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
		{
            _context.RequestsForInformationSimilesPost.Add(requestsforinformationsimilesPost); 
        }

        public void RegisterEdit(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
        {
            _context.Entry(requestsforinformationsimilesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
        {
            _context.RequestsForInformationSimilesPost.Remove(requestsforinformationsimilesPost);
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
  

