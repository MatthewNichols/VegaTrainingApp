using System.Collections.Generic;

namespace Vega.Models.ApiResources
{
	public class SaveVehicleResource
	{
		public SaveVehicleResource()
		{
			Features = new List<int>();
		}

		public int Id { get; set; }
		public int ModelId { get; set; }
		public Model Model { get; set; }
		public bool IsRegistered { get; set; }
		public ContactResource Contact { get; set; }
		public ICollection<int> Features { get; set; }
	}
}