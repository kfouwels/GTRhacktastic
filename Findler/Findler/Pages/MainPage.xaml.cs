﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Findler.Templates;

namespace Findler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ResultsPanel.Visibility = Visibility.Collapsed;
            QueryPanel.Visibility = Visibility.Visible;
        }

        public async void MainSequence()
        {
            QueryPanelFadeOut.Begin();

            //Search(ExpertIs.Text, ExpertFor.Text);
            var peopleData = await GetPeople(ExpertIs.Text, ExpertFor.Text);

            String output = "";

            foreach (var personReportCard in peopleData)
            {
                output += "\n" + personReportCard.Value.firstname + " - " + personReportCard.Value.lastname;
                foreach (var project in personReportCard.Value.projects)
                {
                    output += "\n>>" + project.title + " [" + project.updated + "] " + "\n";
                }
                //output += "\n";
            }

            DumpBlock.Text = output;
            ResultsPanelFadeIn.Begin();       
        }

        private void ExpertFor_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                MainSequence();
            }
        }

        private async void Search(string expertIs, string expertFor)
        {
            var fapi = new Services.ApiInteract();
            DumpBlock.Text = await fapi.ApiSearch(expertFor);
        }

        private async Task<Dictionary<string, PersonReportCard>>  GetPeople(string expertIs, string expertFor )
        {
            var fapi = new Services.ApiInteract();
            var register = await fapi.GetPeople(expertFor);
            return register;
        }
    }
}
