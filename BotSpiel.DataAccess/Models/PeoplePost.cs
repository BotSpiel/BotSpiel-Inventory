using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PeoplePost : IPeoplePost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPerson", controller: "People", AdditionalFields = nameof(ixPerson))]
		[Display(Name = "Person")]
		public virtual String sPerson { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "First Name")]
		public virtual String sFirstName { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Last Name")]
		public virtual String sLastName { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

