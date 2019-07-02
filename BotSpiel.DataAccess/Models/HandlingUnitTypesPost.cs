using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class HandlingUnitTypesPost : IHandlingUnitTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyHandlingUnitType", controller: "HandlingUnitTypes", AdditionalFields = nameof(ixHandlingUnitType))]
		[Display(Name = "Handling Unit Type")]
		public virtual String sHandlingUnitType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

