using FileSignatures;
using FileSignatures.Formats;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServiceLayer.Services;
using ServiceLayer.Settings;
using System.IO;
using System.Linq;
using System.Net;
using WebDav;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

//Swagger.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy("AllowOrigin",
            builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });

//Create webDavClient
var webDavClient = new WebDavClient(new WebDavClientParams()
{
    BaseAddress = new Uri(builder.Configuration["WebDAV:NextCloudServer"]),
    Credentials = new NetworkCredential
    {
        UserName = builder.Configuration["WebDAV:NextCloudUserName"],
        Password = builder.Configuration["WebDAV:NextCloudPassword"]
    }
});

//Additional settings
ServiceSettings additionalSettings = new()
{
    NextCloudServer = builder.Configuration["WebDAV:NextCloudServer"],
    NextCloudBasePath = builder.Configuration["WebDAV:NextCloudBasePath"],
    NextCloudStripPart = builder.Configuration["WebDAV:NextCloudStripPart"]
};

//Set allowed File Types. See: FileSignatures.Formats for available formats.
//Also see: https://github.com/neilharvey/FileSignatures/
var allowedFileList = new AllowedFileFormatList()
{
    AllowedFileList = new List<AllowedFileFormat>()
    {
        new AllowedFileFormat()
        {
            Extension = "jpg",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Jpeg>()
        },
        new AllowedFileFormat()
        {
            Extension = "png",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Png>()
        },
        new AllowedFileFormat()
        {
            Extension = "gif",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Gif>()
        },
        new AllowedFileFormat()
        {
            Extension = "pdf",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Pdf>()
        },
        new AllowedFileFormat()
        {
            Extension = "docx",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Word>()
        },
        new AllowedFileFormat()
        {
            Extension = "doc",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.WordLegacy>()
        },
        new AllowedFileFormat()
        {
            Extension = "rtf",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.RichTextFormat>()
        },
        new AllowedFileFormat()
        {
            Extension = "odt",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.OpenDocumentText>()
        }
    }
};

//Register services.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWebDavClient, WebDavClient>((IServiceProvider sp) => webDavClient);
builder.Services.AddSingleton<IServiceSettings, ServiceSettings>((IServiceProvider sp) => additionalSettings);
builder.Services.AddSingleton<IAllowedFileFormatList, AllowedFileFormatList>((IServiceProvider sp) => allowedFileList);
builder.Services.AddSingleton<IFileFormatInspector>(new FileFormatInspector());
builder.Services.AddScoped<INextCloudService, NextCloudService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();