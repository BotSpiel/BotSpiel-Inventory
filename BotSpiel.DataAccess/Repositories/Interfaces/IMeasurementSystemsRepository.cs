using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMeasurementSystemsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MeasurementSystemsPost GetPost(Int64 ixMeasurementSystem);        
		MeasurementSystems Get(Int64 ixMeasurementSystem);
        IQueryable<MeasurementSystems> Index();
        IQueryable<MeasurementSystems> IndexDb();
        bool VerifyMeasurementSystemUnique(Int64 ixMeasurementSystem, string sMeasurementSystem);
        List<string> VerifyMeasurementSystemDeleteOK(Int64 ixMeasurementSystem, string sMeasurementSystem);
        void RegisterCreate(MeasurementSystemsPost measurementsystemsPost);
        void RegisterEdit(MeasurementSystemsPost measurementsystemsPost);
        void RegisterDelete(MeasurementSystemsPost measurementsystemsPost);
        void Rollback();
        void Commit();
    }
}
  

