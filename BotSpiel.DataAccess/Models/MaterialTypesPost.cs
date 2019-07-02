using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MaterialTypesPost : IMaterialTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Material Type ID")]
		public virtual Int64 ixMaterialType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMaterialType", controller: "MaterialTypes", AdditionalFields = nameof(ixMaterialType))]
		[Display(Name = "Material Type")]
		public virtual String sMaterialType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

