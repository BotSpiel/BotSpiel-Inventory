using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatchesPost : IPickBatchesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Pick Batch ID")]
		public virtual Int64 ixPickBatch { get; set; }
		[Display(Name = "Pick Batch")]
		public virtual String sPickBatch { get; set; }
		[Required]
		[Display(Name = "Pick Batch Type ID")]
		public virtual Int64 ixPickBatchType { get; set; }
		[Required]
		[Display(Name = "Multi Resource")]
		public virtual Boolean bMultiResource { get; set; }
		[Required]
		[Display(Name = "Start By")]
		public virtual DateTime dtStartBy { get; set; }
		[Required]
		[Display(Name = "Complete By")]
		public virtual DateTime dtCompleteBy { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

