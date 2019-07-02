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
    public class DeleteCarrierServicesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteCarrierServicesDialogId = "deleteCarrierServicesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteCarrierServicesDialog);
        private const string DialogKeyOptions = "deleteCarrierServicesDialogOptions";
        private const string SearchColumnsKey = "DeleteCarrierServicesDialogSearchColumns";
        private const string SearchTextKey = "DeleteCarrierServicesDialogSearchText";
        private const string EditColumnsKey = "DeleteCarrierServicesDialogEditColumns";
        private const string EditTextKey = "DeleteCarrierServicesDialogEditText";
        private const string SelectedRecordKey = "DeleteCarrierServicesDialogSelectedRecordKey";

        private readonly ICarrierServicesService _carrierservicesService;
        CarrierServicesPost _carrierservicesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carrierservices" };
        string[] edit = { "Edit carrierservices" };
        string[] details = { "Display carrierservices" };
        string[] delete = { "Delete carrierservices" };

        public DeleteCarrierServicesDialog(string id, ICarrierServicesService carrierservicesService, CarrierServicesPost carrierservicesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carrierservicesService = carrierservicesService;
            _carrierservicesPost = carrierservicesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              confirmDeletePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> confirmDeletePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new CarrierServicesPost();
            step.Values[DialogKeyOptions] = (CarrierServicesPost)step.Options;
            ((CarrierServicesPost)step.Values[DialogKey]).ixCarrierService = ((CarrierServicesPost)step.Values[DialogKeyOptions]).ixCarrierService;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((CarrierServicesPost)step.Options).sCarrierService}:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var yesNo = (bool)step.Result;

            if (!yesNo)
            {
                ((CarrierServicesPost)step.Values[DialogKey]).ixCarrierService = -1;
            }

            return await step.EndDialogAsync(
                (CarrierServicesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


