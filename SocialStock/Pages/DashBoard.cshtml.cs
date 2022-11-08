using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialStock.BasicCompanyInfo;
using SocialStock.Financials;
using SocialStock.Response;

namespace SocialStock.Pages
{
    public class DashBoardModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public SSResponse SSResponse = new SSResponse();

        public async Task<IActionResult> OnGetAsync(string CompanySymbol)
        {

            await UpdateSSResponse(CompanySymbol);

            return Page();
        }
        public async Task UpdateSSResponse(string CompanySymbol)
        {
            SSResponse.CompanySymbol = CompanySymbol;
            await GetStockMetrics(CompanySymbol);
            await GetCompanyProfile(CompanySymbol);

        }
        private async Task GetCompanyProfile(string CompanySymbol)
        {
            HttpResponseMessage responseCompanyData = await client.GetAsync("https://finnhub.io/api/v1/stock/profile2?symbol=" + CompanySymbol + "&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseCompanyData.IsSuccessStatusCode)
            {
                string companyDataResult = await responseCompanyData.Content.ReadAsStringAsync();
                if (companyDataResult != "{}")
                {
                    SSResponse.CompanyData = CompanyData.FromJson(companyDataResult);
                }
                else
                {
                    SSResponse.IncorrectCompanySymbol = true;
                }


            }
        }
        private async Task GetStockMetrics(string CompanySymbol)
        {
            HttpResponseMessage responseFinancials = await client.GetAsync("https://finnhub.io/api/v1/stock/metric?symbol=" + CompanySymbol + "&metric=all&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg");
            if (responseFinancials.IsSuccessStatusCode)
            {
                string financialAPIResult = await responseFinancials.Content.ReadAsStringAsync();
                SSResponse.FinHubFinancials = FinHubFinancials.FromJson(financialAPIResult);

            }
        }
    }
}
