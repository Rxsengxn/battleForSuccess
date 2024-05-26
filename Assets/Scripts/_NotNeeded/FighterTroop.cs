using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FighterTroop : MonoBehaviour// : Troop
{

    /*public override float health { get; set; }
    public override float movingSpeed { get; set; }
    public override float damageAmount { get; set; }
    public override GameObject homeBase { get; set; }
    public override GameObject enemyBase { get; set; }

    public FighterTroop(float health, float movingSpeed, float damageAmount, GameObject homeBase, GameObject enemyBase)
    {
        this.health = health;
        this.movingSpeed = movingSpeed;
        this.damageAmount = damageAmount;
        //this.startTime = startTime;
        this.homeBase = homeBase;
        this.enemyBase = enemyBase;
    }

    /*public FighterTroop(float baseHealth, float baseMovementSpeed, object value)
    {
        this.baseHealth = baseHealth;
        this.baseMovementSpeed = baseMovementSpeed;
        this.value = value;
    }*//*

    void Start()
    {
        /*this.health = 100f; // Get this value from the type of troop
       //this.movingSpeed = 2f; // Get this value from the type of troop
        //this.damageAmount = 5; // Get this value from the type of troop
        // get my base info and transform.location and position this prefab to it
        this.transform.position = homeBase.transform.position + new Vector3(2f, -1f, 0);


    }
    void Update()
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
    }

    public override void TakeDamage(float amount)
    {
        this.health -= amount;
        if (health <= 0) { this.gameObject.SetActive(false); }
    }

    float startTime;
    private float baseHealth;
    private float baseMovementSpeed;
    private object value;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision == null) { return; }
        if (collision.gameObject.tag == "Ground") { return; }

        if ((collision.gameObject.GetComponent<Troop>() != null) || (collision.gameObject.GetComponent<EnemyBase>() != null))
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageAmount);
            startTime = Time.fixedTime;
        }
        movingSpeed = 0.5f;
    }

    public override void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Troop>() != null)
        {
            Debug.Log("Collision stay - should take dmg rn");
            if (Time.fixedTime > startTime + 1)
            {
                collision.gameObject.GetComponent<Troop>().TakeDamage(damageAmount);
                startTime = Time.fixedTime;
            }
        }
        else if (collision.gameObject.GetComponent<EnemyBase>() != null)
        {
            if (Time.fixedTime > startTime + 1)
            {
                collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageAmount);
                startTime = Time.fixedTime;
            }
        }
    }*/
}