using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWASM.Auth;
using Domain.Auth;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5258") });  //contact of WEB API
//create user - register as service
builder.Services.AddScoped<IUserService, UserHttpClient>();
//create post
builder.Services.AddScoped<IProductService, ProductHttpClient>();
//cat
builder.Services.AddScoped<ICategoryService, CategoryHttpClient>();
//subcat 
 builder.Services.AddScoped<ISubCategoryService, SubCategoryHttpClient>();
//create dayContent
builder.Services.AddScoped<IDayContentService, DayContentHttpClient>();
//log in auth
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);
await builder.Build().RunAsync();

