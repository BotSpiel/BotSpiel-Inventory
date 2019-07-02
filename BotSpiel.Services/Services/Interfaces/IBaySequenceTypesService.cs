using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IBaySequenceTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        BaySequenceTypesPost GetPost(Int64 ixBaySequenceType);        
		BaySequenceTypes Get(Int64 ixBaySequenceType);
        IQueryable<BaySequenceTypes> Index();
        bool VerifyBaySequenceTypeUnique(Int64 ixBaySequenceType, string sBaySequenceType);
        List<string> VerifyBaySequenceTypeDeleteOK(Int64 ixBaySequenceType, string sBaySequenceType);

        Task<Int64> Create(BaySequenceTypesPost baysequencetypesPost);
        Task Edit(BaySequenceTypesPost baysequencetypesPost);
        Task Delete(BaySequenceTypesPost baysequencetypesPost);
    }
}
  

