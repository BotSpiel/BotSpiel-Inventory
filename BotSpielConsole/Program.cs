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
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
using BotSpiel.Models;
using BotSpiel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
//Custom Code End

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
                      //Custom Code Start | Replaced Code Block
                      //Replaced Code Block Start
                      //.AddJsonFile(@"C:\xInventory\xInventoryDev75\WebCore\BotSpiel\BotSpiel\appsettings.json", true, true)
                      //Replaced Code Block End
                      .AddJsonFile(@"C:\Software\SourceBotSpielInventory\WebCore\BotSpiel\BotSpiel\appsettings.json", true, true)
                      //Custom Code End
                      .Build();

            BotSpielModule.LoadModule(services, Configuration);
            services.AddTransient<BotUserData>();
            services.AddSingleton(new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options)
                );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

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


            //Custom Code Start | Added Code Block 
            var _userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            //Custom Code End

            var consoleBot = new BotSpielBot(serviceProvider.GetRequiredService<ILoggerFactory>(), accessors, serviceProvider.GetRequiredService<BotUserData>(), serviceProvider.GetRequiredService<BotUserEntityContext>(), serviceProvider.GetRequiredService<NavigationEntityData>()
                , serviceProvider.GetRequiredService<DropInventoryUnitsPost>()
                , serviceProvider.GetRequiredService<PickBatchPickingPost>()
                , serviceProvider.GetRequiredService<PutAwayHandlingUnitsPost>()
                , serviceProvider.GetRequiredService<SetUpExecutionParametersPost>()
                , serviceProvider.GetRequiredService<IPutAwayHandlingUnitsService>()
                , serviceProvider.GetRequiredService<ISetUpExecutionParametersService>()
                , serviceProvider.GetRequiredService<IHandlingUnitsService>()
                , _userManager
                , serviceProvider.GetRequiredService<IFacilitiesService>()
                , serviceProvider.GetRequiredService<PutAway>()
                , serviceProvider.GetRequiredService<IInventoryLocationsService>()
                , serviceProvider.GetRequiredService<ILocationFunctionsService>()
                , serviceProvider.GetRequiredService<IMoveQueueTypesService>()
                , serviceProvider.GetRequiredService<IMoveQueueContextsService>()
                , serviceProvider.GetRequiredService<IInventoryUnitsService>()
                , serviceProvider.GetRequiredService<IStatusesService>()
                , serviceProvider.GetRequiredService<IMoveQueuesService>()
                , serviceProvider.GetRequiredService<IPickBatchesService>()
                , serviceProvider.GetRequiredService<CommonLookUps>()
                , serviceProvider.GetRequiredService<Picking>()
                , serviceProvider.GetRequiredService<Shipping>()
                , serviceProvider.GetRequiredService<IPickBatchPickingService>()
                , serviceProvider.GetRequiredService<IOutboundOrderLinesInventoryAllocationService>()
                , serviceProvider.GetRequiredService<IOutboundOrderLinePackingService>()

                );

            //Custom Code Start | Removed Block 
            //Console.WriteLine("Hello. Please type something to get us started.");
            //Custom Code End			

            //Custom Code Start | Added Code Block 
            Console.WriteLine(@"Hi. Enter your 
username/email.");
            //Custom Code End

            var UserName = Console.ReadLine();

            //var user = _userManager.FindByEmailAsync(UserName);
            //var debuf = _userManager.Users.Where(x => x.UserName == UserName).Count();
            //Console.WriteLine(user.Result.Email);

            adapter.ProcessActivityAsync(
                async (turnContext, cancellationToken) => await consoleBot.OnTurnAsync(turnContext)).Wait();

        }




    }
}


