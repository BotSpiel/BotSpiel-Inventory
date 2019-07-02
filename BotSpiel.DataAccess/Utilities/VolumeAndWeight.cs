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

        public VolumeAndWeight(
            IUnitOfMeasurementConversionsRepository unitofmeasurementconversionsRepository, 
            IInventoryUnitsRepository inventoryunitsRepository, 
            IInventoryLocationsRepository inventorylocationsRepository,
            IHandlingUnitsRepository handlingunitsRepository
            )
        {
            _unitofmeasurementconversionsRepository = unitofmeasurementconversionsRepository;
            _inventoryunitsRepository = inventoryunitsRepository;
            _inventorylocationsRepository = inventorylocationsRepository;
            _handlingunitsRepository = handlingunitsRepository;
        }

        public bool inventoryUnitWillFitLocation(InventoryUnitsPost inventoryUnitsPost, InventoryLocationsPost inventoryLocationsPost)
        {
            if (inventoryLocationsPost.bTrackUtilisation && inventoryLocationsPost.ixInventoryLocationSize > 0)
            {
                var inventoryUnitDimensions = getInventoryUnitDimensions(inventoryUnitsPost);
                var inventoryLocations = _inventorylocationsRepository.Get(inventoryLocationsPost.ixInventoryLocation);
                Space spaceToFit = new Space(inventoryUnitDimensions.nHeight, inventoryUnitDimensions.nWidth, inventoryUnitDimensions.nHeight, 0, inventoryUnitDimensions.ixLengthUnit, inventoryUnitDimensions.ixWidthUnit, inventoryUnitDimensions.ixHeightUnit, 0, _unitofmeasurementconversionsRepository);
                Space spaceToFitInto = new Space(inventoryLocations.InventoryLocationSizes.nLength, inventoryLocations.InventoryLocationSizes.nWidth, inventoryLocations.InventoryLocationSizes.nHeight, inventoryLocations.InventoryLocationSizes.nUsableVolume, inventoryLocations.InventoryLocationSizes.ixLengthUnit, inventoryLocations.InventoryLocationSizes.ixWidthUnit, inventoryLocations.InventoryLocationSizes.ixHeightUnit, inventoryLocations.InventoryLocationSizes.ixUsableVolumeUnit, _unitofmeasurementconversionsRepository);
                if (spaceToFitInto.CompareTo(spaceToFit) == 1)
                { return true; }
                else
                { return false; }
            }
            else
            { return true; }
        }

        public (Double nLength, Double nWidth, Double nHeight, Int64 ixLengthUnit, Int64 ixWidthUnit, Int64 ixHeightUnit) getInventoryUnitDimensions(InventoryUnitsPost inventoryUnitsPost)
        {
            var inventoryUnit = _inventoryunitsRepository.Get(inventoryUnitsPost.ixInventoryUnit);

            // Does IU have a HU?
            if (inventoryUnitsPost.ixHandlingUnit > 0)
            {
                var handlingUnit = _handlingunitsRepository.Get(inventoryUnitsPost.ixHandlingUnit ?? 0);
                if (inventoryUnit.HandlingUnits.nLength > 0 && inventoryUnit.HandlingUnits.nWidth > 0 && inventoryUnit.HandlingUnits.nHeight > 0 && inventoryUnit.HandlingUnits.ixLengthUnit > 0 && inventoryUnit.HandlingUnits.ixWidthUnit > 0 && inventoryUnit.HandlingUnits.ixHeightUnit > 0)
                {
                    // The HU has dimensions we use it
                    return (inventoryUnit.HandlingUnits.nLength ?? 0, inventoryUnit.HandlingUnits.nWidth ?? 0, inventoryUnit.HandlingUnits.nHeight ?? 0, inventoryUnit.HandlingUnits.ixLengthUnit ?? 0, inventoryUnit.HandlingUnits.ixWidthUnit ?? 0, inventoryUnit.HandlingUnits.ixHeightUnit ?? 0);
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
            }
            else if (inventoryUnit.Materials.nLength > 0 && inventoryUnit.Materials.nWidth > 0 && inventoryUnit.Materials.nHeight > 0 && inventoryUnit.Materials.ixLengthUnit > 0 && inventoryUnit.Materials.ixWidthUnit > 0 && inventoryUnit.Materials.ixHeightUnit > 0)
            {
                // The material has unit dimensions we use it
                return (inventoryUnit.Materials.nLength * inventoryUnit.nBaseUnitQuantity ?? 0, inventoryUnit.Materials.nWidth * inventoryUnit.nBaseUnitQuantity ?? 0, inventoryUnit.Materials.nHeight * inventoryUnit.nBaseUnitQuantity ?? 0, inventoryUnit.Materials.ixLengthUnit ?? 0, inventoryUnit.Materials.ixWidthUnit ?? 0, inventoryUnit.Materials.ixHeightUnit ?? 0);
            }
            else
            {
                return (0, 0, 0, 0, 0, 0);
            }
            return (0, 0, 0, 0, 0, 0);
        }

    }

    public class Space : IComparable<Space>
    {
        private readonly IUnitOfMeasurementConversionsRepository _unitofmeasurementconversionsRepository;
        public Space(Double nLength, Double nWidth, Double nHeight, Double nUsableVolume, Int64 ixLengthUnit, Int64 ixWidthUnit, Int64 ixHeightUnit, Int64 ixUsableVolumeUnit, IUnitOfMeasurementConversionsRepository unitofmeasurementconversionsRepository)
        {
            _unitofmeasurementconversionsRepository = unitofmeasurementconversionsRepository;
            _nLength = convertToCommonUnitValue(nLength, ixLengthUnit);
            _nWidth = convertToCommonUnitValue(nWidth, ixWidthUnit);
            _nHeight = convertToCommonUnitValue(nHeight, ixHeightUnit);
            if (convertToCommonUnitValue(nUsableVolume, ixUsableVolumeUnit) > 0)
            {
                _nUsableVolume = convertToCommonUnitValue(nUsableVolume, ixUsableVolumeUnit);
            }
            else
            {
                _nUsableVolume = _nLength * _nWidth * _nHeight;
            }
        }

        public virtual Double _nLength { get; set; }
        public virtual Double _nWidth { get; set; }
        public virtual Double _nHeight { get; set; }
        public virtual Double _nUsableVolume { get; set; }

        public int CompareTo(Space otherSpace)
        {
            if (otherSpace == null) return 1;
            // We check if the volume is available and also if any of the LWH permutions fit
            if (_nLength >= otherSpace._nLength && _nHeight >= otherSpace._nHeight && _nWidth >= otherSpace._nWidth) return 1;
            return -1;
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

            
            
            
            //lwh
            //whl
            //wlh
            rotations.Add(new Tuple<double, double, double>(space._nHeight, space._nLength, space._nWidth)); //hlw
            rotations.Add(new Tuple<double, double, double>(space._nHeight, space._nWidth, space._nLength)); //hwl
            rotations.Add(new Tuple<double, double, double>(space._nLength, space._nHeight, space._nWidth)); //lhw

            return rotations;
        }


    }


    }
