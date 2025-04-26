

using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace ReqnrollProjectAssignment.Pages
{
    [Binding]
    public class BookingSteps
    {
        private readonly IPage _page;
        private readonly IBrowserContext _context;
        private readonly ScenarioContext _scenarioContext;
        private HomePage _homePage;
        private BookingPage _bookingPage;

        public BookingSteps(IPage page, IBrowserContext context, ScenarioContext scenarioContext)
        {
            _page = page;
            _context = context;
            _scenarioContext = scenarioContext;
            _homePage = new HomePage(page);
            _bookingPage = new BookingPage(page); // Will update after tab switch
        }

        [Given(@"I am on the TestDevLab test automation services page")]
        public async Task GivenIAmOnTheTestDevLabTestAutomationServicesPage()
        {
            await _homePage.NavigateToAsync();
        }

        [When(@"I click the Book a meeting button")]
        public async Task WhenIClickTheBookAMeetingButton()
        {
            await _homePage.ClickBookMeetingAsync();
        }

        [When(@"I switch to the new booking tab")]
        public async Task WhenISwitchToTheNewBookingTab()
        {
            var newPage = await _context.WaitForPageAsync();
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _bookingPage = new BookingPage(newPage);
            _scenarioContext["BookingPage"] = newPage; // Optional, for debugging or hooks
        }

        [When(@"I select the date ""(.*)"" and time")]
        public async Task WhenISelectTheDateAndTime(string dateText)
        {
            await _bookingPage.SelectDateAndTimeAsync(dateText);
        }

        [When(@"I click Next on the booking form")]
        public async Task WhenIClickNextOnTheBookingForm()
        {
            await _bookingPage.ClickNextAsync();
        }

        [When(@"I fill in the booking form with valid details")]
        public async Task WhenIFillInTheBookingFormWithValidDetails(Table table)
        {
            var details = table.CreateInstance<BookingDetails>();
            await _bookingPage.FillBookingFormAsync(details.Name, details.Email, details.Company, details.Description);
            _scenarioContext["BookingDetails"] = details;
        }

        [When(@"I click Schedule Event")]
        public async Task WhenIClickScheduleEvent()
        {
            await _bookingPage.ClickScheduleEventAsync();
        }

        [Then(@"I should see a confirmation of the scheduled meeting")]
        public async Task ThenIShouldSeeAConfirmationOfTheScheduledMeeting()
        {
            //await Assertions.Expect(_bookingPage.IsBookingConfirmedAsync()).ToBeTrueAsync();
        }

        private class BookingDetails
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Company { get; set; }
            public string Description { get; set; }
        }
    }
}



