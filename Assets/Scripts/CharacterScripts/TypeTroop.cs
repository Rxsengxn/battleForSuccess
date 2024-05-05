using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class TypeTroop
{
    private string TroopName;
    private float Health;
    private float DamageAmount;
    private float MovingSpeed;
    private float DamageRange;
    private GameObject gameObject;
    private bool IsFriendly;
    private bool AoE;
   

    // Lisa siia teised väljad vastavalt vajadusele

    public TypeTroop(string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, GameObject gameObject, bool isFriendly, bool AoE)
    {
        this.TroopName = TroopName;
        this.Health = health;
        this.MovingSpeed = movingSpeed;
        this.DamageAmount = damageAmount;
        this.DamageRange = damageRange;
        this.gameObject = gameObject;
        this.IsFriendly = isFriendly;
        this.AoE = AoE;
        //Debug.Log(this);
        // create a pool of objects to be used in game

        





        //Pools.Instance.AddPool(typeTroop:, prefab:gameObject, count:3);
        
        //this.HomeBase = homeBase;
        //this.EnemyBase = enemyBase;
    }

    public Troop GetTroop(TypeTroop typeTroop)
    {
        //return new Troop(troopType);
        return Pools.Instance.GetPooledObject(typeTroop).troop;
    }

    public void MakeAPool(int count, TypeTroop typeTroop)
    {
        Pools.Instance.AddPool(typeTroop, this.gameObject, count);
    }

    public void KillTroop(Troop troop)
    {
        Pools.Instance.KillPooledObject(troop.getPooledObject());
    }

    //getters for all the fields
    public string getName() { return TroopName; }
    public float getHealth() { return Health; }
    public float getDamageAmount() { return DamageAmount; }
    public float getMovingSpeed() { return MovingSpeed; }
    public float getDamageRange() { return DamageRange; }
    public bool getIsFriendly() { return IsFriendly; }
    public bool getAoE() { return AoE; }

  
}