public class Snake : Animal
{
    public double length;
    public bool venomous;
    public double range;
    public double speed;

    public double Length { get; set; }
    public bool Venomous { get; set; }

    public double Range { get; set; }
    public double Speed { get; set; }

    public Snake()
    {
        Range = 3;
        Speed = 14;
    }

    public void Eat(ArrayList<Animal> animal, Birds bird)
    {
        animal.DeleteItem(bird);
        //  Console.WriteLine($"Snake: {Name} ate Bird: {bird.Name}");
    }

    public override string[] GetSymbol()
    {
        return new string[] { "S", ID.ToString() };
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Age: {Age:F1}, Position: {Pos}, Length: {Length}, Venomous: {Venomous}";
    }
}