using GpuStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace GpuStore.Services
{
    public class GpuService
    {
        private readonly List<Gpu> _gpus = new List<Gpu>
        {
            new Gpu { Id = 1, Manufacturer = "NVIDIA", Model = "RTX 3080", Price = 79990, Stock = 5 },
            new Gpu { Id = 2, Manufacturer = "AMD", Model = "RX 6800 XT", Price = 69990, Stock = 3 }
        };

        public List<Gpu> GetAllGpus()
        {
            return _gpus;
        }

        public Gpu GetGpuById(int id)
        {
            var gpu = _gpus.FirstOrDefault(g => g.Id == id);
            if (gpu == null)
            {
                throw new GpuNotFoundException(id);
            }
            return gpu;
        }

        public void ReduceStock(int gpuId, int quantity)
        {
            var gpu = GetGpuById(gpuId);
            if (gpu.Stock < quantity)
            {
                throw new InsufficientStockException(gpu.Stock);
            }
            gpu.Stock -= quantity;
        }
    }
}