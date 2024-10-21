
public class Position
{
    private double x;
    private double y;
    private double z;
    private double max_x;
    private double max_y;
    private double max_z;

    public Position(double x, double y, double z, double max_x, double max_y, double max_z)
    {
        this.max_x = max_x;
        this.max_y = max_y;
        this.max_z = max_z;
        this.X = x;
        this.Y = y;
        this.Z = z;

    }

    public double X
    {
        get { return x; }
        set { this.x = Math.Clamp(value, 0, max_x); }
    }
    public double Y
    {
        get { return y; }
        set { this.y = Math.Clamp(value, 0, max_y); }
    }
    public double Z
    {
        get { return z; }
        set { this.z = Math.Clamp(value, 0, max_z); }
    }

    public void Move(double dx, double dy, double dz)
    {
        X += dx;
        Y += dy;
        Z += dz;
        X = Math.Clamp(X, 0, 100);
        Y = Math.Clamp(Y, 0, 70);
        Z = Math.Clamp(Z, 0, 2);
    }

    public override string ToString()
    {
        return $"({X:F1}, {Y:F1}, {Z:F1})";
    }

}
