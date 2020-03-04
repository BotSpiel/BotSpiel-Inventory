using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ITopicsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        TopicsPost GetPost(Int64 ixTopic);        
		Topics Get(Int64 ixTopic);
        IQueryable<Topics> Index();
        IQueryable<Topics> IndexDb();
        List<string> VerifyTopicDeleteOK(Int64 ixTopic, string sTopic);
        void RegisterCreate(TopicsPost topicsPost);
        void RegisterEdit(TopicsPost topicsPost);
        void RegisterDelete(TopicsPost topicsPost);
        void Rollback();
        void Commit();
    }
}
  

