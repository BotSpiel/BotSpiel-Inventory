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
    public class CreateInventoryLocationSizesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInventoryLocationSizesDialogId = "createInventoryLocationSizesDialog";
       private const string InventoryLocationSizePromptId = "inventorylocationsizePrompt";
        private const string LengthPromptId = "lengthPrompt";
        private const string LengthUnitPromptId = "lengthunitPrompt";
        private const string WidthPromptId = "widthPrompt";
        private const string WidthUnitPromptId = "widthunitPrompt";
        private const string HeightPromptId = "heightPrompt";
        private const string HeightUnitPromptId = "heightunitPrompt";
        private const string UsableVolumePromptId = "usablevolumePrompt";
        private const string UsableVolumeUnitPromptId = "usablevolumeunitPrompt";

        private const string DialogKey = nameof(CreateInventoryLocationSizesDialog);
        private const string DialogKeyOptions = "createInventoryLocationSizesDialogOptions";
        private const string SearchColumnsKey = "CreateInventoryLocationSizesDialogSearchColumns";
        private const string SearchTextKey = "CreateInventoryLocationSizesDialogSearchText";
        private const string EditColumnsKey = "CreateInventoryLocationSizesDialogEditColumns";
        private const string EditTextKey = "CreateInventoryLocationSizesDialogEditText";
        private const string SelectedRecordKey = "CreateInventoryLocationSizesDialogSelectedRecordKey";

        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        InventoryLocationSizesPost _inventorylocationsizesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsizes" };
        string[] edit = { "Edit inventorylocationsizes" };
        string[] details = { "Display inventorylocationsizes" };
        string[] delete = { "Delete inventorylocationsizes" };

        public CreateInventoryLocationSizesDialog(string id, IInventoryLocationSizesService inventorylocationsizesService, InventoryLocationSizesPost inventorylocationsizesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsizesService = inventorylocationsizesService;
            _inventorylocationsizesPost = inventorylocationsizesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inventorylocationsizeValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inventorylocationsizesService.VerifyInventoryLocationSizeUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventorylocationsize {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(InventoryLocationSizePromptId, inventorylocationsizeValidator));
            AddDialog(new NumberPrompt<float>(LengthPromptId));
            AddDialog(new ChoicePrompt(LengthUnitPromptId));
            AddDialog(new NumberPrompt<float>(WidthPromptId));
            AddDialog(new ChoicePrompt(WidthUnitPromptId));
            AddDialog(new NumberPrompt<float>(HeightPromptId));
            AddDialog(new ChoicePrompt(HeightUnitPromptId));
            AddDialog(new NumberPrompt<float>(UsableVolumePromptId));
            AddDialog(new ChoicePrompt(UsableVolumeUnitPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             InventoryLocationSizePrompt,
              LengthPrompt,
              LengthUnitPrompt,
              WidthPrompt,
              WidthUnitPrompt,
              HeightPrompt,
              HeightUnitPrompt,
              UsableVolumePrompt,
              UsableVolumeUnitPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> InventoryLocationSizePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InventoryLocationSizesPost();

            return await step.PromptAsync(
                InventoryLocationSizePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocationSize:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sInventoryLocationSize = (string)step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).sInventoryLocationSize = sInventoryLocationSize;

            return await step.PromptAsync(
                LengthPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Length:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nLength = step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);

            return await step.PromptAsync(
                LengthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LengthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LengthUnit = (FoundChoice)step.Result;
            var ixLengthUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationSizesPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;

            return await step.PromptAsync(
                WidthPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Width:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nWidth = step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);

            return await step.PromptAsync(
                WidthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WidthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _WidthUnit = (FoundChoice)step.Result;
            var ixWidthUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationSizesPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;

            return await step.PromptAsync(
                HeightPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Height:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nHeight = step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);

            return await step.PromptAsync(
                HeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> UsableVolumePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HeightUnit = (FoundChoice)step.Result;
            var ixHeightUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationSizesPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;

            return await step.PromptAsync(
                UsableVolumePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a UsableVolume:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> UsableVolumeUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nUsableVolume = step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).nUsableVolume = Convert.ToDouble(nUsableVolume);

            return await step.PromptAsync(
                UsableVolumeUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a UsableVolumeUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixUsableVolumeUnit = (Int64)step.Result;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).ixUsableVolumeUnit = ixUsableVolumeUnit;


            return await step.EndDialogAsync(
                (InventoryLocationSizesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


