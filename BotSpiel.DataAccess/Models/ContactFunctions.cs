using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class ContactFunctions : IContactFunctions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Contact Function ID")]
		public virtual Int64 ixContactFunction { get; set; }
		[Display(Name = "Contact Function ID")]
		public virtual Int64 ixContactFunctionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Contact Function")]
		public virtual String sContactFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Contact Function Code")]
		public virtual String sContactFunctionCode { get; set; }
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
  

