using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IUnitsOfMeasurementService
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
        IQueryable<UnitsOfMeasurement> IndexDb();
       IQueryable<MeasurementSystems> selectMeasurementSystems();
        IQueryable<MeasurementUnitsOf> selectMeasurementUnitsOf();
       IQueryable<MeasurementSystems> MeasurementSystemsDb();
        IQueryable<MeasurementUnitsOf> MeasurementUnitsOfDb();
        bool VerifyUnitOfMeasurementUnique(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement);
        List<string> VerifyUnitOfMeasurementDeleteOK(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement);

        Task<Int64> Create(UnitsOfMeasurementPost unitsofmeasurementPost);
        Task Edit(UnitsOfMeasurementPost unitsofmeasurementPost);
        Task Delete(UnitsOfMeasurementPost unitsofmeasurementPost);
    }
}
  

