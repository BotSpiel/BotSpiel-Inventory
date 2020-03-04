using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Repositories;

namespace BotSpiel.DataAccess.Utilities
{
    public class VolumeAndWeight
    {
        private readonly IUnitOfMeasurementConversionsRepository _unitofmeasurementconversionsRepository;
        private readonly IInventoryUnitsRepository _inventoryunitsRepository;
        private readonly IInventoryLocationsRepository _inventorylocationsRepository;
        private readonly IHandlingUnitsRepository _handlingunitsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IInventoryUnitTransactionsRepository _inventoryunittransactionsRepository;

        public VolumeAndWeight(
            IUnitOfMeasurementConversionsRepository unitofmeasurementconversionsRepository, 
            IInventoryUnitsRepository inventoryunitsRepository, 
            IInventoryLocationsRepository inventorylocationsRepository,
            IHandlingUnitsRepository handlingunitsRepository,
            IMaterialsRepository materialsRepository,
            IInventoryUnitTransactionsRepository inventoryunittransactionsRepository
            )
        {
            _unitofmeasurementconversionsRepository = unitofmeasurementconversionsRepository;
            _inventoryunitsRepository = inventoryunitsRepository;
            _inventorylocationsRepository = inventorylocationsRepository;
            _handlingunitsRepository = handlingunitsRepository;
            _materialsRepository = materialsRepository;
            _inventoryunittransactionsRepository = inventoryunittransactionsRepository;
        }

