using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MessageResponseTypes : IMessageResponseTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Message Response Type ID")]
		public virtual Int64 ixMessageResponseType { get; set; }
		[Display(Name = "Message Response Type ID")]
		public virtual Int64 ixMessageResponseTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Message Response Type")]
		public virtual String sMessageResponseType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Message Response Type Code")]
		public virtual String sMessageResponseTypeCode { get; set; }
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
  

