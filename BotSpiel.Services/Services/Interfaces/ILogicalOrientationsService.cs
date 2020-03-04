using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ILogicalOrientationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        LogicalOrientationsPost GetPost(Int64 ixLogicalOrientation);        
		LogicalOrientations Get(Int64 ixLogicalOrientation);
        IQueryable<LogicalOrientations> Index();
        IQueryable<LogicalOrientations> IndexDb();
        bool VerifyLogicalOrientationUnique(Int64 ixLogicalOrientation, string sLogicalOrientation);
        List<string> VerifyLogicalOrientationDeleteOK(Int64 ixLogicalOrientation, string sLogicalOrientation);

        Task<Int64> Create(LogicalOrientationsPost logicalorientationsPost);
        Task Edit(LogicalOrientationsPost logicalorientationsPost);
        Task Delete(LogicalOrientationsPost logicalorientationsPost);
    }
}
  

