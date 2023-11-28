namespace City;

public class Basic
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public double[] Location { get; init; }

    public CityBasic(string name, double[] location)
    {
        if(location.Length != 2)
            throw new ArgumentException("Location needs two coordinates");
        Name = name;
        Location = location;
    }

    public CityBasic(ICity city)
    {
        Id = city.Id;
        Name = city.Name;
        Location = city.Location;
    }
}
