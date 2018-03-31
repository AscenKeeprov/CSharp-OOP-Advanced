public interface IArmoury
{
    void StoreWeapon(IWeapon weapon);
    IWeapon TakeWeapon(string weaponName);
}