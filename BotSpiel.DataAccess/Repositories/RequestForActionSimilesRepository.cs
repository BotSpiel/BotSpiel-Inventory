using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class RequestForActionSimilesRepository : IRequestForActionSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly RequestForActionSimilesDB _context;
  
        public RequestForActionSimilesRepository(RequestForActionSimilesDB context)
        {
            _context = context;
  
        }

        public RequestForActionSimilesPost GetPost(Int64 ixRequestForActionSimile) => _context.RequestForActionSimilesPost.AsNoTracking().Where(x => x.ixRequestForActionSimile == ixRequestForActionSimile).First();
         
		public RequestForActionSimiles Get(Int64 ixRequestForActionSimile)
        {
            RequestForActionSimiles requestforactionsimiles = _context.RequestForActionSimiles.AsNoTracking().Where(x => x.ixRequestForActionSimile == ixRequestForActionSimile).First();
            requestforactionsimiles.RequestsForAction = _context.RequestsForAction.Find(requestforactionsimiles.ixRequestForAction);

            return requestforactionsimiles;
        }

        public IQueryable<RequestForActionSimiles> Index()
        {
            var requestforactionsimiles = _context.RequestForActionSimiles.Include(a => a.RequestsForAction).AsNoTracking(); 
            return requestforactionsimiles;
        }

        public IQueryable<RequestForActionSimiles> IndexDb()
        {
            var requestforactionsimiles = _context.RequestForActionSimiles.Include(a => a.RequestsForAction).AsNoTracking(); 
            return requestforactionsimiles;
        }
       public IQueryable<RequestsForAction> selectRequestsForAction()
        {
            List<RequestsForAction> requestsforaction = new List<RequestsForAction>();
            _context.RequestsForAction.Include(a => a.Languages).Include(a => a.LanguageStyles).AsNoTracking()
                .ToList()
                .ForEach(x => requestsforaction.Add(x));
            return requestsforaction.AsQueryable();
        }
       public IQueryable<RequestsForAction> RequestsForActionDb()
        {
            List<RequestsForAction> requestsforaction = new List<RequestsForAction>();
            _context.RequestsForAction.Include(a => a.Languages).Include(a => a.LanguageStyles).AsNoTracking()
                .ToList()
                .ForEach(x => requestsforaction.Add(x));
            return requestsforaction.AsQueryable();
        }
        public List<string> VerifyRequestForActionSimileDeleteOK(Int64 ixRequestForActionSimile, string sRequestForActionSimile)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(RequestForActionSimilesPost requestforactionsimilesPost)
		{
            _context.RequestForActionSimilesPost.Add(requestforactionsimilesPost); 
        }

        public void RegisterEdit(RequestForActionSimilesPost requestforactionsimilesPost)
        {
            _context.Entry(requestforactionsimilesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(RequestForActionSimilesPost requestforactionsimilesPost)
        {
            _context.RequestForActionSimilesPost.Remove(requestforactionsimilesPost);
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
  

