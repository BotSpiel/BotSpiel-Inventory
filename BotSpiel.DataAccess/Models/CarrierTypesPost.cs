using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CarrierTypesPost : ICarrierTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Carrier Type ID")]
		public virtual Int64 ixCarrierType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCarrierType", controller: "CarrierTypes", AdditionalFields = nameof(ixCarrierType))]
		[Display(Name = "Carrier Type")]
		public virtual String sCarrierType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

