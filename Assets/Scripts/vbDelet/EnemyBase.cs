using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Destructibles
{
    // /public float Health;
    // /public float DamageAmount;
    //public GameObject troop;
    // /public bool IsFriendly = false;
    // /public bool AoE = true;

    //public Animator animator;
    

    //public GameObject spawner;
    void Start()
    {
        this.Health = 1000f;
        this.DamageAmount = 10f;
        this.IsFriendly = false;
        collidedObjects = new Dictionary<GameObject, bool>();

        
        healthBar.SetMaxHealth(this.Health);
        healthBar.SetHealth(this.Health);

        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        // /this.Health -= amount;
        if (Health <= 0) {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Destroyed");
        yield return new WaitForSeconds(0.8f);
        NotifyObservers();
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    // /float startTimeDoDmg;
    /*void SpawnEnemies()
    {
        Instantiate(troop);

    }*/


    // Update is called once per frame
    void Update()
    {
        
    }
    // /
    /*public void RemoveMeFromDamageTakers(GameObject removable)
    {
        if (collidedObjects.Contains(removable))
        {
            *//*Debug.Log("Removed " + removable.name + " from collidedObjects");
            Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");*//*
            collidedObjects[collidedObjects.IndexOf(removable)] = null;
        }
    }*/

    

    // /public List<GameObject> collidedObjects = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // /
        /*GameObject collisionObj = collision.gameObject;


        if (collisionObj.GetComponent<Troop>() != null)
        {
            if (collisionObj.GetComponent<Troop>().getIsFriendly() != IsFriendly)
            {
                if (collision.isTrigger)
                {
                    // Kui vastase baasi triggerisse siseneb troop, kes pole sõbralik,
                    // siis see troop lisatakse listi, keda teavitada, et uuesti liikuma hakata,
                    // kui baas saab "surma"
                    if (!collidedObjects.Contains(collisionObj))
                    {
                        collidedObjects.Add(collisionObj);
                    }
                    //collidedObjects.Add(collisionObj);
                    return;
                }
                //collisionObj.GetComponent<Troop>().TakeDamage(damageAmount);
                if (collidedObjects.Count == 1)
                    startTimeDoDmg = Time.fixedTime;
            }
        }*/
        
        base.TriggervTrigger(collision);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        // /
        /*if (collision.isTrigger) return;

        GameObject collisionObj = collision.gameObject;
        
        if (collisionObj.GetComponent<Troop>() != null && collisionObj.GetComponent<Troop>().getIsFriendly() != IsFriendly)
        {
            Troop collidedTroop = collisionObj.GetComponent<Troop>();

            if (Time.fixedTime > startTimeDoDmg + 0.5f)
            {
                if (AoE)
                {
                    for (int i = 0; i < collidedObjects.Count; i++)// GameObject collidedTroopN in collidedObjects)
                    {
                        GameObject collidedTroopN = collidedObjects[i];
                        if (collidedTroopN.GetComponent<Troop>() != null)
                        {
                            collidedTroopN.GetComponent<Troop>().TakeDamage(this.DamageAmount);
                            //Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                        }
                    }
                    collidedObjects.RemoveAll(item => item == null);
                }
                else
                {
                    collidedTroop.TakeDamage(this.DamageAmount);
                    //Debug.Log(getName() + " is in collision with " + other.gameObject.name + " With a tag of: " + other.gameObject.tag);
                }
                startTimeDoDmg = Time.fixedTime;


                *//*Debug.Log("Trigger stay - troop should take dmg rn");
                collisionObj.GetComponent<Troop>().TakeDamage(damageAmount);
                startTimeGetDmg = Time.fixedTime;*//*
            }
        }*/
    
        base.TriggerStay(collision, 0.5f);
    
    }

    private void OnDisable()
    {
        base.NotifyHitlistOnDisable(this.gameObject);
        // /
        /*Debug.Log("Base disabled");
        foreach (GameObject collidedTroop in collidedObjects)
        {
            if (collidedTroop.GetComponent<Troop>() != null)
            {
                collidedTroop.GetComponent<Troop>().RestoreMovementSpeed();
                collidedTroop.GetComponent<Troop>().RemoveMeFromDamageTakers(this.gameObject);
            }
        }
        collidedObjects.Clear();*/
    }
}
