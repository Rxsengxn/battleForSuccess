using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    private bool GameOn = false;

    [SerializeField] private GameObject troopObj;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Destroying extra MMM");
            Destroy(this.gameObject);
        }
    }

    public int Difficulty;

    private Dictionary<string, TroopClassParams> allTtroopParams = new Dictionary<string, TroopClassParams>()
    {
        { "default", new TroopClassParams(   //TroopClassParams defaultClassParams =                                
                name:"default",
                troopNames:new string[] { "fighter", "archer", "tough" },
                health:25f,
                dmgAmount:2f,
                movSpd:1f,
                dmgRange:1.5f,
                cost:10,
                dmgFrequency: 1f) },

        { "archer", new TroopClassParams(   //TroopClassParams archerClassParams = 
                name: "archer",
                troopNames: new string[] { "archer", "ranger", "sniper" },
                health: 20f,
                dmgAmount: 3f,
                movSpd: 2f,
                dmgRange: 2.8f,
                cost: 15,
                dmgFrequency: 1f) },

        { "mage", new TroopClassParams(     //TroopClassParams mageClassParams = 
                name: "mage",
                troopNames: new string[] { "apprentice", "shapeshifter", "wizard" },
                health: 30f,
                dmgAmount: 2f,
                movSpd: 1f,
                dmgRange: 2.3f,
                cost: 12,
                dmgFrequency: 1f) },

        { "tough", new TroopClassParams(    //TroopClassParams toughClassParams = 
                name: "tough",
                troopNames: new string[] { "tough", "samurai", "dragon" },
                health: 100f,
                dmgAmount: 5f,
                movSpd: 0.5f,
                dmgRange: 0.8f,
                cost: 18,
                //aoE: true,
                dmgFrequency: 1f) }//,

        
        // vastaste deklaratsioonid
        /*{ "enemy", new TroopClassParams(   //TroopClassParams defaultClassParams =                                
                name:"default",
                troopNames:new string[] { "fighter", "archer", "tough"},
                health:30f,
                dmgAmount:2f,
                movSpd:1f,
                dmgRange:1.5f,
                cost:10,
                dmgFrequency: 1f) },

        { "archer", new TroopClassParams(   //TroopClassParams archerClassParams = 
                name: "archer",
                troopNames: new string[] { "archer", "ranger", "sniper" },
                health: 20f,
                dmgAmount: 3f,
                movSpd: 2f,
                dmgRange: 3f,
                cost: 15,
                dmgFrequency: 1f) },

        { "mage", new TroopClassParams(     //TroopClassParams mageClassParams = 
                name: "mage",
                troopNames: new string[] { "apprentice", "shapeshifter", "wizard" },
                health: 20f,
                dmgAmount: 2f,
                movSpd: 1f,
                dmgRange: 2.5f,
                cost: 12,
                dmgFrequency : 1f)},

        { "tough", new TroopClassParams(    //TroopClassParams toughClassParams = 
                name: "tough",
                troopNames: new string[] { "tough", "samurai", "dragon" },
                health: 100f,
                dmgAmount: 5f,
                movSpd: 0.5f,
                dmgRange: 0.8f,
                cost: 18,
                aoE: true,
                dmgFrequency: 1f) }*/

        /*{ "enemy", new TroopClassParams(   //TroopClassParams defaultClassParams =                                
                name:"default",
                troopNames:new string[] { "fighter", "archer", "tough"},
                health:30f,
                dmgAmount:1.3f,
                movSpd:1f,
                dmgRange:1f,
                cost:10,
                isFriendly:false,
                dmgFrequency : 1f) }*/
    };

    public TypeTroop MakeTypeTroop(string ClassName, string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, float damageFrequency, int cost, bool isFriendly = true, bool AoE = false)
    {
        // ;( exceptions );
        switch (TroopName)
        {
            // default class
            case "tough":
                AoE = true;
                break;

            // archer class
            case "sniper":
                damageRange = 10f;
                break;

            // mage class
            case "wizard":
                damageRange = 3f;
                AoE = true;
                break;
            
            // tough class
            case "samurai":
                damageRange = 3f;
                break;

            case "dragon":
                damageRange = 3f;
                damageAmount = 15f;
                AoE = true;
                break;
        }

        TypeTroop newTroop = new TypeTroop(ClassName, TroopName, health, movingSpeed, damageAmount, damageRange, damageFrequency, this.troopObj, isFriendly, AoE, cost);
        //newTroop.MakeAPool(5, newTroop);
        return newTroop;
    }


    public TypeTroop[] generateTroopClass(string troopClassName)
    {
        TypeTroop[] troopClasses = new TypeTroop[3];
        TypeTroop[] enemyTroopClasses = new TypeTroop[3*Difficulty];

        TroopClassParams troopClassParams = allTtroopParams[troopClassName];
        //TroopClassParams enemyTroopClassParams = allTtroopParams["enemy"];

        ConcreteConst[] troopConsts = TroopClassParams.troopConsts;

        for (int i = 0; i < 3; i++)
        {
            // make player troops
            troopClasses[i] = MakeTypeTroop(ClassName: troopClassName,
                TroopName: troopClassParams.TroopNames[i],
                health: troopClassParams.Health * troopConsts[i].healthConst,
                movingSpeed: troopClassParams.MovingSpeed * troopConsts[i].movSpdConst,
                damageAmount: troopClassParams.DamageAmount * troopConsts[i].dmgConst,
                damageRange: troopClassParams.DamageRange * troopConsts[i].dmgRangeConst,
                damageFrequency: troopClassParams.DmgFrequency * troopConsts[i].dmgFrequency,
                cost: (int)(troopClassParams.Cost * troopConsts[i].costConst),
                isFriendly: troopClassParams.IsFriendly,
                AoE: troopClassParams.AoE);
        }

        
        // Vastaste klasside loomine
        for (int i = 0 ; i < Difficulty ; i++)
        {
            TroopClassParams enemyTroopClassParams = allTtroopParams[allTtroopParams.Keys.ToArray().ElementAt(i)];
            for (int j = 0; j < 3; j++)
            {
                // make enemy troops
                enemyTroopClasses[j+(i*3)] = MakeTypeTroop(ClassName: allTtroopParams.Keys.ToArray().ElementAt(i),
                    TroopName: enemyTroopClassParams.TroopNames[j],
                    health: enemyTroopClassParams.Health * troopConsts[j].healthConst,
                    movingSpeed: enemyTroopClassParams.MovingSpeed * troopConsts[j].movSpdConst,
                    damageAmount: enemyTroopClassParams.DamageAmount * troopConsts[j].dmgConst,
                    damageRange: enemyTroopClassParams.DamageRange * troopConsts[j].dmgRangeConst,
                    damageFrequency: troopClassParams.DmgFrequency * troopConsts[j].dmgFrequency,
                    cost: (int)(enemyTroopClassParams.Cost * troopConsts[j].costConst),
                    isFriendly: false,
                    AoE: enemyTroopClassParams.AoE);
            }
        }

        // See tuleb valja kutsuda GameScene loadides
        //canvas.GetComponent<TroopButtons>().CreateButtons();
        // siin voib mangu kaima panna nt

        Difficulty = PlayerPrefs.GetInt("Difficulty", Difficulty);
        PlayerData.BestPlayerScore = PlayerPrefs.GetInt("BestPlayerScore", 0);

        PlayerData.Difficulty = Difficulty;
        PlayerData.selectedClassTypeTroops = troopClasses;
        PlayerData.selectedEnemyTypeTroops = enemyTroopClasses;

        return troopClasses;
    }

    // Funktsioonid nuppude jaoks
    public void generateDefaultTroopClass()
    {
        Difficulty = 1;
        generateTroopClass("default");
    }
    public void generateArcherTroopClass()
    {
        Difficulty = 2;
        generateTroopClass("archer");
    }
    public void generateMageTroopClass()
    {
        Difficulty = 3;
        generateTroopClass("mage");
    }
    public void generateToughTroopClass()
    {
        Difficulty = 4;
        generateTroopClass("tough");
    }

    public void SetGameOn()
    {
        AudioManager.Instance.Stop("Theme");
        //AudioManager.Instance.Play("GameTheme");
        SceneManager.LoadScene("GameScene");
        GameOn = true;
        PlayerData.gameOn = GameOn;
    }
}
