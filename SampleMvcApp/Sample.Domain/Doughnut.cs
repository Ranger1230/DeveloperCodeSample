using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain
{
    public class Doughnut
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Flavor")]
        public int FlavorId { get; set; }
        public Flavor Flavor { get; set; }
    }

    public class Flavor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doughnut> Doughnuts { get; set; }
    }

	public enum FlavorEnum
	{
		[Display(Name = "Glaze")]
		Glaze = 1,
		[Display(Name = "Chacolate")]
		Chacolate = 2,
		[Display(Name = "Vanilla")]
		Vanilla = 3,
		[Display(Name = "Sugar")]
		Sugar = 4
	}
}
