using System.Collections.Generic;

public interface IWarehouse
{
    IReadOnlyDictionary<string, int> Ammunitions { get; }

    void AddAmmunitions(string ammunitionType, int ammunitionQuantity);
    bool TryEquipSoldier(ISoldier soldier);
    void TryEquipArmy(IArmy army);
}
