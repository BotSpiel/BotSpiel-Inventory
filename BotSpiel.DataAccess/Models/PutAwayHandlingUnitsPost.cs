using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PutAwayHandlingUnitsPost : IPutAwayHandlingUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Put Away Handling Unit ID")]
		public virtual Int64 ixPutAwayHandlingUnit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Put Away Handling Unit")]
		public virtual String sPutAwayHandlingUnit { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Inventory Drop Location")]
		public virtual String sInventoryDropLocation { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

