using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>()
        };
        
        public decimal TotalPrice
        {
            get
            {
                return (decimal)Purchase.PurchaseItems.Sum(item => item.PurchasingPrice * item.Quantity);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddCarToPurchase(CarForPurchaseDTO car)
        {
            //before adding a car we checked whether it has been already added
            if (!Purchase.PurchaseItems.Any(ri => ri.CarID == car.Id))
                //we add it if it is not in the list
                Purchase.PurchaseItems.Add(new PurchaseItemDTO()
                {
                    CarID = car.Id,
                    Model = car.Model,
                    PurchasingPrice = car.PurchasingPrice,
                    Color = car.Color,
                }
            );

        }

        //to delete cars from the list of selected cars
        public void RemovePurchaseItemToPurchase(PurchaseItemDTO item)
        {
            Purchase.PurchaseItems.Remove(item);

        }
        //we eliminate all the cars from the list
        public void ClearPurchaseingCart()
        {
            Purchase.PurchaseItems.Clear();

        }

        //we have already finished the process of purchasing, thus, we create a new Purchase 
        public void PurchaseProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Purchase = new PurchaseForCreateDTO()
            {
                PurchaseItems= new List<PurchaseItemDTO>()
            };
        }

    }
}
