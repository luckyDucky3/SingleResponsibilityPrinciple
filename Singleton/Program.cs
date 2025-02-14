namespace Singleton;

public class Engine
{
    private static Engine? _instance;

    public string Name { get; }

    private Engine(string name)
    {
        Name = name;
    }
    
    public static Engine GetInstance(string name)
    {
        return _instance ??= new Engine(name);
    }
    
}

public class Car
{
    public Engine? Instance;

    public void Collect(string name)
    {
        Instance = Engine.GetInstance(name);
    }
    
}

internal static class Program
{
    static void Main()
    {
        Car car = new Car();
        car.Collect("Двигатель v8.1");
        Console.WriteLine(car.Instance?.Name);
        
        car.Collect("Двигатель v12.0");
        Console.WriteLine(car.Instance?.Name);
    }
}