using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class StatusesPost : IStatusesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyStatus", controller: "Statuses", AdditionalFields = nameof(ixStatus))]
		[Display(Name = "Status")]
		public virtual String sStatus { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Status Code")]
		public virtual String sStatusCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

