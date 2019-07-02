using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class BotspielBotMessagesRepository : IBotspielBotMessagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly BotspielBotMessagesDB _context;
  
        public BotspielBotMessagesRepository(BotspielBotMessagesDB context)
        {
            _context = context;
  
        }

        public BotspielBotMessagesPost GetPost(Int64 ixBotspielBotMessage) => _context.BotspielBotMessagesPost.AsNoTracking().Where(x => x.ixBotspielBotMessage == ixBotspielBotMessage).First();
         
		public BotspielBotMessages Get(Int64 ixBotspielBotMessage)
        {
            BotspielBotMessages botspielbotmessages = _context.BotspielBotMessages.AsNoTracking().Where(x => x.ixBotspielBotMessage == ixBotspielBotMessage).First();
            return botspielbotmessages;
        }

        public IQueryable<BotspielBotMessages> Index()
        {
            var botspielbotmessages = _context.BotspielBotMessages.AsNoTracking(); 
            return botspielbotmessages;
        }
        public bool VerifyBotspielBotMessageUnique(Int64 ixBotspielBotMessage, string sBotspielBotMessage)
        {
            if (_context.BotspielBotMessages.AsNoTracking().Where(x => x.sBotspielBotMessage == sBotspielBotMessage).Any() && ixBotspielBotMessage == 0L) return false;
            else if (_context.BotspielBotMessages.AsNoTracking().Where(x => x.sBotspielBotMessage == sBotspielBotMessage && x.ixBotspielBotMessage != ixBotspielBotMessage).Any() && ixBotspielBotMessage != 0L) return false;
            else return true;
        }

        public List<string> VerifyBotspielBotMessageDeleteOK(Int64 ixBotspielBotMessage, string sBotspielBotMessage)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(BotspielBotMessagesPost botspielbotmessagesPost)
		{
            _context.BotspielBotMessagesPost.Add(botspielbotmessagesPost); 
        }

        public void RegisterEdit(BotspielBotMessagesPost botspielbotmessagesPost)
        {
            _context.Entry(botspielbotmessagesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(BotspielBotMessagesPost botspielbotmessagesPost)
        {
            _context.BotspielBotMessagesPost.Remove(botspielbotmessagesPost);
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
  

