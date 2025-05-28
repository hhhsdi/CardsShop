using System;
using System.Collections.Generic;

public class ManufacturerService
{
    private readonly IManufacturerRepository _repository;

    public ManufacturerService(IManufacturerRepository repository)
    {
        _repository = repository;
    }

    public List<Manufacturer> GetAllManufacturers()
    {
        return _repository.GetAllManufacturers();
    }

    public Manufacturer GetManufacturerById(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid ID");
        return _repository.GetManufacturerById(id) ?? throw new KeyNotFoundException("Manufacturer not found");
    }
}