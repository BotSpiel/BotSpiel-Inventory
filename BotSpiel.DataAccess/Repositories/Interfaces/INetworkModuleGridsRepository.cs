using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface INetworkModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        NetworkModuleGridsPost GetPost(Int64 ixNetworkModuleGrid);        
		NetworkModuleGrids Get(Int64 ixNetworkModuleGrid);
        IQueryable<NetworkModuleGrids> Index();
        IQueryable<NetworkModuleGrids> IndexDb();
		IQueryable<NetworkModuleGridsconfig> Indexconfig();
		IQueryable<NetworkModuleGridsmd> Indexmd();
		IQueryable<NetworkModuleGridstx> Indextx();
		IQueryable<NetworkModuleGridsanalytics> Indexanalytics();
        List<string> VerifyNetworkModuleGridDeleteOK(Int64 ixNetworkModuleGrid, string sNetworkModuleGrid);
        void RegisterCreate(NetworkModuleGridsPost networkmodulegridsPost);
        void RegisterEdit(NetworkModuleGridsPost networkmodulegridsPost);
        void RegisterDelete(NetworkModuleGridsPost networkmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

