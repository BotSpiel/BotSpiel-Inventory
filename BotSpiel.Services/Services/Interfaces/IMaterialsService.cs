using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMaterialsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MaterialsPost GetPost(Int64 ixMaterial);        
		Materials Get(Int64 ixMaterial);
        IQueryable<Materials> Index();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<MaterialTypes> selectMaterialTypes();
       List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        bool VerifyMaterialUnique(Int64 ixMaterial, string sMaterial);
        List<string> VerifyMaterialDeleteOK(Int64 ixMaterial, string sMaterial);

        Task<Int64> Create(MaterialsPost materialsPost);
        Task Edit(MaterialsPost materialsPost);
        Task Delete(MaterialsPost materialsPost);
    }
}
  

