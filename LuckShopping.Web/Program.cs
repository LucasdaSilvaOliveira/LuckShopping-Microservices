using LuckShopping.Web.Services.IServices;
using LuckShopping.Web.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductService>(c =>
                   c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
               );

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc",opt =>
{
    opt.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
    opt.GetClaimsFromUserInfoEndpoint = true;
    opt.ClientId = "luck_shopping";
    opt.ClientSecret = "my_super_secret";
    opt.ResponseType = "code";
    opt.ClaimActions.MapJsonKey("role", "role", "role");
    opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
    opt.TokenValidationParameters.NameClaimType = "name";
    opt.TokenValidationParameters.RoleClaimType = "role";
    opt.Scope.Add("luck_shopping");
    opt.SaveTokens = true;
    opt.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
