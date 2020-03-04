using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICurrenciesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CurrenciesPost GetPost(Int64 ixCurrency);        
		Currencies Get(Int64 ixCurrency);
        IQueryable<Currencies> Index();
        IQueryable<Currencies> IndexDb();
        bool VerifyCurrencyUnique(Int64 ixCurrency, string sCurrency);
        List<string> VerifyCurrencyDeleteOK(Int64 ixCurrency, string sCurrency);

        Task<Int64> Create(CurrenciesPost currenciesPost);
        Task Edit(CurrenciesPost currenciesPost);
        Task Delete(CurrenciesPost currenciesPost);
    }
}
  

