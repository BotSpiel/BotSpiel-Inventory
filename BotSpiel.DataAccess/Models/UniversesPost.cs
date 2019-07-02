using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class UniversesPost : IUniversesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Universe ID")]
		public virtual Int64 ixUniverse { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyUniverse", controller: "Universes", AdditionalFields = nameof(ixUniverse))]
		[Display(Name = "Universe")]
		public virtual String sUniverse { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

