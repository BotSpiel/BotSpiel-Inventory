using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class UnitOfMeasurementConversionsRepository : IUnitOfMeasurementConversionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly UnitOfMeasurementConversionsDB _context;
  
        public UnitOfMeasurementConversionsRepository(UnitOfMeasurementConversionsDB context)
        {
            _context = context;
  
        }

        public UnitOfMeasurementConversionsPost GetPost(Int64 ixUnitOfMeasurementConversion) => _context.UnitOfMeasurementConversionsPost.AsNoTracking().Where(x => x.ixUnitOfMeasurementConversion == ixUnitOfMeasurementConversion).First();
         
		public UnitOfMeasurementConversions Get(Int64 ixUnitOfMeasurementConversion)
        {
            UnitOfMeasurementConversions unitofmeasurementconversions = _context.UnitOfMeasurementConversions.AsNoTracking().Where(x => x.ixUnitOfMeasurementConversion == ixUnitOfMeasurementConversion).First();
            unitofmeasurementconversions.UnitsOfMeasurementFKDiffUnitOfMeasurementFrom = _context.UnitsOfMeasurement.Find(unitofmeasurementconversions.ixUnitOfMeasurementFrom);
            unitofmeasurementconversions.UnitsOfMeasurementFKDiffUnitOfMeasurementTo = _context.UnitsOfMeasurement.Find(unitofmeasurementconversions.ixUnitOfMeasurementTo);

            return unitofmeasurementconversions;
        }

        public IQueryable<UnitOfMeasurementConversions> Index()
        {
            var unitofmeasurementconversions = _context.UnitOfMeasurementConversions.Include(a => a.UnitsOfMeasurementFKDiffUnitOfMeasurementFrom).Include(a => a.UnitsOfMeasurementFKDiffUnitOfMeasurementTo).AsNoTracking(); 
            return unitofmeasurementconversions;
        }
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
        public bool VerifyUnitOfMeasurementConversionUnique(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion)
        {
            if (_context.UnitOfMeasurementConversions.AsNoTracking().Where(x => x.sUnitOfMeasurementConversion == sUnitOfMeasurementConversion).Any() && ixUnitOfMeasurementConversion == 0L) return false;
            else if (_context.UnitOfMeasurementConversions.AsNoTracking().Where(x => x.sUnitOfMeasurementConversion == sUnitOfMeasurementConversion && x.ixUnitOfMeasurementConversion != ixUnitOfMeasurementConversion).Any() && ixUnitOfMeasurementConversion != 0L) return false;
            else return true;
        }

        public List<string> VerifyUnitOfMeasurementConversionDeleteOK(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
		{
            _context.UnitOfMeasurementConversionsPost.Add(unitofmeasurementconversionsPost); 
        }

        public void RegisterEdit(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
        {
            _context.Entry(unitofmeasurementconversionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
        {
            _context.UnitOfMeasurementConversionsPost.Remove(unitofmeasurementconversionsPost);
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
  

