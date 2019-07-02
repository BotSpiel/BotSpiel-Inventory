using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueueTypesPost : IMoveQueueTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Move Queue Type ID")]
		public virtual Int64 ixMoveQueueType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMoveQueueType", controller: "MoveQueueTypes", AdditionalFields = nameof(ixMoveQueueType))]
		[Display(Name = "Move Queue Type")]
		public virtual String sMoveQueueType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

