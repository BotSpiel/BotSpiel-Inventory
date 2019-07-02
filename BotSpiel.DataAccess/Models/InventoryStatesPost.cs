using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryStatesPost : IInventoryStatesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory State ID")]
		public virtual Int64 ixInventoryState { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyInventoryState", controller: "InventoryStates", AdditionalFields = nameof(ixInventoryState))]
		[Display(Name = "Inventory State")]
		public virtual String sInventoryState { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

