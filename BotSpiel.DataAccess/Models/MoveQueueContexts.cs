using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueueContexts : IMoveQueueContexts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Move Queue Context ID")]
		public virtual Int64 ixMoveQueueContext { get; set; }
		[Display(Name = "Move Queue Context ID")]
		public virtual Int64 ixMoveQueueContextEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Move Queue Context")]
		public virtual String sMoveQueueContext { get; set; }
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
  

