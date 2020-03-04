using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFoundationModuleGridsService
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
        IQueryable<FoundationModuleGrids> IndexDb();
		IQueryable<FoundationModuleGridsconfig> Indexconfig();
		IQueryable<FoundationModuleGridsmd> Indexmd();
		IQueryable<FoundationModuleGridstx> Indextx();
		IQueryable<FoundationModuleGridsanalytics> Indexanalytics();
        List<string> VerifyFoundationModuleGridDeleteOK(Int64 ixFoundationModuleGrid, string sFoundationModuleGrid);

        Task<Int64> Create(FoundationModuleGridsPost foundationmodulegridsPost);
        Task Edit(FoundationModuleGridsPost foundationmodulegridsPost);
        Task Delete(FoundationModuleGridsPost foundationmodulegridsPost);
    }
}
  

