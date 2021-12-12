// See https://aka.ms/new-console-template for more information
using Brady.Application.Commands.Handlers;
using Brady.Application.Commands.Handlers.Interface;
using Brady.Application.Entities.Input;
using Brady.Application.Events;
using Brady.Application.Services;
using Brady.Application.Services.Interface;
using Brady.Application.Strategies;
using Brady.Application.Strategies.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;

using IHost host = Host.CreateDefaultBuilder(args)   
    .ConfigureServices((_, services) =>
        services
        .AddLogging(configure => configure.AddConsole())
        .AddScoped<IProcessFileHandler, ProcessFileHandler>()
        .AddScoped<IXmlReadService, XmlReadService>()
        .AddScoped(typeof(IXmlWriteService<>), typeof(XmlWriteService<>))
        .AddScoped<IEmissionStrategy<GasGenerator>, GasEmissionStrategy>()
        .AddScoped<IEmissionStrategy<CoalGenerator>, CoalEmissionStrategy>()
        .AddScoped<IHeatRateStrategy<CoalGenerator>, HeatRateStrategy>()
        .AddScoped<ITotalStrategy<CoalGenerator>, CoalTotalStrategy>()
        .AddScoped<ITotalStrategy<GasGenerator>, GasTotalStrategy>()
        .AddScoped<ITotalStrategy<WindGenerator>, WindTotalStrategy>())
    .Build();

Console.Clear();

var inputFolder = ConfigurationManager.AppSettings.Get("inputFolder");
if (!ValidateFolderPath(inputFolder))
{
    return;
}

using var fileWatcher = new FileSystemWatcher(inputFolder);
fileWatcher.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;


fileWatcher.Created += OnCreated;


fileWatcher.Filter = "*.xml";
fileWatcher.IncludeSubdirectories = true;
fileWatcher.EnableRaisingEvents = true;

await host.RunAsync();


void OnCreated(object sender, FileSystemEventArgs e)
{
    ProcessFile(e.FullPath);
}

void ProcessFile(string fullPath)
{
    try
    {
        using IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        var processFileHandler = provider.GetRequiredService<IProcessFileHandler>();
        var outputFolder = ConfigurationManager.AppSettings.Get("outputFolder");
        if (!ValidateFolderPath(outputFolder))
        {
            return;
        }

        var referenceData = ConfigurationManager.AppSettings.Get("referenceData");
        if (!ValidateFolderPath(referenceData))
        {
            return;
        }
        var fileCreated = new FileCreated(fullPath, outputFolder, referenceData);
        processFileHandler.Handle(fileCreated);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }    
}

bool ValidateFolderPath(string folderPath)
{
    if (string.IsNullOrEmpty(folderPath))
    {
        Console.WriteLine($"Folder path {nameof(folderPath)} required");
        return false;
    }
    else if (!Directory.Exists(folderPath))
    {
        Console.WriteLine($"Invalid directory {folderPath}");
        return false;
    }
    return true;
}