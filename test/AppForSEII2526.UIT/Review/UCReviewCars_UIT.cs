using AppForSEII2526.UIT.Review;
using Microsoft.VisualBasic.FileIO;
using OpenQA.Selenium.DevTools.V137.FedCm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Review
{
    public class CUReview_UIT : UC_UIT
    {
        private const int carId = 6; // Civic Honda
        public CUReview_UIT(ITestOutputHelper output) : base(output) { }
        private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }

        private void InitialStepsForReview()
        {
            Initial_step_opening_the_web_page();
            Precondition_perform_login();
            _driver.Navigate().GoToUrl(_URI + "Review/SelectCarsForReview");
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_01() // No poder continuar sin coches seleccionados
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: Verificar que no se puede continuar sin coches seleccionados
            Assert.True(selectPO.ReviewNotAvailable());
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_02() // Quitar un coche seleccionado para reseña
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Act
            selectPO.AddCarToReview(carId);
            selectPO.RemoveCarFromReview(carId);

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.True(
                selectPO.ReviewNotAvailable(),
                "No debería ser posible continuar sin coches seleccionados"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_03() // Filtrar coches por fabricante
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Act
            selectPO.SearchCars("Honda", "");

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.True(
                selectPO.IsCarShownByModel("Civic"),
                "Honda Civic debería estar visible al filtrar por Honda"
            );

        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_04() // Filtrar coches por tipo de combustible
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Act
            selectPO.SearchCars("", "Gasolina");

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.True(
                selectPO.IsCarNotShownByModel("Leaf e+"),
                "Nissan Leaf e+ NO debería estar visible al filtrar por Gasolina"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_05() // Filtrar coches por fabricante y tipo de combustible
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Act
            selectPO.SearchCars("Honda", "Gasolina");

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.True(
                selectPO.IsCarShownByModel("Civic"),
                "Honda Civic debería estar visible al filtrar por Honda y Gasolina"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_06() // Filtrar coches por fabricante y tipo de combustible sin resultados
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);

            // Act
            selectPO.SearchCars("Prueba", ""); // Fabricante que no existe

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.True(
                selectPO.IsCarNotShownByModel("Civic"), // Verificamos que no aparece ningún Civic
                "No debería aparecer ningún coche al filtrar por fabricante Prueba"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_07() // Reseña completa de un coche
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);
            var detailPO = new DetailReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: crear reseña (CustomerUserName se auto-rellena con elena@uclm.es)
            createPO.FillReviewerData("elena@uclm.es", "España", "Experto");
            createPO.FillReviewItemByCarId(carId, "5", "Muy buen coche");

            createPO.SubmitReview();
            createPO.ConfirmDialog();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: ReviewerUserName muestra el email del usuario logueado (elena@uclm.es)
            Assert.True(
                detailPO.CheckReviewHeader("elena@uclm.es", "España", "Experto")
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_08() // Modificar coches seleccionados y completar reseña
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);
            var detailPO = new DetailReviewPO(_driver, _output);

            // Act: seleccionar coche inicial
            selectPO.AddCarToReview(7);
            selectPO.Continue();

            // Act: comprobar que al volver los datos no desaparecen
            createPO.FillReviewerData("elena@uclm.es", "España", "Experto");

            // Act: volver atrás y seleccionar otro coche (carId)
            createPO.GoBackToSelectCars();
            selectPO.RemoveCarFromReview(7);
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: llenar la reseña y publicarla
            createPO.FillReviewItemByCarId(carId, "5", "Excelente coche");
            createPO.SubmitReview();
            createPO.ConfirmDialog();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: verificar que el coche que aparece en los detalles es el Honda Civic (carId 6)
            Assert.True(
                detailPO.CheckCarModelInDetail(carId),
                "El coche reseñado debe ser el Honda Civic"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_09() // Falta de datos (CustomerUserName vacío)
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: rellenar datos válidos y luego vaciar el campo CustomerUserName
            createPO.FillReviewerData("", "España", "Experto");
            createPO.OverrideCustomerUserName(""); // Vaciar (falla [Required] client-side)

            // Submit: DataAnnotationsValidator bloquea -> no abre diálogo
            createPO.SubmitReview();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: ValidationSummary muestra el error [Required] del cliente NSwag
            var errorMessage = createPO.GetValidationErrorText();
            Assert.True(
                errorMessage.Contains("The CustomerUserName field is required"),
                "Debería aparecer error de campo requerido cuando CustomerUserName está vacío"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_10() // Falta de datos (Pais)
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: dejar el campo País vacío (falla [Required] client-side)
            createPO.FillReviewerData("elena@uclm.es", "", "Experto"); // Sin pais

            // Submit: DataAnnotationsValidator bloquea -> no abre diálogo
            createPO.SubmitReview();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: ValidationSummary muestra el error [Required] del cliente NSwag
            var errorMessage = createPO.GetValidationErrorText();
            Assert.True(
                errorMessage.Contains("The Country field is required"),
                "Debería aparecer error de campo requerido cuando Country está vacío"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_11() // Falta de datos (Tipo de conductor)
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: dejar el DriverType en la opción vacía (falla [Required] client-side)
            createPO.FillReviewerData("elena@uclm.es", "España", ""); // Sin tipo de conductor

            // Submit: DataAnnotationsValidator bloquea -> no abre diálogo
            createPO.SubmitReview();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: ValidationSummary muestra el error [Required] del cliente NSwag
            var errorMessage = createPO.GetValidationErrorText();
            Assert.True(
                errorMessage.Contains("The DriverType field is required"),
                "Debería aparecer error de campo requerido cuando DriverType está vacío"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_12() // Falta de datos (CustomerUserName con formato de email incorrecto)
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: valor con formato de email incorrecto
            // El cliente NSwag NO tiene [EmailAddress], pasa validación client-side.
            // El servidor (API) sí tiene [EmailAddress] y lo rechaza.
            createPO.FillReviewerData("Elena", "España", "Experto");
            createPO.OverrideCustomerUserName("no_es_un_email");
            createPO.FillReviewItemByCarId(carId, "5", "Descripción");

            // El diálogo SÍ se abre (cliente lo deja pasar) y el servidor devuelve error
            createPO.SubmitReview();
            createPO.ConfirmDialog();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: error server-side con mensaje [EmailAddress] de la API
            var errorMessage = createPO.GetServerErrorText();
            Assert.True(
                errorMessage.Contains("not a valid e-mail address"),
                "Debería aparecer error de formato de email inválido"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_13() // Intentar crear reseña con usuario inexistente
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: rellenar datos válidos de reseña
            createPO.FillReviewerData("Usuario Prueba", "España", "Experto");
            createPO.FillReviewItemByCarId(carId, "5", "Muy buen coche");

            // Act: sobrescribir CustomerUserName con un email que no existe en el sistema
            createPO.OverrideCustomerUserName("usuario_noexiste@fake.com");

            // Enviar y confirmar el diálogo (email tiene formato válido, pasa validación client-side)
            createPO.SubmitReview();
            createPO.ConfirmDialog();

            // Esperar un momento para asegurar que el error se muestre
            System.Threading.Thread.Sleep(1000);

            // Assert: el servidor rechaza el usuario con el mensaje del controlador
            var errorMessage = createPO.GetServerErrorText();

            Assert.True(
                errorMessage.Contains("El nombre de usuario no está registrado"),
                "Debería aparecer un mensaje de error si el usuario no existe"
            );
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_14() // Intentar crear reseña con valoración fuera de rango
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: intentar crear reseña con valoración fuera de rango (mayor a 5)
            // El input tiene pattern="[1-5]*" -> el navegador bloquea el submit
            createPO.FillReviewerData("elena@uclm.es", "España", "Experto");
            createPO.FillReviewItemByCarId(carId, "6", "Descripción errónea");

            // Hacer clic en el botón "Publicar reseña"
            createPO.SubmitReview();

            // Esperar un momento para asegurar
            System.Threading.Thread.Sleep(1000);

            // Assert: No debe aparecer la ventana de confirmación (pattern HTML5 lo bloquea)
            var modalHeader = _driver.FindElements(By.ClassName("modal-header"));
            Assert.Empty(modalHeader);
        }

        [Fact]
        [Trait("LevelTesting", "Functional Testing")]
        public void UC4_15() // Intentar crear reseña con descripción vacía
        {
            // Arrange
            InitialStepsForReview();

            var selectPO = new SelectCarsForReviewPO(_driver, _output);
            var createPO = new CreateReviewPO(_driver, _output);

            // Act: seleccionar coche
            selectPO.AddCarToReview(carId);
            selectPO.Continue();

            // Act: intentar crear reseña con descripción vacía
            // El cliente no valida objetos anidados -> el diálogo SÍ se abre
            // El servidor rechaza porque "" tiene longitud 0 < MinimumLength 5
            createPO.FillReviewerData("elena@uclm.es", "España", "Experto");
            createPO.FillReviewItemByCarId(carId, "5", "");  // Descripción vacía

            // Submit y confirmar el diálogo (pasa validación client-side)
            createPO.SubmitReview();
            createPO.ConfirmDialog();

            // Esperar un momento para asegurar que el error del servidor se muestre
            System.Threading.Thread.Sleep(1000);

            // Assert: el servidor devuelve el error de StringLength del ReviewItemsDTO
            var errorMessage = createPO.GetServerErrorText();
            Assert.True(
                errorMessage.Contains("La descripción de la reseña debe tener una longitud máxima de 500 caracteres y minima de 5"),
                "Debería aparecer error de longitud mínima cuando la descripción está vacía"
            );
        }



    }
}
