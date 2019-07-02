using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BaySequenceTypes : IBaySequenceTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Bay Sequence Type ID")]
		public virtual Int64 ixBaySequenceType { get; set; }
		[Display(Name = "Bay Sequence Type ID")]
		public virtual Int64 ixBaySequenceTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Bay Sequence Type")]
		public virtual String sBaySequenceType { get; set; }
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
  

