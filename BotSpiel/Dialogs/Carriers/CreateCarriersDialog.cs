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
    public class CreateCarriersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateCarriersDialogId = "createCarriersDialog";
       private const string CarrierPromptId = "carrierPrompt";
        private const string CarrierTypePromptId = "carriertypePrompt";
        private const string StandardCarrierAlphaCodePromptId = "standardcarrieralphacodePrompt";
        private const string CarrierConsignmentNumberPrefixPromptId = "carrierconsignmentnumberprefixPrompt";
        private const string CarrierConsignmentNumberStartPromptId = "carrierconsignmentnumberstartPrompt";
        private const string CarrierConsignmentNumberLastUsedPromptId = "carrierconsignmentnumberlastusedPrompt";
        private const string ScheduledPickupTimePromptId = "scheduledpickuptimePrompt";

        private const string DialogKey = nameof(CreateCarriersDialog);
        private const string DialogKeyOptions = "createCarriersDialogOptions";
        private const string SearchColumnsKey = "CreateCarriersDialogSearchColumns";
        private const string SearchTextKey = "CreateCarriersDialogSearchText";
        private const string EditColumnsKey = "CreateCarriersDialogEditColumns";
        private const string EditTextKey = "CreateCarriersDialogEditText";
        private const string SelectedRecordKey = "CreateCarriersDialogSelectedRecordKey";

        private readonly ICarriersService _carriersService;
        CarriersPost _carriersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carriers" };
        string[] edit = { "Edit carriers" };
        string[] details = { "Display carriers" };
        string[] delete = { "Delete carriers" };

        public CreateCarriersDialog(string id, ICarriersService carriersService, CarriersPost carriersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carriersService = carriersService;
            _carriersPost = carriersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> carrierValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_carriersService.VerifyCarrierUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The carrier {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(CarrierPromptId, carrierValidator));
            AddDialog(new ChoicePrompt(CarrierTypePromptId));
            AddDialog(new TextPrompt(StandardCarrierAlphaCodePromptId));
            AddDialog(new TextPrompt(CarrierConsignmentNumberPrefixPromptId));
            AddDialog(new NumberPrompt<Int64>(CarrierConsignmentNumberStartPromptId));
            AddDialog(new NumberPrompt<Int64>(CarrierConsignmentNumberLastUsedPromptId));
            AddDialog(new DateTimePrompt(ScheduledPickupTimePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             CarrierPrompt,
              CarrierTypePrompt,
              StandardCarrierAlphaCodePrompt,
              CarrierConsignmentNumberPrefixPrompt,
              CarrierConsignmentNumberStartPrompt,
              CarrierConsignmentNumberLastUsedPrompt,
              ScheduledPickupTimePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> CarrierPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new CarriersPost();

            return await step.PromptAsync(
                CarrierPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Carrier:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCarrier = (string)step.Result;
            ((CarriersPost)step.Values[DialogKey]).sCarrier = sCarrier;

            return await step.PromptAsync(
                CarrierTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_carriersService.selectCarrierTypes().Select(ct => ct.sCarrierType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StandardCarrierAlphaCodePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _CarrierType = (FoundChoice)step.Result;
            var ixCarrierType = _carriersService.selectCarrierTypes().Where(ct => ct.sCarrierType == _CarrierType.Value).Select(ct => ct.ixCarrierType).First();
            ((CarriersPost)step.Values[DialogKey]).ixCarrierType = ixCarrierType;

            return await step.PromptAsync(
                StandardCarrierAlphaCodePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StandardCarrierAlphaCode:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierConsignmentNumberPrefixPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sStandardCarrierAlphaCode = (string)step.Result;
            ((CarriersPost)step.Values[DialogKey]).sStandardCarrierAlphaCode = sStandardCarrierAlphaCode;

            return await step.PromptAsync(
                CarrierConsignmentNumberPrefixPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberPrefix:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierConsignmentNumberStartPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCarrierConsignmentNumberPrefix = (string)step.Result;
            ((CarriersPost)step.Values[DialogKey]).sCarrierConsignmentNumberPrefix = sCarrierConsignmentNumberPrefix;

            return await step.PromptAsync(
                CarrierConsignmentNumberStartPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberStart:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierConsignmentNumberLastUsedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nCarrierConsignmentNumberStart = (Int64)step.Result;
            ((CarriersPost)step.Values[DialogKey]).nCarrierConsignmentNumberStart = nCarrierConsignmentNumberStart;

            return await step.PromptAsync(
                CarrierConsignmentNumberLastUsedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumberLastUsed:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ScheduledPickupTimePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nCarrierConsignmentNumberLastUsed = (Int64)step.Result;
            ((CarriersPost)step.Values[DialogKey]).nCarrierConsignmentNumberLastUsed = nCarrierConsignmentNumberLastUsed;

            return await step.PromptAsync(
                ScheduledPickupTimePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ScheduledPickupTime:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtScheduledPickupTime = (TimeSpan)step.Result;
            ((CarriersPost)step.Values[DialogKey]).dtScheduledPickupTime = dtScheduledPickupTime;


            return await step.EndDialogAsync(
                (CarriersPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


