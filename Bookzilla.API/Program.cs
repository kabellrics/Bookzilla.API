using Bookzilla.API;
using Bookzilla.API.DataAccessLayer;
using Bookzilla.API.Mapper;
using Bookzilla.API.Models;
using Bookzilla.API.Repository;
using Bookzilla.API.Services.Implémentation;
using Bookzilla.API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
FTPConfig FTPsettings = builder.Configuration.GetSection("FTPConfig").Get<FTPConfig>();
const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllHeadersPolicy,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Add services to the container.
builder.Services.AddDbContext<BookzillaDbContext>(options => options.UseSqlite("Data Source = bookzilla.db"));
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICollectionService, CollectionService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<ISerieService, SerieService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICoverExtractorService, CoverExtractorService>();
builder.Services.AddTransient<IFTPService>(x=> new FTPService(FTPsettings));
builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(BookzillaMapper));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

var appInitializator = new AppInitializator();
appInitializator.CreateDbIfNotExists(app);
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(AllowAllHeadersPolicy);
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
