using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialStock.CompanyNews;
using SocialStock.MoodsNameSpace;

namespace SocialStock.Pages
{
    public class MoodsModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public Moods? moods { get; set; }
        public async Task<PageResult> OnGetAsync()
        {
            HttpResponseMessage responseNews = await client.GetAsync("https://is7024fall22finalproject20221104125806.azurewebsites.net/api/v1");
            if (responseNews.IsSuccessStatusCode)
            {
                string financialNewsResult = await responseNews.Content.ReadAsStringAsync();
                moods = Moods.FromJson(financialNewsResult);

            }
            return Page();
        }
    }
}
