using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// sauce https://docs.simpleinjector.org/en/latest/aspnetintegration.html
var container = new SimpleInjector.Container();
builder.Services.AddSimpleInjector(container, options =>
{
    // AddAspNetCore() wraps web requests in a Simple Injector scope and
    // allows request-scoped framework services to be resolved.
    options.AddAspNetCore()

        // Ensure activation of a specific framework type to be created by
        // Simple Injector instead of the built-in configuration system.
        // All calls are optional. You can enable what you need. For instance,
        // ViewComponents, PageModels, and TagHelpers are not needed when you
        // build a Web API.
        .AddControllerActivation();
    //.AddViewComponentActivation()
    //.AddPageModelActivation()
    //.AddTagHelperActivation();

    // Optionally, allow application components to depend on the non-generic
    // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
    // (Microsoft.Extensions.Localization) abstractions.
    options.AddLogging();
    //options.AddLocalization();
});


// Add application services. For instance:
container.Register<YABM.BL.IBoatRepository, YABM.BL.BoatRepository>(Lifestyle.Scoped);

var app = builder.Build();




// UseSimpleInjector() finalizes the integration process.
app.Services.UseSimpleInjector(container);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // if environemnet is developement generate database & data to use.
    using (var context = new YABM.DL.YABMContext())
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated(); // migration is now unsupported.
        context.AddSomeBoats();
    }
}




app.UseHttpsRedirection();
// TODO set it up so CORS will not bother you.
app.UseCors( x => x // **** VERY UNSAFE **** //
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();


container.Verify();
app.Run();

