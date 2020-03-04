using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MessageResponseTypesRepository : IMessageResponseTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MessageResponseTypesDB _context;
  
        public MessageResponseTypesRepository(MessageResponseTypesDB context)
        {
            _context = context;
  
        }

        public MessageResponseTypesPost GetPost(Int64 ixMessageResponseType) => _context.MessageResponseTypesPost.AsNoTracking().Where(x => x.ixMessageResponseType == ixMessageResponseType).First();
         
		public MessageResponseTypes Get(Int64 ixMessageResponseType)
        {
            MessageResponseTypes messageresponsetypes = _context.MessageResponseTypes.AsNoTracking().Where(x => x.ixMessageResponseType == ixMessageResponseType).First();
            return messageresponsetypes;
        }

        public IQueryable<MessageResponseTypes> Index()
        {
            var messageresponsetypes = _context.MessageResponseTypes.AsNoTracking(); 
            return messageresponsetypes;
        }

        public IQueryable<MessageResponseTypes> IndexDb()
        {
            var messageresponsetypes = _context.MessageResponseTypes.AsNoTracking(); 
            return messageresponsetypes;
        }
        public bool VerifyMessageResponseTypeUnique(Int64 ixMessageResponseType, string sMessageResponseType)
        {
            if (_context.MessageResponseTypes.AsNoTracking().Where(x => x.sMessageResponseType == sMessageResponseType).Any() && ixMessageResponseType == 0L) return false;
            else if (_context.MessageResponseTypes.AsNoTracking().Where(x => x.sMessageResponseType == sMessageResponseType && x.ixMessageResponseType != ixMessageResponseType).Any() && ixMessageResponseType != 0L) return false;
            else return true;
        }

        public List<string> VerifyMessageResponseTypeDeleteOK(Int64 ixMessageResponseType, string sMessageResponseType)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(MessageResponseTypesPost messageresponsetypesPost)
		{
            _context.MessageResponseTypesPost.Add(messageresponsetypesPost); 
        }

        public void RegisterEdit(MessageResponseTypesPost messageresponsetypesPost)
        {
            _context.Entry(messageresponsetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MessageResponseTypesPost messageresponsetypesPost)
        {
            _context.MessageResponseTypesPost.Remove(messageresponsetypesPost);
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
  

