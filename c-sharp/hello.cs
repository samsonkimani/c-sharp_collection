using System;

public class Point{
    public int X {get;}
    public int Y {get;}

    public Point(int x, int y) => (X, Y) = (x, y);
}

public class PointFactory(int numberOfPoints)
{
    public IEnumerable<Point> CreatePoints()
    {
        var random = new Random();
        for (int i = 0; i < numberOfPoints; i++)
        {
            yield return new Point(random.Next(), random.Next());
        }
    }
}

public class Pair<TFirst, TSecond>
{
    public TFirst First {get;}
    public TSecond Second {get;}

    public Pair(TFirst first, TSecond second) => (First, Second) = (first, second);
}

public class Point3D : Point
{
    public int Z {get;}

    public Point3D(int x, int y, int z) : base(x, y)
    {
        Z = z;
    }
}

public struct PointStructure
{
    public double X {get;}
    public double Y {get;}

    public PointStructure(double x, double y) => (X, Y) = (x, y);
}


// interfaces

interface IControl
{
    void Paint();
}

interface ITextBox : IControl{
    void SetText(string text);
}

interface IListBox : IControl
{
    void SetItems(string[] items);
}

interface IComboBox : IListBox, ITextBox {}


interface IDataBound
{
    void Bind(Binder b);
}


public class EditBox : IControl, IDataBound
{
    public void Paint() {}
    public void Bind(Binder b) {}
}

public class Binder {}

// enums

public enum SomeRootVegetable
{
    HorseRadish,
    Radish,
    Turnip
}

// enums as flags

[Flags]
public enum Seasons
{
    None = 0,
    Summer = 1,
    Autumn = 2,
    Winter = 4,
    Spring = 8,
    All = Summer | Autumn | Winter | Spring
}


class Squares
{
    public static void PrintSquares()
    {
        int i = 0;
        int j;
        while (i < 10)
        {
            j = i * i;
            Console.WriteLine($"{i} * {i} = {j}");
            i++;
        }
    }
}


class Entity
{
    static int s_nextSerialNo;
    int _serialNo;

    public Entity()
    {
        _serialNo = s_nextSerialNo++;
    }

    public int GetSerialNo()
    {
        return _serialNo;
    }

    public static int GetNextSerialNo()
    {
        return s_nextSerialNo;
    }

    public static void SetNextSerialNo(int value)
    {
        s_nextSerialNo = value;
    }
}

public abstract class Expression
{
    public abstract double Evaluate(Dictionary<string, object> var);
}

public class Constant : Expression
{
    double _value;
    
    public Constant(double value)
    {
        _value = value;
    }

    public override double Evalueate(Dictionary<string, object> vars)
    {
        return _value;
    }
}

public class VariableReference : Expression{
    string _name;

    public VariableReference(string name)
    {
        _name = name;
    }

    public override double Evaluate(Dictionary<string, object> vars)
    {
        object value = vars[_name] ?? throw new Exception($"Unknown variable: {_name}");
        return Convert.ToDouble(value);
    }
}

public class Operation : Expression{
    Expression _left;
    char _op;
    Expression _right;

    public Operation(Expression left, char op, Expression right)
    {
        _left = left;
        _op = op;
        _right = right;
    }

    public override double Evaluate(Dictionary<string, object> vars)
    {
        double x = _left.Evaluate(vars);
        double y = _right.Evaluate(vars);
        switch (_op)
        {
            case '+': return x + y;
            case '-': return x - y;
            case '*': return x * y;
            case '/': return x / y;
            default: throw new Exception($"Unknown operator: {_op}");
        }
    }
}

public class Program
{

    static void swap<T>(ref T lhs, ref T rhs) {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static void swapExample()
    {
        int a = 1;
        int b = 2;
        swap<int>(ref a, ref b);
        Console.WriteLine($"a: {a}, b: {b}");
    }

    static void Divide(int x, int y, out int result, out int reminder)
    {
        result = x / y;
        reminder = x % y;
    }

    static void DivideExample()
    {
        Divide(10, 3, out int result, out int reminder);
        Console.WriteLine($"result : {result}, reminder: {reminder}");
    }
    public static void Main()
    {
        var pointFactory = new PointFactory(10);
        foreach (var point in pointFactory.CreatePoints())
        {
            Console.WriteLine($"({point.X}, {point.Y})");
        }

        var pair = new Pair<int, string>(1, "two");
        Console.WriteLine($"({pair.First}, {pair.Second})");

        var point3D = new Point3D(1, 2, 3);
        Console.WriteLine($"({point3D.X}, {point3D.Y}, {point3D.Z})");

        var pointStructure = new PointStructure(1.3, 2.0);
        Console.WriteLine($"({pointStructure.X}, {pointStructure.Y})");

        EditBox editBox = new();
        IControl control = editBox;
        IDataBound dataBound = editBox;

        var turnip = SomeRootVegetable.Turnip;
        Console.WriteLine(turnip);
        var allSeasons = Seasons.All;
        Console.WriteLine(allSeasons);
        var startEquinox = Seasons.Spring | Seasons.Autumn;
        Console.WriteLine(startEquinox);

        (double Sum, int Count) t2 = (4.5, 3);
        Console.WriteLine(t2.Sum);
        Console.WriteLine(t2.Count);

        swapExample();
        DivideExample();

        Squares.PrintSquares();

        Entity.SetNextSerialNo(1000);
        Entity e1 = new();
        Entity e2 = new();
        Console.WriteLine($"e1.GetSetialNo(): {e1.GetSerialNo()}");
        Console.WriteLine($"e2.GetSerialNo(): {e2.GetSerialNo()}");
        Console.WriteLine($"Entity.GetNextSerialNo(): {Entity.GetNextSerialNo()}");
    }
}