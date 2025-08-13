using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Reporter.BusinessLayer.Abstract;
using Reporter.BusinessLayer.Concrete;
using Reporter.DataAccessLayer.Abstract;
using Reporter.DataAccessLayer.Context;
using Reporter.DataAccessLayer.EntityFramework;
using Reporter.EntityLayer.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();

builder.Services.AddScoped<ISliderService, SliderManager>();
builder.Services.AddScoped<ISliderDal, EfSliderDal>();

builder.Services.AddScoped<IArticleService, ArticleManager>();
builder.Services.AddScoped<IArticleDal, EfArticleDal>();

builder.Services.AddScoped<ITagService, TagManager>();
builder.Services.AddScoped<ITagDal, EfTagDal>();

builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICommentDal, EfCommentDal>();

builder.Services.AddHttpClient<IToxicityDetectionService, ToxicityManager>();
builder.Services.AddHttpClient<ITranslationService, TranslationManager>();
builder.Services.AddScoped<ISlugService, SlugManager>();


builder.Services.AddDbContext<ArticleContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ArticleContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "articleDetail",
    pattern: "Article/ArticleDetail/{Slug}",
    defaults: new { controller = "Article", action = "ArticleDetail" }
).WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
