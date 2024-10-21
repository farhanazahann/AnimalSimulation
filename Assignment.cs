using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public class Assignment
{
    // Initialize random
    private static Random rand = new Random();
    static int rounds = 0;

    public static void Main()
    {
        // Window Setter
        Console.SetWindowSize(100, 35);
        Console.SetBufferSize(201, 71);

        // Get names from the give files
        List<string> catnames = GetNamesFromFile("Assignment 1 catnames.txt");
        List<string> snakenames = GetNamesFromFile("Assignment 1 snakenames.txt");

        //ArrayList of type Animals to store animals
        ArrayList<Animal> cats = new ArrayList<Animal>(3);
        ArrayList<Animal> snakes = new ArrayList<Animal>(3);
        ArrayList<Animal> birds = new ArrayList<Animal>(10);

        // Generate cats with random properties
        for (int i = 0; i < 3; i++)
        {
            Cat cat = new Cat();
            cat.ID = i + 10;
            cat.Name = GetName(catnames);
            cat.Age = rand.NextDouble() * 10;
            cat.Pos = new Position(rand.NextDouble() * 30.0, rand.NextDouble() * 30.0, 0, 30, 30, 0);

            cat.Breed = (Cat.CatBreeds)rand.Next(Enum.GetValues(typeof(Cat.CatBreeds)).Length);
            cats.AddFront(cat);
        }

        // Generate snakes with random properties
        for (int i = 3; i < 6; i++)
        {
            Snake snake = new Snake();
            snake.ID = i + 10;
            snake.Name = GetName(snakenames);
            snake.Age = rand.NextDouble() * 12;
            snake.Pos = new Position(rand.NextDouble() * 30.0, rand.NextDouble() * 30.0, 0, 30, 30, 0);
            snake.Length = rand.NextDouble() * 15.0;
            snake.Venomous = rand.Next(2) == 0;
            snakes.AddLast(snake);
        }

        // Merge the lists of snakes and cats
        ArrayList<Animal> animals = new ArrayList<Animal>(cats.GetCount() + snakes.GetCount());
        animals = animals.Merge(cats, snakes);



        // Generate birds with random properties
        for (int i = 0; i < 10; i++)
        {
            Birds bird = new Birds();
            bird.ID = i;
            bird.Name = ((Birds.BirdNames)rand.Next(Enum.GetValues(typeof(Birds.BirdNames)).Length)).ToString();
            bird.Age = rand.NextDouble() * 10;
            bird.Pos = new Position(rand.NextDouble() * 100.0, rand.NextDouble() * 70.0, rand.NextDouble() * 10.0, 100.0, 70.0, 10.0);
            birds.AddLast(bird);
        }

        // Merge the lists of animals and birds
        ArrayList<Animal> MergedList = new ArrayList<Animal>(6);
        animals = animals.Merge(animals, birds);
        MergedList.Merge(snakes, cats);
        Console.WriteLine(MergedList.StringPrintAllForward());
        // While loop to iterate over each case
        while (BirdCount(animals)!=0)
        {

            // Update screen
            ClearSpace();
            UpdateSpace(animals);
            PrintSpace();


            // foreach (Animal animal in animals)
            for (int i = 0; i < animals.GetCount(); i++)
            {
                Animal animal = animals.At(i);

                // If the animal is a cat
                if (animal is Cat cat)
                {
                    // Get a list if all the birds in range and eat them, or move to the nearest bird
                    ArrayList<Animal> birdsInRange = GetBirdsInRange(cat, animals, cat.Range);
                    if (birdsInRange.GetCount() > 0)
                    {
                        for (int j = 0; j< birdsInRange.GetCount(); j++)
                        {
                            Birds bird = (Birds)birdsInRange.At(j);
                            cat.Eat(animals, bird);
                        }
                    }
                    else
                    {
                        Animal nearestBird = GetNearestBird(cat, animals);
                        MoveAnimalTowardsBird(cat, nearestBird, cat.Speed);
                    }


                }
                // If the animal is a snake
                else if (animal is Snake snake)
                {
                    // Get a list if all the birds in range and eat them, or move to the nearest bird
                    ArrayList<Animal> birdsInRange = GetBirdsInRange(snake, animals, snake.Range);
                    if (birdsInRange.GetCount() > 0)
                    {
                        for (int j = 0; j< birdsInRange.GetCount(); j++)
                        {
                            Birds bird = (Birds)birdsInRange.At(j);
                            snake.Eat(animals, bird);
                        }
                    }
                    else
                    {
                        Animal nearestBird = GetNearestBird(snake, animals);
                        MoveAnimalTowardsBird(snake, nearestBird, snake.Speed);
                    }
                }

            }

            //foreach (Birds bird in birds)
            for (int i = 0; i< animals.GetCount(); i++)
            {
                Animal animal = animals.At(i);
                if (animal is Birds bird)
                    bird.MoveRandomly();
            }

            rounds++;

            System.Threading.Thread.Sleep(800);
        }
        ClearSpace();
        
        Console.WriteLine($"It took {rounds} rounds to eat all birds.");

        Console.ReadLine();

    }


    // Method to get the names from the files provided
    private static List<string> GetNamesFromFile(string filename)
    {
        List<string> names = new List<string>();
        string text;
        foreach (string line in File.ReadLines(filename))
        {
            if (filename == "Assignment 1 catnames.txt")
                text = line.Substring(line.IndexOf(' ') + 2);
            else
                text = line;

            names.Add(text);
        }
        // returns the list of names of the animals
        return names;
    }

    // Method to select a random name for the animal with no repetition
    private static string GetName(List<string> name)
    {
        int index = rand.Next(name.Count);
        string Name = name[index];
        name.RemoveAt(index);
        return Name;
    }

    // Method to get a list of all the birds in the range of the animal
    private static ArrayList<Animal> GetBirdsInRange(Animal animal, ArrayList<Animal> animals, double range)
    {
        ArrayList<Animal> birdsInRange = new ArrayList<Animal>(animals.GetCount());

        //foreach (Animal bird in birds)
        for (int i = 0; i < animals.GetCount(); i++)
        {
            Animal birds = animals.At(i);
            if (birds is Birds bird)
            {
                double distance = CalculateDistance(animal.Pos, bird.Pos);
                if (distance <= range)
                {
                    birdsInRange.AddLast(bird);
                }
            }
        }
        return birdsInRange;
    }

    // Method to calculate the nearest bird to the animal
    private static Animal GetNearestBird(Animal animal, ArrayList<Animal> animals)
    {
        Animal nearestBird = null;
        double minDistance = double.MaxValue;

        for (int i = 0; i < animals.GetCount(); i++)
        {
            Animal birds = animals.At(i);
            if (birds is Birds bird)
            {
                double distance = CalculateDistance(bird.Pos, animal.Pos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestBird = bird;
                }
            }
        }

        return nearestBird;

    }

    // Method to move towards the nearest bird
    private static void MoveAnimalTowardsBird(Animal animal, Animal bird, double speed)
    {
        if (bird==null)
        {
            return;
        }

        double dx = bird.Pos.X - animal.Pos.X;
        double dy = bird.Pos.Y - animal.Pos.Y;

        double distance = CalculateDistance(animal.Pos, bird.Pos);

        double scale = distance / speed;

        double moveX = dx * scale;
        double moveY = dy * scale;
        double moveZ = 0;

        animal.Move(moveX, moveY, moveZ);
    }

    // Method to calculate the distance between two animals
    private static double CalculateDistance(Position pos1, Position pos2)
    {
        double dx = pos2.X - pos1.X;
        double dy = pos2.Y - pos1.Y;
        double dz = pos2.Z - pos1.Z;

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    // Method to count the number of birds remaining in the ArrayList<Animal> animals
    private static int BirdCount(ArrayList<Animal> animals)
    {
        int bird_count = 0;

        for (int i = 0; i<animals.GetCount(); i++)
        {
            Animal animal = animals.At(i);
            if (animal is Birds birds)
            {
                bird_count++;
            }

        }

        return bird_count;
    }


    private static void ClearSpace()
    {
        Console.Clear();
    }

    private static void UpdateSpace(ArrayList<Animal> ani)
    {
        //  foreach (Animal animal in ani)
        for (int i = 0; i < ani.GetCount(); i++)
        {
            Animal animal = ani.At(i);
            double x = animal.Pos.X * 2;
            double y = animal.Pos.Y;
            Console.SetCursorPosition((int)x, (int)y);
            string[] symbol = animal.GetSymbol();
            Console.Write(symbol[0] + symbol[1]);
        }
    }

    private static void PrintSpace()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
    }

    }