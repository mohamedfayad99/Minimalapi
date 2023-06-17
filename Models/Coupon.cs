using Microsoft.VisualBasic;

namespace MinimalVilla.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Persent { get; set; }
        public bool isActive { get; set; }
        public DateTime? created { get; set; }= DateTime.Now;
        public DateTime? updated { get; set; }
    }
    
}
