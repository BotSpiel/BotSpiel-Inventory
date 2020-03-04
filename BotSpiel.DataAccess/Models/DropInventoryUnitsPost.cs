using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DropInventoryUnitsPost : IDropInventoryUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Drop Inventory Unit ID")]
		public virtual Int64 ixDropInventoryUnit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Drop Inventory Unit")]
		public virtual String sDropInventoryUnit { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

