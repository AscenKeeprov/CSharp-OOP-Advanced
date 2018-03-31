using System;

public class TrafficLight
{
    public ELight Light { get; set; }

    public TrafficLight(string light)
    {
	Light = (ELight)Enum.Parse(typeof(ELight), light);
    }

    public void ChangeLight()
    {
	int nextLightIndex = (int)Light + 1;
	nextLightIndex %= Enum.GetNames(typeof(ELight)).Length;
	Light = (ELight)nextLightIndex;
    }

    public override string ToString()
    {
	return Light.ToString();
    }
}
