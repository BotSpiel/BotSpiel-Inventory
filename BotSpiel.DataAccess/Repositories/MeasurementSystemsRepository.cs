using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MeasurementSystemsRepository : IMeasurementSystemsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MeasurementSystemsDB _context;
       private readonly UnitsOfMeasurementDB _contextUnitsOfMeasurement;
  
        public MeasurementSystemsRepository(MeasurementSystemsDB context, UnitsOfMeasurementDB contextUnitsOfMeasurement)
        {
            _context = context;
           _contextUnitsOfMeasurement = contextUnitsOfMeasurement;
  
        }

        public MeasurementSystemsPost GetPost(Int64 ixMeasurementSystem) => _context.MeasurementSystemsPost.AsNoTracking().Where(x => x.ixMeasurementSystem == ixMeasurementSystem).First();
         
		public MeasurementSystems Get(Int64 ixMeasurementSystem)
        {
            MeasurementSystems measurementsystems = _context.MeasurementSystems.AsNoTracking().Where(x => x.ixMeasurementSystem == ixMeasurementSystem).First();
            return measurementsystems;
        }

        public IQueryable<MeasurementSystems> Index()
        {
            var measurementsystems = _context.MeasurementSystems.AsNoTracking(); 
            return measurementsystems;
        }
        public bool VerifyMeasurementSystemUnique(Int64 ixMeasurementSystem, string sMeasurementSystem)
        {
            if (_context.MeasurementSystems.AsNoTracking().Where(x => x.sMeasurementSystem == sMeasurementSystem).Any() && ixMeasurementSystem == 0L) return false;
            else if (_context.MeasurementSystems.AsNoTracking().Where(x => x.sMeasurementSystem == sMeasurementSystem && x.ixMeasurementSystem != ixMeasurementSystem).Any() && ixMeasurementSystem != 0L) return false;
            else return true;
        }

        public List<string> VerifyMeasurementSystemDeleteOK(Int64 ixMeasurementSystem, string sMeasurementSystem)
        {
            List<string> existInEntities = new List<string>();
           if (_contextUnitsOfMeasurement.UnitsOfMeasurement.AsNoTracking().Where(x => x.ixMeasurementSystem == ixMeasurementSystem).Any()) existInEntities.Add("UnitsOfMeasurement");

            return existInEntities;
        }


        public void RegisterCreate(MeasurementSystemsPost measurementsystemsPost)
		{
            _context.MeasurementSystemsPost.Add(measurementsystemsPost); 
        }

        public void RegisterEdit(MeasurementSystemsPost measurementsystemsPost)
        {
            _context.Entry(measurementsystemsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MeasurementSystemsPost measurementsystemsPost)
        {
            _context.MeasurementSystemsPost.Remove(measurementsystemsPost);
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
  

