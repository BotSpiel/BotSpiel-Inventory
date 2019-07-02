using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IDateTimePeriodFunctionsService
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
        bool VerifyDateTimePeriodFunctionUnique(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction);
        List<string> VerifyDateTimePeriodFunctionDeleteOK(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction);

        Task<Int64> Create(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
        Task Edit(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
        Task Delete(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost);
    }
}
  

