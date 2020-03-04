using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMeasurementUnitsOfRepository
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
        IQueryable<MeasurementUnitsOf> IndexDb();
        bool VerifyMeasurementUnitOfUnique(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf);
        List<string> VerifyMeasurementUnitOfDeleteOK(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf);
        void RegisterCreate(MeasurementUnitsOfPost measurementunitsofPost);
        void RegisterEdit(MeasurementUnitsOfPost measurementunitsofPost);
        void RegisterDelete(MeasurementUnitsOfPost measurementunitsofPost);
        void Rollback();
        void Commit();
    }
}
  

