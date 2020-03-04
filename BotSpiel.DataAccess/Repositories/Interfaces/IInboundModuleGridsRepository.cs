using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInboundModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InboundModuleGridsPost GetPost(Int64 ixInboundModuleGrid);        
		InboundModuleGrids Get(Int64 ixInboundModuleGrid);
        IQueryable<InboundModuleGrids> Index();
        IQueryable<InboundModuleGrids> IndexDb();
		IQueryable<InboundModuleGridsconfig> Indexconfig();
		IQueryable<InboundModuleGridsmd> Indexmd();
		IQueryable<InboundModuleGridstx> Indextx();
		IQueryable<InboundModuleGridsanalytics> Indexanalytics();
        List<string> VerifyInboundModuleGridDeleteOK(Int64 ixInboundModuleGrid, string sInboundModuleGrid);
        void RegisterCreate(InboundModuleGridsPost inboundmodulegridsPost);
        void RegisterEdit(InboundModuleGridsPost inboundmodulegridsPost);
        void RegisterDelete(InboundModuleGridsPost inboundmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

