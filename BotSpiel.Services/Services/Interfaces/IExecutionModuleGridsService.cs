using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IExecutionModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ExecutionModuleGridsPost GetPost(Int64 ixExecutionModuleGrid);        
		ExecutionModuleGrids Get(Int64 ixExecutionModuleGrid);
        IQueryable<ExecutionModuleGrids> Index();
        IQueryable<ExecutionModuleGrids> IndexDb();
		IQueryable<ExecutionModuleGridsconfig> Indexconfig();
		IQueryable<ExecutionModuleGridsmd> Indexmd();
		IQueryable<ExecutionModuleGridstx> Indextx();
		IQueryable<ExecutionModuleGridsanalytics> Indexanalytics();
        List<string> VerifyExecutionModuleGridDeleteOK(Int64 ixExecutionModuleGrid, string sExecutionModuleGrid);

        Task<Int64> Create(ExecutionModuleGridsPost executionmodulegridsPost);
        Task Edit(ExecutionModuleGridsPost executionmodulegridsPost);
        Task Delete(ExecutionModuleGridsPost executionmodulegridsPost);
    }
}
  

