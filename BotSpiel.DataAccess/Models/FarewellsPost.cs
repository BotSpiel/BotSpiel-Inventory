using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FarewellsPost : IFarewellsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Farewell ID")]
		public virtual Int64 ixFarewell { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Farewell")]
		public virtual String sFarewell { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Farewell Offered")]
		public virtual String sFarewellOffered { get; set; }
		[Required]
		[Display(Name = "Farewell Response")]
		public virtual String sFarewellResponse { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

