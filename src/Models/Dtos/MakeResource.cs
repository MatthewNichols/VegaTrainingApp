using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models.Dtos
{
	public class MakeResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<ModelResource> Models { get; set; } = new List<ModelResource>();
	}
}
