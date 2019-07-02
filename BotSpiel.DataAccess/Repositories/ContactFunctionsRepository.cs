using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ContactFunctionsRepository : IContactFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ContactFunctionsDB _context;
  
        public ContactFunctionsRepository(ContactFunctionsDB context)
        {
            _context = context;
  
        }

        public ContactFunctionsPost GetPost(Int64 ixContactFunction) => _context.ContactFunctionsPost.AsNoTracking().Where(x => x.ixContactFunction == ixContactFunction).First();
         
		public ContactFunctions Get(Int64 ixContactFunction)
        {
            ContactFunctions contactfunctions = _context.ContactFunctions.AsNoTracking().Where(x => x.ixContactFunction == ixContactFunction).First();
            return contactfunctions;
        }

        public IQueryable<ContactFunctions> Index()
        {
            var contactfunctions = _context.ContactFunctions.AsNoTracking(); 
            return contactfunctions;
        }
        public bool VerifyContactFunctionUnique(Int64 ixContactFunction, string sContactFunction)
        {
            if (_context.ContactFunctions.AsNoTracking().Where(x => x.sContactFunction == sContactFunction).Any() && ixContactFunction == 0L) return false;
            else if (_context.ContactFunctions.AsNoTracking().Where(x => x.sContactFunction == sContactFunction && x.ixContactFunction != ixContactFunction).Any() && ixContactFunction != 0L) return false;
            else return true;
        }

        public List<string> VerifyContactFunctionDeleteOK(Int64 ixContactFunction, string sContactFunction)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(ContactFunctionsPost contactfunctionsPost)
		{
            _context.ContactFunctionsPost.Add(contactfunctionsPost); 
        }

        public void RegisterEdit(ContactFunctionsPost contactfunctionsPost)
        {
            _context.Entry(contactfunctionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ContactFunctionsPost contactfunctionsPost)
        {
            _context.ContactFunctionsPost.Remove(contactfunctionsPost);
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
  

