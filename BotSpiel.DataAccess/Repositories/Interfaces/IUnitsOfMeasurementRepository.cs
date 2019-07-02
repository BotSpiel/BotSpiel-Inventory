using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IUnitsOfMeasurementRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        UnitsOfMeasurementPost GetPost(Int64 ixUnitOfMeasurement);        
		UnitsOfMeasurement Get(Int64 ixUnitOfMeasurement);
        IQueryable<UnitsOfMeasurement> Index();
       IQueryable<MeasurementSystems> selectMeasurementSystems();
        IQueryable<MeasurementUnitsOf> selectMeasurementUnitsOf();
        bool VerifyUnitOfMeasurementUnique(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement);
        List<string> VerifyUnitOfMeasurementDeleteOK(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement);
        void RegisterCreate(UnitsOfMeasurementPost unitsofmeasurementPost);
        void RegisterEdit(UnitsOfMeasurementPost unitsofmeasurementPost);
        void RegisterDelete(UnitsOfMeasurementPost unitsofmeasurementPost);
        void Rollback();
        void Commit();
    }
}
  

