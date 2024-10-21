using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

public class Cat : Animal
{
    public double range;
    public double speed;

    public enum CatBreeds
    {
        Abyssinian,
        [Description("American Wirehair")]
        AmericanWirehair,
        Bengal,
        Himalayan,
        Ocicat,
        Serval
    }

    public CatBreeds breed;

    public CatBreeds Breed { get; set; }

    public double Range { get; set; }
    public double Speed { get; set; }

    public Cat()
    {
        Range = 8;
        Speed = 16;
    }

    public void Eat(ArrayList<Animal> animal, Birds bird)
    {
        animal.DeleteItem(bird);
        //  Console.WriteLine($"Cat: {Name} ate Bird: {bird.Name}");
    }

    public override string[] GetSymbol()
    {
        return new string[] { "C", ID.ToString() };
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Age: {Age:F1}, Position: {Pos}, Breed: {Breed}";
    }
}