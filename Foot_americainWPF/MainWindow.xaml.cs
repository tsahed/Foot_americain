﻿using ModelLayers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foot_americainWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(DAOpays thedaopays, DAOequipe thedaoequipe, DAOjoueur thedaojoueur, DAOposte thedaoposte)
        {
            InitializeComponent();
            Globale.DataContext = new viewModel.viewModelJoueur(thedaopays, thedaoposte, thedaojoueur, thedaoequipe);
        }
    }
}
