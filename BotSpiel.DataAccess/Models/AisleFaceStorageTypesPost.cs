using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class AisleFaceStorageTypesPost : IAisleFaceStorageTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Aisle Face Storage Type ID")]
		public virtual Int64 ixAisleFaceStorageType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyAisleFaceStorageType", controller: "AisleFaceStorageTypes", AdditionalFields = nameof(ixAisleFaceStorageType))]
		[Display(Name = "Aisle Face Storage Type")]
		public virtual String sAisleFaceStorageType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

