using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MessageFunctionsRepository : IMessageFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MessageFunctionsDB _context;
  
        public MessageFunctionsRepository(MessageFunctionsDB context)
        {
            _context = context;
  
        }

        public MessageFunctionsPost GetPost(Int64 ixMessageFunction) => _context.MessageFunctionsPost.AsNoTracking().Where(x => x.ixMessageFunction == ixMessageFunction).First();
         
		public MessageFunctions Get(Int64 ixMessageFunction)
        {
            MessageFunctions messagefunctions = _context.MessageFunctions.AsNoTracking().Where(x => x.ixMessageFunction == ixMessageFunction).First();
            return messagefunctions;
        }

        public IQueryable<MessageFunctions> Index()
        {
            var messagefunctions = _context.MessageFunctions.AsNoTracking(); 
            return messagefunctions;
        }

        public IQueryable<MessageFunctions> IndexDb()
        {
            var messagefunctions = _context.MessageFunctions.AsNoTracking(); 
            return messagefunctions;
        }
        public bool VerifyMessageFunctionUnique(Int64 ixMessageFunction, string sMessageFunction)
        {
            if (_context.MessageFunctions.AsNoTracking().Where(x => x.sMessageFunction == sMessageFunction).Any() && ixMessageFunction == 0L) return false;
            else if (_context.MessageFunctions.AsNoTracking().Where(x => x.sMessageFunction == sMessageFunction && x.ixMessageFunction != ixMessageFunction).Any() && ixMessageFunction != 0L) return false;
            else return true;
        }

        public List<string> VerifyMessageFunctionDeleteOK(Int64 ixMessageFunction, string sMessageFunction)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(MessageFunctionsPost messagefunctionsPost)
		{
            _context.MessageFunctionsPost.Add(messagefunctionsPost); 
        }

        public void RegisterEdit(MessageFunctionsPost messagefunctionsPost)
        {
            _context.Entry(messagefunctionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MessageFunctionsPost messagefunctionsPost)
        {
            _context.MessageFunctionsPost.Remove(messagefunctionsPost);
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
  

