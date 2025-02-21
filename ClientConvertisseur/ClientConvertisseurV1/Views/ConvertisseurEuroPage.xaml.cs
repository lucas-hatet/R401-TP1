using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.



namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>



    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        private double nb;
        private ObservableCollection<Devise> devises;
        private Devise deviseSelect;
        private double resultat;



        public double Nb
        {
            get { return nb; }
            set { nb = value; }
        }
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set {
                devises = value;
                OnPropertyChanged("Devises");
            }
        }
        public Devise DeviseSelect
        {
            get { return deviseSelect; }
            set { deviseSelect = value; }
        }
        public double Resultat
        {
            get { return resultat; }
            set
            {
                resultat = value;
                OnPropertyChanged("Resultat");
            }
        }



        public ConvertisseurEuroPage()
        {
            this.DataContext = this;
            GetDataOnLoadAsync();
        }



        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:5267/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
                MessageAsync("API non disponible");
            else
                Devises = new ObservableCollection<Devise>(result);
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }



        private void btnConvertir_Click(object sender, RoutedEventArgs e)
        {
            if (DeviseSelect == null)
                MessageAsync("Veuillez sélectionner une devise");
            else
                Resultat = Nb * DeviseSelect.Taux;
        }



        private async void MessageAsync(string msg, string titre = "Erreur", string texteBtn= "OK")
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = titre,
                Content = msg,
                CloseButtonText = texteBtn
            };
            noWifiDialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }



    }
}
