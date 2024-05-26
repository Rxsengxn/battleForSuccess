using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver
{

    public static GameManager Instance;

    public GameObject canvas;
    public GameObject endScreen;
    public TextMeshProUGUI endScreenText;

    public GameObject gameBG;
    public GameObject gameBGRoots;
    public GameObject gameBGGround;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI scoreText;

    private int troopsKilled = 0;
    private int score = 0;

    public bool GameOn = false;

    [field: SerializeField] public int Gold { private set; get; } = 0;

    private readonly bool DebugMode = true;


    /*private TypeTroop defaultClass;
    private TypeTroop archerClassTroop;
    private TypeTroop defaultEnemyClass;*/

    public GameObject troopObj;

    public GameObject homeBase;
    public GameObject enemyBase;

    public Vector3 InitialHealthbarPos = new Vector3(0, 0.74f, 0);
    /*private Dictionary<string, List<float>> defaultTroopStats =
        new Dictionary<string, List<float>>(){  {"default", new List<float>() { 100f, 1f, 1f, 1f } },
                                                {"archer", new List<float>() { 100f, 1f, 1f, 1f } },
                                                {"mage", new List<float>() { 100f, 1f, 1f, 1f } },
                                                {"tough", new List<float>() { 100f, 1f, 1f, 1f } }};*/


    /*private Dictionary<string, TroopClassParams> allTtroopParams = new Dictionary<string, TroopClassParams>()
    {
        { "default", new TroopClassParams(   //TroopClassParams defaultClassParams =                                
                name:"default",
                troopNames:new string[] { "fighter", "archer", "tough"},
                health:30f,
                dmgAmount:2f,
                movSpd:1f,
                dmgRange:1f,
                cost:10) },

        { "archer", new TroopClassParams(   //TroopClassParams archerClassParams = 
                name: "archer",
                troopNames: new string[] { "archer", "ranger", "sniper" },
                health: 20f,
                dmgAmount: 3f,
                movSpd: 2f,
                dmgRange: 3f,
                cost: 15) },

        { "mage", new TroopClassParams(     //TroopClassParams mageClassParams = 
                name: "mage",
                troopNames: new string[] { "apprentice", "shapeshifter", "wizard" },
                health: 20f,
                dmgAmount: 2f,
                movSpd: 1f,
                dmgRange: 2.5f,
                cost: 12)},

        { "tough", new TroopClassParams(    //TroopClassParams toughClassParams = 
                name: "tough",
                troopNames: new string[] { "tough", "samurai", "dragon" },
                health: 100f,
                dmgAmount: 5f,
                movSpd: 0.5f,
                dmgRange: 0.8f,
                cost: 18,
                aoE: true) }
    };*/



    // stats(andmestruktuur)
    //      string "classi nimi" :
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //      string "classi nimi" :
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //      string "classi nimi" :
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //      string "classi nimi" :
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"
    //                   string "troopi nimi" :
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   float "troopi omadused"
    //                                   bool "troopi omadused"
    //                                   bool "troopi omadused"




    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Debug.Log("Destroying extra GM");
            Destroy(this.gameObject);
        }

        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "GameScene")
            AudioManager.Instance.Play("GameMusic");

    }

    void Start()
    {

        goldText.text = "$ " + Gold.ToString();
        scoreText.text = "Score\n" + score.ToString();

        homeBase.GetComponent<Base>().Attach(this);
        enemyBase.GetComponent<Base>().Attach(this);

        InitializeTroops();

        this.GameOn = PlayerData.gameOn;

        gameBG.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/BGs/BG_" + PlayerData.selectedClassTypeTroops[0].ClassName);
        gameBGRoots.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/BGs/BG_" + PlayerData.selectedClassTypeTroops[0].ClassName + "_roots");
        gameBGGround.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/BGs/BG_" + PlayerData.selectedClassTypeTroops[0].ClassName + "_ground");

        /*// default classi troopide tegemine
        TypeTroop defaultFighter = MakeTypeTroop(ClassName: "default",
                                                TroopName: "fighter",
                                                health: 100f,
                                                movingSpeed: 1f,
                                                damageAmount: 3f,
                                                damageRange: 1f,
                                                gameObject: troop,
                                                cost: 10);

        TypeTroop defaultArcher = MakeTypeTroop(ClassName: "default",
                                                TroopName: "archer",
                                                health: 30f,
                                                movingSpeed: 3f,
                                                damageAmount: 7f,
                                                damageRange: 5f,
                                                gameObject: troop,
                                                cost: 15);

        TypeTroop defaultTough = MakeTypeTroop(ClassName: "default",
                                                TroopName: "tough",
                                                health: 500f,
                                                movingSpeed: 0.5f,
                                                damageAmount: 10f,
                                                damageRange: 0.5f,
                                                gameObject: troop,
                                                AoE: true,
                                                cost: 25);



        // archer classi troopide tegemine
        TypeTroop archerArcher = MakeTypeTroop(ClassName: "archer",
                                                TroopName: "archer",
                                                health: 50f,
                                                movingSpeed: 1f,
                                                damageAmount: 2f,
                                                damageRange: 1f,
                                                gameObject: troop,
                                                cost: 9);

        TypeTroop archerRanger = MakeTypeTroop(ClassName: "archer",
                                                TroopName: "ranger",
                                                health: 30f,
                                                movingSpeed: 3f,
                                                damageAmount: 7f,
                                                damageRange: 5f,
                                                gameObject: troop,
                                                cost: 13);

        TypeTroop archerSniper = MakeTypeTroop(ClassName: "archer",
                                                TroopName: "sniper",
                                                health: 80f,
                                                movingSpeed: 0.2f,
                                                damageAmount: 12f,
                                                damageRange: 8f,
                                                gameObject: troop,
                                                cost: 30);

        canvas.GetComponent<TroopButtons>().CreateButtons();

        TypeTroop enemyFighter = MakeTypeTroop(ClassName: "default",
                                                TroopName: "fighter",
                                                health: 80f,
                                                movingSpeed: 1f,
                                                damageAmount: 3f,
                                                damageRange: 1f,
                                                gameObject: troop,
                                                isFriendly: false,
                                                cost: 10);

        TypeTroop enemyArcher = MakeTypeTroop(ClassName: "default",
                                                TroopName: "archer",
                                                health: 80f,
                                                movingSpeed: 1f,
                                                damageAmount: 3f,
                                                damageRange: 3f,
                                                gameObject: troop,
                                                isFriendly: false,
                                                cost: 15);

        TypeTroop enemyTough = MakeTypeTroop(ClassName: "default",
                                                TroopName: "tough",
                                                health: 80f,
                                                movingSpeed: 1f,
                                                damageAmount: 3f,
                                                damageRange: 0.5f,
                                                gameObject: troop,
                                                isFriendly: false,
                                                cost: 25);*/

    }

    private void InitializeTroops()
    {
        for (int i = 0; i < PlayerData.selectedClassTypeTroops.Length; i++)
        {
            PlayerData.selectedClassTypeTroops[i].MakeAPool(5, PlayerData.selectedClassTypeTroops[i]);
        }
        canvas.GetComponent<TroopButtons>().CreateButtons();

        // Generate enemy troops
        //TypeTroop[] enemyTroops = generateTroopClass("default");

        //PlayerData.selectedEnemyTypeTroops = enemyTroops;

        for (int i = 0; i < PlayerData.selectedEnemyTypeTroops.Length; i++)
        {
            //enemyTroops[i].MakeAPool(5, enemyTroops[i]);
            PlayerData.selectedEnemyTypeTroops[i].MakeAPool(5, PlayerData.selectedEnemyTypeTroops[i]);
        }
    }

    

    /*public TypeTroop MakeTypeTroop(string ClassName, string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, int cost, bool isFriendly = true, bool AoE = false)
    {
        TypeTroop newTroop = new TypeTroop(ClassName, TroopName, health, movingSpeed, damageAmount, damageRange, this.troopObj, isFriendly, AoE, cost);
        //newTroop.MakeAPool(5, newTroop);
        return newTroop;
    }

    public TypeTroop[] generateTroopClass(string troopClassName)
    {
        TypeTroop[] troopClasses = new TypeTroop[3];

        TroopClassParams troopClassParams = allTtroopParams[troopClassName];

        ConcreteConst[] troopConsts = TroopClassParams.troopConsts;

        for (int i = 0; i < 3; i++)
        {
            troopClasses[i] = MakeTypeTroop(ClassName: troopClassName,
                TroopName: troopClassParams.TroopNames[i],
                health: troopClassParams.Health * troopConsts[i].healthConst,
                movingSpeed: troopClassParams.MovingSpeed * troopConsts[i].movSpdConst,
                damageAmount: troopClassParams.DamageAmount * troopConsts[i].dmgConst,
                damageRange: troopClassParams.DamageRange * troopConsts[i].dmgRangeConst,
                cost: (int)(troopClassParams.Cost * troopConsts[i].costConst),
                isFriendly: troopClassParams.IsFriendly,
                AoE:troopClassParams.AoE);
        }

        // See tuleb valja kutsuda GameScene loadides
        //canvas.GetComponent<TroopButtons>().CreateButtons();
        // siin voib mangu kaima panna nt

        PlayerData.selectedClassTypeTroops = troopClasses;

        return troopClasses;
    }*/

    private float lastTime = 0;
    private float currentTime = 0;
    [Range(0f, 10f)]
    [SerializeField] private float addGoldTime = 1;
    [Range(0, 100)]
    [SerializeField] private int addedGoldAmount = 1;

    private void Update()
    {
        if (GameOn)
        {

            if (gameOver)
            {
                AudioManager.Instance.Stop(AudioManager.Instance.GetCurPlayingSong());
                //AudioManager.Instance.Play("GameOver");
                endScreen.SetActive(true);
                if (win)
                {
                    // lahuta score'ist maha punktid, mis kaotasid kodubaasi elude arvelt
                    /*score -= (int) ((FindAnyObjectByType<HomeBase>().getInitialHealth() -
                                               FindAnyObjectByType<HomeBase>().getHealth())/10);*/
                    endScreen.GetComponent<Image>().color = new Color(0, 1, 0, 1f);
                    if (score < 0) score = 0;

                    //canvas.GetComponent<EndScreen>().WinScreen();
                    endScreenText.text = "You win!";
                    //Debug.LogError("You win!");
                    if (PlayerData.BestPlayerScore == 0)
                    {
                        endScreen.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Score: " +
                            (score).ToString() +
                            "\nYou killed " + troopsKilled.ToString() + " troops!";
                    } 
                    else {
                        if (score > PlayerData.BestPlayerScore)
                        {
                            endScreen.GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                                "New best score: " + (score).ToString() + "!" +
                                "\nYou killed " + troopsKilled.ToString() + " troops!";
                        }
                        else
                        {
                            endScreen.GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                                "Best score: " + PlayerData.BestPlayerScore.ToString() +
                                "\nScore: " + (score).ToString() +
                                "\nYou killed " + troopsKilled.ToString() + " troops!";
                        }
                    }
                }
                else
                {
                    endScreen.GetComponent<Image>().color = new Color(1, 0, 0, 1f);
                    endScreenText.text = "You Lose!";
                    endScreen.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Score: " +
                        (score) +
                        "\nYou killed " + troopsKilled.ToString() + " troops!";
                    //Debug.LogError("You lose!");
                }
                if (score > PlayerData.BestPlayerScore)
                {
                    PlayerData.BestPlayerScore = score;
                    PlayerPrefs.SetInt("BestPlayerScore", score);
                }
                Time.timeScale = 0;
                GameOn = false;
                GameManager.Instance.GameOn = GameOn;
            }

            if (DebugMode)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AddGold(100);
                }
            }
            currentTime = Time.timeSinceLevelLoad;
            if (currentTime - lastTime > addGoldTime)
            {
                lastTime = currentTime;
                AddGold(addedGoldAmount);
            }
        }
    }


    /*public TypeTroop MakeTypeTroop(string ClassName, string TroopName, float health, float movingSpeed, float damageAmount, float damageRange, int cost, bool isFriendly = true, bool AoE = false)
    {
        TypeTroop newTroop = new TypeTroop(ClassName, TroopName, health, movingSpeed, damageAmount, damageRange, this.troopObj, isFriendly, AoE, cost);
        //newTroop.MakeAPool(5, newTroop);
        return newTroop;
    }*/


    private bool gameOver = false;
    private bool win = false;
    public void OnDeath(Destructibles subject)
    {
        if (subject == null) return;

        if (subject is Base) { 
            gameOver = true;
            win = !subject.IsFriendly;
            if (win)
            {
                score -= (int)((homeBase.GetComponent<Base>().InitialHealth -
                    homeBase.GetComponent<Base>().Health) / 10);
                UpdateScore();
            }
            return;
        }
        /*if (subject is EnemyBase) { gameOver = true; win = true; return; }
        if (subject is HomeBase) { gameOver = true; win = false; return; }*/

        if (subject is Troop)
        {
            if (subject.IsFriendly) return;
            AddGold(Mathf.CeilToInt(subject.GetComponent<Troop>().TypeofTroop.Cost * 0.6f));
            troopsKilled++;
        }
        /*if (subject.getIsFriendly()) return;

        if (subject.GetComponent<Troop>())
        {
            AddGold(Mathf.CeilToInt(subject.GetComponent<Troop>().getTypeTroop().getCost() * 1f));

        }*/
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        score += amount;
        goldText.text = "$ " + Gold.ToString();
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score\n" + score.ToString();
    }

    public void RemoveGold(int amount)
    {
        Gold -= amount;
        goldText.text = "$ " + Gold.ToString();
    }

    /*public int GetGold()
    {
        return this.Gold;
    }*/

