using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class SendEmailsRepository : ISendEmailsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly SendEmailsDB _context;
       private readonly PurchaseEmailsDB _contextPurchaseEmails;
  
        public SendEmailsRepository(SendEmailsDB context, PurchaseEmailsDB contextPurchaseEmails)
        {
            _context = context;
           _contextPurchaseEmails = contextPurchaseEmails;
  
        }

        public SendEmailsPost GetPost(Int64 ixSendEmail) => _context.SendEmailsPost.AsNoTracking().Where(x => x.ixSendEmail == ixSendEmail).First();
         
		public SendEmails Get(Int64 ixSendEmail)
        {
            SendEmails sendemails = _context.SendEmails.AsNoTracking().Where(x => x.ixSendEmail == ixSendEmail).First();
            sendemails.People = _context.People.Find(sendemails.ixPerson);

            return sendemails;
        }

        public IQueryable<SendEmails> Index()
        {
            var sendemails = _context.SendEmails.Include(a => a.People).AsNoTracking(); 
            return sendemails;
        }

        public IQueryable<SendEmails> IndexDb()
        {
            var sendemails = _context.SendEmails.Include(a => a.People).AsNoTracking(); 
            return sendemails;
        }
       public IQueryable<People> selectPeople()
        {
            List<People> people = new List<People>();
            _context.People.Include(a => a.Languages).AsNoTracking()
                .ToList()
                .ForEach(x => people.Add(x));
            return people.AsQueryable();
        }
       public IQueryable<People> PeopleDb()
        {
            List<People> people = new List<People>();
            _context.People.Include(a => a.Languages).AsNoTracking()
                .ToList()
                .ForEach(x => people.Add(x));
            return people.AsQueryable();
        }
        public bool VerifySendEmailUnique(Int64 ixSendEmail, string sSendEmail)
        {
            if (_context.SendEmails.AsNoTracking().Where(x => x.sSendEmail == sSendEmail).Any() && ixSendEmail == 0L) return false;
            else if (_context.SendEmails.AsNoTracking().Where(x => x.sSendEmail == sSendEmail && x.ixSendEmail != ixSendEmail).Any() && ixSendEmail != 0L) return false;
            else return true;
        }

        public List<string> VerifySendEmailDeleteOK(Int64 ixSendEmail, string sSendEmail)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPurchaseEmails.PurchaseEmails.AsNoTracking().Where(x => x.ixSendEmail == ixSendEmail).Any()) existInEntities.Add("PurchaseEmails");

            return existInEntities;
        }


        public void RegisterCreate(SendEmailsPost sendemailsPost)
		{
            _context.SendEmailsPost.Add(sendemailsPost); 
        }

        public void RegisterEdit(SendEmailsPost sendemailsPost)
        {
            _context.Entry(sendemailsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(SendEmailsPost sendemailsPost)
        {
            _context.SendEmailsPost.Remove(sendemailsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

