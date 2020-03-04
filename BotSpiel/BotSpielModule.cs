using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BotSpiel.Data;
using BotSpiel.DataAccess.Data;
using BotSpiel.Models;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;
using BotSpiel.Services;
//Custom Code Start | Added Code Block 
using BotSpiel.DataAccess.Utilities;
using BotSpiel.Services.Utilities;
using FluentValidation;
//Custom Code End

namespace BotSpiel
{
    public static class BotSpielModule
    {
        public static void LoadModule(IServiceCollection services, IConfiguration Configuration)
        {
			services.AddDbContext<AccusationsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<AisleFaceStorageTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<AssemblyModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<BaySequenceTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<BotModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<BusinessPartnerTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CarrierTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CommunicationMediumsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ComplementsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ContactFunctionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CurrencyTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<DateTimePeriodFormatsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<DateTimePeriodFunctionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<DocumentMessageTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ExecutionModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FarewellsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FoundationModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<GreetingsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<HandlingUnitTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InboundModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InboundOrderTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryStatesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryUnitTransactionContextsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InvitationsOffersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<LanguageStylesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<LocationFunctionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<LogicalOrientationsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MaterialTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MeasurementUnitsOfDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MessageFunctionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MessageResponseTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MonetaryAmountTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MoveQueueContextsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MoveQueueTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<NetworkModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundOrderTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PickBatchTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<QuestionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<QuestionSimilesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<RequestForActionSimilesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<RequestsForActionDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<RequestsForInformationDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<RequestsForInformationSimilesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ResponseTypesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ShopModuleGridsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<StatusesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<TopicsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<UnitOfMeasurementConversionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<UnitsOfMeasurementDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<AddressesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<BusinessPartnersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CarriersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CarrierServicesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CompaniesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CountriesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CountryLocationsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CountrySubDivisionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<CurrenciesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FacilitiesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FacilityAisleFacesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FacilityFloorsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FacilityWorkAreasDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<FacilityZonesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<GalaxiesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryLocationsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryLocationSizesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryLocationsSlottingDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<LanguagesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MaterialHandlingUnitConfigurationsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MaterialsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MeasurementSystemsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PeopleDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PlanetarySystemsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PlanetRegionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PlanetsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PlanetSubRegionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<TaxesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<UniversesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<DocumentsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<DropInventoryUnitsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<HandlingUnitsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<HandlingUnitsShippingDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InboundOrderLinesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InboundOrdersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryUnitsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InventoryUnitTransactionsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InvoicePurchaseLineAmountsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InvoicePurchaseLineTaxAmountsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<InvoicesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<MoveQueuesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundCarrierManifestPickupsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundCarrierManifestsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundOrderLinePackingDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundOrderLinesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundOrderLinesInventoryAllocationDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundOrdersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<OutboundShipmentsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PaymentAddressesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PaymentCreditCardsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PaymentsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PickBatchesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PickBatchPickingDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PurchaseEmailsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PurchaseLinesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PurchasesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PurchaseTextMessagesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<PutAwayHandlingUnitsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ReceivingDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<SendEmailsDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<SendTextMessagesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<SetUpExecutionParametersDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddTransient<IAccusationsRepository, AccusationsRepository>();
			services.AddTransient<IAisleFaceStorageTypesRepository, AisleFaceStorageTypesRepository>();
			services.AddTransient<IAssemblyModuleGridsRepository, AssemblyModuleGridsRepository>();
			services.AddTransient<IBaySequenceTypesRepository, BaySequenceTypesRepository>();
			services.AddTransient<IBotModuleGridsRepository, BotModuleGridsRepository>();
			services.AddTransient<IBusinessPartnerTypesRepository, BusinessPartnerTypesRepository>();
			services.AddTransient<ICarrierTypesRepository, CarrierTypesRepository>();
			services.AddTransient<ICommunicationMediumsRepository, CommunicationMediumsRepository>();
			services.AddTransient<IComplementsRepository, ComplementsRepository>();
			services.AddTransient<IContactFunctionsRepository, ContactFunctionsRepository>();
			services.AddTransient<ICurrencyTypesRepository, CurrencyTypesRepository>();
			services.AddTransient<IDateTimePeriodFormatsRepository, DateTimePeriodFormatsRepository>();
			services.AddTransient<IDateTimePeriodFunctionsRepository, DateTimePeriodFunctionsRepository>();
			services.AddTransient<IDocumentMessageTypesRepository, DocumentMessageTypesRepository>();
			services.AddTransient<IExecutionModuleGridsRepository, ExecutionModuleGridsRepository>();
			services.AddTransient<IFarewellsRepository, FarewellsRepository>();
			services.AddTransient<IFoundationModuleGridsRepository, FoundationModuleGridsRepository>();
			services.AddTransient<IGreetingsRepository, GreetingsRepository>();
			services.AddTransient<IHandlingUnitTypesRepository, HandlingUnitTypesRepository>();
			services.AddTransient<IInboundModuleGridsRepository, InboundModuleGridsRepository>();
			services.AddTransient<IInboundOrderTypesRepository, InboundOrderTypesRepository>();
			services.AddTransient<IInventoryModuleGridsRepository, InventoryModuleGridsRepository>();
			services.AddTransient<IInventoryStatesRepository, InventoryStatesRepository>();
			services.AddTransient<IInventoryUnitTransactionContextsRepository, InventoryUnitTransactionContextsRepository>();
			services.AddTransient<IInvitationsOffersRepository, InvitationsOffersRepository>();
			services.AddTransient<ILanguageStylesRepository, LanguageStylesRepository>();
			services.AddTransient<ILocationFunctionsRepository, LocationFunctionsRepository>();
			services.AddTransient<ILogicalOrientationsRepository, LogicalOrientationsRepository>();
			services.AddTransient<IMaterialTypesRepository, MaterialTypesRepository>();
			services.AddTransient<IMeasurementUnitsOfRepository, MeasurementUnitsOfRepository>();
			services.AddTransient<IMessageFunctionsRepository, MessageFunctionsRepository>();
			services.AddTransient<IMessageResponseTypesRepository, MessageResponseTypesRepository>();
			services.AddTransient<IMonetaryAmountTypesRepository, MonetaryAmountTypesRepository>();
			services.AddTransient<IMoveQueueContextsRepository, MoveQueueContextsRepository>();
			services.AddTransient<IMoveQueueTypesRepository, MoveQueueTypesRepository>();
			services.AddTransient<INetworkModuleGridsRepository, NetworkModuleGridsRepository>();
			services.AddTransient<IOutboundModuleGridsRepository, OutboundModuleGridsRepository>();
			services.AddTransient<IOutboundOrderTypesRepository, OutboundOrderTypesRepository>();
			services.AddTransient<IPickBatchTypesRepository, PickBatchTypesRepository>();
			services.AddTransient<IQuestionsRepository, QuestionsRepository>();
			services.AddTransient<IQuestionSimilesRepository, QuestionSimilesRepository>();
			services.AddTransient<IRequestForActionSimilesRepository, RequestForActionSimilesRepository>();
			services.AddTransient<IRequestsForActionRepository, RequestsForActionRepository>();
			services.AddTransient<IRequestsForInformationRepository, RequestsForInformationRepository>();
			services.AddTransient<IRequestsForInformationSimilesRepository, RequestsForInformationSimilesRepository>();
			services.AddTransient<IResponseTypesRepository, ResponseTypesRepository>();
			services.AddTransient<IShopModuleGridsRepository, ShopModuleGridsRepository>();
			services.AddTransient<IStatusesRepository, StatusesRepository>();
			services.AddTransient<ITopicsRepository, TopicsRepository>();
			services.AddTransient<IUnitOfMeasurementConversionsRepository, UnitOfMeasurementConversionsRepository>();
			services.AddTransient<IUnitsOfMeasurementRepository, UnitsOfMeasurementRepository>();
			services.AddTransient<IAddressesRepository, AddressesRepository>();
			services.AddTransient<IBusinessPartnersRepository, BusinessPartnersRepository>();
			services.AddTransient<ICarriersRepository, CarriersRepository>();
			services.AddTransient<ICarrierServicesRepository, CarrierServicesRepository>();
			services.AddTransient<ICompaniesRepository, CompaniesRepository>();
			services.AddTransient<ICountriesRepository, CountriesRepository>();
			services.AddTransient<ICountryLocationsRepository, CountryLocationsRepository>();
			services.AddTransient<ICountrySubDivisionsRepository, CountrySubDivisionsRepository>();
			services.AddTransient<ICurrenciesRepository, CurrenciesRepository>();
			services.AddTransient<IFacilitiesRepository, FacilitiesRepository>();
			services.AddTransient<IFacilityAisleFacesRepository, FacilityAisleFacesRepository>();
			services.AddTransient<IFacilityFloorsRepository, FacilityFloorsRepository>();
			services.AddTransient<IFacilityWorkAreasRepository, FacilityWorkAreasRepository>();
			services.AddTransient<IFacilityZonesRepository, FacilityZonesRepository>();
			services.AddTransient<IGalaxiesRepository, GalaxiesRepository>();
			services.AddTransient<IInventoryLocationsRepository, InventoryLocationsRepository>();
			services.AddTransient<IInventoryLocationSizesRepository, InventoryLocationSizesRepository>();
			services.AddTransient<IInventoryLocationsSlottingRepository, InventoryLocationsSlottingRepository>();
			services.AddTransient<ILanguagesRepository, LanguagesRepository>();
			services.AddTransient<IMaterialHandlingUnitConfigurationsRepository, MaterialHandlingUnitConfigurationsRepository>();
			services.AddTransient<IMaterialsRepository, MaterialsRepository>();
			services.AddTransient<IMeasurementSystemsRepository, MeasurementSystemsRepository>();
			services.AddTransient<IPeopleRepository, PeopleRepository>();
			services.AddTransient<IPlanetarySystemsRepository, PlanetarySystemsRepository>();
			services.AddTransient<IPlanetRegionsRepository, PlanetRegionsRepository>();
			services.AddTransient<IPlanetsRepository, PlanetsRepository>();
			services.AddTransient<IPlanetSubRegionsRepository, PlanetSubRegionsRepository>();
			services.AddTransient<ITaxesRepository, TaxesRepository>();
			services.AddTransient<IUniversesRepository, UniversesRepository>();
			services.AddTransient<IDocumentsRepository, DocumentsRepository>();
			services.AddTransient<IDropInventoryUnitsRepository, DropInventoryUnitsRepository>();
			services.AddTransient<IHandlingUnitsRepository, HandlingUnitsRepository>();
			services.AddTransient<IHandlingUnitsShippingRepository, HandlingUnitsShippingRepository>();
			services.AddTransient<IInboundOrderLinesRepository, InboundOrderLinesRepository>();
			services.AddTransient<IInboundOrdersRepository, InboundOrdersRepository>();
			services.AddTransient<IInventoryUnitsRepository, InventoryUnitsRepository>();
			services.AddTransient<IInventoryUnitTransactionsRepository, InventoryUnitTransactionsRepository>();
			services.AddTransient<IInvoicePurchaseLineAmountsRepository, InvoicePurchaseLineAmountsRepository>();
			services.AddTransient<IInvoicePurchaseLineTaxAmountsRepository, InvoicePurchaseLineTaxAmountsRepository>();
			services.AddTransient<IInvoicesRepository, InvoicesRepository>();
			services.AddTransient<IMoveQueuesRepository, MoveQueuesRepository>();
			services.AddTransient<IOutboundCarrierManifestPickupsRepository, OutboundCarrierManifestPickupsRepository>();
			services.AddTransient<IOutboundCarrierManifestsRepository, OutboundCarrierManifestsRepository>();
			services.AddTransient<IOutboundOrderLinePackingRepository, OutboundOrderLinePackingRepository>();
			services.AddTransient<IOutboundOrderLinesRepository, OutboundOrderLinesRepository>();
			services.AddTransient<IOutboundOrderLinesInventoryAllocationRepository, OutboundOrderLinesInventoryAllocationRepository>();
			services.AddTransient<IOutboundOrdersRepository, OutboundOrdersRepository>();
			services.AddTransient<IOutboundShipmentsRepository, OutboundShipmentsRepository>();
			services.AddTransient<IPaymentAddressesRepository, PaymentAddressesRepository>();
			services.AddTransient<IPaymentCreditCardsRepository, PaymentCreditCardsRepository>();
			services.AddTransient<IPaymentsRepository, PaymentsRepository>();
			services.AddTransient<IPickBatchesRepository, PickBatchesRepository>();
			services.AddTransient<IPickBatchPickingRepository, PickBatchPickingRepository>();
			services.AddTransient<IPurchaseEmailsRepository, PurchaseEmailsRepository>();
			services.AddTransient<IPurchaseLinesRepository, PurchaseLinesRepository>();
			services.AddTransient<IPurchasesRepository, PurchasesRepository>();
			services.AddTransient<IPurchaseTextMessagesRepository, PurchaseTextMessagesRepository>();
			services.AddTransient<IPutAwayHandlingUnitsRepository, PutAwayHandlingUnitsRepository>();
			services.AddTransient<IReceivingRepository, ReceivingRepository>();
			services.AddTransient<ISendEmailsRepository, SendEmailsRepository>();
			services.AddTransient<ISendTextMessagesRepository, SendTextMessagesRepository>();
			services.AddTransient<ISetUpExecutionParametersRepository, SetUpExecutionParametersRepository>();
			services.AddTransient<IAccusationsService, AccusationsService>();
			services.AddTransient<IAisleFaceStorageTypesService, AisleFaceStorageTypesService>();
			services.AddTransient<IAssemblyModuleGridsService, AssemblyModuleGridsService>();
			services.AddTransient<IBaySequenceTypesService, BaySequenceTypesService>();
			services.AddTransient<IBotModuleGridsService, BotModuleGridsService>();
			services.AddTransient<IBusinessPartnerTypesService, BusinessPartnerTypesService>();
			services.AddTransient<ICarrierTypesService, CarrierTypesService>();
			services.AddTransient<ICommunicationMediumsService, CommunicationMediumsService>();
			services.AddTransient<IComplementsService, ComplementsService>();
			services.AddTransient<IContactFunctionsService, ContactFunctionsService>();
			services.AddTransient<ICurrencyTypesService, CurrencyTypesService>();
			services.AddTransient<IDateTimePeriodFormatsService, DateTimePeriodFormatsService>();
			services.AddTransient<IDateTimePeriodFunctionsService, DateTimePeriodFunctionsService>();
			services.AddTransient<IDocumentMessageTypesService, DocumentMessageTypesService>();
			services.AddTransient<IExecutionModuleGridsService, ExecutionModuleGridsService>();
			services.AddTransient<IFarewellsService, FarewellsService>();
			services.AddTransient<IFoundationModuleGridsService, FoundationModuleGridsService>();
			services.AddTransient<IGreetingsService, GreetingsService>();
			services.AddTransient<IHandlingUnitTypesService, HandlingUnitTypesService>();
			services.AddTransient<IInboundModuleGridsService, InboundModuleGridsService>();
			services.AddTransient<IInboundOrderTypesService, InboundOrderTypesService>();
			services.AddTransient<IInventoryModuleGridsService, InventoryModuleGridsService>();
			services.AddTransient<IInventoryStatesService, InventoryStatesService>();
			services.AddTransient<IInventoryUnitTransactionContextsService, InventoryUnitTransactionContextsService>();
			services.AddTransient<IInvitationsOffersService, InvitationsOffersService>();
			services.AddTransient<ILanguageStylesService, LanguageStylesService>();
			services.AddTransient<ILocationFunctionsService, LocationFunctionsService>();
			services.AddTransient<ILogicalOrientationsService, LogicalOrientationsService>();
			services.AddTransient<IMaterialTypesService, MaterialTypesService>();
			services.AddTransient<IMeasurementUnitsOfService, MeasurementUnitsOfService>();
			services.AddTransient<IMessageFunctionsService, MessageFunctionsService>();
			services.AddTransient<IMessageResponseTypesService, MessageResponseTypesService>();
			services.AddTransient<IMonetaryAmountTypesService, MonetaryAmountTypesService>();
			services.AddTransient<IMoveQueueContextsService, MoveQueueContextsService>();
			services.AddTransient<IMoveQueueTypesService, MoveQueueTypesService>();
			services.AddTransient<INetworkModuleGridsService, NetworkModuleGridsService>();
			services.AddTransient<IOutboundModuleGridsService, OutboundModuleGridsService>();
			services.AddTransient<IOutboundOrderTypesService, OutboundOrderTypesService>();
			services.AddTransient<IPickBatchTypesService, PickBatchTypesService>();
			services.AddTransient<IQuestionsService, QuestionsService>();
			services.AddTransient<IQuestionSimilesService, QuestionSimilesService>();
			services.AddTransient<IRequestForActionSimilesService, RequestForActionSimilesService>();
			services.AddTransient<IRequestsForActionService, RequestsForActionService>();
			services.AddTransient<IRequestsForInformationService, RequestsForInformationService>();
			services.AddTransient<IRequestsForInformationSimilesService, RequestsForInformationSimilesService>();
			services.AddTransient<IResponseTypesService, ResponseTypesService>();
			services.AddTransient<IShopModuleGridsService, ShopModuleGridsService>();
			services.AddTransient<IStatusesService, StatusesService>();
			services.AddTransient<ITopicsService, TopicsService>();
			services.AddTransient<IUnitOfMeasurementConversionsService, UnitOfMeasurementConversionsService>();
			services.AddTransient<IUnitsOfMeasurementService, UnitsOfMeasurementService>();
			services.AddTransient<IAddressesService, AddressesService>();
			services.AddTransient<IBusinessPartnersService, BusinessPartnersService>();
			services.AddTransient<ICarriersService, CarriersService>();
			services.AddTransient<ICarrierServicesService, CarrierServicesService>();
			services.AddTransient<ICompaniesService, CompaniesService>();
			services.AddTransient<ICountriesService, CountriesService>();
			services.AddTransient<ICountryLocationsService, CountryLocationsService>();
			services.AddTransient<ICountrySubDivisionsService, CountrySubDivisionsService>();
			services.AddTransient<ICurrenciesService, CurrenciesService>();
			services.AddTransient<IFacilitiesService, FacilitiesService>();
			services.AddTransient<IFacilityAisleFacesService, FacilityAisleFacesService>();
			services.AddTransient<IFacilityFloorsService, FacilityFloorsService>();
			services.AddTransient<IFacilityWorkAreasService, FacilityWorkAreasService>();
			services.AddTransient<IFacilityZonesService, FacilityZonesService>();
			services.AddTransient<IGalaxiesService, GalaxiesService>();
			services.AddTransient<IInventoryLocationsService, InventoryLocationsService>();
			services.AddTransient<IInventoryLocationSizesService, InventoryLocationSizesService>();
			services.AddTransient<IInventoryLocationsSlottingService, InventoryLocationsSlottingService>();
			services.AddTransient<ILanguagesService, LanguagesService>();
			services.AddTransient<IMaterialHandlingUnitConfigurationsService, MaterialHandlingUnitConfigurationsService>();
			services.AddTransient<IMaterialsService, MaterialsService>();
			services.AddTransient<IMeasurementSystemsService, MeasurementSystemsService>();
			services.AddTransient<IPeopleService, PeopleService>();
			services.AddTransient<IPlanetarySystemsService, PlanetarySystemsService>();
			services.AddTransient<IPlanetRegionsService, PlanetRegionsService>();
			services.AddTransient<IPlanetsService, PlanetsService>();
			services.AddTransient<IPlanetSubRegionsService, PlanetSubRegionsService>();
			services.AddTransient<ITaxesService, TaxesService>();
			services.AddTransient<IUniversesService, UniversesService>();
			services.AddTransient<IDocumentsService, DocumentsService>();
			services.AddTransient<IDropInventoryUnitsService, DropInventoryUnitsService>();
			services.AddTransient<IHandlingUnitsService, HandlingUnitsService>();
			services.AddTransient<IHandlingUnitsShippingService, HandlingUnitsShippingService>();
			services.AddTransient<IInboundOrderLinesService, InboundOrderLinesService>();
			services.AddTransient<IInboundOrdersService, InboundOrdersService>();
			services.AddTransient<IInventoryUnitsService, InventoryUnitsService>();
			services.AddTransient<IInventoryUnitTransactionsService, InventoryUnitTransactionsService>();
			services.AddTransient<IInvoicePurchaseLineAmountsService, InvoicePurchaseLineAmountsService>();
			services.AddTransient<IInvoicePurchaseLineTaxAmountsService, InvoicePurchaseLineTaxAmountsService>();
			services.AddTransient<IInvoicesService, InvoicesService>();
			services.AddTransient<IMoveQueuesService, MoveQueuesService>();
			services.AddTransient<IOutboundCarrierManifestPickupsService, OutboundCarrierManifestPickupsService>();
			services.AddTransient<IOutboundCarrierManifestsService, OutboundCarrierManifestsService>();
			services.AddTransient<IOutboundOrderLinePackingService, OutboundOrderLinePackingService>();
			services.AddTransient<IOutboundOrderLinesService, OutboundOrderLinesService>();
			services.AddTransient<IOutboundOrderLinesInventoryAllocationService, OutboundOrderLinesInventoryAllocationService>();
			services.AddTransient<IOutboundOrdersService, OutboundOrdersService>();
			services.AddTransient<IOutboundShipmentsService, OutboundShipmentsService>();
			services.AddTransient<IPaymentAddressesService, PaymentAddressesService>();
			services.AddTransient<IPaymentCreditCardsService, PaymentCreditCardsService>();
			services.AddTransient<IPaymentsService, PaymentsService>();
			services.AddTransient<IPickBatchesService, PickBatchesService>();
			services.AddTransient<IPickBatchPickingService, PickBatchPickingService>();
			services.AddTransient<IPurchaseEmailsService, PurchaseEmailsService>();
			services.AddTransient<IPurchaseLinesService, PurchaseLinesService>();
			services.AddTransient<IPurchasesService, PurchasesService>();
			services.AddTransient<IPurchaseTextMessagesService, PurchaseTextMessagesService>();
			services.AddTransient<IPutAwayHandlingUnitsService, PutAwayHandlingUnitsService>();
			services.AddTransient<IReceivingService, ReceivingService>();
			services.AddTransient<ISendEmailsService, SendEmailsService>();
			services.AddTransient<ISendTextMessagesService, SendTextMessagesService>();
			services.AddTransient<ISetUpExecutionParametersService, SetUpExecutionParametersService>();
			services.AddTransient<AccusationsPost>();
			services.AddTransient<AisleFaceStorageTypesPost>();
			services.AddTransient<AssemblyModuleGridsPost>();
			services.AddTransient<BaySequenceTypesPost>();
			services.AddTransient<BotModuleGridsPost>();
			services.AddTransient<BusinessPartnerTypesPost>();
			services.AddTransient<CarrierTypesPost>();
			services.AddTransient<CommunicationMediumsPost>();
			services.AddTransient<ComplementsPost>();
			services.AddTransient<ContactFunctionsPost>();
			services.AddTransient<CurrencyTypesPost>();
			services.AddTransient<DateTimePeriodFormatsPost>();
			services.AddTransient<DateTimePeriodFunctionsPost>();
			services.AddTransient<DocumentMessageTypesPost>();
			services.AddTransient<ExecutionModuleGridsPost>();
			services.AddTransient<FarewellsPost>();
			services.AddTransient<FoundationModuleGridsPost>();
			services.AddTransient<GreetingsPost>();
			services.AddTransient<HandlingUnitTypesPost>();
			services.AddTransient<InboundModuleGridsPost>();
			services.AddTransient<InboundOrderTypesPost>();
			services.AddTransient<InventoryModuleGridsPost>();
			services.AddTransient<InventoryStatesPost>();
			services.AddTransient<InventoryUnitTransactionContextsPost>();
			services.AddTransient<InvitationsOffersPost>();
			services.AddTransient<LanguageStylesPost>();
			services.AddTransient<LocationFunctionsPost>();
			services.AddTransient<LogicalOrientationsPost>();
			services.AddTransient<MaterialTypesPost>();
			services.AddTransient<MeasurementUnitsOfPost>();
			services.AddTransient<MessageFunctionsPost>();
			services.AddTransient<MessageResponseTypesPost>();
			services.AddTransient<MonetaryAmountTypesPost>();
			services.AddTransient<MoveQueueContextsPost>();
			services.AddTransient<MoveQueueTypesPost>();
			services.AddTransient<NetworkModuleGridsPost>();
			services.AddTransient<OutboundModuleGridsPost>();
			services.AddTransient<OutboundOrderTypesPost>();
			services.AddTransient<PickBatchTypesPost>();
			services.AddTransient<QuestionsPost>();
			services.AddTransient<QuestionSimilesPost>();
			services.AddTransient<RequestForActionSimilesPost>();
			services.AddTransient<RequestsForActionPost>();
			services.AddTransient<RequestsForInformationPost>();
			services.AddTransient<RequestsForInformationSimilesPost>();
			services.AddTransient<ResponseTypesPost>();
			services.AddTransient<ShopModuleGridsPost>();
			services.AddTransient<StatusesPost>();
			services.AddTransient<TopicsPost>();
			services.AddTransient<UnitOfMeasurementConversionsPost>();
			services.AddTransient<UnitsOfMeasurementPost>();
			services.AddTransient<AddressesPost>();
			services.AddTransient<BusinessPartnersPost>();
			services.AddTransient<CarriersPost>();
			services.AddTransient<CarrierServicesPost>();
			services.AddTransient<CompaniesPost>();
			services.AddTransient<CountriesPost>();
			services.AddTransient<CountryLocationsPost>();
			services.AddTransient<CountrySubDivisionsPost>();
			services.AddTransient<CurrenciesPost>();
			services.AddTransient<FacilitiesPost>();
			services.AddTransient<FacilityAisleFacesPost>();
			services.AddTransient<FacilityFloorsPost>();
			services.AddTransient<FacilityWorkAreasPost>();
			services.AddTransient<FacilityZonesPost>();
			services.AddTransient<GalaxiesPost>();
			services.AddTransient<InventoryLocationsPost>();
			services.AddTransient<InventoryLocationSizesPost>();
			services.AddTransient<InventoryLocationsSlottingPost>();
			services.AddTransient<LanguagesPost>();
			services.AddTransient<MaterialHandlingUnitConfigurationsPost>();
			services.AddTransient<MaterialsPost>();
			services.AddTransient<MeasurementSystemsPost>();
			services.AddTransient<PeoplePost>();
			services.AddTransient<PlanetarySystemsPost>();
			services.AddTransient<PlanetRegionsPost>();
			services.AddTransient<PlanetsPost>();
			services.AddTransient<PlanetSubRegionsPost>();
			services.AddTransient<TaxesPost>();
			services.AddTransient<UniversesPost>();
			services.AddTransient<DocumentsPost>();
			services.AddTransient<DropInventoryUnitsPost>();
			services.AddTransient<HandlingUnitsPost>();
			services.AddTransient<HandlingUnitsShippingPost>();
			services.AddTransient<InboundOrderLinesPost>();
			services.AddTransient<InboundOrdersPost>();
			services.AddTransient<InventoryUnitsPost>();
			services.AddTransient<InventoryUnitTransactionsPost>();
			services.AddTransient<InvoicePurchaseLineAmountsPost>();
			services.AddTransient<InvoicePurchaseLineTaxAmountsPost>();
			services.AddTransient<InvoicesPost>();
			services.AddTransient<MoveQueuesPost>();
			services.AddTransient<OutboundCarrierManifestPickupsPost>();
			services.AddTransient<OutboundCarrierManifestsPost>();
			services.AddTransient<OutboundOrderLinePackingPost>();
			services.AddTransient<OutboundOrderLinesPost>();
			services.AddTransient<OutboundOrderLinesInventoryAllocationPost>();
			services.AddTransient<OutboundOrdersPost>();
			services.AddTransient<OutboundShipmentsPost>();
			services.AddTransient<PaymentAddressesPost>();
			services.AddTransient<PaymentCreditCardsPost>();
			services.AddTransient<PaymentsPost>();
			services.AddTransient<PickBatchesPost>();
			services.AddTransient<PickBatchPickingPost>();
			services.AddTransient<PurchaseEmailsPost>();
			services.AddTransient<PurchaseLinesPost>();
			services.AddTransient<PurchasesPost>();
			services.AddTransient<PurchaseTextMessagesPost>();
			services.AddTransient<PutAwayHandlingUnitsPost>();
			services.AddTransient<ReceivingPost>();
			services.AddTransient<SendEmailsPost>();
			services.AddTransient<SendTextMessagesPost>();
			services.AddTransient<SetUpExecutionParametersPost>();

            services.AddDbContext<BotspielBotMessagesDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IBotspielBotMessagesRepository, BotspielBotMessagesRepository>();
            services.AddTransient<IBotspielBotMessagesService, BotspielBotMessagesService>();
            services.AddTransient<BotspielBotMessagesPost>();
            services.AddTransient<BotUserEntityContext>();
            services.AddSingleton<NavigationEntityData>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<BotSpielBotAdapter>();
            services.AddTransient<BotUserData>();
            //Custom Code Start | Added Code Block 
            var serviceProvider = services.BuildServiceProvider();

            services.AddTransient<Countries>();
            services.AddTransient<CountrySubDivisions>();
            services.AddTransient<IValidator<InventoryLocationsSlottingPost>, InventoryLocationsSlottingPostValidator>();
            services.AddTransient<IValidator<InventoryLocationsPost>, InventoryLocationsPostValidator>();
            services.AddTransient<IValidator<InventoryUnitsPost>, InventoryUnitsPostValidator>();
            services.AddTransient<IValidator<InboundOrdersPost>, InboundOrdersPostValidator>();
            services.AddTransient<IValidator<InboundOrderLinesPost>, InboundOrderLinesPostValidator>();
            services.AddTransient<IValidator<ReceivingPost>, ReceivingPostValidator>();
            services.AddTransient<IValidator<OutboundCarrierManifestPickupsPost>, OutboundCarrierManifestPickupsPostValidator>();

            services.AddScoped<SelectOptionStings>();
            services.AddScoped<CommonlyUsedSelects>();
            //services.AddSingleton(new CommonlyUsedSelects(
            //        serviceProvider.GetRequiredService<UnitsOfMeasurementDB>()
            //    ));
            services.AddScoped<VolumeAndWeight>();
            services.AddScoped<Inventory>();
            services.AddScoped<PutAway>();
            services.AddScoped<Picking>();
            services.AddScoped<CommonLookUps>();
            services.AddScoped<CommonLookUpsRepository>();
			services.AddScoped<Shipping>();

			services.AddSingleton(new Shipping(
						serviceProvider.GetRequiredService<ILocationFunctionsService>(),
						//serviceProvider.GetRequiredService<IInventoryUnitsService>(),
						new InventoryUnitsService(
							new InventoryUnitsRepository(
								new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new PickBatchPickingDB(new DbContextOptionsBuilder<PickBatchPickingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options)
								),
							new InventoryUnitTransactionsService(
								new InventoryUnitTransactionsRepository(
									new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options)
									)
								),
							new InventoryUnitTransactionsPost(),
							new InventoryLocationsService(new InventoryLocationsRepository(
								new InventoryLocationsDB(new DbContextOptionsBuilder<InventoryLocationsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryLocationsSlottingDB(new DbContextOptionsBuilder<InventoryLocationsSlottingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new OutboundCarrierManifestsDB(new DbContextOptionsBuilder<OutboundCarrierManifestsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new ReceivingDB(new DbContextOptionsBuilder<ReceivingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new CommonlyUsedSelects(new UnitsOfMeasurementDB(new DbContextOptionsBuilder<UnitsOfMeasurementDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options))
								)),
							new InventoryLocationsPost(),
							new VolumeAndWeight(
									serviceProvider.GetRequiredService<IUnitOfMeasurementConversionsRepository>(),
									new InventoryUnitsRepository(
									new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
									new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
									new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
									new PickBatchPickingDB(new DbContextOptionsBuilder<PickBatchPickingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options)
									),
									new InventoryLocationsRepository(
										new InventoryLocationsDB(new DbContextOptionsBuilder<InventoryLocationsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new InventoryLocationsSlottingDB(new DbContextOptionsBuilder<InventoryLocationsSlottingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new OutboundCarrierManifestsDB(new DbContextOptionsBuilder<OutboundCarrierManifestsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new ReceivingDB(new DbContextOptionsBuilder<ReceivingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
										new CommonlyUsedSelects(new UnitsOfMeasurementDB(new DbContextOptionsBuilder<UnitsOfMeasurementDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options))
										),
									serviceProvider.GetRequiredService<IHandlingUnitsRepository>(),
									serviceProvider.GetRequiredService<IMaterialsRepository>(),
									serviceProvider.GetRequiredService<IInventoryUnitTransactionsRepository>()
								)
							),
					serviceProvider.GetRequiredService<IInventoryLocationsSlottingService>(),
					new VolumeAndWeight(
							serviceProvider.GetRequiredService<IUnitOfMeasurementConversionsRepository>(),
							new InventoryUnitsRepository(
							new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
							new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
							new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
							new PickBatchPickingDB(new DbContextOptionsBuilder<PickBatchPickingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options)
							),
							new InventoryLocationsRepository(
								new InventoryLocationsDB(new DbContextOptionsBuilder<InventoryLocationsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryLocationsSlottingDB(new DbContextOptionsBuilder<InventoryLocationsSlottingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new OutboundCarrierManifestsDB(new DbContextOptionsBuilder<OutboundCarrierManifestsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new ReceivingDB(new DbContextOptionsBuilder<ReceivingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
								new CommonlyUsedSelects(new UnitsOfMeasurementDB(new DbContextOptionsBuilder<UnitsOfMeasurementDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options))
								),
							serviceProvider.GetRequiredService<IHandlingUnitsRepository>(),
							serviceProvider.GetRequiredService<IMaterialsRepository>(),
							serviceProvider.GetRequiredService<IInventoryUnitTransactionsRepository>()
						),
					//serviceProvider.GetRequiredService<IInventoryLocationsService>(),
					new InventoryLocationsService(new InventoryLocationsRepository(
						new InventoryLocationsDB(new DbContextOptionsBuilder<InventoryLocationsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new InventoryLocationsSlottingDB(new DbContextOptionsBuilder<InventoryLocationsSlottingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new InventoryUnitsDB(new DbContextOptionsBuilder<InventoryUnitsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new InventoryUnitTransactionsDB(new DbContextOptionsBuilder<InventoryUnitTransactionsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new MoveQueuesDB(new DbContextOptionsBuilder<MoveQueuesDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new OutboundCarrierManifestsDB(new DbContextOptionsBuilder<OutboundCarrierManifestsDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new ReceivingDB(new DbContextOptionsBuilder<ReceivingDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options),
						new CommonlyUsedSelects(new UnitsOfMeasurementDB(new DbContextOptionsBuilder<UnitsOfMeasurementDB>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options))
						)),
					new CommonLookUps(
						serviceProvider.GetRequiredService<IMoveQueueTypesService>(),
						serviceProvider.GetRequiredService<IMoveQueueContextsService>(),
						serviceProvider.GetRequiredService<IStatusesService>(),
						serviceProvider.GetRequiredService<ILocationFunctionsService>(),
						serviceProvider.GetRequiredService<IInventoryUnitTransactionContextsService>(),
						serviceProvider.GetRequiredService<IInventoryStatesService>(),
						serviceProvider.GetRequiredService<IHandlingUnitTypesService>()
						),
					serviceProvider.GetRequiredService<IOutboundCarrierManifestsService>(),
					serviceProvider.GetRequiredService<IOutboundOrdersRepository>(),
					//serviceProvider.GetRequiredService<IOutboundCarrierManifestPickupsService>(),
					serviceProvider.GetRequiredService<IOutboundShipmentsService>(),
					serviceProvider.GetRequiredService<IOutboundOrderLinesService>(),
					serviceProvider.GetRequiredService<IOutboundOrderLinePackingService>(),
					serviceProvider.GetRequiredService<IOutboundOrderLinesInventoryAllocationService>(),
					serviceProvider.GetRequiredService<IHandlingUnitsService>()
					//new HandlingUnitsService(new HandlingUnitsRepository(
					//	serviceProvider.GetRequiredService<HandlingUnitsDB>(),
					//	serviceProvider.GetRequiredService<HandlingUnitsDB>(),
					//	serviceProvider.GetRequiredService<HandlingUnitsShippingDB>(),
					//	serviceProvider.GetRequiredService<InventoryUnitsDB>(),
					//	serviceProvider.GetRequiredService<InventoryUnitTransactionsDB>(),
					//	serviceProvider.GetRequiredService<MoveQueuesDB>(),
					//	serviceProvider.GetRequiredService<OutboundOrderLinePackingDB>(),
					//	serviceProvider.GetRequiredService<ReceivingDB>(),
					//	new CommonLookUpsRepository(
					//		serviceProvider.GetRequiredService<IMoveQueueTypesRepository>(),
					//		serviceProvider.GetRequiredService<IMoveQueueContextsRepository>(),
					//		serviceProvider.GetRequiredService<IStatusesRepository>(),
					//		serviceProvider.GetRequiredService<ILocationFunctionsRepository>(),
					//		serviceProvider.GetRequiredService<IInventoryUnitTransactionContextsRepository>(),
					//		serviceProvider.GetRequiredService<IInventoryStatesRepository>(),
					//		serviceProvider.GetRequiredService<IHandlingUnitTypesRepository>()
					//		)
					//	)
					//	)
				));

			//Custom Code End
		}
    }
}


 

