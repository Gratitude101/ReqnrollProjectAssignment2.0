using Microsoft.Playwright;
using Reqnroll;

namespace ReqnrollProjectAssignment.Drivers
{
    [Binding]
    public sealed class Setup
    {
        private readonly ScenarioContext _scenarioContext;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        public Setup(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "msedge",
                Headless = false,
                SlowMo = 50
            });
            _context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            });
            _page = await _context.NewPageAsync();

            _scenarioContext["BrowserContext"] = _context;
            _scenarioContext["Page"] = _page;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _page.CloseAsync();
            await _context.CloseAsync();
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}









