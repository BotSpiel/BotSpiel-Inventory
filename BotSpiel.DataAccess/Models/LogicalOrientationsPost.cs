using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LogicalOrientationsPost : ILogicalOrientationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Logical Orientation ID")]
		public virtual Int64 ixLogicalOrientation { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyLogicalOrientation", controller: "LogicalOrientations", AdditionalFields = nameof(ixLogicalOrientation))]
		[Display(Name = "Logical Orientation")]
		public virtual String sLogicalOrientation { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

