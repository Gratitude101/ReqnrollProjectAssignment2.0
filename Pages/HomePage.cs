namespace ReqnrollProjectAssignment.Pages
{
    public class HomePage
    {
        private readonly IPage _page;
        private readonly ILocator _bookMeetingButtonLocator;
        private const string TestAutomationUrl = "https://www.testdevlab.com/services/test-automation";

        private static class Locators
        {
            public const string BookMeetingButtonName = "Book a meeting";
        }

        public HomePage(IPage page)
        {
            _page = page;
            _bookMeetingButtonLocator = _page.GetByRole(AriaRole.Button, new() { Name = Locators.BookMeetingButtonName, Exact = true });
        }

        public async Task NavigateToAsync()
        {
            await _page.GotoAsync(TestAutomationUrl);
        }

        public async Task ClickBookMeetingAsync()
        {
            await _bookMeetingButtonLocator.ScrollIntoViewIfNeededAsync();
            await _bookMeetingButtonLocator.HoverAsync();
            await _bookMeetingButtonLocator.ClickAsync();
        }
    }
}