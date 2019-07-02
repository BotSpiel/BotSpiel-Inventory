using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IStatusesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        StatusesPost GetPost(Int64 ixStatus);        
		Statuses Get(Int64 ixStatus);
        IQueryable<Statuses> Index();
        bool VerifyStatusUnique(Int64 ixStatus, string sStatus);
        List<string> VerifyStatusDeleteOK(Int64 ixStatus, string sStatus);
        void RegisterCreate(StatusesPost statusesPost);
        void RegisterEdit(StatusesPost statusesPost);
        void RegisterDelete(StatusesPost statusesPost);
        void Rollback();
        void Commit();
    }
}
  

