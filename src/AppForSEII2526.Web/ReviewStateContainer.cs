
using AppForSEII2526.Web.API;


namespace AppForSEII2526.Web
{
    public class ReviewStateContainer
    {
        public ReviewForCreateDTO Review { get; set; } = new ReviewForCreateDTO()
        {
            ReviewItems = new List<ReviewItemsDTO>()
        };



        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddCarToReview(CarForReviewDTO car)
        {
            var existing = Review.ReviewItems.FirstOrDefault(ri => ri.CarID==car.Id);

            if (existing == null)
            {
                Review.ReviewItems.Add(new ReviewItemsDTO()
                {
                    CarID = car.Id,
                    Model = car.Modelo,
                    Color = car.Color,
                    Fueltype = car.FuelType,
                    Manufacturer = car.Manufacturer,
                    Rating = 1, //default rating
                    ReviewDescription = ""
                });
            }
           

        }
    
        

        //to delete cars from the list of selected cars
        public void RemoveReviewItemToReview(ReviewItemsDTO item)
        {
            Review.ReviewItems.Remove(item);

        }
        //we eliminate all the cars from the list
        public void ClearReviewCart()
        {
            Review.ReviewItems.Clear();

        }

        //we have already finished the process of purchasing, thus, we create a new Purchase 
        public void ReviewProcessed()
        {

            Review = new ReviewForCreateDTO()
            {
                ReviewItems = new List<ReviewItemsDTO>()
            };
        }

    }
}
