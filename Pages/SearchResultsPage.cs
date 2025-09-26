using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages;

public class SearchResultsPage
{
    // Locators 
    private readonly IPage _page;

    private static readonly Regex CityExactRegex = new Regex("^Nieuw-Amsterdam$");
    private const string PageHeaderTestId = "pageHeader";

    // Constructor
    public SearchResultsPage(IPage page)
    {
        _page = page;
    }
    // Methods / Actions
    // Verify that the selected city is displayed correctly in the header and results
    public async Task VerifyCitySelectionAndResults()
    {
        await _page.Locator("div").Filter(new() { HasTextRegex = CityExactRegex }).First.WaitForAsync();
        await _page.GetByTestId(PageHeaderTestId).GetByText("in Nieuw-Amsterdam").WaitForAsync();
    }

}