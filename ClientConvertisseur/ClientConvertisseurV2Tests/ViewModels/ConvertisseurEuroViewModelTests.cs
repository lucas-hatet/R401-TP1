using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        /// <summary>
        /// Test constructeur.
        /// </summary>
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);
        }

        /// <summary>
        /// Test GetDataOnLoadAsyncTest OK
        /// </summary>
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            
            //Act
            convertisseurEuro.GetDataOnLoadAsync(); Thread.Sleep(1000);

            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);

            CollectionAssert.AreEqual(convertisseurEuro.Devises, new ObservableCollection<Devise>(
                [
                    new Devise(1, "Dollar", 1.08),
                    new Devise(2, "Franc Suisse", 1.07),
                    new Devise(3, "Yen", 120)
                ]
            ));
        }

        /// <summary>
        /// Test conversion OK
        /// </summary>
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuroViewModel = new ConvertisseurEuroViewModel();
            convertisseurEuroViewModel.Nb = 100;
            //Création d'un objet Devise, dont Taux=1.07
            //Property DeviseSelectionnee = objet Devise instancié
            //Act
            //Appel de la méthode ActionSetConversion
            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 107
        }
    }

}