using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryUnitTransactionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryUnitTransactionsPost GetPost(Int64 ixInventoryUnitTransaction);        
		InventoryUnitTransactions Get(Int64 ixInventoryUnitTransaction);
        IQueryable<InventoryUnitTransactions> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<Materials> selectMaterials();
        IQueryable<InventoryUnits> selectInventoryUnits();
        IQueryable<InventoryStates> selectInventoryStates();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<InventoryUnitTransactionContexts> selectInventoryUnitTransactionContexts();
       List<KeyValuePair<Int64?, string>> selectStatusesNullable();
        List<KeyValuePair<Int64?, string>> selectCompaniesNullable();
        List<KeyValuePair<Int64?, string>> selectFacilitiesNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryStatesNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable();
        bool VerifyInventoryUnitTransactionUnique(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction);
        List<string> VerifyInventoryUnitTransactionDeleteOK(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction);
        void RegisterCreate(InventoryUnitTransactionsPost inventoryunittransactionsPost);
        void RegisterEdit(InventoryUnitTransactionsPost inventoryunittransactionsPost);
        void RegisterDelete(InventoryUnitTransactionsPost inventoryunittransactionsPost);
        void Rollback();
        void Commit();
    }
}
  

