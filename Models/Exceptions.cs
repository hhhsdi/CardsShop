namespace GpuStore.Models
{
    public class GpuNotFoundException : Exception
    {
        public GpuNotFoundException(int id)
            : base($"Видеокарта с ID {id} не найдена") { }
    }

    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(int available)
            : base($"Недостаточно товара на складе. Доступно: {available}") { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("Неверный email или пароль") { }
    }
}