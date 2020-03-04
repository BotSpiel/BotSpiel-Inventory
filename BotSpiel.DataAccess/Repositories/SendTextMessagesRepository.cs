using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class SendTextMessagesRepository : ISendTextMessagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly SendTextMessagesDB _context;
       private readonly PurchaseTextMessagesDB _contextPurchaseTextMessages;
  
        public SendTextMessagesRepository(SendTextMessagesDB context, PurchaseTextMessagesDB contextPurchaseTextMessages)
        {
            _context = context;
           _contextPurchaseTextMessages = contextPurchaseTextMessages;
  
        }

        public SendTextMessagesPost GetPost(Int64 ixSendTextMessage) => _context.SendTextMessagesPost.AsNoTracking().Where(x => x.ixSendTextMessage == ixSendTextMessage).First();
         
		public SendTextMessages Get(Int64 ixSendTextMessage)
        {
            SendTextMessages sendtextmessages = _context.SendTextMessages.AsNoTracking().Where(x => x.ixSendTextMessage == ixSendTextMessage).First();
            sendtextmessages.People = _context.People.Find(sendtextmessages.ixPerson);

            return sendtextmessages;
        }

        public IQueryable<SendTextMessages> Index()
        {
            var sendtextmessages = _context.SendTextMessages.Include(a => a.People).AsNoTracking(); 
            return sendtextmessages;
        }

        public IQueryable<SendTextMessages> IndexDb()
        {
            var sendtextmessages = _context.SendTextMessages.Include(a => a.People).AsNoTracking(); 
            return sendtextmessages;
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
        public bool VerifySendTextMessageUnique(Int64 ixSendTextMessage, string sSendTextMessage)
        {
            if (_context.SendTextMessages.AsNoTracking().Where(x => x.sSendTextMessage == sSendTextMessage).Any() && ixSendTextMessage == 0L) return false;
            else if (_context.SendTextMessages.AsNoTracking().Where(x => x.sSendTextMessage == sSendTextMessage && x.ixSendTextMessage != ixSendTextMessage).Any() && ixSendTextMessage != 0L) return false;
            else return true;
        }

        public List<string> VerifySendTextMessageDeleteOK(Int64 ixSendTextMessage, string sSendTextMessage)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPurchaseTextMessages.PurchaseTextMessages.AsNoTracking().Where(x => x.ixSendTextMessage == ixSendTextMessage).Any()) existInEntities.Add("PurchaseTextMessages");

            return existInEntities;
        }


        public void RegisterCreate(SendTextMessagesPost sendtextmessagesPost)
		{
            _context.SendTextMessagesPost.Add(sendtextmessagesPost); 
        }

        public void RegisterEdit(SendTextMessagesPost sendtextmessagesPost)
        {
            _context.Entry(sendtextmessagesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(SendTextMessagesPost sendtextmessagesPost)
        {
            _context.SendTextMessagesPost.Remove(sendtextmessagesPost);
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
  

