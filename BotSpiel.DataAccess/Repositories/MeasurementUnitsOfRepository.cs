using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MeasurementUnitsOfRepository : IMeasurementUnitsOfRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MeasurementUnitsOfDB _context;
       private readonly UnitsOfMeasurementDB _contextUnitsOfMeasurement;
  
        public MeasurementUnitsOfRepository(MeasurementUnitsOfDB context, UnitsOfMeasurementDB contextUnitsOfMeasurement)
        {
            _context = context;
           _contextUnitsOfMeasurement = contextUnitsOfMeasurement;
  
        }

        public MeasurementUnitsOfPost GetPost(Int64 ixMeasurementUnitOf) => _context.MeasurementUnitsOfPost.AsNoTracking().Where(x => x.ixMeasurementUnitOf == ixMeasurementUnitOf).First();
         
		public MeasurementUnitsOf Get(Int64 ixMeasurementUnitOf)
        {
            MeasurementUnitsOf measurementunitsof = _context.MeasurementUnitsOf.AsNoTracking().Where(x => x.ixMeasurementUnitOf == ixMeasurementUnitOf).First();
            return measurementunitsof;
        }

        public IQueryable<MeasurementUnitsOf> Index()
        {
            var measurementunitsof = _context.MeasurementUnitsOf.AsNoTracking(); 
            return measurementunitsof;
        }

        public IQueryable<MeasurementUnitsOf> IndexDb()
        {
            var measurementunitsof = _context.MeasurementUnitsOf.AsNoTracking(); 
            return measurementunitsof;
        }
        public bool VerifyMeasurementUnitOfUnique(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf)
        {
            if (_context.MeasurementUnitsOf.AsNoTracking().Where(x => x.sMeasurementUnitOf == sMeasurementUnitOf).Any() && ixMeasurementUnitOf == 0L) return false;
            else if (_context.MeasurementUnitsOf.AsNoTracking().Where(x => x.sMeasurementUnitOf == sMeasurementUnitOf && x.ixMeasurementUnitOf != ixMeasurementUnitOf).Any() && ixMeasurementUnitOf != 0L) return false;
            else return true;
        }

        public List<string> VerifyMeasurementUnitOfDeleteOK(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf)
        {
            List<string> existInEntities = new List<string>();
           if (_contextUnitsOfMeasurement.UnitsOfMeasurement.AsNoTracking().Where(x => x.ixMeasurementUnitOf == ixMeasurementUnitOf).Any()) existInEntities.Add("UnitsOfMeasurement");

            return existInEntities;
        }


        public void RegisterCreate(MeasurementUnitsOfPost measurementunitsofPost)
		{
            _context.MeasurementUnitsOfPost.Add(measurementunitsofPost); 
        }

        public void RegisterEdit(MeasurementUnitsOfPost measurementunitsofPost)
        {
            _context.Entry(measurementunitsofPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MeasurementUnitsOfPost measurementunitsofPost)
        {
            _context.MeasurementUnitsOfPost.Remove(measurementunitsofPost);
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
  

