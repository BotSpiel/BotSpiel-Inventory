using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICurrencyTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CurrencyTypesPost GetPost(Int64 ixCurrencyType);        
		CurrencyTypes Get(Int64 ixCurrencyType);
        IQueryable<CurrencyTypes> Index();
        IQueryable<CurrencyTypes> IndexDb();
        bool VerifyCurrencyTypeUnique(Int64 ixCurrencyType, string sCurrencyType);
        List<string> VerifyCurrencyTypeDeleteOK(Int64 ixCurrencyType, string sCurrencyType);

        Task<Int64> Create(CurrencyTypesPost currencytypesPost);
        Task Edit(CurrencyTypesPost currencytypesPost);
        Task Delete(CurrencyTypesPost currencytypesPost);
    }
}
  

