using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IDateTimePeriodFormatsRepository
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
        void RegisterCreate(DateTimePeriodFormatsPost datetimeperiodformatsPost);
        void RegisterEdit(DateTimePeriodFormatsPost datetimeperiodformatsPost);
        void RegisterDelete(DateTimePeriodFormatsPost datetimeperiodformatsPost);
        void Rollback();
        void Commit();
    }
}
  

