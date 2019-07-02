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
    public class FindOutboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundOrdersDialogId = "editOutboundOrdersDialog";
        private const string DetailsOutboundOrdersDialogId = "detailsOutboundOrdersDialog";
        private const string DeleteOutboundOrdersDialogId = "deleteOutboundOrdersDialog";

        private const string FindOutboundOrdersDialogId = "findOutboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindOutboundOrdersDialog);
        private const string DialogKeyOptions = "findOutboundOrdersDialogOptions";
        private const string SearchColumnsKey = "FindOutboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "FindOutboundOrdersDialogSearchText";
        private const string EditColumnsKey = "FindOutboundOrdersDialogEditColumns";
        private const string EditTextKey = "FindOutboundOrdersDialogEditText";
        private const string SelectedRecordKey = "FindOutboundOrdersDialogSelectedRecordKey";

        private readonly IOutboundOrdersService _outboundordersService;
        OutboundOrdersPost _outboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorders" };
        string[] edit = { "Edit outboundorders" };
        string[] details = { "Display outboundorders" };
        string[] delete = { "Delete outboundorders" };

        public FindOutboundOrdersDialog(string id, IOutboundOrdersService outboundordersService, OutboundOrdersPost outboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundordersService = outboundordersService;
            _outboundordersPost = outboundordersPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditOutboundOrdersDialog(EditOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteOutboundOrdersDialog(DeleteOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new OutboundOrdersPost();
            step.Values[SelectedRecordKey] = _outboundordersPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("OutboundOrders");

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
            var outboundordersIndex = _outboundordersService.Index();
            var recordCountTotal = outboundordersIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "OutboundOrder":
                    var searchRecordsOutboundOrder = outboundordersIndex.Where(o => o.sOutboundOrder.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundOrder).Select(o => o.sOutboundOrder.ToString());
                    var recordCountOutboundOrder = searchRecordsOutboundOrder.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundorders. Your search resulted in {recordCountOutboundOrder} records. I show the top 15. Please choose a OutboundOrder or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsOutboundOrder.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "OrderReference":
                    var searchRecordsOrderReference = outboundordersIndex.Where(o => o.sOrderReference.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundOrder).Select(o => o.sOutboundOrder.ToString());
                    var recordCountOrderReference = searchRecordsOrderReference.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundorders. Your search resulted in {recordCountOrderReference} records. I show the top 15. Please choose a OutboundOrder or refine the search:"),
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
            var outboundordersIndex = _outboundordersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit outboundorders"))
            {

                if (selection.Value == "Refine search")
                {
                    ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrder = 0;
                }
                else if (selection.Value == "Exit outboundorders")
                {
                    ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrder = -1;
                }
                returnResult = await step.EndDialogAsync(
                (OutboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _outboundordersService.GetPost(outboundordersIndex.Where(o => o.sOutboundOrder == selection.Value).Select(o => o.ixOutboundOrder).First());
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
            var outboundordersIndex = _outboundordersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit outboundorders")
            {
                ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrder = -1;
                returnResult = await step.EndDialogAsync(
                (OutboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit outboundorders") || (selection.Value == "Display outboundorders") || (selection.Value == "Delete outboundorders"))
            {
                currentBotUserData.ixOutboundOrder = ((OutboundOrdersPost)step.Values[SelectedRecordKey]).ixOutboundOrder;
                switch (selection.Value)
                {
                    case "Edit outboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditOutboundOrdersDialogId, (OutboundOrdersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display outboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete outboundorders":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteOutboundOrdersDialogId, (OutboundOrdersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (OutboundOrdersPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


