using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IExecutionModuleGridsRepository
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
		IQueryable<ExecutionModuleGridsconfig> Indexconfig();
		IQueryable<ExecutionModuleGridsmd> Indexmd();
		IQueryable<ExecutionModuleGridstx> Indextx();
		IQueryable<ExecutionModuleGridsanalytics> Indexanalytics();
        List<string> VerifyExecutionModuleGridDeleteOK(Int64 ixExecutionModuleGrid, string sExecutionModuleGrid);
        void RegisterCreate(ExecutionModuleGridsPost executionmodulegridsPost);
        void RegisterEdit(ExecutionModuleGridsPost executionmodulegridsPost);
        void RegisterDelete(ExecutionModuleGridsPost executionmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

