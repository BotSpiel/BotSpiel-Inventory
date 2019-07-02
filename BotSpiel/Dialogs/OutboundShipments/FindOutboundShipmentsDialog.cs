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
    public class FindOutboundShipmentsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundShipmentsDialogId = "editOutboundShipmentsDialog";
        private const string DetailsOutboundShipmentsDialogId = "detailsOutboundShipmentsDialog";
        private const string DeleteOutboundShipmentsDialogId = "deleteOutboundShipmentsDialog";

        private const string FindOutboundShipmentsDialogId = "findOutboundShipmentsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindOutboundShipmentsDialog);
        private const string DialogKeyOptions = "findOutboundShipmentsDialogOptions";
        private const string SearchColumnsKey = "FindOutboundShipmentsDialogSearchColumns";
        private const string SearchTextKey = "FindOutboundShipmentsDialogSearchText";
        private const string EditColumnsKey = "FindOutboundShipmentsDialogEditColumns";
        private const string EditTextKey = "FindOutboundShipmentsDialogEditText";
        private const string SelectedRecordKey = "FindOutboundShipmentsDialogSelectedRecordKey";

        private readonly IOutboundShipmentsService _outboundshipmentsService;
        OutboundShipmentsPost _outboundshipmentsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundshipments" };
        string[] edit = { "Edit outboundshipments" };
        string[] details = { "Display outboundshipments" };
        string[] delete = { "Delete outboundshipments" };

        public FindOutboundShipmentsDialog(string id, IOutboundShipmentsService outboundshipmentsService, OutboundShipmentsPost outboundshipmentsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundshipmentsService = outboundshipmentsService;
            _outboundshipmentsPost = outboundshipmentsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditOutboundShipmentsDialog(EditOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteOutboundShipmentsDialog(DeleteOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             chooseSearchColumnPrompt,
             enterSearchTextPrompt,
             selectFromResultPrompt,
             editDeleteDetailsPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }

        private async Task<DialogTurnResult> chooseSearchColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string searchColumn = "";
            string searchText = "";

            step.Values[DialogKey] = new OutboundShipmentsPost();
            step.Values[SelectedRecordKey] = _outboundshipmentsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("OutboundShipments");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose a column to search on:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> enterSearchTextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice searchColumn = (FoundChoice)step.Result;
            step.Values[SearchColumnsKey] = searchColumn.Value;

            return await step.PromptAsync(
                TextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter text to search for in {step.Values[SearchColumnsKey]}:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }


        private async Task<DialogTurnResult> selectFromResultPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[SearchTextKey] = (string)step.Result;
            var outboundshipmentsIndex = _outboundshipmentsService.Index();
            var recordCountTotal = outboundshipmentsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "CarrierConsignmentNumber":
                    var searchRecordsCarrierConsignmentNumber = outboundshipmentsIndex.Where(o => o.sCarrierConsignmentNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundShipment).Select(o => o.sOutboundShipment.ToString());
                    var recordCountCarrierConsignmentNumber = searchRecordsCarrierConsignmentNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundshipments. Your search resulted in {recordCountCarrierConsignmentNumber} records. I show the top 15. Please choose a OutboundShipment or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCarrierConsignmentNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }



        private async Task<DialogTurnResult> editDeleteDetailsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);
            var outboundshipmentsIndex = _outboundshipmentsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit outboundshipments"))
            {

                if (selection.Value == "Refine search")
                {
                    ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundShipment = 0;
                }
                else if (selection.Value == "Exit outboundshipments")
                {
                    ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundShipment = -1;
                }
                returnResult = await step.EndDialogAsync(
                (OutboundShipmentsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _outboundshipmentsService.GetPost(outboundshipmentsIndex.Where(o => o.sOutboundShipment == selection.Value).Select(o => o.ixOutboundShipment).First());
                returnResult = await step.PromptAsync(
                    ChoicePromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Would you like to edit, display or delete {selection.Value}. Please choose an option or exit to continue."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(edit.Union(details).Union(delete).Union(exit).ToList())
                    },
                    cancellationToken);
            }
            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var outboundshipmentsIndex = _outboundshipmentsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit outboundshipments")
            {
                ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundShipment = -1;
                returnResult = await step.EndDialogAsync(
                (OutboundShipmentsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit outboundshipments") || (selection.Value == "Display outboundshipments") || (selection.Value == "Delete outboundshipments"))
            {
                currentBotUserData.ixOutboundShipment = ((OutboundShipmentsPost)step.Values[SelectedRecordKey]).ixOutboundShipment;
                switch (selection.Value)
                {
                    case "Edit outboundshipments":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditOutboundShipmentsDialogId, (OutboundShipmentsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display outboundshipments":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete outboundshipments":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteOutboundShipmentsDialogId, (OutboundShipmentsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (OutboundShipmentsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


