public interface IWeapon : ISocketable
{
    string Name { get; }
    ERarity Rarity { get; }
}
