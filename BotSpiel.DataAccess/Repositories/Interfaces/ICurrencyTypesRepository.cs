using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICurrencyTypesRepository
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
        void RegisterCreate(CurrencyTypesPost currencytypesPost);
        void RegisterEdit(CurrencyTypesPost currencytypesPost);
        void RegisterDelete(CurrencyTypesPost currencytypesPost);
        void Rollback();
        void Commit();
    }
}
  

