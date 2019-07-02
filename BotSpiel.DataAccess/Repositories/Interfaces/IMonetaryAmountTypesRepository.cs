using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMonetaryAmountTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MonetaryAmountTypesPost GetPost(Int64 ixMonetaryAmountType);        
		MonetaryAmountTypes Get(Int64 ixMonetaryAmountType);
        IQueryable<MonetaryAmountTypes> Index();
        bool VerifyMonetaryAmountTypeUnique(Int64 ixMonetaryAmountType, string sMonetaryAmountType);
        List<string> VerifyMonetaryAmountTypeDeleteOK(Int64 ixMonetaryAmountType, string sMonetaryAmountType);
        void RegisterCreate(MonetaryAmountTypesPost monetaryamounttypesPost);
        void RegisterEdit(MonetaryAmountTypesPost monetaryamounttypesPost);
        void RegisterDelete(MonetaryAmountTypesPost monetaryamounttypesPost);
        void Rollback();
        void Commit();
    }
}
  

