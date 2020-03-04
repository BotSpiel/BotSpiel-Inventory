using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IBotModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        BotModuleGridsPost GetPost(Int64 ixBotModuleGrid);        
		BotModuleGrids Get(Int64 ixBotModuleGrid);
        IQueryable<BotModuleGrids> Index();
        IQueryable<BotModuleGrids> IndexDb();
		IQueryable<BotModuleGridsconfig> Indexconfig();
		IQueryable<BotModuleGridsmd> Indexmd();
		IQueryable<BotModuleGridstx> Indextx();
		IQueryable<BotModuleGridsanalytics> Indexanalytics();
        List<string> VerifyBotModuleGridDeleteOK(Int64 ixBotModuleGrid, string sBotModuleGrid);

        Task<Int64> Create(BotModuleGridsPost botmodulegridsPost);
        Task Edit(BotModuleGridsPost botmodulegridsPost);
        Task Delete(BotModuleGridsPost botmodulegridsPost);
    }
}
  

