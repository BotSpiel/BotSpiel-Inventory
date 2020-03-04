using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IStatusesService
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
        IQueryable<Statuses> IndexDb();
        bool VerifyStatusUnique(Int64 ixStatus, string sStatus);
        List<string> VerifyStatusDeleteOK(Int64 ixStatus, string sStatus);

        //Custom Code Start | Added Code Block 
        string getStatus(Int64 ixStatus);
        //Custom Code End

        Task<Int64> Create(StatusesPost statusesPost);
        Task Edit(StatusesPost statusesPost);
        Task Delete(StatusesPost statusesPost);
    }
}
  

