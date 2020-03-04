using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IBotModuleGridsRepository
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
        void RegisterCreate(BotModuleGridsPost botmodulegridsPost);
        void RegisterEdit(BotModuleGridsPost botmodulegridsPost);
        void RegisterDelete(BotModuleGridsPost botmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

