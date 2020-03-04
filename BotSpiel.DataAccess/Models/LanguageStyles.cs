using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LanguageStyles : ILanguageStyles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyleEdit { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Language Style")]
		public virtual String sLanguageStyle { get; set; }
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
  

