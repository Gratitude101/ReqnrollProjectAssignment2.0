namespace ReqnrollProjectAssignment.Pages
{
    public class BookingPage
    {
        private readonly IPage _page;
        private readonly ILocator _timeSlotLocator;
        private readonly ILocator _nextTimeButtonLocator;
        private readonly ILocator _scheduleEventButtonLocator;
        private readonly ILocator _confirmationMessageLocator;
        private readonly Dictionary<string, ILocator> _formInputLocators;

        private static class Locators
        {
            public const string TimeSlotName = "7:30am";
            public const string NextTimeButtonName = "Next 7:30am";
            public const string ScheduleEventButtonName = "Schedule Event"; // Adjust if different
            public const string ConfirmationMessageName = "Booking confirmed"; // Adjust based on actual confirmation
            public static readonly Dictionary<string, string> FormInputs = new()
            {
                { "Name", "Name *" },
                { "Email", "Email *" },
                { "Company", "Company *" },
                { "Description", "Topic description" }
            };
        }

        public BookingPage(IPage page)
        {
            _page = page;
            _timeSlotLocator = GetLocator(AriaRole.Button, new() { Name = Locators.TimeSlotName, Exact = true });
            _nextTimeButtonLocator = GetLocator(AriaRole.Button, new() { Name = Locators.NextTimeButtonName, Exact = true });
            _scheduleEventButtonLocator = GetLocator(AriaRole.Button, new() { Name = Locators.ScheduleEventButtonName, Exact = true });
            _confirmationMessageLocator = GetLocator(AriaRole.Alert, new() { Name = Locators.ConfirmationMessageName });

            _formInputLocators = new Dictionary<string, ILocator>();
            foreach (var (key, name) in Locators.FormInputs)
            {
                _formInputLocators[key] = GetLocator(AriaRole.Textbox, new() { Name = name, Exact = true });
            }
        }

        private ILocator GetLocator(AriaRole role, PageGetByRoleOptions options = null)
        {
            return _page.GetByRole(role, options);
        }

        public async Task SelectDateAndTimeAsync(string dateText)
        {
            var dateLocator = GetLocator(AriaRole.Button, new() { Name = dateText, Exact = true });
            await dateLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await dateLocator.ClickAsync();

            await _timeSlotLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await _timeSlotLocator.ClickAsync();
        }

        public async Task ClickNextAsync()
        {
            await _nextTimeButtonLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await _nextTimeButtonLocator.ClickAsync();
        }

        public async Task FillBookingFormAsync(string name, string email, string company, string description)
        {
            await _formInputLocators["Name"].FillAsync(name);
            await _formInputLocators["Email"].FillAsync(email);
            await _formInputLocators["Company"].FillAsync(company);
            await _formInputLocators["Description"].FillAsync(description);
        }

        public async Task ClickScheduleEventAsync()
        {
            await _scheduleEventButtonLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await _scheduleEventButtonLocator.ClickAsync();
        }

        public async Task<bool> IsBookingConfirmedAsync()
        {
            await _confirmationMessageLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10000 });
            return await _confirmationMessageLocator.IsVisibleAsync();
        }
    }
}


