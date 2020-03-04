using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMaterialsRepository
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
        IQueryable<Materials> IndexDb();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<MaterialTypes> selectMaterialTypes();
       IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        IQueryable<MaterialTypes> MaterialTypesDb();
       List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        bool VerifyMaterialUnique(Int64 ixMaterial, string sMaterial);
        List<string> VerifyMaterialDeleteOK(Int64 ixMaterial, string sMaterial);
        void RegisterCreate(MaterialsPost materialsPost);
        void RegisterEdit(MaterialsPost materialsPost);
        void RegisterDelete(MaterialsPost materialsPost);
        void Rollback();
        void Commit();
    }
}
  

