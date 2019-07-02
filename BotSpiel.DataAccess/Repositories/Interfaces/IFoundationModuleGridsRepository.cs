using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFoundationModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FoundationModuleGridsPost GetPost(Int64 ixFoundationModuleGrid);        
		FoundationModuleGrids Get(Int64 ixFoundationModuleGrid);
        IQueryable<FoundationModuleGrids> Index();
		IQueryable<FoundationModuleGridsconfig> Indexconfig();
		IQueryable<FoundationModuleGridsmd> Indexmd();
		IQueryable<FoundationModuleGridstx> Indextx();
		IQueryable<FoundationModuleGridsanalytics> Indexanalytics();
        List<string> VerifyFoundationModuleGridDeleteOK(Int64 ixFoundationModuleGrid, string sFoundationModuleGrid);
        void RegisterCreate(FoundationModuleGridsPost foundationmodulegridsPost);
        void RegisterEdit(FoundationModuleGridsPost foundationmodulegridsPost);
        void RegisterDelete(FoundationModuleGridsPost foundationmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

