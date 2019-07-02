using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueueTypes : IMoveQueueTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Move Queue Type ID")]
		public virtual Int64 ixMoveQueueType { get; set; }
		[Display(Name = "Move Queue Type ID")]
		public virtual Int64 ixMoveQueueTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Move Queue Type")]
		public virtual String sMoveQueueType { get; set; }
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
  

