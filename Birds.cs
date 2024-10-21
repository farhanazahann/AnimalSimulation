
public class Birds : Animal
{
    public enum BirdNames { Tweety, Zazu, Iago, Hula, Manu, Couscous, Roo, Tookie, Plucky, Kiwi };


    public void MoveRandomly()
    {
        Random rand = new Random();
        double dx = rand.NextDouble() * rand.Next(-10, 10);
        double dy = rand.NextDouble() * rand.Next(-10, 10);
        double dz = rand.NextDouble() * rand.Next(-2, 2);
        Pos.Move(dx, dy, dz);
    }

    public override string[] GetSymbol()
    {
        return new string[] { "B", ID.ToString() };
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Age: {Age:F1}, Position: {Pos}";
    }
}