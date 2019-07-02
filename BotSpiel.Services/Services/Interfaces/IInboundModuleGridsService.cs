using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInboundModuleGridsService
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
		IQueryable<InboundModuleGridsconfig> Indexconfig();
		IQueryable<InboundModuleGridsmd> Indexmd();
		IQueryable<InboundModuleGridstx> Indextx();
		IQueryable<InboundModuleGridsanalytics> Indexanalytics();
        List<string> VerifyInboundModuleGridDeleteOK(Int64 ixInboundModuleGrid, string sInboundModuleGrid);

        Task<Int64> Create(InboundModuleGridsPost inboundmodulegridsPost);
        Task Edit(InboundModuleGridsPost inboundmodulegridsPost);
        Task Delete(InboundModuleGridsPost inboundmodulegridsPost);
    }
}
  

