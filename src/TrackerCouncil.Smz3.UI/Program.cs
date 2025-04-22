﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using AvaloniaControls.Controls;
using AvaloniaControls.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSURandomizer.Services;
using MSURandomizerLibrary.Models;
using MSURandomizerLibrary.Services;
using Serilog;
using TrackerCouncil.Smz3.Data;
using TrackerCouncil.Smz3.Shared;

namespace TrackerCouncil.Smz3.UI;

sealed class Program
{
    internal static IHost? MainHost { get; private set; }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var loggerConfiguration = new LoggerConfiguration();

#if DEBUG
        loggerConfiguration = loggerConfiguration.MinimumLevel.Debug();
#else
        loggerConfiguration = args.Contains("-d")
            ? loggerConfiguration.MinimumLevel.Debug()
            : loggerConfiguration.MinimumLevel.Information();
#endif

        Log.Logger = loggerConfiguration
            .Enrich.FromLogContext()
            .WriteTo.File(Directories.LogPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
#if DEBUG
            .WriteTo.Debug()
            .WriteTo.Console()
#endif
            .CreateLogger();

        Log.Information("Starting SMZ3 Cas' Randomizer {Version}", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
        Log.Information("Config Path: {Directory}", Directories.ConfigPath);
        Log.Information("Sprite Path: {Directory}", Directories.SpritePath);
        Log.Information("Tracker Sprite Path: {Directory}", Directories.TrackerSpritePath);

        CopyDefaultFolder();

        MainHost = Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureLogging(logging =>
            {
                logging.AddSerilog(dispose: true);
            })
            .ConfigureServices(services =>
            {
                services.ConfigureServices();
            })
            .Build();

        MainHost.Services.GetRequiredService<ITaskService>();
        MainHost.Services.GetRequiredService<IControlServiceFactory>();
        MainHost.Services.GetRequiredService<AppInitializationService>().IsEnabled = false;

        InitializeMsuRandomizer();

        ExceptionWindow.GitHubUrl = "https://github.com/TheTrackerCouncil/SMZ3Randomizer/issues";
        ExceptionWindow.LogPath = Directories.LogFolder;

        using var source = new CancellationTokenSource();

        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            Log.Error(e, "[CRASH] Uncaught {Name}: ", e.GetType().Name);
            ShowExceptionPopup(e).ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .With(new Win32PlatformOptions() { RenderingMode = new List<Win32RenderingMode>() { Win32RenderingMode.Software }  })
            .With(new X11PlatformOptions() { UseDBusFilePicker = false })
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();

    private static void InitializeMsuRandomizer()
    {
        var settingsStream =  Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("TrackerCouncil.Smz3.UI.msu-randomizer-settings.yml");
        var msuInitializationRequest = new MsuRandomizerInitializationRequest()
        {
            MsuAppSettingsStream = settingsStream,
            LookupMsus = false
        };
#if DEBUG
        msuInitializationRequest.UserOptionsPath = "%LocalAppData%\\SMZ3CasRandomizer\\msu-user-settings-debug.yml";
#endif
        MainHost!.Services.GetRequiredService<IMsuRandomizerInitializationService>().Initialize(msuInitializationRequest);
    }

    private static async Task ShowExceptionPopup(Exception e)
    {
        Log.Error(e, "[CRASH] Uncaught {Name}: ", e.GetType().Name);
        Log.Error(e.StackTrace ?? "");
        var window = new ExceptionWindow();
        window.Show();
        await Dispatcher.UIThread.Invoke(async () =>
        {
            while (window.IsVisible)
            {
                await Task.Delay(500);
            }
        });
    }

    // Copies bundled data on Linux and Mac builds to the local data folder
    private static void CopyDefaultFolder()
    {
        var source = Path.Combine(AppContext.BaseDirectory, "DefaultData");
        if (!Directory.Exists(source))
        {
            return;
        }

        if (Directory.Exists(Directories.DefaultDataPath))
        {
            try
            {
                Directory.Delete(Directories.DefaultDataPath, true);
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to delete default data folder {Path}", Directories.DefaultDataPath);
                return;
            }
        }

        try
        {
            CopyDirectory(source, Directories.DefaultDataPath);
        }
        catch (Exception e)
        {
            Log.Error(e, "Unable to copy default data folder from {OldPath} to {NewPath}", source, Directories.DefaultDataPath);
            return;
        }

        Log.Information("Copied default data folder from {OldPath} to {NewPath}", source, Directories.DefaultDataPath);
    }

    private static void CopyDirectory(string source, string destination)
    {
        var sourceDirectory = new DirectoryInfo(source);
        if (!sourceDirectory.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory.FullName}");

        var directories = sourceDirectory.GetDirectories();
        Directory.CreateDirectory(destination);

        foreach (var file in sourceDirectory.GetFiles())
        {
            var targetFilePath = Path.Combine(destination, file.Name);
            file.CopyTo(targetFilePath);
        }

        foreach (var subDirectory in directories)
        {
            var newDestinationDir = Path.Combine(destination, subDirectory.Name);
            CopyDirectory(subDirectory.FullName, newDestinationDir);
        }
    }
}
