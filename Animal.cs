
public abstract class Animal : IComparable<Animal>
{
    public int id;
    public string name;
    public double age;
    public Position pos;

    public int ID { get; set; }

    public string Name { get; set; }

    public double Age { get; set; }

    public Position Pos { get; set; }

    public abstract string[] GetSymbol();


    public void Move(double dx, double dy, double dz)
    {
        Pos.Move(dx, dy, dz);
    }

    public int CompareTo(Animal other)
    {
        return string.Compare(Name, other.Name);
    }

    public void Eat(Birds target)
    {

    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Age: {Age}, Position: {Pos}";
    }
}