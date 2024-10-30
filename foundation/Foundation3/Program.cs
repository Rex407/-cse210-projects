using System;
using System.Collections.Generic;

public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min): " +
               $"Distance {GetDistance()} " +
               $"{(IsMetric() ? "km" : "miles")}, Speed: {GetSpeed()} " +
               $"{(IsMetric() ? "kph" : "mph")}, Pace: {GetPace()} " +
               $"{(IsMetric() ? "min per km" : "min per mile")}";
    }
    
    protected abstract bool IsMetric();
}

public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (GetDistance() / Minutes) * 60; // mph

    public override double GetPace() => Minutes / GetDistance(); // min per mile

    protected override bool IsMetric() => false; // Using miles
}

public class Cycling : Activity
{
    private double _speed; // in mph

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance() => (GetSpeed() * Minutes) / 60; // miles

    public override double GetSpeed() => _speed; // mph

    public override double GetPace() => 60 / GetSpeed(); // min per mile

    protected override bool IsMetric() => false; // Using miles
}

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0; // in km

    public override double GetSpeed() => (GetDistance() / Minutes) * 60; // kph

    public override double GetPace() => Minutes / GetDistance(); // min per km

    protected override bool IsMetric() => true; // Using kilometers
}

public class Program
{
    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 15.0),
            new Swimming(new DateTime(2022, 11, 5), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}