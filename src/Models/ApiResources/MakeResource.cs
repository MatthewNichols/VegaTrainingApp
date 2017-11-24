using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models.ApiResources
{
	public class MakeResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<KeyValuePairResource> Models { get; set; } = new List<KeyValuePairResource>();
	}
}
