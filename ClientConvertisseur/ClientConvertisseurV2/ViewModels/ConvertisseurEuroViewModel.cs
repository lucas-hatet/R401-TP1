using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel: ObservableObject
    {



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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }



        public IRelayCommand BtnSetConversion { get; }




        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }



        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:5267/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
                MessageAsync("API non disponible");
            else
                Devises = new ObservableCollection<Devise>(result);
        }



        public void ActionSetConversion()
        {
            if (DeviseSelect == null)
                MessageAsync("Veuillez sélectionner une devise");
            else
                Resultat = Nb * DeviseSelect.Taux;
        }



        private async void MessageAsync(string msg, string titre = "Erreur", string texteBtn = "OK")
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = titre,
                Content = msg,
                CloseButtonText = texteBtn
            };
            noWifiDialog.XamlRoot = App.MainRoot.XamlRoot;
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }



    }
}
