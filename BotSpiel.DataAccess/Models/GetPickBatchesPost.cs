using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class GetPickBatchesPost : IGetPickBatchesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Get Pick Batch ID")]
		public virtual Int64 ixGetPickBatch { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Get Pick Batch")]
		public virtual String sGetPickBatch { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

