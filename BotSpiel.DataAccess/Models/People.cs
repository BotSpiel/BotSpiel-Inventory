using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class People : IPeople
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public People()
        {
		Languages _Languages = new Languages();
		Languages = _Languages;

        }
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Display(Name = "Person ID")]
		public virtual Int64 ixPersonEdit { get; set; }
		[Required]
		[StringLength(300)]
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
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixLanguage")]
		public virtual Languages Languages { get; set; }
    }
}
  

