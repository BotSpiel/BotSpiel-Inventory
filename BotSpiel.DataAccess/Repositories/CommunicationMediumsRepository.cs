using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CommunicationMediumsRepository : ICommunicationMediumsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CommunicationMediumsDB _context;
  
        public CommunicationMediumsRepository(CommunicationMediumsDB context)
        {
            _context = context;
  
        }

        public CommunicationMediumsPost GetPost(Int64 ixCommunicationMedium) => _context.CommunicationMediumsPost.AsNoTracking().Where(x => x.ixCommunicationMedium == ixCommunicationMedium).First();
         
		public CommunicationMediums Get(Int64 ixCommunicationMedium)
        {
            CommunicationMediums communicationmediums = _context.CommunicationMediums.AsNoTracking().Where(x => x.ixCommunicationMedium == ixCommunicationMedium).First();
            return communicationmediums;
        }

        public IQueryable<CommunicationMediums> Index()
        {
            var communicationmediums = _context.CommunicationMediums.AsNoTracking(); 
            return communicationmediums;
        }
        public bool VerifyCommunicationMediumUnique(Int64 ixCommunicationMedium, string sCommunicationMedium)
        {
            if (_context.CommunicationMediums.AsNoTracking().Where(x => x.sCommunicationMedium == sCommunicationMedium).Any() && ixCommunicationMedium == 0L) return false;
            else if (_context.CommunicationMediums.AsNoTracking().Where(x => x.sCommunicationMedium == sCommunicationMedium && x.ixCommunicationMedium != ixCommunicationMedium).Any() && ixCommunicationMedium != 0L) return false;
            else return true;
        }

        public List<string> VerifyCommunicationMediumDeleteOK(Int64 ixCommunicationMedium, string sCommunicationMedium)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(CommunicationMediumsPost communicationmediumsPost)
		{
            _context.CommunicationMediumsPost.Add(communicationmediumsPost); 
        }

        public void RegisterEdit(CommunicationMediumsPost communicationmediumsPost)
        {
            _context.Entry(communicationmediumsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CommunicationMediumsPost communicationmediumsPost)
        {
            _context.CommunicationMediumsPost.Remove(communicationmediumsPost);
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
  