        public bool inventoryUnitWillFitLocation(InventoryUnitsPost inventoryUnitsPost, InventoryLocationsPost inventoryLocationsPost)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                var inventoryUnitDimensions = getInventoryUnitDimensions(inventoryUnitsPost);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, 0, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, 0, _unitofmeasurementconversionsRepository, inventoryUnitsPost.nBaseUnitQuantity);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);
                if (spaceToFitInto.CompareTo(spaceToFit) == 1)
                { return true; }
                else
                { return false; }
            }
            else
            { return true; }
        }

        public bool handlingUnitWillFitLocation(Int64 ixHandlingUnit, List<InventoryUnits> inventoryUnitsOnHandlingUnit, InventoryLocationsPost inventoryLocationsPost)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                Double nVolumeToFit = 0;
                inventoryUnitsOnHandlingUnit.ForEach(x =>
                {
                    nVolumeToFit += convertToCommonUnitValue(x.Materials.nLength ?? 0, x.Materials.ixLengthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nWidth ?? 0, x.Materials.ixWidthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nHeight ?? 0, x.Materials.ixHeightUnit ?? 0) * x.nBaseUnitQuantity;
                }
                );

                var inventoryUnitDimensions = getHandlingUnitDimensions(ixHandlingUnit, inventoryUnitsOnHandlingUnit);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, nVolumeToFit, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, _unitofmeasurementconversionsRepository.UnitsOfMeasurementDb().Where(x => x.sUnitOfMeasurement == "Cubic meter").Select(x => x.ixUnitOfMeasurement).FirstOrDefault(), _unitofmeasurementconversionsRepository, 1);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);
                if (spaceToFitInto.CompareTo(spaceToFit) == 1)
                { return true; }
                else
                { return false; }
            }
            else
            { return true; }
        }

        public Double getNewLocationUtilisationPercent(InventoryUnitsPost inventoryUnitsPost, InventoryLocationsPost inventoryLocationsPost)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                bool bInventoryUnitsExists = false;
                var inventoryUnitDimensions = getInventoryUnitDimensions(inventoryUnitsPost);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, 0, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, 0, _unitofmeasurementconversionsRepository, inventoryUnitsPost.nBaseUnitQuantity);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);
                //Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);


                if (_inventoryunitsRepository.IndexDb().Where(x => x.ixInventoryUnit == inventoryUnitsPost.ixInventoryUnit && x.ixInventoryLocation == inventoryLocationsPost.ixInventoryLocation).Any()) //The existing inventory unit is being adjusted
                {
                    var inventoryUnitsPostCurrent = _inventoryunitsRepository.GetPost(inventoryUnitsPost.ixInventoryUnit);
                    var inventoryUnitDimensionsCurrent = getInventoryUnitDimensions(inventoryUnitsPostCurrent);
                    Space spaceToFitCurrent = new Space(inventoryUnitDimensionsCurrent.nLength, inventoryUnitDimensionsCurrent.nWidth, inventoryUnitDimensionsCurrent.nHeight, 0, inventoryUnitDimensionsCurrent.ixLengthUnit, inventoryUnitDimensionsCurrent.ixWidthUnit, inventoryUnitDimensionsCurrent.ixHeightUnit, 0, _unitofmeasurementconversionsRepository, inventoryUnitsPostCurrent.nBaseUnitQuantity);
                    bInventoryUnitsExists = true;

                    if ((spaceToFitInto._nUsableVolume > 0) && (bInventoryUnitsExists))
                    {
                        Double nNewUtilisationPercent = (inventoryLocationsPost.nUtilisationPercent ?? 0) + (((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0) - ((spaceToFitCurrent._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                            return nNewUtilisationPercent;
                    }
                    else
                    {
                        return inventoryLocationsPost.nUtilisationPercent ?? 0;
                    }

                }


                if ((spaceToFitInto._nUsableVolume > 0) && (!bInventoryUnitsExists))
                {
                    return ((inventoryLocationsPost.nUtilisationPercent ?? 0) + ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                }
                else
                {
                    return inventoryLocationsPost.nUtilisationPercent ?? 0;
                }
            }
            else
            { return inventoryLocationsPost.nUtilisationPercent ?? 0; }
        }

        public Double getNewLocationUtilisationPercent(Int64 ixHandlingUnit, List<InventoryUnits> inventoryUnitsOnHandlingUnit, InventoryLocationsPost inventoryLocationsPost, bool bIsSource)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                Double nVolumeToFit = 0;
                inventoryUnitsOnHandlingUnit.ForEach(x =>
                {
                    nVolumeToFit += convertToCommonUnitValue(x.Materials.nLength ?? 0, x.Materials.ixLengthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nWidth ?? 0, x.Materials.ixWidthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nHeight ?? 0, x.Materials.ixHeightUnit ?? 0) * x.nBaseUnitQuantity;
                }
                );

                var inventoryUnitDimensions = getHandlingUnitDimensions(ixHandlingUnit, inventoryUnitsOnHandlingUnit);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, nVolumeToFit, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, _unitofmeasurementconversionsRepository.UnitsOfMeasurementDb().Where(x => x.sUnitOfMeasurement == "Cubic meter").Select(x => x.ixUnitOfMeasurement).FirstOrDefault(), _unitofmeasurementconversionsRepository, 1);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);
                //Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);

                if (bIsSource)
                {
                    return ((inventoryLocationsPost.nUtilisationPercent ?? 0) - ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                }
                else
                {
                    return ((inventoryLocationsPost.nUtilisationPercent ?? 0) + ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                }
            }
            else
            { return inventoryLocationsPost.nUtilisationPercent ?? 0; }
        }

        public Double getNewLocationQueuedUtilisationPercent(InventoryUnitsPost inventoryUnitsPost, InventoryLocationsPost inventoryLocationsPost, bool bAdd)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                bool bInventoryUnitsExists = false;
                var inventoryUnitDimensions = getInventoryUnitDimensions(inventoryUnitsPost);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, 0, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, 0, _unitofmeasurementconversionsRepository, inventoryUnitsPost.nBaseUnitQuantity);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);


                if (_inventoryunitsRepository.IndexDb().Where(x => x.ixInventoryUnit == inventoryUnitsPost.ixInventoryUnit && x.ixInventoryLocation == inventoryLocationsPost.ixInventoryLocation).Any()) //The existing inventory unit is being adjusted
                {
                    var inventoryUnitsPostCurrent = _inventoryunitsRepository.GetPost(inventoryUnitsPost.ixInventoryUnit);
                    var inventoryUnitDimensionsCurrent = getInventoryUnitDimensions(inventoryUnitsPostCurrent);
                    Space spaceToFitCurrent = new Space(inventoryUnitDimensionsCurrent.nLength, inventoryUnitDimensionsCurrent.nWidth, inventoryUnitDimensionsCurrent.nHeight, 0, inventoryUnitDimensionsCurrent.ixLengthUnit, inventoryUnitDimensionsCurrent.ixWidthUnit, inventoryUnitDimensionsCurrent.ixHeightUnit, 0, _unitofmeasurementconversionsRepository, inventoryUnitsPostCurrent.nBaseUnitQuantity);
                    bInventoryUnitsExists = true;

                    if ((spaceToFitInto._nUsableVolume > 0) && (bInventoryUnitsExists))
                    {
                        Double nNewUtilisationPercent = (inventoryLocationsPost.nQueuedUtilisationPercent ?? 0) + (((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0) - ((spaceToFitCurrent._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                        return nNewUtilisationPercent;
                    }
                    else
                    {
                        return inventoryLocationsPost.nQueuedUtilisationPercent ?? 0;
                    }

                }


                if ((spaceToFitInto._nUsableVolume > 0) && (!bInventoryUnitsExists))
                {
                    if (bAdd)
                    {
                        return ((inventoryLocationsPost.nQueuedUtilisationPercent ?? 0) + ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                    }
                    else
                    {
                        return ((inventoryLocationsPost.nQueuedUtilisationPercent ?? 0) - ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                    }
                }
                else
                {
                    return inventoryLocationsPost.nQueuedUtilisationPercent ?? 0;
                }
            }
            else
            { return inventoryLocationsPost.nQueuedUtilisationPercent ?? 0; }
        }

        public Double getNewLocationQueuedUtilisationPercent(Int64 ixHandlingUnit, List<InventoryUnits> inventoryUnitsOnHandlingUnit, InventoryLocationsPost inventoryLocationsPost, bool bAdd)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                Double nVolumeToFit = 0;
                inventoryUnitsOnHandlingUnit.ForEach(x =>
                {
                    nVolumeToFit += convertToCommonUnitValue(x.Materials.nLength ?? 0, x.Materials.ixLengthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nWidth ?? 0, x.Materials.ixWidthUnit ?? 0) *
                    convertToCommonUnitValue(x.Materials.nHeight ?? 0, x.Materials.ixHeightUnit ?? 0) * x.nBaseUnitQuantity;
                }
                );

                var inventoryUnitDimensions = getHandlingUnitDimensions(ixHandlingUnit, inventoryUnitsOnHandlingUnit);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nLength, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, nVolumeToFit, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, _unitofmeasurementconversionsRepository.UnitsOfMeasurementDb().Where(x => x.sUnitOfMeasurement == "Cubic meter").Select(x => x.ixUnitOfMeasurement).FirstOrDefault(), _unitofmeasurementconversionsRepository, 1);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume - inventoryLocations.InventoryLocationSizes.nUsableVolume * ((inventoryLocations.nUtilisationPercent ?? 0) + (inventoryLocations.nQueuedUtilisationPercent ?? 0)) / 100.0, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository, 1);

                if (bAdd)
                {
                    return ((inventoryLocationsPost.nQueuedUtilisationPercent ?? 0) + ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                }
                else
                {
                    return ((inventoryLocationsPost.nQueuedUtilisationPercent ?? 0) - ((spaceToFit._nUsableVolume / spaceToFitInto._nUsableVolume) * 100.0));
                }

            }
            else
            { return inventoryLocationsPost.nQueuedUtilisationPercent ?? 0; }
        }

        public (Double nLength, Double nWidth, Double nHeight, Int64 ixLengthUnit, Int64 ixWidthUnit, Int64 ixHeightUnit) getInventoryUnitDimensions(InventoryUnitsPost inventoryUnitsPost)
        {
            //var inventoryUnit = _inventoryunitsRepository.Get(inventoryUnitsPost.ixInventoryUnit);

            var _material = _materialsRepository.Get(inventoryUnitsPost.ixMaterial);

            // Does IU have a HU? and we have to make sure that the Handling UNIT is not broken
            if ((inventoryUnitsPost.ixHandlingUnit > 0) && (_inventoryunittransactionsRepository.IndexDb().Where(x => x.ixHandlingUnitAfter == inventoryUnitsPost.ixHandlingUnit).Count() < 2 ))
            {
                var handlingUnit = _handlingunitsRepository.Get(inventoryUnitsPost.ixHandlingUnit ?? 0);
                if (handlingUnit.nLength > 0 && handlingUnit.nWidth > 0 && handlingUnit.nHeight > 0 && handlingUnit.ixLengthUnit > 0 && handlingUnit.ixWidthUnit > 0 && handlingUnit.ixHeightUnit > 0)
                {
                    // The HU has dimensions we use it
                    return (handlingUnit.nLength ?? 0, handlingUnit.nWidth ?? 0, handlingUnit.nHeight ?? 0, handlingUnit.ixLengthUnit ?? 0, handlingUnit.ixWidthUnit ?? 0, handlingUnit.ixHeightUnit ?? 0);
                }
                // Does the HU have a MHC
                else if (handlingUnit.ixMaterialHandlingUnitConfiguration > 0 && (handlingUnit.MaterialHandlingUnitConfigurations.nLength > 0 && handlingUnit.MaterialHandlingUnitConfigurations.nWidth > 0 && handlingUnit.MaterialHandlingUnitConfigurations.nHeight > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixLengthUnit > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixWidthUnit > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixHeightUnit > 0))
                {
                    // The MHC has dimensions we use it
                    return (handlingUnit.MaterialHandlingUnitConfigurations.nLength ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.nWidth ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.nHeight ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixLengthUnit ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixWidthUnit ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixHeightUnit ?? 0);
                }
                // Does HU have a packing material?
                else if (handlingUnit.ixPackingMaterial > 0 && (handlingUnit.MaterialsFKDiffPackingMaterial.nLength > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.nWidth > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.nHeight > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixLengthUnit > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixWidthUnit > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixHeightUnit > 0))
                {
                    // The packing material has dimensions we use it
                    return (handlingUnit.MaterialsFKDiffPackingMaterial.nLength ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.nWidth ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.nHeight ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixLengthUnit ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixWidthUnit ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixHeightUnit ?? 0);
                }
                else if (_material.nLength > 0 && _material.nWidth > 0 && _material.nHeight > 0 && _material.ixLengthUnit > 0 && _material.ixWidthUnit > 0 && _material.ixHeightUnit > 0)
                {
                    // The material has unit dimensions we use it
                    return (_material.nLength ?? 0, _material.nWidth ?? 0, _material.nHeight ?? 0, _material.ixLengthUnit ?? 0, _material.ixWidthUnit ?? 0, _material.ixHeightUnit ?? 0);

                }
            }
            else if (_material.nLength > 0 && _material.nWidth > 0 && _material.nHeight > 0 && _material.ixLengthUnit > 0 && _material.ixWidthUnit > 0 && _material.ixHeightUnit > 0)
            {
                // The material has unit dimensions we use it
                return (_material.nLength ?? 0, _material.nWidth ?? 0, _material.nHeight ?? 0, _material.ixLengthUnit ?? 0, _material.ixWidthUnit ?? 0, _material.ixHeightUnit ?? 0);

            }
            else
            {
                return (0, 0, 0, 0, 0, 0);
            }
            return (0, 0, 0, 0, 0, 0);
        }

        public (Double nLength, Double nWidth, Double nHeight, Int64 ixLengthUnit, Int64 ixWidthUnit, Int64 ixHeightUnit) getHandlingUnitDimensions(Int64 ixHandlingUnit, List<InventoryUnits> inventoryUnitsOnHandlingUnit )
        {
            var materials = inventoryUnitsOnHandlingUnit.GroupBy(x => x.ixMaterial)
                            .Select(y =>
                            new
                            {
                                ixMaterial = y.Key,
                                nTotalBaseUnits = y.Sum(u => u.nBaseUnitQuantity)
                            }
                            ).ToList();

            var _material = _materialsRepository.Get(materials.OrderByDescending(x => x.nTotalBaseUnits).Select(x => x.ixMaterial).FirstOrDefault());

            // Does IU have a HU? and we have to make sure that the Handling UNIT is not broken
            if ((ixHandlingUnit > 0) && (_inventoryunittransactionsRepository.IndexDb().Where(x => x.ixHandlingUnitAfter == ixHandlingUnit).Count() < 2))
            {
                var handlingUnit = _handlingunitsRepository.Get(ixHandlingUnit);
                if (handlingUnit.nLength > 0 && handlingUnit.nWidth > 0 && handlingUnit.nHeight > 0 && handlingUnit.ixLengthUnit > 0 && handlingUnit.ixWidthUnit > 0 && handlingUnit.ixHeightUnit > 0)
                {
                    // The HU has dimensions we use it
                    return (handlingUnit.nLength ?? 0, handlingUnit.nWidth ?? 0, handlingUnit.nHeight ?? 0, handlingUnit.ixLengthUnit ?? 0, handlingUnit.ixWidthUnit ?? 0, handlingUnit.ixHeightUnit ?? 0);
                }
                // Does the HU have a MHC
                else if (handlingUnit.ixMaterialHandlingUnitConfiguration > 0 && (handlingUnit.MaterialHandlingUnitConfigurations.nLength > 0 && handlingUnit.MaterialHandlingUnitConfigurations.nWidth > 0 && handlingUnit.MaterialHandlingUnitConfigurations.nHeight > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixLengthUnit > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixWidthUnit > 0 && handlingUnit.MaterialHandlingUnitConfigurations.ixHeightUnit > 0))
                {
                    // The MHC has dimensions we use it
                    return (handlingUnit.MaterialHandlingUnitConfigurations.nLength ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.nWidth ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.nHeight ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixLengthUnit ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixWidthUnit ?? 0, handlingUnit.MaterialHandlingUnitConfigurations.ixHeightUnit ?? 0);
                }
                // Does HU have a packing material?
                else if (handlingUnit.ixPackingMaterial > 0 && (handlingUnit.MaterialsFKDiffPackingMaterial.nLength > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.nWidth > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.nHeight > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixLengthUnit > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixWidthUnit > 0 && handlingUnit.MaterialsFKDiffPackingMaterial.ixHeightUnit > 0))
                {
                    // The packing material has dimensions we use it
                    return (handlingUnit.MaterialsFKDiffPackingMaterial.nLength ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.nWidth ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.nHeight ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixLengthUnit ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixWidthUnit ?? 0, handlingUnit.MaterialsFKDiffPackingMaterial.ixHeightUnit ?? 0);
                }
                else if (_material.nLength > 0 && _material.nWidth > 0 && _material.nHeight > 0 && _material.ixLengthUnit > 0 && _material.ixWidthUnit > 0 && _material.ixHeightUnit > 0)
                {
                    return (_material.nLength ?? 0, _material.nWidth ?? 0, _material.nHeight ?? 0, _material.ixLengthUnit ?? 0, _material.ixWidthUnit ?? 0, _material.ixHeightUnit ?? 0);
                }
            }
            else if (_material.nLength > 0 && _material.nWidth > 0 && _material.nHeight > 0 && _material.ixLengthUnit > 0 && _material.ixWidthUnit > 0 && _material.ixHeightUnit > 0)
            {
                return (_material.nLength ?? 0, _material.nWidth ?? 0, _material.nHeight ?? 0, _material.ixLengthUnit ?? 0, _material.ixWidthUnit ?? 0, _material.ixHeightUnit ?? 0);

            }
            else
            {
                return (0, 0, 0, 0, 0, 0);
            }
            return (0, 0, 0, 0, 0, 0);
        }

        public Double convertToCommonUnitValue(Double nValue, Int64 ixUnit)
        {
            if (_unitofmeasurementconversionsRepository.Index().Where(x => x.ixUnitOfMeasurementFrom == ixUnit).Any())
            {
                return _unitofmeasurementconversionsRepository.Index().Where(x => x.ixUnitOfMeasurementFrom == ixUnit).Select(x => x.nMultiplier).FirstOrDefault() * nValue;
            }
            else
            {
                return 0;
            }
        }

    }

    public class Space : IComparable<Space>
    {
        private readonly IUnitOfMeasurementConversionsRepository _unitofmeasurementconversionsRepository;
        public Space(Double nLength, Double nWidth, Double nHeight, Double nUsableVolume, Int64 ixLengthUnit, Int64 ixWidthUnit, Int64 ixHeightUnit, Int64 ixUsableVolumeUnit, IUnitOfMeasurementConversionsRepository unitofmeasurementconversionsRepository, Double nUnitMultiplier)
        {
            _unitofmeasurementconversionsRepository = unitofmeasurementconversionsRepository;
            _nLength = convertToCommonUnitValue(nLength, ixLengthUnit);
            _nWidth = convertToCommonUnitValue(nWidth, ixWidthUnit);
            _nHeight = convertToCommonUnitValue(nHeight, ixHeightUnit);
            if (convertToCommonUnitValue(nUsableVolume, ixUsableVolumeUnit) > 0)
            {
                _nUsableVolume = convertToCommonUnitValue(nUsableVolume, ixUsableVolumeUnit) * nUnitMultiplier;
            }
            else
            {
                _nUsableVolume = (_nLength * _nWidth * _nHeight) * nUnitMultiplier;
            }
        }

        public virtual Double _nLength { get; set; }
        public virtual Double _nWidth { get; set; }
        public virtual Double _nHeight { get; set; }
        public virtual Double _nUsableVolume { get; set; }

        public int CompareTo(Space otherSpace)
        {
            int fitOK = -1;
            if (otherSpace == null) return 1;
            // If we have space we confirm the fit in greater detail otherwise return -1
            if (_nUsableVolume >= otherSpace._nUsableVolume)
            {
                //We check in greater detail
                //First we attempt a basic fit 
                if (_nLength >= otherSpace._nLength && _nHeight >= otherSpace._nHeight && _nWidth >= otherSpace._nWidth)
                {
                    return 1;
                }
                else //We try all the rotations
                {
                    var allRotations = getAllRotations(otherSpace);

                    allRotations.ForEach(x =>
                        {
                            if (_nLength >= x.Item1 && _nHeight >= x.Item2 && _nWidth >= x.Item3)
                            {
                                fitOK = 1;
                            }
                        }
                        );
                    if (fitOK == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                return -1;
            }
            // We check if the volume is available and also if any of the LWH permutions fit

            //return -1;
        }

        public Double convertToCommonUnitValue(Double nValue, Int64 ixUnit)
        {
            if (_unitofmeasurementconversionsRepository.Index().Where(x => x.ixUnitOfMeasurementFrom == ixUnit).Any())
            {
                return _unitofmeasurementconversionsRepository.Index().Where(x => x.ixUnitOfMeasurementFrom == ixUnit).Select(x => x.nMultiplier).FirstOrDefault() * nValue;
            }
            else
            {
                return 0;
            }
        }

        public List<Tuple<Double, Double, Double>> getAllRotations(Space space)
        {
            List<Tuple<Double, Double, Double>> rotations = new List<Tuple<double, double, double>>();
                   
            rotations.Add(new Tuple<double, double, double>(space._nHeight, space._nLength, space._nWidth)); //hlw
            rotations.Add(new Tuple<double, double, double>(space._nHeight, space._nWidth, space._nLength)); //hwl
            rotations.Add(new Tuple<double, double, double>(space._nLength, space._nHeight, space._nWidth)); //lhw
            rotations.Add(new Tuple<double, double, double>(space._nLength, space._nWidth, space._nHeight)); //lwh
            rotations.Add(new Tuple<double, double, double>(space._nWidth, space._nHeight, space._nLength)); //whl
            rotations.Add(new Tuple<double, double, double>(space._nWidth, space._nLength, space._nHeight)); //wlh

            return rotations;
        }


    }


    }
