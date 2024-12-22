using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using AvaloniaControls;
using AvaloniaControls.Controls;
using AvaloniaControls.Services;
using Microsoft.Extensions.DependencyInjection;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.UI.Services;
using TrackerCouncil.Smz3.UI.ViewModels;

namespace TrackerCouncil.Smz3.UI.Views;

public partial class MainWindow : RestorableWindow
{
    private readonly MainWindowService? _service;
    private readonly MainWindowViewModel _model;
    private readonly IServiceProvider? _serviceProvider;
    private SpriteDownloadWindow? _spriteDownloadWindow;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _model = new MainWindowViewModel();
    }

    public MainWindow(MainWindowService service, IServiceProvider? serviceProvider, OptionsFactory options)
    {
        _service = service;
        _serviceProvider = serviceProvider;
        GlobalScaleFactor = Math.Clamp(options.Create().GeneralOptions.UIScaleFactor, 1, 3);
        InitializeComponent();
        DataContext = _model = _service.InitializeModel(this);

        _service.SpriteDownloadStart += (sender, args) =>
        {
            _spriteDownloadWindow = new SpriteDownloadWindow();
            _spriteDownloadWindow.Closed += (o, eventArgs) =>
            {
                _spriteDownloadWindow = null;
            };
            _spriteDownloadWindow.ShowDialog(this);
        };

        _service.SpriteDownloadEnd += (sender, args) => _spriteDownloadWindow?.Close();
    }

    public void Reload()
    {
        SoloRomListPanel.Reload();
        MultiRomListPanel.Reload();
    }

    protected override string RestoreFilePath => Path.Combine(Directories.AppDataFolder, "Windows", "main-window.json");
    protected override int DefaultWidth => 800;
    protected override int DefaultHeight => 600;

    private void CloseUpdateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _model.DisplayNewVersionBanner = false;
    }

    private void GitHubUrlLink_OnClick(object? sender, RoutedEventArgs e)
    {
        CrossPlatformTools.OpenUrl(_model.NewVersionGitHubUrl);
    }

    private void IgnoreVersionLink_OnClick(object? sender, RoutedEventArgs e)
    {
        _service?.IgnoreUpdate();
    }

    private void DisableUpdatesLink_OnClick(object? sender, RoutedEventArgs e)
    {
        _service?.DisableUpdates();
    }

    private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (_service == null)
        {
            return;
        }

        _ = ITaskService.Run(_service.ValidateTwitchToken);
        _ = ITaskService.Run(_service.DownloadConfigsAsync);
        _ = ITaskService.Run(_service.DownloadSpritesAsync);

        if (_model.OpenSetupWindow && _serviceProvider != null)
        {
            var result = await _serviceProvider.GetRequiredService<SetupWindow>()
                .ShowDialog<SetupWindowCloseBehavior>(this, SetupWindowStep.Roms);
            if (result == SetupWindowCloseBehavior.OpenSettingsWindow)
            {
                _serviceProvider?.GetRequiredService<OptionsWindow>().ShowDialog(this);
            }
            else if (result == SetupWindowCloseBehavior.OpenGenerationWindow)
            {
                _ = SoloRomListPanel.OpenGenerationWindow();
            }
        }
    }

    private void OptionsMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        _serviceProvider?.GetRequiredService<OptionsWindow>().ShowDialog(this);
    }

    private void AboutButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _serviceProvider?.GetRequiredService<AboutWindow>().ShowDialog(this);
    }
}
