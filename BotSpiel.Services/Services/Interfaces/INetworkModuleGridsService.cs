using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface INetworkModuleGridsService
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

        Task<Int64> Create(NetworkModuleGridsPost networkmodulegridsPost);
        Task Edit(NetworkModuleGridsPost networkmodulegridsPost);
        Task Delete(NetworkModuleGridsPost networkmodulegridsPost);
    }
}
  

