using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CarrierServicesPost : ICarrierServicesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Carrier Service ID")]
		public virtual Int64 ixCarrierService { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCarrierService", controller: "CarrierServices", AdditionalFields = nameof(ixCarrierService))]
		[Display(Name = "Carrier Service")]
		public virtual String sCarrierService { get; set; }
		[Required]
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

