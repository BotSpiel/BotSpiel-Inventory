using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Statuses : IStatuses
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatusEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Status")]
		public virtual String sStatus { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Status Code")]
		public virtual String sStatusCode { get; set; }
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
    }
}
  

