using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IAssemblyModuleGridsRepository
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
		IQueryable<AssemblyModuleGridsconfig> Indexconfig();
		IQueryable<AssemblyModuleGridsmd> Indexmd();
		IQueryable<AssemblyModuleGridstx> Indextx();
		IQueryable<AssemblyModuleGridsanalytics> Indexanalytics();
        List<string> VerifyAssemblyModuleGridDeleteOK(Int64 ixAssemblyModuleGrid, string sAssemblyModuleGrid);
        void RegisterCreate(AssemblyModuleGridsPost assemblymodulegridsPost);
        void RegisterEdit(AssemblyModuleGridsPost assemblymodulegridsPost);
        void RegisterDelete(AssemblyModuleGridsPost assemblymodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

