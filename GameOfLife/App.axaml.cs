using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Bierman.Abm.Model;

namespace Bierman.Abm;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            var mainWindow = new MainWindow();

            var field = new Landscape();
            var game = new Game(field);
            game.Start();
            mainWindow.DataContext = field;

            lifetime.MainWindow = mainWindow;
        }
    }
}