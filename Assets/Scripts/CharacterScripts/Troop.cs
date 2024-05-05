using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Troop : Destructables
{
    private string TroopName;
    // /[SerializeField] private float Health;
    // /[SerializeField] private float DamageAmount;
    [SerializeField] private float MovingSpeed = 0f;
    private float DamageRange;
    //private GameObject GameObject;
    private TypeTroop typeTroop;
    private PooledObject po;
    //public HealthBarUI healthBar;
    // /[SerializeField] private bool IsFriendly;
    // /[SerializeField] private bool AoE;

    //TroopType& getTypeTroop() { return troopType; }
    // Lisa siia teised väljad vastavalt vajadusele

    /*public Troop(TypeTroop typeTroop)
    {
        this.TroopName = typeTroop.getName();
        this.Health = typeTroop.getHealth();
        this.MovingSpeed = typeTroop.getMovingSpeed();
        this.DamageAmount = typeTroop.getDamageAmount();
        this.DamageRange = typeTroop.getDamageRange();
        this.typeTroop = typeTroop;

        //this.HomeBase = homeBase;
        //this.EnemyBase = enemyBase;
    }*/

    private void Awake()
    {
        //collidedObjects = new Dictionary<GameObject, bool>();
    }

    private void Start()
    {
        /*this.Health = this.typeTroop.getHealth();
        this.MovingSpeed = this.typeTroop.getMovingSpeed();
        this.DamageAmount = this.typeTroop.getDamageAmount();
        this.DamageRange = this.typeTroop.getDamageRange();*/
        // get my base info and transform.location and position this prefab to it
        //transform.position = homeBase.transform.position + new Vector3(2f, -1f, 0);

        /*GameObject[] player = GameObject.FindGameObjectsWithTag("Troop");
        if (player.Length != 0)
        {
            foreach (GameObject troop in player)
            {
                Physics.IgnoreCollision();
            }
        }
        Physics.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());*/
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ally"), LayerMask.NameToLayer("Ally"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        
}





    private void Update()
    {
        // Simplify this

        if (IsFriendly) 
        {
        transform.position += new Vector3(MovingSpeed, 0f, 0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-MovingSpeed, 0f, 0f) * Time.deltaTime;
        }
    }

    public void SetTroopType(TypeTroop typeTroop)
    {
        this.typeTroop = typeTroop;
        this.Health = this.typeTroop.getHealth();
        healthBar.SetMaxHealth(this.Health);
        healthBar.SetHealth(this.Health);
        //Debug.Log("Troop health set to " + this.Health.ToString());
        this.MovingSpeed = this.typeTroop.getMovingSpeed();
        this.DamageAmount = this.typeTroop.getDamageAmount();
        this.DamageRange = this.typeTroop.getDamageRange();
        this.TroopName = this.typeTroop.getName();
        this.IsFriendly = this.typeTroop.getIsFriendly();
        this.AoE = this.typeTroop.getAoE();
        
        gameObject.layer = IsFriendly ? LayerMask.NameToLayer("Ally") : LayerMask.NameToLayer("Enemy");

        BoxCollider modifiedCollider = gameObject.GetComponent<BoxCollider>();
        modifiedCollider.isTrigger = true;
        modifiedCollider.center = new Vector3(((DamageRange * 0.5f) / 2 + 0.5f) * (IsFriendly ? 1f : -1f), 0.1f, 0);
        modifiedCollider.size = new Vector3((DamageRange * 0.5f), 1.8f, 1);
    }

    public void SetPooledObject(PooledObject po)
    {
        this.po = po;
        this.TroopName += " " + po.index.ToString();
    }

    //getters for all the fields
    public string getName() { return this.TroopName; }
    //public float getHealth() { return this.Health; }
    public float getDamageAmount() { return this.DamageAmount; }
    public float getMovingSpeed() { return this.MovingSpeed; }
    public float getDamageRange() { return this.DamageRange; }
    //public GameObject getGameObject() { return this.GameObject; }
    public TypeTroop getTypeTroop() { return this.typeTroop; }
    public PooledObject getPooledObject() { return this.po; }
    //public bool getIsFriendly() { return this.IsFriendly; }


    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        //healthBar.SetHealth(this.Health);

        //this.Health -= amount;
        if (Health <= 0) {
            //Debug.Log("Troop died");
            //Debug.Log("Name: " + TroopName + ", TroopType: " + this.typeTroop.getName().ToString());

            // Debug.log all fields of this.po
            /*Debug.Log("PooledObject go name: " + this.po.go.name);
            Debug.Log("PooledObject index: " + this.po.index);
            Debug.Log("PooledObject troop: " + this.po.go.GetComponent<Troop>().getName());
            Debug.Log("PooledObject typeTroop: " + this.po.go.GetComponent<Troop>().getTypeTroop().getName());*/

            base.NotifyHitlistOnDisable(this.gameObject);

            //Debug.Log(this.po.);
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.typeTroop.KillTroop(this); 
        }
    }

    public override void RestoreMovementSpeed()
    {
        MovingSpeed = this.typeTroop.getMovingSpeed();
    }



    // /
    /*public void RemoveMeFromDamageTakers(GameObject removable)
    {
        if (collidedObjects.Contains(removable))
        {
            Debug.Log("Removed " + removable.name + " from collidedObjects");
            Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            collidedObjects[collidedObjects.IndexOf(removable)] = null;
        }
    }*/

    // /
    /*private void OnCollisionStay(Collision collision)
    {
        Debug.Log(getName() + " collided with " + collision.gameObject.name + " With a tag of: " + collision.gameObject.tag);
        if (!(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Troop")))
        {
            *//*if (MovingSpeed > 0f) MovingSpeed -= 0.01f;
            if (MovingSpeed < 0f) MovingSpeed = 0f;*//*
            MovingSpeed = 0f;
        }   
    }*/
    //GameObject collidedTroopPerm;
    // /public List<GameObject> collidedObjects = new List<GameObject>();
    // /float startTimeDoDmg;
    private void OnTriggerEnter(Collider other)
    {
        // /
        /*GameObject collidedObj = other.gameObject;

        // Debug.Log(getName() + " started colliding with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);


        // Kui puutub kokku troopiga
        if (collidedObj.GetComponent<Troop>() != null)
        {
            Troop collidedTroop = collidedObj.GetComponent<Troop>();

            // Kui troop on vaenlane
            if (collidedTroop.getIsFriendly() != this.IsFriendly)
            {
                // Kui minu trigger puutus kokku vaenlase troopi triggeriga
                if (other.isTrigger)
                {
                    // Lisame troopide listi, et surres saaksime teavitada,
                    // et võite edasi liikuda
                    //Debug.Log("SIIA PEAKS AINULT 1 KORRA JOUDMA!!!!!!!!!!!!!!!!!!!!!!!");
                    //Debug.Log(getName() + " Collision with " + other.name + " : " + other.GetType().ToString() + " : " + other.isTrigger.ToString() + " : " + other.tag.ToString() + " : " + other.gameObject.name + " : " + other.gameObject.tag.ToString() + " : " + other.gameObject.GetComponent<Troop>().getName());
                    if (!collidedObjects.Contains(collidedObj))
                        collidedObjects.Add(collidedObj);
                    return;
                }
                // Kui meie trigger puutus kokku vaenlase hitboxiga,
                // teeme dmg
                collidedTroop.TakeDamage(this.DamageAmount);
                if (collidedObjects.Count == 1)
                    startTimeDoDmg = Time.fixedTime;

                //collidedTroopPerm = collidedObj;
            }
        }

        // Kui puutub kokku vaenlase baasiga
        if (collidedObj.GetComponent<EnemyBase>() != null)
        {
            if (other.isTrigger == true)
            { 
                if (!collidedObjects.Contains(collidedObj))
                    collidedObjects.Add(collidedObj);
                return;
            }
            
            collidedObj.GetComponent<EnemyBase>().TakeDamage(this.DamageAmount);
            if (collidedObjects.Count == 1)
                startTimeDoDmg = Time.fixedTime;
        }*/

        base.TriggervTrigger(other);

        //other.GetComponent<Destructables>().TakeDamage(this.DamageAmount);
    }


    private void OnTriggerStay(Collider other)
    {
        // /
        /*// Kui meie trigger on teise objekti triggeris, ei tee midagi
        if (other.isTrigger) return;

        //Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
        
        GameObject collidedObj = other.gameObject;



        // Test univeraalsemaks tegemiseks
        if (collidedObj.GetComponent<Destructables>() != null)
        {
            Destructables collidedObjectScript = collidedObj.GetComponent<Destructables>();
            Debug.Log(collidedObjectScript.ToString() + " !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!********************************");


            
               

                // Kui troop on vaenlane
                if (collidedObjectScript.getIsFriendly() != this.IsFriendly)
                {
                    // Tee dmg iga 1 sekundi tagant
                    if (Time.fixedTime > startTimeDoDmg + 1f)
                    {
                        if (AoE)
                        {
                            for (int i = 0; i < collidedObjects.Count; i++)// GameObject collidedTroopN in collidedObjects)
                            {
                                GameObject collidedTroopN = collidedObjects[i];
                                if (collidedTroopN.GetComponent<Troop>() != null)
                                {
                                    collidedTroopN.GetComponent<Troop>().TakeDamage(this.DamageAmount);
                                    Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                                }
                                else if (collidedTroopN.GetComponent<EnemyBase>() != null)
                                {
                                    collidedTroopN.GetComponent<EnemyBase>().TakeDamage(this.DamageAmount);
                                    //Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                                }
                            }
                            collidedObjects.RemoveAll(item => item == null);
                        }
                        else
                        {
                            collidedTroop.TakeDamage(this.DamageAmount);
                            Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                        }
                        startTimeDoDmg = Time.fixedTime;
                    }
                    // Aeglusta mind
                    if (MovingSpeed > 0f) MovingSpeed -= 0.2f;
                    if (MovingSpeed < 0f) MovingSpeed = 0f;
                }
            
        }





        // Kui meie trigger on teise troopi hitboxis
        if (collidedObj.GetComponent<Troop>() != null)
        {
            Troop collidedTroop = collidedObj.GetComponent<Troop>();

            // Kui troop on vaenlane
            if (collidedTroop.getIsFriendly() != this.IsFriendly)
            {
                // Tee dmg iga 1 sekundi tagant
                if (Time.fixedTime > startTimeDoDmg + 1f)
                {
                    if (AoE)
                    {
                        for (int i = 0; i < collidedObjects.Count; i++)// GameObject collidedTroopN in collidedObjects)
                        {
                            GameObject collidedTroopN = collidedObjects[i];
                            if (collidedTroopN.GetComponent<Troop>() != null)
                            {
                                collidedTroopN.GetComponent<Troop>().TakeDamage(this.DamageAmount);
                                Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                            }
                            else if (collidedTroopN.GetComponent<EnemyBase>() != null)
                            {
                                collidedTroopN.GetComponent<EnemyBase>().TakeDamage(this.DamageAmount);
                                //Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                            }
                        }
                        collidedObjects.RemoveAll(item => item == null);
                    }
                    else
                    {
                        collidedTroop.TakeDamage(this.DamageAmount);
                        Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                    }
                    startTimeDoDmg = Time.fixedTime;
                }
                // Aeglusta mind
                if (MovingSpeed > 0f) MovingSpeed -= 0.2f;
                if (MovingSpeed < 0f) MovingSpeed = 0f;
            }
        }

        // Kui meie trigger on vaenlase baasi hitboxis
        if (collidedObj.GetComponent<EnemyBase>() != null)
        {
            // Tee dmg iga 1 sekundi tagant
            if (Time.fixedTime > startTimeDoDmg + 1f)
            {
                collidedObj.GetComponent<EnemyBase>().TakeDamage(this.DamageAmount);
                startTimeDoDmg = Time.fixedTime;
            }
            // Aeglusta mind
            if (MovingSpeed > 0f) MovingSpeed -= 0.2f;
            if (MovingSpeed < 0f) MovingSpeed = 0f;
        }*/
    
        bool returned = base.TriggerStay(collision:other, DmgFrequency:1.5f);

        if (!returned) return;

        if (other.GetComponent<Destructables>() != null &&
            other.GetComponent<Destructables>().getIsFriendly() != IsFriendly &&
            !other.isTrigger &&
            other.GetComponent<Destructables>().getHealth() > 0)
        {
            if (MovingSpeed > 0f) MovingSpeed -= 0.2f;
            if (MovingSpeed < 0f) MovingSpeed = 0f;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(getName() + " collided with " + collision.gameObject.name + " With a tag of: " + collision.gameObject.tag);


        if (collision.gameObject.GetComponent<EnemyBase>() != null)
        {
            /*if (MovingSpeed < this.typeTroop.getMovingSpeed()) MovingSpeed += 0.01f;
             *            if (MovingSpeed > this.typeTroop.getMovingSpeed()) MovingSpeed = this.typeTroop.getMovingSpeed();*/
            MovingSpeed = 0;
            
        }

        /*if (collision.gameObject.GetComponent<Troop>() != null)
        {
            if (!collision.collider.isTrigger)
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }*/
    }

    

    private void OnDisable()
    {
        //Debug.Log("Troop " + gameObject.name + " diabled");
        /*foreach ( GameObject destructables in collidedObjects)
        {
            if (destructables != null)
                Debug.Log(destructables.name);
        }*/
        //Debug.Log(coll)
        //Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        base.NotifyHitlistOnDisable(this.gameObject);
        // /
        /*Debug.Log("Troop disabled");
        foreach (GameObject collidedObject in collidedObjects)
        {
            if (collidedObject.GetComponent<Troop>() != null)
            {
                collidedObject.GetComponent<Troop>().RestoreMovementSpeed();
                collidedObject.GetComponent<Troop>().RemoveMeFromDamageTakers(gameObject);
            }
            else if (collidedObject.GetComponent<EnemyBase>() != null)
            {
                collidedObject.GetComponent<EnemyBase>().RemoveMeFromDamageTakers(gameObject);
            }
        }
        collidedObjects.Clear();*/
    }

    private void OnEnable()
    {
        //Debug.Log("Troop enabled");
        //collidedObjects.Clear();
    }

    /*public float DoDamage() 
    { 
        return this.DamageAmount;
    }*/



    /*
   friend class TroopType;

  public const char* getAttack() { return troopType.getAttack(); }

  private Troop(TroopType& troopType) : health_(troopType.getHealth()),troopType(troopType){ }

    int health_; // Current health.
    Breed& breed_;

    // Lisa siia meetodid vastavalt vajadusele
    


    /*public abstract float health { get; set;}
    public abstract float movingSpeed { get; set; }

    public abstract GameObject homeBase { get; set; }
    public abstract GameObject enemyBase { get; set; }

    public abstract float damageAmount { get; set; }
    // Start is called before the first frame update
    *//*void Start()
    {
        this.health = 100f; // Get this value from the type of troop
        movingSpeed = 2f; // Get this value from the type of troop
        damageAmount = 5; // Get this value from the type of troop
        // get my base info and transform.location and position this prefab to it
        transform.position = homeBase.transform.position + new Vector3(2f, -1f, 0);

    }*//*

    // Update is called once per frame
    *//*void Update()
    {
        if (health < 0) { health = 0; }
        if (homeBase.GetComponent<HomeBase>() != null)
        {
            transform.position += new Vector3(movingSpeed, 0f, 0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-movingSpeed, 0f, 0f) * Time.deltaTime;

        }
    }*//*

    public abstract void TakeDamage(float amount);
    *//*{
        this.health -= amount;
        if (health <= 0) { this.gameObject.SetActive(false); }
    }

    float startTime;*//*

    public abstract void OnCollisionEnter(Collision collision);
    *//*{
        if (collision == null) { return; }
        if (collision.gameObject.tag == "Ground") { return; }

        if ((collision.gameObject.GetComponent<Troop>() != null) || (collision.gameObject.GetComponent<EnemyBase>() != null)) {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageAmount);
            startTime = Time.fixedTime;
        }
            movingSpeed = 0.5f;
    }*//*

    public abstract void OnCollisionStay(Collision collision);
    *//*{
        if ((collision.gameObject.GetComponent<Troop>() != null) || (collision.gameObject.GetComponent<EnemyBase>() != null))
        {
            Debug.Log("Collision stay - should take dmg rn");
            if (Time.fixedTime > startTime + 1)
            {
                collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageAmount);
                startTime = Time.fixedTime;
            }
        }
    }*/
}
