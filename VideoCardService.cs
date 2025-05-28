using System;
using System.Collections.Generic;

public class VideoCardService
{
    private readonly IVideoCardRepository _videoCardRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public VideoCardService(IVideoCardRepository videoCardRepository, IManufacturerRepository manufacturerRepository)
    {
        _videoCardRepository = videoCardRepository;
        _manufacturerRepository = manufacturerRepository;
    }

    public List<VideoCard> GetAllVideoCards()
    {
        return _videoCardRepository.GetAllVideoCards();
    }

    public VideoCard GetVideoCardById(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid ID");
        return _videoCardRepository.GetVideoCardById(id) ?? throw new KeyNotFoundException("Video card not found");
    }

    public void AddVideoCard(VideoCard videoCard)
    {
        ValidateVideoCard(videoCard);
        CheckManufacturerExists(videoCard.ManufacturerID);
        _videoCardRepository.AddVideoCard(videoCard);
    }

    public void UpdateVideoCard(VideoCard videoCard)
    {
        ValidateVideoCard(videoCard);
        CheckManufacturerExists(videoCard.ManufacturerID);
        if (_videoCardRepository.GetVideoCardById(videoCard.VideoCardID) == null)
            throw new KeyNotFoundException("Video card not found");

        _videoCardRepository.UpdateVideoCard(videoCard);
    }

    public void DeleteVideoCard(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid ID");
        if (_videoCardRepository.GetVideoCardById(id) == null)
            throw new KeyNotFoundException("Video card not found");

        _videoCardRepository.DeleteVideoCard(id);
    }

    public List<VideoCard> SearchVideoCards(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            throw new ArgumentException("Search term cannot be empty");

        return _videoCardRepository.SearchVideoCardsByName(searchTerm);
    }

    private void ValidateVideoCard(VideoCard videoCard)
    {
        if (videoCard == null) throw new ArgumentNullException(nameof(videoCard));
        if (string.IsNullOrWhiteSpace(videoCard.ModelName)) throw new ArgumentException("Model name required");
        if (videoCard.Price <= 0) throw new ArgumentException("Price must be positive");
        if (videoCard.ClockSpeedMHz <= 0 || videoCard.BoostClockSpeedMHz <= 0)
            throw new ArgumentException("Clock speeds must be positive");
    }

    private void CheckManufacturerExists(int manufacturerId)
    {
        if (_manufacturerRepository.GetManufacturerById(manufacturerId) == null)
            throw new ArgumentException("Manufacturer not found");
    }
}