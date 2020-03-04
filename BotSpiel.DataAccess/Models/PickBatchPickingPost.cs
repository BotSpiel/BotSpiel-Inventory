using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatchPickingPost : IPickBatchPickingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Pick Batch Pick ID")]
		public virtual Int64 ixPickBatchPick { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Pick Batch Pick")]
		public virtual String sPickBatchPick { get; set; }
		[Required]
		[Display(Name = "Pick Batch ID")]
		public virtual Int64 ixPickBatch { get; set; }
		[Required]
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnit { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Picked")]
		public virtual Double nBaseUnitQuantityPicked { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Pack To Handling Unit")]
		public virtual String sPackToHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

