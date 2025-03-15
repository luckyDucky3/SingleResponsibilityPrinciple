using System;
using System.Collections.Generic;
using System.Reflection;

namespace FactoryMethod
{
    internal class Program
    {
        static void Main()
        {
            ShapeCreator factory = ShapeSelector.SelectShape();
            if (factory != null)
            {
                Shape shape = factory.Create();
                Console.WriteLine("\nСозданный объект:");
                shape.ShapeName();
                Console.WriteLine(shape); // Вывод ToString()
            }

            Console.ReadLine();
        }
    }

    // Интерфейс для всех фигур
    public interface Shape
    {
        void ShapeName();
    }

    // Классы фигур
    public class Rectangle : Shape
    {
        public void ShapeName() => Console.WriteLine("It's Rectangle");
        public override string ToString() => "Type Rectangle";
    }

    public class Circle : Shape
    {
        public void ShapeName() => Console.WriteLine("It's Circle");
        public override string ToString() => "Type Circle";
    }

    public class Point : Shape
    {
        public void ShapeName() => Console.WriteLine("It's Point");
        public override string ToString() => "Type Point";
    }

    // Абстрактный класс "Создатель фигур"
    public abstract class ShapeCreator
    {
        public abstract Shape Create();
    }

    // Фабрики для создания фигур
    public class RectangleCreator : ShapeCreator
    {
        public override Shape Create() => new Rectangle();
    }

    public class CircleCreator : ShapeCreator
    {
        public override Shape Create() => new Circle();
    }

    public class PointCreator : ShapeCreator
    {
        public override Shape Create() => new Point();
    }

    // Класс, автоматически собирающий доступные фабрики
    public class ShapeSelector
    {
        private static Dictionary<string, ShapeCreator> shapeFactories = new Dictionary<string, ShapeCreator>();

        static ShapeSelector()
        {
            int index = 1;
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ShapeCreator)))
                {
                    shapeFactories[index.ToString()] = (ShapeCreator)Activator.CreateInstance(type);
                    index++;
                }
            }
        }

        public static ShapeCreator SelectShape()
        {
            Console.WriteLine("Выберите фигуру:");
            foreach (var entry in shapeFactories)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value.GetType().Name.Replace("Creator", "")}");
            }
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            return shapeFactories.TryGetValue(choice, out ShapeCreator factory) ? factory : null;
        }
    }
}
