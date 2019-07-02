using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueueContextsPost : IMoveQueueContextsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Move Queue Context ID")]
		public virtual Int64 ixMoveQueueContext { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMoveQueueContext", controller: "MoveQueueContexts", AdditionalFields = nameof(ixMoveQueueContext))]
		[Display(Name = "Move Queue Context")]
		public virtual String sMoveQueueContext { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

