using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CurrencyTypesRepository : ICurrencyTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CurrencyTypesDB _context;
  
        public CurrencyTypesRepository(CurrencyTypesDB context)
        {
            _context = context;
  
        }

        public CurrencyTypesPost GetPost(Int64 ixCurrencyType) => _context.CurrencyTypesPost.AsNoTracking().Where(x => x.ixCurrencyType == ixCurrencyType).First();
         
		public CurrencyTypes Get(Int64 ixCurrencyType)
        {
            CurrencyTypes currencytypes = _context.CurrencyTypes.AsNoTracking().Where(x => x.ixCurrencyType == ixCurrencyType).First();
            return currencytypes;
        }

        public IQueryable<CurrencyTypes> Index()
        {
            var currencytypes = _context.CurrencyTypes.AsNoTracking(); 
            return currencytypes;
        }

        public IQueryable<CurrencyTypes> IndexDb()
        {
            var currencytypes = _context.CurrencyTypes.AsNoTracking(); 
            return currencytypes;
        }
        public bool VerifyCurrencyTypeUnique(Int64 ixCurrencyType, string sCurrencyType)
        {
            if (_context.CurrencyTypes.AsNoTracking().Where(x => x.sCurrencyType == sCurrencyType).Any() && ixCurrencyType == 0L) return false;
            else if (_context.CurrencyTypes.AsNoTracking().Where(x => x.sCurrencyType == sCurrencyType && x.ixCurrencyType != ixCurrencyType).Any() && ixCurrencyType != 0L) return false;
            else return true;
        }

        public List<string> VerifyCurrencyTypeDeleteOK(Int64 ixCurrencyType, string sCurrencyType)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(CurrencyTypesPost currencytypesPost)
		{
            _context.CurrencyTypesPost.Add(currencytypesPost); 
        }

        public void RegisterEdit(CurrencyTypesPost currencytypesPost)
        {
            _context.Entry(currencytypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CurrencyTypesPost currencytypesPost)
        {
            _context.CurrencyTypesPost.Remove(currencytypesPost);
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
  

