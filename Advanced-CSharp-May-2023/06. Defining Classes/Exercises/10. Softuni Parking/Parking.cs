using System.Collections.Generic;
using System.Linq;

namespace SoftUniParking;

public class Parking
{
    private List<Car> cars;
    private int capacity;

    public List<Car> Cars
    {
        get => cars;
        set => cars = value;
    }

    public int Capacity
    {
        get => capacity;
        set => capacity = value;
    }

    public Parking(int capacity)
    {
        Capacity = capacity;
        Cars = new List<Car>();
    }

    public string AddCar(Car car)
    {
        if (Cars.Any(x => x.RegistrationNumber == car.RegistrationNumber))
        {
            return "Car with that registration number, already exists!";
        }

        if (Cars.Count == Capacity)
        {
            return "Parking is full!";
        }

        Cars.Add(car);
        return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
    }

    public string RemoveCar(string registrationNumber)
    {
        Car carToRemove = Cars.Find(x => x.RegistrationNumber == registrationNumber);
        if (carToRemove == null)
        {
            return "Car with that registration number, doesn't exist!";
        }

        Cars.Remove(carToRemove);
        return $"Successfully removed {registrationNumber}";
    }

    public Car GetCar(string registrationNumber)
    {
        return Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);
    }

    public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
    {
        Cars = Cars.Where(x => !registrationNumbers.Contains(x.RegistrationNumber)).ToList();
    }

    public int Count => Cars.Count;
}