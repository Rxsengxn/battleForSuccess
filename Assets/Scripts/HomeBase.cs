using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomeBase : Destructables
{
    //public float health;
    public GameObject troop;
    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    //public GameObject troop2;
    //public GameObject enemyBase;

    //private GameObject oldTroop = null;

    private TypeTroop defaultClass;
    private TypeTroop archerClassTroop;

    //public GameObject spawner;
    
    
    //float startTime;
    
    void Start()
    {

        //sprites = Resources.load<Sprite>("Sprites/Archer*");

        this.Health = 1000f;
        this.DamageAmount = 10f;
        this.IsFriendly = true;
        this.AoE = true;
        collidedObjects = new Dictionary<GameObject, bool>();


        // uue classi tegemine
        defaultClass = new TypeTroop(TroopName:"default",
                                        health:100,
                                        movingSpeed:1f,
                                        damageAmount:10,
                                        damageRange:1,
                                        gameObject:troop,
                                        isFriendly:true,
                                        AoE:true);

        defaultClass.MakeAPool(5, defaultClass);

        archerClassTroop = new TypeTroop(TroopName:"archer",
                                        health:50,
                                        movingSpeed:2,
                                        damageAmount:20,
                                        damageRange:5,
                                        gameObject:troop,
                                        isFriendly: true,
                                        AoE:false);

        
        archerClassTroop.MakeAPool(5, archerClassTroop);

        healthBar.SetMaxHealth(this.Health);
        healthBar.SetHealth(this.Health);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (Health <= 0) { this.gameObject.SetActive(false); }

        // uue troopi tegemine "default" classi põhjal
        //Troop defaultFighter = defaultClass.CreateTroop(defaultClass);

    }

    

    // Update is called once per frame
    void Update()
    {
        /*if(Time.fixedTime > startTime+2f) {

            GameObject fighterTroopobject = spawner.GetComponent<Spawner>().SpawnTroop(defaultClass);

            /*PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(defaultClass);
            GameObject fighterTroopobject = fighterTroopPO.go;*/
            // this needs fixing-- cant do instanciate with giving constructor values.
            // Idea: make a fuction which sets the values of the specific troop "manually"
        //troopInstance   (100f, 2f, 5f, Time.fixedTime, this, enemyBase);
        /*if (Pools.Instance.pools[defaultClass].index > 4)
            {
                Debug.Log("Pool is full, killing the oldest object");
                Debug.Log(Pools.Instance.pools[defaultClass]);
                Debug.Log(Pools.Instance.pools[defaultClass].pool[0]);
                Pools.Instance.KillPooledObject(Pools.Instance.pools[defaultClass].pool[0]);
            }
            
            oldTroop = fighterTroopobject;//
            
            startTime = Time.fixedTime;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.TriggervTrigger(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        base.TriggerStay(other, 0.5f);
    }

    private void OnDisable()
    {
        base.NotifyHitlistOnDisable(this.gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.GetComponent<Troop>() != null)
        {
            if (collisionObj.GetComponent<Troop>().getDamageAmount() > 0)
            {
                TakeDamage(collisionObj.GetComponent<Troop>().getDamageAmount());
                startTimeDmg = Time.fixedTime;
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.GetComponent<Troop>() != null)
        {
            Debug.Log("Collision stay - should take dmg rn");
            if (Time.fixedTime > startTimeDmg + 1)
            {
                TakeDamage(collisionObj.GetComponent<Troop>().getDamageAmount());
                startTimeDmg = Time.fixedTime;
            }
        }
    }*/
}
