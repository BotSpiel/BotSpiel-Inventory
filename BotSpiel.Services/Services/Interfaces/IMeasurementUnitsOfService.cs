using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMeasurementUnitsOfService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MeasurementUnitsOfPost GetPost(Int64 ixMeasurementUnitOf);        
		MeasurementUnitsOf Get(Int64 ixMeasurementUnitOf);
        IQueryable<MeasurementUnitsOf> Index();
        bool VerifyMeasurementUnitOfUnique(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf);
        List<string> VerifyMeasurementUnitOfDeleteOK(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf);

        Task<Int64> Create(MeasurementUnitsOfPost measurementunitsofPost);
        Task Edit(MeasurementUnitsOfPost measurementunitsofPost);
        Task Delete(MeasurementUnitsOfPost measurementunitsofPost);
    }
}
  

