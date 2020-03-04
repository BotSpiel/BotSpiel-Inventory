using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public class BotModuleGridsconfig 
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		[Display(Name = "Bot Module Grid ID")]
		public virtual Int64 ixBotModuleGrid { get; set; }
		[Display(Name = "Bot Module Grid ID")]
		public virtual Int64 ixBotModuleGridCreate { get; set; }
		[Display(Name = "Bot Module Grid")]
		public virtual String sBotModuleGrid { get; set; }
		[Display(Name = "Short Description")]
		public virtual String sShortDescription { get; set; }
		[Display(Name = "Data Entity Type")]
		public virtual String sDataEntityType { get; set; }
		[Display(Name = "Can Create")]
		public virtual Boolean bCanCreate { get; set; }
		[Display(Name = "Can Edit")]
		public virtual Boolean bCanEdit { get; set; }
		[Display(Name = "Can Delete")]
		public virtual Boolean bCanDelete { get; set; }
    }
}
  

