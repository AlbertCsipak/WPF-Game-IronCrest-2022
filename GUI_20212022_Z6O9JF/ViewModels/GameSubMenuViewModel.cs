﻿using GUI_20212022_Z6O9JF.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212022_Z6O9JF.ViewModels
{
    public class GameSubMenuViewModel : ObservableRecipient
    {
        public IClientLogic clientLogic { get; set; }
        public IGameLogic gameLogic { get; set; }
        public double CurrentVolume { get { return MainWindow.background_music.Volume; } }
        public ICommand ResumeCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                return
                    (bool)DependencyPropertyDescriptor
                    .FromProperty(DesignerProperties.
                    IsInDesignModeProperty,
                    typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }
        public GameSubMenuViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IClientLogic>(), Ioc.Default.GetService<IGameLogic>()) { }
        public GameSubMenuViewModel(IClientLogic clientLogic, IGameLogic gameLogic)
        {
            this.clientLogic = clientLogic;
            this.gameLogic = gameLogic;
            ResumeCommand = new RelayCommand(() =>
            {
                clientLogic.ESCChange("");
            });
            ExitCommand = new RelayCommand(() =>
            {
                ProcessStartInfo uj = new ProcessStartInfo();
                uj.FileName = "GUI_20212022_Z6O9JF.exe";
                Process.Start(uj);
                System.Threading.Thread.Sleep(1500);
                Application.Current.Shutdown();
            });

            Messenger.Register<GameSubMenuViewModel, string, string>(this, "Base", (recipient, msg) =>
            {
                OnPropertyChanged("CanSend");
            });
        }
    }
}

