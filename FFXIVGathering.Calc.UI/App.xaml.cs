using Accessibility;
using Autofac;
using Config.Net;
using FFXIVGathering.Calc.Core;
using FFXIVGathering.Calc.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FFXIVGathering.Calc.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer? _container;
        private MainWindow? _mainWindow;
        private IMainVm? _mainVm;
        private ILifetimeScope? _lifetimeScope;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            IoC.RegisterComponents(builder);
            RegisterViewModels(builder);
            _container = builder.Build();
            _lifetimeScope = _container.BeginLifetimeScope();
            _mainVm = _lifetimeScope.Resolve<IMainVm>();
            _mainVm.LoadSettings(_lifetimeScope.Resolve<IMainSettings>());
            _mainWindow = new MainWindow() { DataContext = _mainVm};
            _mainWindow.Loaded += _mainWindow_Loaded;
            _mainWindow.Show();
        }

        private void _mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mainWindow == null || _lifetimeScope == null)
            {
                return;
            }

            var settings = _lifetimeScope.Resolve<IMainSettings>();

            if (!double.IsNaN(settings.WindowTop))
            {
                _mainWindow.Top = settings.WindowTop;
            }

            if (!double.IsNaN(settings.WindowLeft))
            {
                _mainWindow.Left = settings.WindowLeft;
            }

            if (!double.IsNaN(settings.WindowWidth))
            {
                _mainWindow.Width = settings.WindowWidth;
            }

            if (!double.IsNaN(settings.WindowHeight))
            {
                _mainWindow.Height = settings.WindowHeight;
            }

            _mainWindow.WindowState = settings.IsWindowMaximised ? WindowState.Maximized : WindowState.Normal;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_lifetimeScope != null)
            {
                var settings = _lifetimeScope.Resolve<IMainSettings>();
                _mainVm?.SaveSettings(settings);

                if (_mainWindow != null)
                {
                    settings.WindowTop = _mainWindow.Top;
                    settings.WindowLeft = _mainWindow.Left;
                    settings.WindowHeight = _mainWindow.Height;
                    settings.WindowWidth = _mainWindow.Width;
                    settings.IsWindowMaximised = _mainWindow.WindowState == WindowState.Maximized;
                }

                _lifetimeScope.Dispose();
            }
            
            _container?.Dispose();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainVm>().As<IMainVm>();
            builder.RegisterInstance(new ConfigurationBuilder<IMainSettings>().UseIniFile("settings.ini").Build());
        }
    }
}
