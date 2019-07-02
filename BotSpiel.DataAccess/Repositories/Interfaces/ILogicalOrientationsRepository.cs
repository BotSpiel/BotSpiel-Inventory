using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ILogicalOrientationsRepository
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
        bool VerifyLogicalOrientationUnique(Int64 ixLogicalOrientation, string sLogicalOrientation);
        List<string> VerifyLogicalOrientationDeleteOK(Int64 ixLogicalOrientation, string sLogicalOrientation);
        void RegisterCreate(LogicalOrientationsPost logicalorientationsPost);
        void RegisterEdit(LogicalOrientationsPost logicalorientationsPost);
        void RegisterDelete(LogicalOrientationsPost logicalorientationsPost);
        void Rollback();
        void Commit();
    }
}
  

