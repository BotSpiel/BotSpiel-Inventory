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
    public class FindInboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInboundOrdersDialogId = "editInboundOrdersDialog";
        private const string DetailsInboundOrdersDialogId = "detailsInboundOrdersDialog";
        private const string DeleteInboundOrdersDialogId = "deleteInboundOrdersDialog";

        private const string FindInboundOrdersDialogId = "findInboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInboundOrdersDialog);
        private const string DialogKeyOptions = "findInboundOrdersDialogOptions";
        private const string SearchColumnsKey = "FindInboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "FindInboundOrdersDialogSearchText";
        private const string EditColumnsKey = "FindInboundOrdersDialogEditColumns";
        private const string EditTextKey = "FindInboundOrdersDialogEditText";
        private const string SelectedRecordKey = "FindInboundOrdersDialogSelectedRecordKey";

        private readonly IInboundOrdersService _inboundordersService;
        InboundOrdersPost _inboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorders" };
        string[] edit = { "Edit inboundorders" };
        string[] details = { "Display inboundorders" };
        string[] delete = { "Delete inboundorders" };

        public FindInboundOrdersDialog(string id, IInboundOrdersService inboundordersService, InboundOrdersPost inboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundordersService = inboundordersService;
            _inboundordersPost = inboundordersPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInboundOrdersDialog(EditInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInboundOrdersDialog(DeleteInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new InboundOrdersPost();
            step.Values[SelectedRecordKey] = _inboundordersPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InboundOrders");

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
            var inboundordersIndex = _inboundordersService.Index();
            var recordCountTotal = inboundordersIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "OrderReference":
                    var searchRecordsOrderReference = inboundordersIndex.Where(o => o.sOrderReference.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInboundOrder).Select(o => o.sInboundOrder.ToString());
                    var recordCountOrderReference = searchRecordsOrderReference.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inboundorders. Your search resulted in {recordCountOrderReference} records. I show the top 15. Please choose a InboundOrder or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsOrderReference.Take(15).Union(refine).Union(exit).ToList()),
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
            var inboundordersIndex = _inboundordersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inboundorders"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrder = 0;
                }
                else if (selection.Value == "Exit inboundorders")
                {
                    ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrder = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inboundordersService.GetPost(inboundordersIndex.Where(o => o.sInboundOrder == selection.Value).Select(o => o.ixInboundOrder).First());
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
            var inboundordersIndex = _inboundordersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inboundorders")
            {
                ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrder = -1;
                returnResult = await step.EndDialogAsync(
                (InboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inboundorders") || (selection.Value == "Display inboundorders") || (selection.Value == "Delete inboundorders"))
            {
                currentBotUserData.ixInboundOrder = ((InboundOrdersPost)step.Values[SelectedRecordKey]).ixInboundOrder;
                switch (selection.Value)
                {
                    case "Edit inboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInboundOrdersDialogId, (InboundOrdersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInboundOrdersDialogId, (InboundOrdersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InboundOrdersPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


