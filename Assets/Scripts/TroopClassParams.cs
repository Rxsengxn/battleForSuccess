using UnityEngine;

public struct ConcreteConst
{

    public ConcreteConst(float healthConst, float movSpdConst, float dmgConst, float dmgRangeConst, float costConst, float dmgFrequency)
    {
        this.healthConst = healthConst;
        this.movSpdConst = movSpdConst;
        this.dmgConst = dmgConst;
        this.dmgRangeConst = dmgRangeConst;
        this.costConst = costConst;
        this.dmgFrequency = dmgFrequency;
    }

    public float healthConst;
    public float movSpdConst;
    public float dmgConst;
    public float dmgRangeConst;
    public float costConst;
    public float dmgFrequency;

    public override string ToString() => $"({healthConst}, {movSpdConst}," +
        $" {dmgConst}, {dmgRangeConst}, {costConst}, {dmgFrequency})";
}

/*public struct Coords
{
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public override string ToString() => $"({X}, {Y})";
}*/
public class TroopClassParams
{
    public string Name;
    public string[] TroopNames = new string[3];

    public float Health;
    public float DamageAmount;
    public float MovingSpeed;
    public float DamageRange;
    public float DmgFrequency;

    public bool IsFriendly;
    public bool AoE;

    public int Cost;



    public static ConcreteConst[] troopConsts = {

        //public static ConcreteConst troop1Consts = 
        new ConcreteConst(
            healthConst: 1f,
            movSpdConst: 1f,
            dmgConst: 1.5f,
            dmgRangeConst: 1f,
            costConst: 1f,
            dmgFrequency: 1f
        ),

        //public static ConcreteConst troop2Consts = 
        new ConcreteConst(
            healthConst: 0.8f,
            movSpdConst: 2f,
            dmgConst: 3f,
            dmgRangeConst: 3f,
            costConst: 2.5f,
            dmgFrequency: 0.8f
        ),

        //public static ConcreteConst troop3Consts =
        new ConcreteConst
        (
            healthConst: 3f,
            movSpdConst: 0.8f,
            dmgConst: 8f,
            dmgRangeConst: 0.8f,
            costConst: 5f,
            dmgFrequency: 1.5f
        )
    };




    //public TypeTroop[] TroopClasses = new TypeTroop[3];

    public TroopClassParams(string name, string[] troopNames, float health, float dmgAmount, float movSpd, float dmgRange, float dmgFrequency, int cost, bool isFriendly = true, bool aoE = false)
    {
        this.Name = name;
        this.TroopNames = troopNames;
        this.Health = health;
        this.DamageAmount = dmgAmount;
        this.MovingSpeed = movSpd;
        this.DamageRange = dmgRange;
        this.Cost = cost;
        this.IsFriendly = isFriendly;
        this.AoE = aoE;
        this.DmgFrequency = dmgFrequency;

        /*for (int i = 0; i < troopNames.Length; i++)
        {
            MakeTypeTroop(ClassName: name,
                            TroopName: troopNames[i],
                            health: health * troopConsts[i].healthConst,
                            movingSpeed: movSpd * troopConsts[i].movSpdConst,
                            damageAmount: dmgAmount * troopConsts[i].dmgConst,
                            damageRange: dmgRange * troopConsts[i].dmgRangeConst,
                            gameObject: troopObject,
                            cost: (int) (cost * troopConsts[i].costConst),
                            isFriendly: isFriendly,
                            AoE:aoE);
            //TroopClasses[i] = new TypeTroop(name, troops[i].TroopName, troops[i].Health, troops[i].MovingSpeed, troops[i].DamageAmount, troops[i].DamageRange, null, troops[i].IsFriendly, troops[i].AoE, troops[i].Cost);
        }*/
    }

    /*private TypeTroop MakeTypeTroop(string ClassName, string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, GameObject gameObject, int cost, bool isFriendly = true, bool AoE = false)
    {
        TypeTroop newTroop = new TypeTroop(ClassName, TroopName, health, movingSpeed, damageAmount, damageRange, gameObject, isFriendly, AoE, cost);
        newTroop.MakeAPool(5, newTroop);
        return newTroop;
    }*/
}