/*    public void setGameOn()
    {
        SceneManager.LoadScene("GameScene");
        GameOn = true;
    }*/

    // See voib tehniliselt void ka olla, sest selle peaks button valja kutsuma,,,, nvm
    /*public TypeTroop[] generateTroopClass(string troopClassName)
    {
        TypeTroop[] troopClasses = new TypeTroop[3];

        TroopClassParams troopClassParams = allTtroopParams[troopClassName];

        ConcreteConst[] troopConsts = TroopClassParams.troopConsts;

        for (int i = 0; i < 3; i++)
        {
            troopClasses[i] = MakeTypeTroop(ClassName: troopClassName,
                TroopName: troopClassParams.TroopNames[i],
                health: troopClassParams.Health * troopConsts[i].healthConst,
                movingSpeed: troopClassParams.MovingSpeed * troopConsts[i].movSpdConst,
                damageAmount: troopClassParams.DamageAmount * troopConsts[i].dmgConst,
                damageRange: troopClassParams.DamageRange * troopConsts[i].dmgRangeConst,
                cost: (int) (troopClassParams.Cost * troopConsts[i].costConst) );
        }

        // See tuleb valja kutsuda GameScene loadides
        //canvas.GetComponent<TroopButtons>().CreateButtons();
        // siin voib mangu kaima panna nt

        PlayerData.selectedClassTypeTroops = troopClasses;

        return troopClasses;
    }

    // Funktsioonid nuppude jaoks
    public void generateDefaultTroopClass()
    {
        generateTroopClass("default");
    }
    public void generateArcherTroopClass()
    {
        generateTroopClass("archer");
    }
    public void generateMageTroopClass()
    {
        generateTroopClass("mage");
    }
    public void generateToughTroopClass()
    {
        generateTroopClass("tough");
    }*/
    
    /*public List<TypeTroop> makeClassesOfTroops(string troopClassName)
    {
        List<TypeTroop> MakeClassOfTroops = new List<TypeTroop>();

        MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "fighter",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

        MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "archer",
                                            health: defaultTroopStats[troopClass][0] * 0.5f,
                                            movingSpeed: defaultTroopStats[troopClass][1],
                                            damageAmount: defaultTroopStats[troopClass][2],
                                            damageRange: defaultTroopStats[troopClass][3],
                                            gameObject: troop));

        MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "tough",
                                            health: defaultTroopStats[troopClass][0],
                                            movingSpeed: defaultTroopStats[troopClass][1],
                                            damageAmount: defaultTroopStats[troopClass][2],
                                            damageRange: defaultTroopStats[troopClass][3],
                                            gameObject: troop,
                                            AoE: true));

        return MakeClassOfTroops;
    }

    private List<TypeTroop> MakeClassOfTroops(TroopClassStats troopClass)
    {
        List<TypeTroop> MakeClassOfTroops = new List<TypeTroop>();



        foreach (TroopStats TroopStats in troopClass.Troops)
        {
            
        }




        switch (troopClass)
        {
            case "default":

                MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "fighter",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

                MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "archer",
                                                    health: defaultTroopStats[troopClass][0] * 0.5f,
                                                    movingSpeed: defaultTroopStats[troopClass][1],
                                                    damageAmount: defaultTroopStats[troopClass][2],
                                                    damageRange: defaultTroopStats[troopClass][3],
                                                    gameObject: troop));

                MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "tough",
                                                    health: defaultTroopStats[troopClass][0],
                                                    movingSpeed: defaultTroopStats[troopClass][1],
                                                    damageAmount: defaultTroopStats[troopClass][2],
                                                    damageRange: defaultTroopStats[troopClass][3],
                                                    gameObject: troop,
                                                    AoE: true));

                break;

            case "archer":
                
                

                break;
        }











        if (troopClass == "default")
        {


            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "fighter",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "archer",
                                                health: defaultTroopStats[troopClass][0]*0.5f,
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "tough",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop,
                                                AoE: true));
        }
        else if (troopClass == "archer")
        {
            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "archer",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "ranger",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));

            MakeClassOfTroops.Add(MakeTypeTroop(TroopName: "sniper",
                                                health: defaultTroopStats[troopClass][0],
                                                movingSpeed: defaultTroopStats[troopClass][1],
                                                damageAmount: defaultTroopStats[troopClass][2],
                                                damageRange: defaultTroopStats[troopClass][3],
                                                gameObject: troop));
        }

        return MakeClassOfTroops;
    }*/
}
