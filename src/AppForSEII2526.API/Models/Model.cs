namespace AppForSEII2526.API.Models
{

	public class Model
	{
        public Model() { }
        public Model(string name)
        {
            Name = name;
        }
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.", MinimumLength = 4)]
        public string Name { get; set; }

        public IList<Car> Cars { get; set; }= new List<Car>();



    }
}
