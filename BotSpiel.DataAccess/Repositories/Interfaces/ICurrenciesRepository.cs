using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICurrenciesRepository
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
        void RegisterCreate(CurrenciesPost currenciesPost);
        void RegisterEdit(CurrenciesPost currenciesPost);
        void RegisterDelete(CurrenciesPost currenciesPost);
        void Rollback();
        void Commit();
    }
}
  

