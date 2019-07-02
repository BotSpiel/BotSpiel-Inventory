using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.Services;

namespace BotSpiel.Dialogs
{
    public class CreateCarrierServicesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateCarrierServicesDialogId = "createCarrierServicesDialog";
       private const string CarrierServicePromptId = "carrierservicePrompt";
        private const string CarrierPromptId = "carrierPrompt";

        private const string DialogKey = nameof(CreateCarrierServicesDialog);
        private const string DialogKeyOptions = "createCarrierServicesDialogOptions";
        private const string SearchColumnsKey = "CreateCarrierServicesDialogSearchColumns";
        private const string SearchTextKey = "CreateCarrierServicesDialogSearchText";
        private const string EditColumnsKey = "CreateCarrierServicesDialogEditColumns";
        private const string EditTextKey = "CreateCarrierServicesDialogEditText";
        private const string SelectedRecordKey = "CreateCarrierServicesDialogSelectedRecordKey";

        private readonly ICarrierServicesService _carrierservicesService;
        CarrierServicesPost _carrierservicesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carrierservices" };
        string[] edit = { "Edit carrierservices" };
        string[] details = { "Display carrierservices" };
        string[] delete = { "Delete carrierservices" };

        public CreateCarrierServicesDialog(string id, ICarrierServicesService carrierservicesService, CarrierServicesPost carrierservicesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carrierservicesService = carrierservicesService;
            _carrierservicesPost = carrierservicesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> carrierserviceValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_carrierservicesService.VerifyCarrierServiceUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The carrierservice {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(CarrierServicePromptId, carrierserviceValidator));
            AddDialog(new ChoicePrompt(CarrierPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             CarrierServicePrompt,
              CarrierPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> CarrierServicePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new CarrierServicesPost();

            return await step.PromptAsync(
                CarrierServicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierService:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCarrierService = (string)step.Result;
            ((CarrierServicesPost)step.Values[DialogKey]).sCarrierService = sCarrierService;

            return await step.PromptAsync(
                CarrierPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Carrier:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_carrierservicesService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixCarrier = (Int64)step.Result;
            ((CarrierServicesPost)step.Values[DialogKey]).ixCarrier = ixCarrier;


            return await step.EndDialogAsync(
                (CarrierServicesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


