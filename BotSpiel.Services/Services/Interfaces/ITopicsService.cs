using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ITopicsService
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

        Task<Int64> Create(TopicsPost topicsPost);
        Task Edit(TopicsPost topicsPost);
        Task Delete(TopicsPost topicsPost);
    }
}
  

