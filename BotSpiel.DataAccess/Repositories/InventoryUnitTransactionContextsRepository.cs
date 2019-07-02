using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryUnitTransactionContextsRepository : IInventoryUnitTransactionContextsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryUnitTransactionContextsDB _context;
       private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
  
        public InventoryUnitTransactionContextsRepository(InventoryUnitTransactionContextsDB context, InventoryUnitTransactionsDB contextInventoryUnitTransactions)
        {
            _context = context;
           _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
  
        }

        public InventoryUnitTransactionContextsPost GetPost(Int64 ixInventoryUnitTransactionContext) => _context.InventoryUnitTransactionContextsPost.AsNoTracking().Where(x => x.ixInventoryUnitTransactionContext == ixInventoryUnitTransactionContext).First();
         
		public InventoryUnitTransactionContexts Get(Int64 ixInventoryUnitTransactionContext)
        {
            InventoryUnitTransactionContexts inventoryunittransactioncontexts = _context.InventoryUnitTransactionContexts.AsNoTracking().Where(x => x.ixInventoryUnitTransactionContext == ixInventoryUnitTransactionContext).First();
            return inventoryunittransactioncontexts;
        }

        public IQueryable<InventoryUnitTransactionContexts> Index()
        {
            var inventoryunittransactioncontexts = _context.InventoryUnitTransactionContexts.AsNoTracking(); 
            return inventoryunittransactioncontexts;
        }
        public bool VerifyInventoryUnitTransactionContextUnique(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext)
        {
            if (_context.InventoryUnitTransactionContexts.AsNoTracking().Where(x => x.sInventoryUnitTransactionContext == sInventoryUnitTransactionContext).Any() && ixInventoryUnitTransactionContext == 0L) return false;
            else if (_context.InventoryUnitTransactionContexts.AsNoTracking().Where(x => x.sInventoryUnitTransactionContext == sInventoryUnitTransactionContext && x.ixInventoryUnitTransactionContext != ixInventoryUnitTransactionContext).Any() && ixInventoryUnitTransactionContext != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryUnitTransactionContextDeleteOK(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryUnitTransactionContext == ixInventoryUnitTransactionContext).Any()) existInEntities.Add("InventoryUnitTransactions");

            return existInEntities;
        }


        public void RegisterCreate(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
		{
            _context.InventoryUnitTransactionContextsPost.Add(inventoryunittransactioncontextsPost); 
        }

        public void RegisterEdit(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
        {
            _context.Entry(inventoryunittransactioncontextsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
        {
            _context.InventoryUnitTransactionContextsPost.Remove(inventoryunittransactioncontextsPost);
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
  

