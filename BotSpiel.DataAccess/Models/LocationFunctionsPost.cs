using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LocationFunctionsPost : ILocationFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Location Function ID")]
		public virtual Int64 ixLocationFunction { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyLocationFunction", controller: "LocationFunctions", AdditionalFields = nameof(ixLocationFunction))]
		[Display(Name = "Location Function")]
		public virtual String sLocationFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Location Function Code")]
		public virtual String sLocationFunctionCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

