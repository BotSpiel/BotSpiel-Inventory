using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Languages : ILanguages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguageEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Language")]
		public virtual String sLanguage { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Language Code")]
		public virtual String sLanguageCode { get; set; }
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
    }
}
  

