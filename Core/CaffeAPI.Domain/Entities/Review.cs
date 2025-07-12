using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }//1-5 arası yıldız mantığında değerlendirme
        public DateTime CreatedAt { get; set; }
    }
}
