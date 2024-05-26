using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomeBase : Destructibles
{
    void Start()
    {
        this.Health = 1000f;
        InitialHealth = this.Health;
        this.DamageAmount = 10f;
        this.IsFriendly = true;
        this.AoE = true;
        collidedObjects = new Dictionary<GameObject, bool>();


        healthBar.SetMaxHealth(this.Health);
        healthBar.SetHealth(this.Health);

        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (Health <= 0) 
        { 
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Destroyed");
        yield return new WaitForSeconds(0.8f);
        NotifyObservers();
        this.gameObject.SetActive(false); 
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

    /*public int getInitialHealth()
    {
        return (int)initialHealth;
    }*/

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
