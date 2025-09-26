using Microsoft.Playwright;
using PlaywrightTests.Pages;
using Xunit;
using Allure.Xunit.Attributes;
using Allure.Net.Commons;


namespace PlaywrightTests.Tests;

[AllureSuite("Funda UI Tests")]
public class FundaTests : PlaywrightTestBase
{
    public object Steps { get; private set; }

    private async Task<(LandingPage landingPage, SearchResultsPage searchResultsPage, HeaderPage headerPage)> InitializePagesAsync(IPage page)
    {
        var landingPage = new LandingPage(page);
        var searchResultsPage = new SearchResultsPage(page);
        var headerPage = new HeaderPage(page);
        return (landingPage, searchResultsPage, headerPage);
    }

    private async Task GoToLandingPageAsync(LandingPage landingPage, HeaderPage headerPage)
    {
        await landingPage.GoTo("https://www.funda.nl/");
        await landingPage.AcceptCookies();
        await headerPage.WaitForLandingPage();
    }



[AllureSuite("Funda UI Tests")]
[AllureFeature("Homepage")]
[AllureStory("Check homepage load and navigation")]
[Fact]
public async Task CheckHomePageLoad()
{
    var page = await CreateFundaPageAsync();


    AllureLifecycle.Instance.StartStep(new StepResult { name = "Navigate to homepage" });
    await page.GotoAsync("https://www.funda.nl/");
    AllureLifecycle.Instance.StopStep();

    AllureLifecycle.Instance.StartStep(new StepResult { name = "Verify page title" });
    var title = await page.TitleAsync();
    Assert.Contains("Funda", title);
    AllureLifecycle.Instance.StopStep();


    await CaptureScreenshotAsync(page, "Homepage Screenshot");
}

[Fact]
[AllureSuite("Funda UI Tests")]
[AllureFeature("Search")]
[AllureStory("Verify search and filtering functionality")]
public async Task VerifySearchAndFiltering()
{
    var page = await CreateFundaPageAsync();

    AllureLifecycle.Instance.StartStep(new StepResult { name = "Initialize page objects" });
    var (landingPage, searchResultsPage, headerPage) = await InitializePagesAsync(page);
    AllureLifecycle.Instance.StopStep();

    AllureLifecycle.Instance.StartStep(new StepResult { name = "Navigate to landing page" });
    await GoToLandingPageAsync(landingPage, headerPage);
    AllureLifecycle.Instance.StopStep();

    AllureLifecycle.Instance.StartStep(new StepResult { name = $"Search for city: {Data.TestData.City}" });
    await landingPage.SearchCity(Data.TestData.City);
    AllureLifecycle.Instance.StopStep();

    AllureLifecycle.Instance.StartStep(new StepResult { name = "Verify search results" });
    await searchResultsPage.VerifyCitySelectionAndResults();
    AllureLifecycle.Instance.StopStep();
}

}
