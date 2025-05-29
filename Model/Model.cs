public class VideoCard
{
    public int VideoCardID { get; set; }
    public int GPUID { get; set; }
    public string ModelName { get; set; }
    public int ManufacturerID { get; set; }
    public double Price { get; set; }
    public int ClockSpeedMHz { get; set; }
    public int BoostClockSpeedMHz { get; set; }
}

public class Manufacturer
{
    public int ManufacturerID { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
}