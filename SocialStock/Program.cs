using Newtonsoft.Json;
using SocialStock.Pages;
using SocialStock.Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/api/v1", async (string? CompanySymbol) =>
{
    DashBoardModel dashBoardModel = new DashBoardModel();
    string response = "";
    if (CompanySymbol != null)
    {
        response = await dashBoardModel.apiCallAsync(CompanySymbol);
    }
    else
    {
        SSResponse SSResponse = new SSResponse();
        SSResponse.IncorrectCompanySymbol = true;
        SSResponse.message = "please provide a Company Symbol in the query";
        response = JsonConvert.SerializeObject(SSResponse, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        });
    }
    return response;

})
.WithName("api");

app.Run();
