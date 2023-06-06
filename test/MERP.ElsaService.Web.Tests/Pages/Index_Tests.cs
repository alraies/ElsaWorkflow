using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MERP.ElsaService.Pages;

public class Index_Tests : ElsaServiceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
