using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IDateTimePeriodFormatsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        DateTimePeriodFormatsPost GetPost(Int64 ixDateTimePeriodFormat);        
		DateTimePeriodFormats Get(Int64 ixDateTimePeriodFormat);
        IQueryable<DateTimePeriodFormats> Index();
        IQueryable<DateTimePeriodFormats> IndexDb();
        bool VerifyDateTimePeriodFormatUnique(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat);
        List<string> VerifyDateTimePeriodFormatDeleteOK(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat);

        Task<Int64> Create(DateTimePeriodFormatsPost datetimeperiodformatsPost);
        Task Edit(DateTimePeriodFormatsPost datetimeperiodformatsPost);
        Task Delete(DateTimePeriodFormatsPost datetimeperiodformatsPost);
    }
}
  

