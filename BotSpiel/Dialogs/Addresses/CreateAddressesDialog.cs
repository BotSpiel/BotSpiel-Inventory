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
    public class CreateAddressesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateAddressesDialogId = "createAddressesDialog";
       private const string StreetAndNumberOrPostOfficeBoxOnePromptId = "streetandnumberorpostofficeboxonePrompt";
        private const string StreetAndNumberOrPostOfficeBoxTwoPromptId = "streetandnumberorpostofficeboxtwoPrompt";
        private const string StreetAndNumberOrPostOfficeBoxThreePromptId = "streetandnumberorpostofficeboxthreePrompt";
        private const string CityOrSuburbPromptId = "cityorsuburbPrompt";
        private const string ZipOrPostCodePromptId = "ziporpostcodePrompt";
        private const string StateOrProvincePromptId = "stateorprovincePrompt";
        private const string CountryPromptId = "countryPrompt";

        private const string DialogKey = nameof(CreateAddressesDialog);
        private const string DialogKeyOptions = "createAddressesDialogOptions";
        private const string SearchColumnsKey = "CreateAddressesDialogSearchColumns";
        private const string SearchTextKey = "CreateAddressesDialogSearchText";
        private const string EditColumnsKey = "CreateAddressesDialogEditColumns";
        private const string EditTextKey = "CreateAddressesDialogEditText";
        private const string SelectedRecordKey = "CreateAddressesDialogSelectedRecordKey";

        private readonly IAddressesService _addressesService;
        AddressesPost _addressesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit addresses" };
        string[] edit = { "Edit addresses" };
        string[] details = { "Display addresses" };
        string[] delete = { "Delete addresses" };

        public CreateAddressesDialog(string id, IAddressesService addressesService, AddressesPost addressesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _addressesService = addressesService;
            _addressesPost = addressesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> addressValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_addressesService.VerifyAddressUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The address {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(StreetAndNumberOrPostOfficeBoxOnePromptId));
            AddDialog(new TextPrompt(StreetAndNumberOrPostOfficeBoxTwoPromptId));
            AddDialog(new TextPrompt(StreetAndNumberOrPostOfficeBoxThreePromptId));
            AddDialog(new TextPrompt(CityOrSuburbPromptId));
            AddDialog(new TextPrompt(ZipOrPostCodePromptId));
            AddDialog(new ChoicePrompt(StateOrProvincePromptId));
            AddDialog(new ChoicePrompt(CountryPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             StreetAndNumberOrPostOfficeBoxOnePrompt,
              StreetAndNumberOrPostOfficeBoxTwoPrompt,
              StreetAndNumberOrPostOfficeBoxThreePrompt,
              CityOrSuburbPrompt,
              ZipOrPostCodePrompt,
              StateOrProvincePrompt,
              CountryPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> StreetAndNumberOrPostOfficeBoxOnePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new AddressesPost();

            return await step.PromptAsync(
                StreetAndNumberOrPostOfficeBoxOnePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxOne:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StreetAndNumberOrPostOfficeBoxTwoPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sStreetAndNumberOrPostOfficeBoxOne = (string)step.Result;
            ((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxOne = sStreetAndNumberOrPostOfficeBoxOne;

            return await step.PromptAsync(
                StreetAndNumberOrPostOfficeBoxTwoPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxTwo:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StreetAndNumberOrPostOfficeBoxThreePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sStreetAndNumberOrPostOfficeBoxTwo = (string)step.Result;
            ((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxTwo = sStreetAndNumberOrPostOfficeBoxTwo;

            return await step.PromptAsync(
                StreetAndNumberOrPostOfficeBoxThreePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxThree:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CityOrSuburbPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sStreetAndNumberOrPostOfficeBoxThree = (string)step.Result;
            ((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxThree = sStreetAndNumberOrPostOfficeBoxThree;

            return await step.PromptAsync(
                CityOrSuburbPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CityOrSuburb:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ZipOrPostCodePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCityOrSuburb = (string)step.Result;
            ((AddressesPost)step.Values[DialogKey]).sCityOrSuburb = sCityOrSuburb;

            return await step.PromptAsync(
                ZipOrPostCodePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ZipOrPostCode:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StateOrProvincePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sZipOrPostCode = (string)step.Result;
            ((AddressesPost)step.Values[DialogKey]).sZipOrPostCode = sZipOrPostCode;

            return await step.PromptAsync(
                StateOrProvincePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StateOrProvince:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_addressesService.selectCountrySubDivisions().Select(ct => ct.sCountrySubDivision).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CountryPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _StateOrProvince = (FoundChoice)step.Result;
            var ixStateOrProvince = _addressesService.selectCountrySubDivisions().Where(ct => ct.sCountrySubDivision == _StateOrProvince.Value).Select(ct => ct.ixCountrySubDivision).First();
            ((AddressesPost)step.Values[DialogKey]).ixStateOrProvince = ixStateOrProvince;

            return await step.PromptAsync(
                CountryPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Country:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_addressesService.selectCountries().Select(ct => ct.sCountry).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixCountry = (Int64)step.Result;
            ((AddressesPost)step.Values[DialogKey]).ixCountry = ixCountry;


            return await step.EndDialogAsync(
                (AddressesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


