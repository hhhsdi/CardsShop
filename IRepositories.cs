using System.Collections.Generic;

public interface IVideoCardRepository
{
    List<VideoCard> GetAllVideoCards();
    VideoCard GetVideoCardById(int id);
    void AddVideoCard(VideoCard videoCard);
    void UpdateVideoCard(VideoCard videoCard);
    void DeleteVideoCard(int id);
    List<VideoCard> SearchVideoCardsByName(string name);
}

public interface IManufacturerRepository
{
    List<Manufacturer> GetAllManufacturers();
    Manufacturer GetManufacturerById(int id);
}