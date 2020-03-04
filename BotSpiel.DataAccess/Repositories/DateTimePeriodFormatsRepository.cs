using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class DateTimePeriodFormatsRepository : IDateTimePeriodFormatsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly DateTimePeriodFormatsDB _context;
  
        public DateTimePeriodFormatsRepository(DateTimePeriodFormatsDB context)
        {
            _context = context;
  
        }

        public DateTimePeriodFormatsPost GetPost(Int64 ixDateTimePeriodFormat) => _context.DateTimePeriodFormatsPost.AsNoTracking().Where(x => x.ixDateTimePeriodFormat == ixDateTimePeriodFormat).First();
         
		public DateTimePeriodFormats Get(Int64 ixDateTimePeriodFormat)
        {
            DateTimePeriodFormats datetimeperiodformats = _context.DateTimePeriodFormats.AsNoTracking().Where(x => x.ixDateTimePeriodFormat == ixDateTimePeriodFormat).First();
            return datetimeperiodformats;
        }

        public IQueryable<DateTimePeriodFormats> Index()
        {
            var datetimeperiodformats = _context.DateTimePeriodFormats.AsNoTracking(); 
            return datetimeperiodformats;
        }

        public IQueryable<DateTimePeriodFormats> IndexDb()
        {
            var datetimeperiodformats = _context.DateTimePeriodFormats.AsNoTracking(); 
            return datetimeperiodformats;
        }
        public bool VerifyDateTimePeriodFormatUnique(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat)
        {
            if (_context.DateTimePeriodFormats.AsNoTracking().Where(x => x.sDateTimePeriodFormat == sDateTimePeriodFormat).Any() && ixDateTimePeriodFormat == 0L) return false;
            else if (_context.DateTimePeriodFormats.AsNoTracking().Where(x => x.sDateTimePeriodFormat == sDateTimePeriodFormat && x.ixDateTimePeriodFormat != ixDateTimePeriodFormat).Any() && ixDateTimePeriodFormat != 0L) return false;
            else return true;
        }

        public List<string> VerifyDateTimePeriodFormatDeleteOK(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(DateTimePeriodFormatsPost datetimeperiodformatsPost)
		{
            _context.DateTimePeriodFormatsPost.Add(datetimeperiodformatsPost); 
        }

        public void RegisterEdit(DateTimePeriodFormatsPost datetimeperiodformatsPost)
        {
            _context.Entry(datetimeperiodformatsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(DateTimePeriodFormatsPost datetimeperiodformatsPost)
        {
            _context.DateTimePeriodFormatsPost.Remove(datetimeperiodformatsPost);
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
  

