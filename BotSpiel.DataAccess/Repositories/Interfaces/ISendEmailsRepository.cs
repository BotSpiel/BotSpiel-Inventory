using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ISendEmailsRepository
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
        void RegisterCreate(SendEmailsPost sendemailsPost);
        void RegisterEdit(SendEmailsPost sendemailsPost);
        void RegisterDelete(SendEmailsPost sendemailsPost);
        void Rollback();
        void Commit();
    }
}
  

