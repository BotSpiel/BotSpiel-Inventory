using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.Services;

namespace BotSpiel.Services.Utilities
{
    public class CommonLookUps
    {
        private readonly IMoveQueueTypesService _movequeuetypesService;
        private readonly IMoveQueueContextsService _movequeuecontextsService;
        private readonly IStatusesService _statusesService;
        private readonly ILocationFunctionsService _locationfunctionsService;
        private readonly IInventoryUnitTransactionContextsService _inventoryunittransactioncontextsService;
        private readonly IInventoryStatesService _inventorystatesService;
        private readonly IHandlingUnitTypesService _handlingunittypesService;

        public CommonLookUps(
              IMoveQueueTypesService movequeuetypesService
            , IMoveQueueContextsService movequeuecontextsService
            , IStatusesService statusesService
            , ILocationFunctionsService locationfunctionsService
            , IInventoryUnitTransactionContextsService inventoryunittransactioncontextsService
            , IInventoryStatesService inventorystatesService
            , IHandlingUnitTypesService handlingunittypesService
            )
        {
            _movequeuetypesService = movequeuetypesService;
            _movequeuecontextsService = movequeuecontextsService;
            _statusesService = statusesService;
            _locationfunctionsService = locationfunctionsService;
            _inventoryunittransactioncontextsService = inventoryunittransactioncontextsService;
            _inventorystatesService = inventorystatesService;
            _handlingunittypesService = handlingunittypesService;
        }

        public List<HandlingUnitTypes> getHandlingUnitTypes()
        {
            return _handlingunittypesService.IndexDb().ToList();
        }

        public List<MoveQueueTypes> getMoveQueueTypes()
        {
            return _movequeuetypesService.IndexDb().ToList();
        }

        public List<MoveQueueContexts> getMoveQueueContexts()
        {
            return _movequeuecontextsService.IndexDb().ToList();
        }

        public List<Statuses> getStatuses()
        {
            return _statusesService.IndexDb().ToList();
        }

        public List<LocationFunctions> getLocationFunctions()
        {
            return _locationfunctionsService.IndexDb().ToList();
        }

        public List<InventoryUnitTransactionContexts> getInventoryUnitTransactionContext()
        {
            return _inventoryunittransactioncontextsService.IndexDb().ToList();
        }

        public List<LocationFunctions> getPickAndPlaceLocationFunctions()
        {
            return _locationfunctionsService.IndexDb().Where(x =>
                x.sLocationFunctionCode == "RV" ||
                x.sLocationFunctionCode == "LD" ||
                x.sLocationFunctionCode == "FP"
                ).ToList();
        }

        public List<InventoryStates> getInventoryStates()
        {
            return _inventorystatesService.IndexDb().ToList();
        }

        public List<InventoryStates> getAvailableInventoryStates()
        {
            return _inventorystatesService.IndexDb()
                .Where(x => !x.sInventoryState.Contains("Unavailable"))
                .ToList();
        }

        public List<InventoryStates> getUnavailableInventoryStates()
        {
            return _inventorystatesService.IndexDb()
                .Where(x => x.sInventoryState.Contains("Unavailable"))
                .ToList();
        }

    }
}
