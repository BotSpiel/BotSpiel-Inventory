using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMeasurementSystemsService
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

        Task<Int64> Create(MeasurementSystemsPost measurementsystemsPost);
        Task Edit(MeasurementSystemsPost measurementsystemsPost);
        Task Delete(MeasurementSystemsPost measurementsystemsPost);
    }
}
  

