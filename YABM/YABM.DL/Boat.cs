using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YABM.DL
{
    public class Boat
    {
        public long BoatId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(10000)]
        public string Description { get; set; }
    }
}
