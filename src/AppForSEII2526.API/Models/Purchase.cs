using System;

public class Purchase
{
	public Purchase(){}

	public Purchase (string deliveryCarDealer, int id, string name, string paymentMethod, DateTime purchasingDate, decimal purchasingPrice, string surname)
	{
		DeliveryCarDealer = deliveryCarDealer;
		Id = id;
		Name = name;
		PaymentMethod = paymentMethod;
		PurchasingDate = purchasingDate;
		PurchasingPrice = purchasingPrice;
		Surname = surname;
    }

}
