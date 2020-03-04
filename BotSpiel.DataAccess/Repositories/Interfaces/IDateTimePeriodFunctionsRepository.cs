using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IDateTimePeriodFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        DateTimePeriodFunctionsPost GetPost(Int64 ixDateTimePeriodFunction);        
		DateTimePeriodFunctions Get(Int64 ixDateTimePeriodFunction);
        IQueryable<DateTimePeriodFunctions> Index();
        IQueryable<DateTimePeriodFunctions> IndexDb();
        bool VerifyDateTimePeriodFunctionUnique(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction);
        List<string> VerifyDateTimePeriodFunctionDeleteOK(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction);
        void RegisterCreate(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
        void RegisterEdit(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
        void RegisterDelete(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
        void Rollback();
        void Commit();
    }
}
  

