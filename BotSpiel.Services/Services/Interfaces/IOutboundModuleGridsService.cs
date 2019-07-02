using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundModuleGridsService
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

        Task<Int64> Create(OutboundModuleGridsPost outboundmodulegridsPost);
        Task Edit(OutboundModuleGridsPost outboundmodulegridsPost);
        Task Delete(OutboundModuleGridsPost outboundmodulegridsPost);
    }
}
  

