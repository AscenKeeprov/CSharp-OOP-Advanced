using System;

public class CustomMath
{
    double value;

    public double Abs => Math.Abs(value);
    public double Floor => Math.Floor(value);

    public CustomMath(double value)
    {
	this.value = value;
    }
}
