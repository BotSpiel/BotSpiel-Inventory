using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PeopleRepository : IPeopleRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PeopleDB _context;
       private readonly PurchasesDB _contextPurchases;
        private readonly SendEmailsDB _contextSendEmails;
        private readonly SendTextMessagesDB _contextSendTextMessages;
  
        public PeopleRepository(PeopleDB context, PurchasesDB contextPurchases, SendEmailsDB contextSendEmails, SendTextMessagesDB contextSendTextMessages)
        {
            _context = context;
           _contextPurchases = contextPurchases;
            _contextSendEmails = contextSendEmails;
            _contextSendTextMessages = contextSendTextMessages;
  
        }

        public PeoplePost GetPost(Int64 ixPerson) => _context.PeoplePost.AsNoTracking().Where(x => x.ixPerson == ixPerson).First();
         
		public People Get(Int64 ixPerson)
        {
            People people = _context.People.AsNoTracking().Where(x => x.ixPerson == ixPerson).First();
            people.Languages = _context.Languages.Find(people.ixLanguage);

            return people;
        }

        public IQueryable<People> Index()
        {
            var people = _context.People.Include(a => a.Languages).AsNoTracking(); 
            return people;
        }

        public IQueryable<People> IndexDb()
        {
            var people = _context.People.Include(a => a.Languages).AsNoTracking(); 
            return people;
        }
       public IQueryable<Languages> selectLanguages()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
       public IQueryable<Languages> LanguagesDb()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public bool VerifyPersonUnique(Int64 ixPerson, string sPerson)
        {
            if (_context.People.AsNoTracking().Where(x => x.sPerson == sPerson).Any() && ixPerson == 0L) return false;
            else if (_context.People.AsNoTracking().Where(x => x.sPerson == sPerson && x.ixPerson != ixPerson).Any() && ixPerson != 0L) return false;
            else return true;
        }

        public List<string> VerifyPersonDeleteOK(Int64 ixPerson, string sPerson)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPurchases.Purchases.AsNoTracking().Where(x => x.ixPerson == ixPerson).Any()) existInEntities.Add("Purchases");
            if (_contextSendEmails.SendEmails.AsNoTracking().Where(x => x.ixPerson == ixPerson).Any()) existInEntities.Add("SendEmails");
            if (_contextSendTextMessages.SendTextMessages.AsNoTracking().Where(x => x.ixPerson == ixPerson).Any()) existInEntities.Add("SendTextMessages");

            return existInEntities;
        }


        public void RegisterCreate(PeoplePost peoplePost)
		{
            _context.PeoplePost.Add(peoplePost); 
        }

        public void RegisterEdit(PeoplePost peoplePost)
        {
            _context.Entry(peoplePost).State = EntityState.Modified;
        }

        public void RegisterDelete(PeoplePost peoplePost)
        {
            _context.PeoplePost.Remove(peoplePost);
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
  

