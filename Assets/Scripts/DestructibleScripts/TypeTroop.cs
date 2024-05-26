using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class TypeTroop
{
    public string TroopName { private set; get; }
    public float Health { private set; get; }
    public float DamageAmount { private set; get; }
    public float MovingSpeed { private set; get; }
    public float DamageRange { private set; get; }
    public float DamageFrequency { private set; get; }
    public GameObject gameObject { private set; get; }
    public bool IsFriendly { private set; get; }
    public bool AoE { private set; get; }
    public string ClassName { private set; get; }
    public int Cost { private set; get; }

    // Lisa siia teised väljad vastavalt vajadusele


    public TypeTroop(string className, string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, float damageFrequency, GameObject gameObject, bool isFriendly, bool AoE, int cost)
    {
        this.TroopName = TroopName;
        this.Health = health;
        this.MovingSpeed = movingSpeed;
        this.DamageAmount = damageAmount;
        this.DamageRange = damageRange;
        this.DamageFrequency = damageFrequency;
        this.gameObject = gameObject;
        this.IsFriendly = isFriendly;
        this.AoE = AoE;

        this.ClassName = className;
        this.Cost = cost;
    }

    public Troop GetTroop(TypeTroop typeTroop)
    {
        return Pools.Instance.GetPooledObject(typeTroop).troop;
    }

    public void MakeAPool(int count, TypeTroop typeTroop)
    {
        Pools.Instance.AddPool(typeTroop, this.gameObject, count);
    }

    public void KillTroop(Troop troop)
    {
        Pools.Instance.KillPooledObject(troop.Po);
    }

    //getters for all the fields
    //public string getName() { return TroopName; }
    /*public float getHealth() { return Health; }
    public float getDamageAmount() { return DamageAmount; }
    public float getMovingSpeed() { return MovingSpeed; }
    public float getDamageRange() { return DamageRange; }
    public bool getIsFriendly() { return IsFriendly; }
    public bool getAoE() { return AoE; }
    public string getClassName() { return ClassName; }*/

    public GameObject getThrowables()
    {
        return Resources.Load("Prefabs/Throwable") as GameObject;
    }

    /*public int getCost()
    {
        return this.Cost;
    }*/

    /*public RuntimeAnimatorController GetAnimator()
    {
        return Resources.Load("Animation/Archer/Archer Animation.controller") as RuntimeAnimatorController;
    }*/

    /*public controller GetAnimator()
    {
        return gameObject.GetComponent<Animation>();
    }*/


}