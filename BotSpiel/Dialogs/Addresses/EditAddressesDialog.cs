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
    public class EditAddressesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditAddressesDialogId = "editAddressesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string StreetAndNumberOrPostOfficeBoxOnePromptId = "streetandnumberorpostofficeboxonePrompt";
        private const string StreetAndNumberOrPostOfficeBoxTwoPromptId = "streetandnumberorpostofficeboxtwoPrompt";
        private const string StreetAndNumberOrPostOfficeBoxThreePromptId = "streetandnumberorpostofficeboxthreePrompt";
        private const string CityOrSuburbPromptId = "cityorsuburbPrompt";
        private const string ZipOrPostCodePromptId = "ziporpostcodePrompt";
        private const string StateOrProvincePromptId = "stateorprovincePrompt";
        private const string CountryPromptId = "countryPrompt";

        private const string DialogKey = nameof(EditAddressesDialog);
        private const string DialogKeyOptions = "editAddressesDialogOptions";
        private const string SearchColumnsKey = "EditAddressesDialogSearchColumns";
        private const string SearchTextKey = "EditAddressesDialogSearchText";
        private const string EditColumnsKey = "EditAddressesDialogEditColumns";
        private const string EditTextKey = "EditAddressesDialogEditText";
        private const string SelectedRecordKey = "EditAddressesDialogSelectedRecordKey";

        private readonly IAddressesService _addressesService;
        AddressesPost _addressesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit addresses" };
        string[] edit = { "Edit addresses" };
        string[] details = { "Display addresses" };
        string[] delete = { "Delete addresses" };

        public EditAddressesDialog(string id, IAddressesService addressesService, AddressesPost addressesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_addressesService.VerifyAddressUnique(_addressesPost.ixAddress, value))
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

            AddDialog(new ChoicePrompt(ChoicePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              chooseEditColumnPrompt,
              editColumnPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> chooseEditColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string editColumn = "";
            string editText = "";

            step.Values[DialogKey] = new AddressesPost();
            step.Values[DialogKeyOptions] = (AddressesPost)step.Options;
            step.Values[DialogKey] = _addressesService.GetPost(((AddressesPost)step.Options).ixAddress);
            _addressesPost = _addressesService.GetPost(((AddressesPost)step.Options).ixAddress);
            step.Values[SelectedRecordKey] = _addressesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Addresses");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose an attribute to change:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> editColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice editColumn = (FoundChoice)step.Result;
            step.Values[EditColumnsKey] = editColumn.Value;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[EditColumnsKey])
            {
                case "StreetAndNumberOrPostOfficeBoxOne":
					returnResult = await step.PromptAsync(
						StreetAndNumberOrPostOfficeBoxOnePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxOne:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "StreetAndNumberOrPostOfficeBoxTwo":
					returnResult = await step.PromptAsync(
						StreetAndNumberOrPostOfficeBoxTwoPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxTwo:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "StreetAndNumberOrPostOfficeBoxThree":
					returnResult = await step.PromptAsync(
						StreetAndNumberOrPostOfficeBoxThreePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StreetAndNumberOrPostOfficeBoxThree:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "CityOrSuburb":
					returnResult = await step.PromptAsync(
						CityOrSuburbPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CityOrSuburb:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "ZipOrPostCode":
					returnResult = await step.PromptAsync(
						ZipOrPostCodePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ZipOrPostCode:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "StateOrProvince":
					returnResult = await step.PromptAsync(
						StateOrProvincePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StateOrProvince:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_addressesService.selectCountrySubDivisions().Select(ct => ct.sCountrySubDivision).ToList()),
						},
						cancellationToken);
                    break;
                case "Country":
					returnResult = await step.PromptAsync(
						CountryPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Country:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_addressesService.selectCountries().Select(ct => ct.sCountry).ToList()),
						},
						cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            switch (step.Values[EditColumnsKey])
            {
                case "StreetAndNumberOrPostOfficeBoxOne":
					var sStreetAndNumberOrPostOfficeBoxOne = (string)step.Result;
					((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxOne = sStreetAndNumberOrPostOfficeBoxOne;
                    break;
                case "StreetAndNumberOrPostOfficeBoxTwo":
					var sStreetAndNumberOrPostOfficeBoxTwo = (string)step.Result;
					((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxTwo = sStreetAndNumberOrPostOfficeBoxTwo;
                    break;
                case "StreetAndNumberOrPostOfficeBoxThree":
					var sStreetAndNumberOrPostOfficeBoxThree = (string)step.Result;
					((AddressesPost)step.Values[DialogKey]).sStreetAndNumberOrPostOfficeBoxThree = sStreetAndNumberOrPostOfficeBoxThree;
                    break;
                case "CityOrSuburb":
					var sCityOrSuburb = (string)step.Result;
					((AddressesPost)step.Values[DialogKey]).sCityOrSuburb = sCityOrSuburb;
                    break;
                case "ZipOrPostCode":
					var sZipOrPostCode = (string)step.Result;
					((AddressesPost)step.Values[DialogKey]).sZipOrPostCode = sZipOrPostCode;
                    break;
                case "StateOrProvince":
					FoundChoice _StateOrProvince = (FoundChoice)step.Result;
					var ixStateOrProvince = _addressesService.selectCountrySubDivisions().Where(ct => ct.sCountrySubDivision == _StateOrProvince.Value).Select(ct => ct.ixCountrySubDivision).First();
					((AddressesPost)step.Values[DialogKey]).ixStateOrProvince = ixStateOrProvince;
                    break;
                case "Country":
					FoundChoice _Country = (FoundChoice)step.Result;
					var ixCountry = _addressesService.selectCountries().Where(ct => ct.sCountry == _Country.Value).Select(ct => ct.ixCountry).First();
					((AddressesPost)step.Values[DialogKey]).ixCountry = ixCountry;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (AddressesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


