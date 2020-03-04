using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MonetaryAmountTypesRepository : IMonetaryAmountTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MonetaryAmountTypesDB _context;
  
        public MonetaryAmountTypesRepository(MonetaryAmountTypesDB context)
        {
            _context = context;
  
        }

        public MonetaryAmountTypesPost GetPost(Int64 ixMonetaryAmountType) => _context.MonetaryAmountTypesPost.AsNoTracking().Where(x => x.ixMonetaryAmountType == ixMonetaryAmountType).First();
         
		public MonetaryAmountTypes Get(Int64 ixMonetaryAmountType)
        {
            MonetaryAmountTypes monetaryamounttypes = _context.MonetaryAmountTypes.AsNoTracking().Where(x => x.ixMonetaryAmountType == ixMonetaryAmountType).First();
            return monetaryamounttypes;
        }

        public IQueryable<MonetaryAmountTypes> Index()
        {
            var monetaryamounttypes = _context.MonetaryAmountTypes.AsNoTracking(); 
            return monetaryamounttypes;
        }

        public IQueryable<MonetaryAmountTypes> IndexDb()
        {
            var monetaryamounttypes = _context.MonetaryAmountTypes.AsNoTracking(); 
            return monetaryamounttypes;
        }
        public bool VerifyMonetaryAmountTypeUnique(Int64 ixMonetaryAmountType, string sMonetaryAmountType)
        {
            if (_context.MonetaryAmountTypes.AsNoTracking().Where(x => x.sMonetaryAmountType == sMonetaryAmountType).Any() && ixMonetaryAmountType == 0L) return false;
            else if (_context.MonetaryAmountTypes.AsNoTracking().Where(x => x.sMonetaryAmountType == sMonetaryAmountType && x.ixMonetaryAmountType != ixMonetaryAmountType).Any() && ixMonetaryAmountType != 0L) return false;
            else return true;
        }

        public List<string> VerifyMonetaryAmountTypeDeleteOK(Int64 ixMonetaryAmountType, string sMonetaryAmountType)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(MonetaryAmountTypesPost monetaryamounttypesPost)
		{
            _context.MonetaryAmountTypesPost.Add(monetaryamounttypesPost); 
        }

        public void RegisterEdit(MonetaryAmountTypesPost monetaryamounttypesPost)
        {
            _context.Entry(monetaryamounttypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MonetaryAmountTypesPost monetaryamounttypesPost)
        {
            _context.MonetaryAmountTypesPost.Remove(monetaryamounttypesPost);
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
  

