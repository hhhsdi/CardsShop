namespace GpuStore.Models
{
    public class Gpu
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string MemorySize { get; set; }
        public string MemoryType { get; set; }
    }
}