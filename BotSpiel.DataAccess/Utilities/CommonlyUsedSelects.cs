using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Utilities
{
    public class CommonlyUsedSelects
    {
        private readonly UnitsOfMeasurementDB _contextUnitsOfMeasurement;

        public CommonlyUsedSelects(UnitsOfMeasurementDB contextUnitsOfMeasurement)
        {
            _contextUnitsOfMeasurement = contextUnitsOfMeasurement;
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementLength()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Length").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementVolume()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Volume (V)").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementArea()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Area").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementDensity()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Density (?)").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementQuantity()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Quantities").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementTime()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Time (t)").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurementWeight()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _contextUnitsOfMeasurement.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Mass").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }

    }
}
