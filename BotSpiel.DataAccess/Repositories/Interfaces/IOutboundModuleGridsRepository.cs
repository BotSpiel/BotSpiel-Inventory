using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundModuleGridsPost GetPost(Int64 ixOutboundModuleGrid);        
		OutboundModuleGrids Get(Int64 ixOutboundModuleGrid);
        IQueryable<OutboundModuleGrids> Index();
		IQueryable<OutboundModuleGridsconfig> Indexconfig();
		IQueryable<OutboundModuleGridsmd> Indexmd();
		IQueryable<OutboundModuleGridstx> Indextx();
		IQueryable<OutboundModuleGridsanalytics> Indexanalytics();
        List<string> VerifyOutboundModuleGridDeleteOK(Int64 ixOutboundModuleGrid, string sOutboundModuleGrid);
        void RegisterCreate(OutboundModuleGridsPost outboundmodulegridsPost);
        void RegisterEdit(OutboundModuleGridsPost outboundmodulegridsPost);
        void RegisterDelete(OutboundModuleGridsPost outboundmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

