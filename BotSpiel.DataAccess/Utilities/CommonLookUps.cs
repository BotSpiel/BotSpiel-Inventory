using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.DataAccess.Utilities
{
    public class CommonLookUpsRepository
    {
        private readonly IMoveQueueTypesRepository _movequeuetypesRepository;
        private readonly IMoveQueueContextsRepository _movequeuecontextsRepository;
        private readonly IStatusesRepository _statusesRepository;
        private readonly ILocationFunctionsRepository _locationfunctionsRepository;
        private readonly IInventoryUnitTransactionContextsRepository _inventoryunittransactioncontextsRepository;
        private readonly IInventoryStatesRepository _inventorystatesRepository;
        private readonly IHandlingUnitTypesRepository _handlingunittypesRepository;

        public CommonLookUpsRepository(
              IMoveQueueTypesRepository movequeuetypesRepository
            , IMoveQueueContextsRepository movequeuecontextsRepository
            , IStatusesRepository statusesRepository
            , ILocationFunctionsRepository locationfunctionsRepository
            , IInventoryUnitTransactionContextsRepository inventoryunittransactioncontextsRepository
            , IInventoryStatesRepository inventorystatesRepository
            , IHandlingUnitTypesRepository handlingunittypesRepository
            )
        {
            _movequeuetypesRepository = movequeuetypesRepository;
            _movequeuecontextsRepository = movequeuecontextsRepository;
            _statusesRepository = statusesRepository;
            _locationfunctionsRepository = locationfunctionsRepository;
            _inventoryunittransactioncontextsRepository = inventoryunittransactioncontextsRepository;
            _inventorystatesRepository = inventorystatesRepository;
            _handlingunittypesRepository = handlingunittypesRepository;
        }

        public List<HandlingUnitTypes> getHandlingUnitTypes()
        {
            return _handlingunittypesRepository.IndexDb().ToList();
        }

        public List<MoveQueueTypes> getMoveQueueTypes()
        {
            return _movequeuetypesRepository.IndexDb().ToList();
        }

        public List<MoveQueueContexts> getMoveQueueContexts()
        {
            return _movequeuecontextsRepository.IndexDb().ToList();
        }

        public List<Statuses> getStatuses()
        {
            return _statusesRepository.IndexDb().ToList();
        }

        public List<LocationFunctions> getLocationFunctions()
        {
            return _locationfunctionsRepository.IndexDb().ToList();
        }

        public List<InventoryUnitTransactionContexts> getInventoryUnitTransactionContext()
        {
            return _inventoryunittransactioncontextsRepository.IndexDb().ToList();
        }

        public List<LocationFunctions> getPickAndPlaceLocationFunctions()
        {
            return _locationfunctionsRepository.IndexDb().Where(x =>
                x.sLocationFunctionCode == "RV" ||
                x.sLocationFunctionCode == "LD" ||
                x.sLocationFunctionCode == "FP"
                ).ToList();
        }

        public List<InventoryStates> getInventoryStates()
        {
            return _inventorystatesRepository.IndexDb().ToList();
        }

        public List<InventoryStates> getAvailableInventoryStates()
        {
            return _inventorystatesRepository.IndexDb()
                .Where(x => !x.sInventoryState.Contains("Unavailable"))
                .ToList();
        }

        public List<InventoryStates> getUnavailableInventoryStates()
        {
            return _inventorystatesRepository.IndexDb()
                .Where(x => x.sInventoryState.Contains("Unavailable"))
                .ToList();
        }

    }
}
