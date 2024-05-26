using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Troop : Destructibles
{
    [field: SerializeField] public string TroopName { private set; get; }
    [field: SerializeField] public float MovingSpeed { private set; get; }
    [field: SerializeField] public float DamageRange { private set; get; }
    [field: SerializeField] public TypeTroop TypeofTroop { private set; get; }
    public PooledObject Po { private set; get; }
    //private Animator animator;
    //public RuntimeAnimatorController animatorController;
    [field: SerializeField] public bool IsDead { private set; get; }


    private void Awake()
    {
        //animator = this.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ally"), LayerMask.NameToLayer("Ally"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
    }


    private void Update()
    {
        if (IsFriendly)
        {
            transform.position += new Vector3(MovingSpeed, 0f, 0f) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-MovingSpeed, 0f, 0f) * Time.deltaTime;
        }
        if (MovingSpeed == 0f)
        {
            animator.SetBool("isHitting", true);
        }
        else
        {
            animator.SetBool("isHitting", false);
        }
    }

    public void SetTroopType(TypeTroop typeTroop)
    {
        healthBar = GetComponentInChildren<HealthBarUI>();
        animator = GetComponent<Animator>();
        this.TypeofTroop = typeTroop;
        this.Health = this.TypeofTroop.Health;
        InitialHealth = this.Health;
        healthBar.SetMaxHealth(this.InitialHealth);
        healthBar.SetHealth(this.Health);
        this.MovingSpeed = this.TypeofTroop.MovingSpeed;
        this.DamageAmount = this.TypeofTroop.DamageAmount;
        this.DamageRange = this.TypeofTroop.DamageRange;
        this.DamageFrequency = this.TypeofTroop.DamageFrequency;
        this.TroopName = this.TypeofTroop.TroopName;
        this.IsFriendly = this.TypeofTroop.IsFriendly;
        this.AoE = this.TypeofTroop.AoE;

        gameObject.layer = IsFriendly ? LayerMask.NameToLayer("Ally") : LayerMask.NameToLayer("Enemy");

        BoxCollider2D modifiedCollider = gameObject.GetComponent<BoxCollider2D>();
        modifiedCollider.isTrigger = true;
        modifiedCollider.offset = new Vector2(((DamageRange * 0.5f) / 2 + 0.5f) * (IsFriendly ? 1f : -1f), 0.1f);
        modifiedCollider.size = new Vector2((DamageRange * 0.5f), 1.8f);

        string path = "Animation/" + this.TypeofTroop.ClassName + "/" + this.TroopName + "/" + (this.IsFriendly ? "" : "enemy ") + this.TroopName + " Animator";
        this.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);
        //Debug.LogError(path);
        /*if (this.animator.runtimeAnimatorController != null)
            Debug.LogWarning("Animator controller already set");
        if (Resources.Load<RuntimeAnimatorController>(path) == null)
            Debug.LogWarning("Animator controller not found");*/
        

        //if (this.IsFriendly) GetComponent<SpriteRenderer>().flipX = false;
        //else GetComponent<SpriteRenderer>().flipX = true;

        if (TroopName == "dragon")
        {
            healthBar.gameObject.transform.parent.transform.localPosition = new Vector3(0, 1f, 0) + GameManager.Instance.InitialHealthbarPos;
        }
        else healthBar.gameObject.transform.parent.transform.localPosition = GameManager.Instance.InitialHealthbarPos;

        IsDead = false;
    }

    public void SetPooledObject(PooledObject po)
    {
        this.Po = po;
        this.TroopName += " " + po.index.ToString();
    }

    //getters for all the fields
    /*public string getName() { return this.TroopName; }
    public float getDamageAmount() { return this.DamageAmount; }
    public float getMovingSpeed() { return this.MovingSpeed; }
    public float getDamageRange() { return this.DamageRange; }
    public TypeTroop getTypeTroop() { return this.TypeofTroop; }
    public PooledObject getPooledObject() { return this.Po; }*/


    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (Health <= 0 && !IsDead)
        {
            //Debug.Log("Troop died");
            //Debug.Log("Name: " + TroopName + ", TroopType: " + this.TypeofTroop.getName().ToString());

            // Debug.log all fields of this.po
            /*Debug.Log("PooledObject go name: " + this.po.go.name);
            Debug.Log("PooledObject index: " + this.po.index);
            Debug.Log("PooledObject troop: " + this.po.go.GetComponent<Troop>().getName());
            Debug.Log("PooledObject TypeofTroop: " + this.po.go.GetComponent<Troop>().getTypeofTroop().getName());*/
            NotifyObservers();
            base.NotifyHitlistOnDisable(this.gameObject);

            //Debug.Log(this.po.);
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.TypeofTroop.KillTroop(this);
            IsDead = true;
        }
    }

    public override void RestoreMovementSpeed()
    {
        if (collidedObjects.Count != 0)
        {
            if (collidedObjects.Values.Contains(true)) return;
        }
            
        MovingSpeed = this.TypeofTroop.MovingSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
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


    private void OnTriggerStay2D(Collider2D other)
    {
        bool returned = base.TriggerStay(collision: other, DmgFrequency: this.DamageFrequency);

        if (!returned) return;

        if (other.GetComponent<Destructibles>() != null &&
            other.GetComponent<Destructibles>().IsFriendly != IsFriendly &&
            !other.isTrigger &&
            other.GetComponent<Destructibles>().Health > 0)
        {
            if (MovingSpeed > 0f) MovingSpeed -= 0.2f;
            if (MovingSpeed < 0f) MovingSpeed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(getName() + " collided with " + collision.gameObject.name + " With a tag of: " + collision.gameObject.tag);
        if (collision.gameObject.GetComponent<EnemyBase>() != null)
        {
            MovingSpeed = 0;
        }
    }

    private void OnDisable()
    {
        //Debug.Log("Troop " + gameObject.name + " diabled");
        /*foreach ( GameObject destructables in collidedObjects)
        {
            if (destructables != null)
                Debug.Log(destructables.name);
        }*/
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
}