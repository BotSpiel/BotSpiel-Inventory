using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMonetaryAmountTypesService
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

        Task<Int64> Create(MonetaryAmountTypesPost monetaryamounttypesPost);
        Task Edit(MonetaryAmountTypesPost monetaryamounttypesPost);
        Task Delete(MonetaryAmountTypesPost monetaryamounttypesPost);
    }
}
  

