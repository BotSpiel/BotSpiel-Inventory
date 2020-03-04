using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestForActionSimilesPost : IRequestForActionSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Request For Action Simile ID")]
		public virtual Int64 ixRequestForActionSimile { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Request For Action Simile")]
		public virtual String sRequestForActionSimile { get; set; }
		[Required]
		[Display(Name = "Request For Action ID")]
		public virtual Int64 ixRequestForAction { get; set; }
		[Required]
		[Display(Name = "Request For Action Simile Text")]
		public virtual String sRequestForActionSimileText { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

