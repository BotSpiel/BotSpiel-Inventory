using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnitTransactionContextsPost : IInventoryUnitTransactionContextsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Unit Transaction Context ID")]
		public virtual Int64 ixInventoryUnitTransactionContext { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyInventoryUnitTransactionContext", controller: "InventoryUnitTransactionContexts", AdditionalFields = nameof(ixInventoryUnitTransactionContext))]
		[Display(Name = "Inventory Unit Transaction Context")]
		public virtual String sInventoryUnitTransactionContext { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

