using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IAssemblyModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        AssemblyModuleGridsPost GetPost(Int64 ixAssemblyModuleGrid);        
		AssemblyModuleGrids Get(Int64 ixAssemblyModuleGrid);
        IQueryable<AssemblyModuleGrids> Index();
        IQueryable<AssemblyModuleGrids> IndexDb();
		IQueryable<AssemblyModuleGridsconfig> Indexconfig();
		IQueryable<AssemblyModuleGridsmd> Indexmd();
		IQueryable<AssemblyModuleGridstx> Indextx();
		IQueryable<AssemblyModuleGridsanalytics> Indexanalytics();
        List<string> VerifyAssemblyModuleGridDeleteOK(Int64 ixAssemblyModuleGrid, string sAssemblyModuleGrid);

        Task<Int64> Create(AssemblyModuleGridsPost assemblymodulegridsPost);
        Task Edit(AssemblyModuleGridsPost assemblymodulegridsPost);
        Task Delete(AssemblyModuleGridsPost assemblymodulegridsPost);
    }
}
  

