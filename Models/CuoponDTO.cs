
using System.Data;

namespace MinimalVilla.Models
{
    public class CuoponDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Persent { get; set; }
        public bool isActive { get; set; }
        public DateTime? created { get; set; } 
    }
}
