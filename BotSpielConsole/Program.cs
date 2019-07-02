using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using BotSpiel;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Models;
using BotSpiel.Services;

namespace BotSpielConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var adapter = new ConsoleAdapter();
            var services = new ServiceCollection()
                .AddLogging();

            IConfiguration Configuration = new ConfigurationBuilder()
                      .AddJsonFile(@"C:\xInventory\xInventoryDev26\WebCore\BotSpiel\BotSpiel\appsettings.json", true, true)
                      .Build();

            BotSpielModule.LoadModule(services, Configuration);
            services.AddTransient<BotUserData>();

            var serviceProvider = services.BuildServiceProvider();
            var ILoggerService = serviceProvider.GetService<ILoggerFactory>();
            var logger = ILoggerService.CreateLogger<BotSpielBot>();

            IStorage dataStore = new MemoryStorage();
            var conversationState = new ConversationState(dataStore);
            var userState = new UserState(dataStore);
            var accessors = new BotSpielUserStateAccessors(conversationState, userState)
            {
                DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotSpielUserStateAccessors.DialogStateAccessorName),
                DidBotWelcomeUser = userState.CreateProperty<bool>(BotSpielUserStateAccessors.DidBotWelcomeUserName),
                BotUserDataAccessor = userState.CreateProperty<BotUserData>(BotSpielUserStateAccessors.BotUserDataAccessorName),
            };

            var consoleBot = new BotSpielBot(serviceProvider.GetRequiredService<ILoggerFactory>(), accessors, serviceProvider.GetRequiredService<BotUserData>(), serviceProvider.GetRequiredService<BotUserEntityContext>(), serviceProvider.GetRequiredService<NavigationEntityData>() 
               ,serviceProvider.GetRequiredService<AddressesPost>()
                ,serviceProvider.GetRequiredService<BusinessPartnersPost>()
                ,serviceProvider.GetRequiredService<CarriersPost>()
                ,serviceProvider.GetRequiredService<CarrierServicesPost>()
                ,serviceProvider.GetRequiredService<CompaniesPost>()
                ,serviceProvider.GetRequiredService<FacilitiesPost>()
                ,serviceProvider.GetRequiredService<FacilityAisleFacesPost>()
                ,serviceProvider.GetRequiredService<FacilityFloorsPost>()
                ,serviceProvider.GetRequiredService<FacilityWorkAreasPost>()
                ,serviceProvider.GetRequiredService<FacilityZonesPost>()
                ,serviceProvider.GetRequiredService<HandlingUnitsPost>()
                ,serviceProvider.GetRequiredService<InboundOrderLinesPost>()
                ,serviceProvider.GetRequiredService<InboundOrdersPost>()
                ,serviceProvider.GetRequiredService<InventoryLocationsPost>()
                ,serviceProvider.GetRequiredService<InventoryLocationSizesPost>()
                ,serviceProvider.GetRequiredService<InventoryLocationsSlottingPost>()
                ,serviceProvider.GetRequiredService<InventoryUnitsPost>()
                ,serviceProvider.GetRequiredService<MaterialHandlingUnitConfigurationsPost>()
                ,serviceProvider.GetRequiredService<MaterialsPost>()
                ,serviceProvider.GetRequiredService<MoveQueuesPost>()
                ,serviceProvider.GetRequiredService<OutboundCarrierManifestPickupsPost>()
                ,serviceProvider.GetRequiredService<OutboundCarrierManifestsPost>()
                ,serviceProvider.GetRequiredService<OutboundOrderLinesPost>()
                ,serviceProvider.GetRequiredService<OutboundOrdersPost>()
                ,serviceProvider.GetRequiredService<OutboundShipmentsPost>()
                ,serviceProvider.GetRequiredService<PeoplePost>()
                ,serviceProvider.GetRequiredService<PickBatchesPost>()
                ,serviceProvider.GetRequiredService<ReceivingPost>()
               ,serviceProvider.GetRequiredService<IAddressesService>()
                ,serviceProvider.GetRequiredService<IBusinessPartnersService>()
                ,serviceProvider.GetRequiredService<ICarriersService>()
                ,serviceProvider.GetRequiredService<ICarrierServicesService>()
                ,serviceProvider.GetRequiredService<ICompaniesService>()
                ,serviceProvider.GetRequiredService<IFacilitiesService>()
                ,serviceProvider.GetRequiredService<IFacilityAisleFacesService>()
                ,serviceProvider.GetRequiredService<IFacilityFloorsService>()
                ,serviceProvider.GetRequiredService<IFacilityWorkAreasService>()
                ,serviceProvider.GetRequiredService<IFacilityZonesService>()
                ,serviceProvider.GetRequiredService<IHandlingUnitsService>()
                ,serviceProvider.GetRequiredService<IInboundOrderLinesService>()
                ,serviceProvider.GetRequiredService<IInboundOrdersService>()
                ,serviceProvider.GetRequiredService<IInventoryLocationsService>()
                ,serviceProvider.GetRequiredService<IInventoryLocationSizesService>()
                ,serviceProvider.GetRequiredService<IInventoryLocationsSlottingService>()
                ,serviceProvider.GetRequiredService<IInventoryUnitsService>()
                ,serviceProvider.GetRequiredService<IMaterialHandlingUnitConfigurationsService>()
                ,serviceProvider.GetRequiredService<IMaterialsService>()
                ,serviceProvider.GetRequiredService<IMoveQueuesService>()
                ,serviceProvider.GetRequiredService<IOutboundCarrierManifestPickupsService>()
                ,serviceProvider.GetRequiredService<IOutboundCarrierManifestsService>()
                ,serviceProvider.GetRequiredService<IOutboundOrderLinesService>()
                ,serviceProvider.GetRequiredService<IOutboundOrdersService>()
                ,serviceProvider.GetRequiredService<IOutboundShipmentsService>()
                ,serviceProvider.GetRequiredService<IPeopleService>()
                ,serviceProvider.GetRequiredService<IPickBatchesService>()
                ,serviceProvider.GetRequiredService<IReceivingService>()

                );

            Console.WriteLine("Hello. Please type something to get us started.");

            adapter.ProcessActivityAsync(
                async (turnContext, cancellationToken) => await consoleBot.OnTurnAsync(turnContext)).Wait();

        }

    }
}


