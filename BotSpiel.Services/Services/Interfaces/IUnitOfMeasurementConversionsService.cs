using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IUnitOfMeasurementConversionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        UnitOfMeasurementConversionsPost GetPost(Int64 ixUnitOfMeasurementConversion);        
		UnitOfMeasurementConversions Get(Int64 ixUnitOfMeasurementConversion);
        IQueryable<UnitOfMeasurementConversions> Index();
        IQueryable<UnitOfMeasurementConversions> IndexDb();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
       IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        bool VerifyUnitOfMeasurementConversionUnique(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion);
        List<string> VerifyUnitOfMeasurementConversionDeleteOK(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion);

        Task<Int64> Create(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
        Task Edit(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
        Task Delete(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
    }
}
  

