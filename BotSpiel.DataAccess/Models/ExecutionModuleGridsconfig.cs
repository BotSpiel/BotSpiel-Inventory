using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public class ExecutionModuleGridsconfig 
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		[Display(Name = "Execution Module Grid ID")]
		public virtual Int64 ixExecutionModuleGrid { get; set; }
		[Display(Name = "Execution Module Grid ID")]
		public virtual Int64 ixExecutionModuleGridCreate { get; set; }
		[Display(Name = "Execution Module Grid")]
		public virtual String sExecutionModuleGrid { get; set; }
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
  

