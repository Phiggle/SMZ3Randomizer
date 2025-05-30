using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Threading;
using AvaloniaControls.Controls;
using AvaloniaControls.ControlServices;
using AvaloniaControls.Models;
using AvaloniaControls.Services;
using GitHubReleaseChecker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PySpeechService.Client;
using PySpeechService.TextToSpeech;
using TrackerCouncil.Smz3.Chat.Integration;
using TrackerCouncil.Smz3.Data;
using TrackerCouncil.Smz3.Data.Configuration;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.Data.Services;
using TrackerCouncil.Smz3.Shared;
using TrackerCouncil.Smz3.UI.ViewModels;
using TrackerCouncil.Smz3.UI.Views;

namespace TrackerCouncil.Smz3.UI.Services;

public class MainWindowService(
    IGitHubReleaseCheckerService gitHubReleaseCheckerService,
    OptionsFactory optionsFactory,
    ILogger<MainWindowService> logger,
    IChatAuthenticationService chatAuthenticationService,
    IGitHubConfigDownloaderService gitHubConfigDownloaderService,
    IGitHubFileSynchronizerService gitHubFileSynchronizerService,
    SpriteService spriteService,
    TrackerSpriteService trackerSpriteService,
    ConfigProvider configs,
    IServiceProvider serviceProvider) : ControlService
{
    private MainWindowViewModel _model = new();
    private RandomizerOptions _options = null!;

    public MainWindowViewModel InitializeModel(MainWindow window)
    {
        _options = optionsFactory.Create();
        _model.OpenSetupWindow = !_options.GeneralOptions.HasOpenedSetupWindow;
        ITaskService.Run(CheckForUpdates);
        ITaskService.Run(StartPySpeechService);
        return _model;
    }

    public void DisableUpdates()
    {
        _options.GeneralOptions.CheckForUpdatesOnStartup = false;
        _options.Save();
        _model.DisplayNewVersionBanner = false;
    }

    public void IgnoreUpdate()
    {
        _options.GeneralOptions.IgnoredUpdateUrl = _model.NewVersionGitHubUrl;
        _options.Save();
        _model.DisplayNewVersionBanner = false;
    }

    public async Task<bool> ValidateTwitchToken()
    {
        if (string.IsNullOrEmpty(_options.GeneralOptions.TwitchOAuthToken))
        {
            return true;
        }

        var isTokenValid = await chatAuthenticationService.ValidateTokenAsync(_options.GeneralOptions.TwitchOAuthToken, default);

        if (!isTokenValid)
        {
            _options.GeneralOptions.TwitchOAuthToken = string.Empty;
            var messageWindow = new MessageWindow(new MessageWindowRequest()
            {
                Message = "Your Twitch login has expired. Please go to Options and log in with Twitch again to re-enable chat integration features.",
                Title = "SMZ3 Cas’ Randomizer",
                Buttons = MessageWindowButtons.OK,
                Icon = MessageWindowIcon.Warning
            });
            messageWindow.ShowDialog();
            return false;
        }

        return true;
    }

    public event EventHandler? SpriteDownloadStart;

    public event EventHandler? SpriteDownloadEnd;

    public async Task StartPySpeechService()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        var pySpeechService = serviceProvider.GetService<IPySpeechService>();
        if (pySpeechService != null)
        {
            pySpeechService.AutoReconnect = true;
            await pySpeechService.StartAsync();
            await pySpeechService.SetSpeechSettingsAsync(new SpeechSettings());
        }
    }
    public async Task DownloadConfigsAsync()
    {
        var configsUpdated = gitHubConfigDownloaderService.InstallDefaultConfigFolder();

        if (string.IsNullOrEmpty(_options.GeneralOptions.Z3RomPath) ||
            !_options.GeneralOptions.DownloadConfigsOnStartup)
        {
            configs.LoadConfigProfiles();
            return;
        }

        var configSource = _options.GeneralOptions.ConfigSources.FirstOrDefault();
        if (configSource == null)
        {
            configSource = new ConfigSource() { Owner = "TheTrackerCouncil", Repo = "SMZ3CasConfigs" };
            _options.GeneralOptions.ConfigSources.Add(configSource);
        }

        if (await gitHubConfigDownloaderService.DownloadFromSourceAsync(configSource))
        {
            configsUpdated = true;
        }

        if (configsUpdated)
        {
            _options.Save();
            configs.LoadConfigProfiles();
        }
    }

    public async Task DownloadSpritesAsync()
    {
        if (string.IsNullOrEmpty(_options.GeneralOptions.Z3RomPath) ||
            !_options.GeneralOptions.DownloadSpritesOnStartup)
        {
            await spriteService.LoadSpritesAsync();
            trackerSpriteService.LoadSprites();
            return;
        }

        var spriteDownloadRequest = new GitHubFileDownloaderRequest
        {
            RepoOwner = "TheTrackerCouncil",
            RepoName = "SMZ3CasSprites",
            DestinationFolder = Directories.SpritePath,
            HashPath = Directories.SpriteHashYamlFilePath,
            InitialJsonPath = Directories.SpriteInitialJsonFilePath,
            ValidPathCheck = p => Sprite.ValidDownloadExtensions.Contains(Path.GetExtension(p).ToLowerInvariant()),
            ConvertGitHubPathToLocalPath = p => p.Replace("Sprites/", ""),
            DeleteExtraFiles = Directories.DeleteSprites
        };

        var toDownload = await gitHubFileSynchronizerService.GetGitHubFileDetailsAsync(spriteDownloadRequest);

        spriteDownloadRequest = new GitHubFileDownloaderRequest
        {
            RepoOwner = "TheTrackerCouncil",
            RepoName = "TrackerSprites",
            DestinationFolder = Directories.TrackerSpritePath,
            HashPath = Directories.TrackerSpriteHashYamlFilePath,
            InitialJsonPath = Directories.TrackerSpriteInitialJsonFilePath,
            ValidPathCheck = p => p.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || p.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) || p.EndsWith(".yml", StringComparison.OrdinalIgnoreCase),
            DeleteExtraFiles = Directories.DeleteSprites
        };

        toDownload.AddRange(await gitHubFileSynchronizerService.GetGitHubFileDetailsAsync(spriteDownloadRequest));

        var numToDownload = toDownload.Count(x => x.Action != GitHubFileAction.Nothing);

        if (numToDownload > 4)
        {
            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                SpriteDownloadStart?.Invoke(this, EventArgs.Empty);
                await gitHubFileSynchronizerService.SyncGitHubFilesAsync(toDownload);
                SpriteDownloadEnd?.Invoke(this, EventArgs.Empty);
            });
        }
        else if (numToDownload > 0)
        {
            await gitHubFileSynchronizerService.SyncGitHubFilesAsync(toDownload);
        }

        await spriteService.LoadSpritesAsync();
        trackerSpriteService.LoadSprites();
    }

    private async Task CheckForUpdates()
    {
        if (!_options.GeneralOptions.CheckForUpdatesOnStartup)
        {
            return;
        }

        var version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        try
        {
            var gitHubRelease = await gitHubReleaseCheckerService
                .GetGitHubReleaseToUpdateToAsync("TheTrackerCouncil", "SMZ3Randomizer", version ?? "", false);

            if (!string.IsNullOrWhiteSpace(gitHubRelease?.Url) && gitHubRelease.Url != _options.GeneralOptions.IgnoredUpdateUrl)
            {
                _model.DisplayNewVersionBanner = true;
                _model.NewVersionGitHubUrl = gitHubRelease.Url;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting GitHub release");
        }
    }
}

