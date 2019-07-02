using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class DateTimePeriodFunctionsRepository : IDateTimePeriodFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly DateTimePeriodFunctionsDB _context;
  
        public DateTimePeriodFunctionsRepository(DateTimePeriodFunctionsDB context)
        {
            _context = context;
  
        }

        public DateTimePeriodFunctionsPost GetPost(Int64 ixDateTimePeriodFunction) => _context.DateTimePeriodFunctionsPost.AsNoTracking().Where(x => x.ixDateTimePeriodFunction == ixDateTimePeriodFunction).First();
         
		public DateTimePeriodFunctions Get(Int64 ixDateTimePeriodFunction)
        {
            DateTimePeriodFunctions datetimeperiodfunctions = _context.DateTimePeriodFunctions.AsNoTracking().Where(x => x.ixDateTimePeriodFunction == ixDateTimePeriodFunction).First();
            return datetimeperiodfunctions;
        }

        public IQueryable<DateTimePeriodFunctions> Index()
        {
            var datetimeperiodfunctions = _context.DateTimePeriodFunctions.AsNoTracking(); 
            return datetimeperiodfunctions;
        }
        public bool VerifyDateTimePeriodFunctionUnique(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction)
        {
            if (_context.DateTimePeriodFunctions.AsNoTracking().Where(x => x.sDateTimePeriodFunction == sDateTimePeriodFunction).Any() && ixDateTimePeriodFunction == 0L) return false;
            else if (_context.DateTimePeriodFunctions.AsNoTracking().Where(x => x.sDateTimePeriodFunction == sDateTimePeriodFunction && x.ixDateTimePeriodFunction != ixDateTimePeriodFunction).Any() && ixDateTimePeriodFunction != 0L) return false;
            else return true;
        }

        public List<string> VerifyDateTimePeriodFunctionDeleteOK(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
		{
            _context.DateTimePeriodFunctionsPost.Add(datetimeperiodfunctionsPost); 
        }

        public void RegisterEdit(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
        {
            _context.Entry(datetimeperiodfunctionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
        {
            _context.DateTimePeriodFunctionsPost.Remove(datetimeperiodfunctionsPost);
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
  

