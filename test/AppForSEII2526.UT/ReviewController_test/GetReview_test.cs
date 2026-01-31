using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.ReviewDTOs;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReviewsController_test
{
    public class GetReview_test : AppForSEII25264SqliteUT
    {

        public GetReview_test()
        {

            ;

            var models = new List<Model>
            {
                new Model ("Golf"),
                new Model ("Q3"),
            };

            var cars = new List<Car>
            {
                new Car("Sedan","Azul","Volkswagen Golf","1.4","Volkswagen",25000,3,"Gasolina","17",models[0]),
                new Car("SUV","Negro","Audi Q3","2.0","Audi",45000,2,"Diésel","18",models[1])
            };

            ApplicationUser user = new ApplicationUser("1", "Rubén", "Cuesta", "ruben@uclm.es", "Calle OBISPOs s/n, Albacete");




            var review = new Review(DateTime.Now, "Rubén", "Spain", 0, new List<ReviewItem>(), user);

            review.ReviewItems.Add(new ReviewItem(cars[0], review));


            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(review);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReview_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            //Act
            var result = await controller.GetReviews(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReview_Found_test()
        {
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);

            var expectedReview = new ReviewDetailDTO(1, "Rubén", "Spain", 0, new List<ReviewItemsDTO>());
            expectedReview.ReviewItems.Add(new ReviewItemsDTO(1, "Golf", "Azul", "Gasolina", "Volkswagen", "Volkswagen Golf"));
            //Act
            var result = await controller.GetReviews(1);

            //Assert
            //we check that the response is OK and obtain the review
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reviewDTOActual = Assert.IsType<ReviewDetailDTO>(okResult.Value);
            var eq = expectedReview.Equals(reviewDTOActual);

            Assert.Equal(expectedReview, reviewDTOActual);


        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReview_id_null_test() {
            var mock = new Mock<ILogger<ReviewsController>>();
            ILogger<ReviewsController> logger = mock.Object;

            var controller = new ReviewsController(_context, logger);
            
            //Act
            var result = await controller.GetReviews(null);

            Assert.IsType<NotFoundResult>(result);
            ;


        }
    } }
