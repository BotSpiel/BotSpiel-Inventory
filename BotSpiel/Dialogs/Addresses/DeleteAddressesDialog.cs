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
    public class DeleteAddressesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteAddressesDialogId = "deleteAddressesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteAddressesDialog);
        private const string DialogKeyOptions = "deleteAddressesDialogOptions";
        private const string SearchColumnsKey = "DeleteAddressesDialogSearchColumns";
        private const string SearchTextKey = "DeleteAddressesDialogSearchText";
        private const string EditColumnsKey = "DeleteAddressesDialogEditColumns";
        private const string EditTextKey = "DeleteAddressesDialogEditText";
        private const string SelectedRecordKey = "DeleteAddressesDialogSelectedRecordKey";

        private readonly IAddressesService _addressesService;
        AddressesPost _addressesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit addresses" };
        string[] edit = { "Edit addresses" };
        string[] details = { "Display addresses" };
        string[] delete = { "Delete addresses" };

        public DeleteAddressesDialog(string id, IAddressesService addressesService, AddressesPost addressesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _addressesService = addressesService;
            _addressesPost = addressesPost;

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
            step.Values[DialogKey] = new AddressesPost();
            step.Values[DialogKeyOptions] = (AddressesPost)step.Options;
            ((AddressesPost)step.Values[DialogKey]).ixAddress = ((AddressesPost)step.Values[DialogKeyOptions]).ixAddress;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((AddressesPost)step.Options).sAddress}:"),
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
                ((AddressesPost)step.Values[DialogKey]).ixAddress = -1;
            }

            return await step.EndDialogAsync(
                (AddressesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


