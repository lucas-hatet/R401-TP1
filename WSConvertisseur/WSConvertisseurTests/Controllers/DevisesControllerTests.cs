using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {

        [TestMethod]
        public void Delete_NotOk_ReturnsNotFound()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.Delete(4);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_Ok_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.Delete(3);
            // Assert
            Assert.AreEqual(new Devise(3, "Yen", 120), result.Value);
        }

        [TestMethod]
        public void GetAll_ReturnsRightsItems()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetAll();
            // Assert
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(4);
            // Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Resultat de la requête de type ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat de type NotFoundResult");
            Assert.IsNull(result.Value, "Valeur null");
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsObjects()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var uneDevise = new Devise(4, "Livre sterling", 0.9);
            var result = controller.Post(uneDevise);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
            Assert.AreEqual(4, controller.GetAll().Count());

        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var uneDevise = controller.GetById(1).Value;
            var result = controller.Put(4, uneDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var uneDevise = new Devise(4, "Livre sterling", 0.9);
            var result = controller.Put(4, uneDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Put_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var uneDevise = new Devise(3, "Livre sterling", 0.9);
            var result = controller.Put(3, uneDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}