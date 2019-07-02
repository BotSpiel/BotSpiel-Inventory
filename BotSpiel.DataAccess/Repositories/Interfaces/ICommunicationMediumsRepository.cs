using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICommunicationMediumsRepository
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
        void RegisterCreate(CommunicationMediumsPost communicationmediumsPost);
        void RegisterEdit(CommunicationMediumsPost communicationmediumsPost);
        void RegisterDelete(CommunicationMediumsPost communicationmediumsPost);
        void Rollback();
        void Commit();
    }
}
  

