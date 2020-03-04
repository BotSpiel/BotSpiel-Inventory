using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ISendEmailsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        SendEmailsPost GetPost(Int64 ixSendEmail);        
		SendEmails Get(Int64 ixSendEmail);
        IQueryable<SendEmails> Index();
        IQueryable<SendEmails> IndexDb();
       IQueryable<People> selectPeople();
       IQueryable<People> PeopleDb();
        bool VerifySendEmailUnique(Int64 ixSendEmail, string sSendEmail);
        List<string> VerifySendEmailDeleteOK(Int64 ixSendEmail, string sSendEmail);

        Task<Int64> Create(SendEmailsPost sendemailsPost);
        Task Edit(SendEmailsPost sendemailsPost);
        Task Delete(SendEmailsPost sendemailsPost);
    }
}
  

