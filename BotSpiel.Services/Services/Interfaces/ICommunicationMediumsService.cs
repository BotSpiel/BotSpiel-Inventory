using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICommunicationMediumsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CommunicationMediumsPost GetPost(Int64 ixCommunicationMedium);        
		CommunicationMediums Get(Int64 ixCommunicationMedium);
        IQueryable<CommunicationMediums> Index();
        bool VerifyCommunicationMediumUnique(Int64 ixCommunicationMedium, string sCommunicationMedium);
        List<string> VerifyCommunicationMediumDeleteOK(Int64 ixCommunicationMedium, string sCommunicationMedium);

        Task<Int64> Create(CommunicationMediumsPost communicationmediumsPost);
        Task Edit(CommunicationMediumsPost communicationmediumsPost);
        Task Delete(CommunicationMediumsPost communicationmediumsPost);
    }
}
  

