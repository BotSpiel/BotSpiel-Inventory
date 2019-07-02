using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IUnitOfMeasurementConversionsRepository
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
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        bool VerifyUnitOfMeasurementConversionUnique(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion);
        List<string> VerifyUnitOfMeasurementConversionDeleteOK(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion);
        void RegisterCreate(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
        void RegisterEdit(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
        void RegisterDelete(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost);
        void Rollback();
        void Commit();
    }
}
  

