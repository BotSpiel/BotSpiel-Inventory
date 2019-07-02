using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IBaySequenceTypesRepository
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
        void RegisterCreate(BaySequenceTypesPost baysequencetypesPost);
        void RegisterEdit(BaySequenceTypesPost baysequencetypesPost);
        void RegisterDelete(BaySequenceTypesPost baysequencetypesPost);
        void Rollback();
        void Commit();
    }
}
  

