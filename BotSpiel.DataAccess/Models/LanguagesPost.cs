using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LanguagesPost : ILanguagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyLanguage", controller: "Languages", AdditionalFields = nameof(ixLanguage))]
		[Display(Name = "Language")]
		public virtual String sLanguage { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Language Code")]
		public virtual String sLanguageCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

