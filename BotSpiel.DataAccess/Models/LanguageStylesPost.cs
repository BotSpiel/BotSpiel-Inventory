using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LanguageStylesPost : ILanguageStylesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Language Style")]
		public virtual String sLanguageStyle { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

