using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatchTypesPost : IPickBatchTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Pick Batch Type ID")]
		public virtual Int64 ixPickBatchType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPickBatchType", controller: "PickBatchTypes", AdditionalFields = nameof(ixPickBatchType))]
		[Display(Name = "Pick Batch Type")]
		public virtual String sPickBatchType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

