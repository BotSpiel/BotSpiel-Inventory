using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MessageFunctions : IMessageFunctions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Message Function ID")]
		public virtual Int64 ixMessageFunction { get; set; }
		[Display(Name = "Message Function ID")]
		public virtual Int64 ixMessageFunctionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Message Function")]
		public virtual String sMessageFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Message Function Code")]
		public virtual String sMessageFunctionCode { get; set; }
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
  

