using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReviewDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReviewsController_test
{
    public class CreateReview_test : AppForSEII25264SqliteUT
    {
        private const string _userName = "ruben@uclm.es";
        private const string _country = "Spain";
        private const string _driverType = "Experto";
        private const string _customerNameSurname = "Ruben Cuesta";

        private const string _car1Model = "Q3";
        private const string _car2Model = "Golf";

        public CreateReview_test()
        {
            var models = new List<Model>()
            {
                new Model(_car1Model),
                new Model(_car2Model),
            };

            var cars = new List<Car>()
            {
                new Car("Q3","Negro","Audi Q3","2.0","Audi",45000,2,"Diésel","18",models[0]),
                new Car("Sedan","Azul","Volkswagen Golf","1.4","Volkswagen",25000,3,"Gasolina","17",models[1]),
            };

            ApplicationUser user = new ApplicationUser("1", "Ruben", _customerNameSurname, _userName, "Avda. España s/n, Albacete");

            var review = new Review(DateTime.Now, _userName, _country, _driverType, new List<ReviewItem>(), user);
            review.ReviewItems.Add(new ReviewItem(cars[0].Id, review, "Muy buen coche", 5));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(review);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCaseFor_CreateReview()
        {
            var reviewNoItem = new ReviewForCreateDTO(_userName, _country, _driverType, new List<ReviewItemsDTO>());

            var reviewItems = new List<ReviewItemsDTO>()
            {
                new ReviewItemsDTO(2, _car2Model, "Azul", "Gasolina", "Volkswagen", 5, "Excelente"),
            };

            var reviewApplicationUser = new ReviewForCreateDTO("usuarioInexistente@uclm.es", _country, _driverType, reviewItems);

            var reviewCarNonExistent = new ReviewForCreateDTO(_userName, _country, _driverType,
                new List<ReviewItemsDTO>() { new ReviewItemsDTO(99, "ModeloFalso", "Blanco", "Gasolina", "MarcaX", 4, "No existe") });

            var reviewInvalidRating = new ReviewForCreateDTO(_userName, _country, _driverType,
                new List<ReviewItemsDTO>() { new ReviewItemsDTO(6, _car1Model, "Negro", "Diésel", "Audi", 7, "Rating inválido") });

            var allTest = new List<object[]>
            {
                new object[] { reviewNoItem, "Error! Debes seleccionar al menos un coche para reseñar" },
                new object[] { reviewApplicationUser, "Error! El nombre de usuario no está registrado" },
                new object[] { reviewCarNonExistent, "Error! El coche 'ModeloFalso' no está disponible" },
                new object[] { reviewInvalidRating, "Error! Rating must be greater than 0 and smaller than 6" },
            };

            return allTest;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCaseFor_CreateReview))]
          public async Task CreateReview_Error_test(ReviewForCreateDTO reviewDTO, string errorExpected)
        {
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            var result = await controller.CreateReview(reviewDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.Equal(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReview_Success_test()
        {
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            var reviewDTO = new ReviewForCreateDTO(_userName, _country, _driverType,
                new List<ReviewItemsDTO>()
                {
                    new ReviewItemsDTO(2, _car2Model, "Azul", "Gasolina", "Volkswagen", 5, "Excelente")
                });

            var expectedDTO = new ReviewDetailDTO(2, _userName, _country, _driverType,
                new List<ReviewItemsDTO>()
                {
                    new ReviewItemsDTO(2, _car2Model, "Azul", "Gasolina", "Volkswagen", 5, "Excelente")   
                });

            var result = await controller.CreateReview(reviewDTO);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualReviewDetailDTO = Assert.IsType<ReviewDetailDTO>(createdResult.Value);

            Assert.Equal(expectedDTO, actualReviewDetailDTO);
        }
    }
}
