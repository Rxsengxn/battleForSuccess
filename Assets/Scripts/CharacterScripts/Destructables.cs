using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Destructables : MonoBehaviour
{
    [SerializeField] protected float Health;
    [SerializeField] protected float DamageAmount;
    [SerializeField] protected bool IsFriendly;
    [SerializeField] protected bool AoE;

    public HealthBarUI healthBar;

    private void Awake()
    {
        //collidedObjects = new Dictionary<GameObject, bool>();
    }

    public virtual bool getIsFriendly() { return this.IsFriendly; }

    public virtual float getHealth() { return this.Health; }

    public virtual void TakeDamage(float amount)
    {
        this.Health -= amount;
        healthBar.SetHealth(this.Health);

        // Continues in child classes
    }

    public virtual void RemoveMeFromDamageTakers(GameObject removable)
    {
        /*Debug.Log("Removing " + removable.name + " from " + gameObject.name 
            + " collidedObjects, because " + removable.name + " Died.");*/
        if (collidedObjects.Keys.Contains(removable))
        {
            /*Debug.Log("Removed " + removable.name + " from collidedObjects");
            Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");*/
            collidedObjects.Remove(removable);
            collidedObjectsList.Remove(removable.name + " trigger");
            collidedObjectsList.Remove(removable.name + " collider");
        }
        else throw new Exception("Object not found in collidedObjects");
    }

    public virtual void RestoreMovementSpeed()
    {
        //Debug.Log("Restoring movement speed");
    }

    //public List<GameObject> collidedObjects;

    [SerializeField] public Dictionary<GameObject, bool> collidedObjects = new Dictionary<GameObject, bool>();
    [SerializeField] public List<string> collidedObjectsList = new List<string>();

    public virtual void NotifyHitlistOnDisable(GameObject self)
    {
        //RemoveMeFromDamageTakers(gameObject);
        /*foreach (GameObject destructables in collidedObjects)
        {
            Debug.Log(destructables.name);
        }*/
        //Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        for (int i = 0; i < collidedObjects.Keys.Count; i++)//GameObject collidedObject in collidedObjects.Keys)
        {
            GameObject collidedObject = collidedObjects.Keys.ElementAt(i);
            if (collidedObject != null)
            {
                collidedObject.GetComponent<Destructables>().RemoveMeFromDamageTakers(self);
                collidedObject.GetComponent<Destructables>().RestoreMovementSpeed();
            }
        }
        //Debug.LogWarning("Clear list");
        collidedObjects.Clear();
        collidedObjectsList.Clear();
        //Debug.LogWarning("list cleared");
        //Debug.Log("List has " + collidedObjects.Count + " elements");
    }


    protected float startTimeDoDmg;
    protected bool startTimeInit;
    // OnTriggerEnter
    protected virtual void TriggervTrigger(Collider2D collision)
    {

        if (Health <= 0) return;

        GameObject collisionObj = collision.gameObject;

        // Kui objekt ei ole Destructable tyypi, siis ei tee midagi
        if (collisionObj.GetComponent<Destructables>() == null) return;

        Destructables destructable = collisionObj.GetComponent<Destructables>();

        // Kui objekt on sõbralik, siis ei tee midagi
        if (destructable.getIsFriendly() == this.IsFriendly) return;
        
        if (collision.isTrigger)
        {
            // Kui vastase baasi triggerisse siseneb troop, kes pole sõbralik,
            // siis see troop lisatakse listi, keda teavitada, et uuesti liikuma hakata,
            // kui baas saab "surma"
            
            if (!collidedObjects.Keys.Contains(collisionObj))
            {
                collidedObjects[collisionObj] = false;//(collisionObj);
                collidedObjectsList.Add(collisionObj.name + " trigger");
            }
            //collidedObjects.Add(collisionObj);
        }
        else
        {
            if (collidedObjects.Count == 1)
            {
                startTimeInit = false;
            }
            if (collidedObjects.Keys.Contains(collisionObj))
            {
                collidedObjects[collisionObj] = true;//(collisionObj);
                collidedObjectsList.Add(collisionObj.name + " collider");
                
            }
        }
        //collisionObj.GetComponent<Troop>().TakeDamage(damageAmount);
        // Kui pole trigger ja esimene vaenlane ilmus range'i siis
        // algv22rtusta kahjustamise algusaeg
        if (collidedObjects.Count == 1 && !startTimeInit)
        {
            // algv22rtustamine
            startTimeDoDmg = Time.fixedTime;
            startTimeInit = true;
        }

        // Kui Objekt teeb esimesel kokkupõrkel kahju, siis
        // implementeeritakse kahjustamise loogika
        // Child klassides
        
    }

    protected virtual bool TriggerStay(Collider2D collision, float DmgFrequency)
    {

        if (Health <= 0) return false;

        GameObject collisionObj = collision.gameObject;

        // Kui objekt ei ole Destructable tyypi, siis ei tee midagi
        if (collisionObj.GetComponent<Destructables>() == null) return false;

        Destructables destructable = collisionObj.GetComponent<Destructables>();

        // Kui objekt on sõbralik, siis ei tee midagi
        if (destructable.getIsFriendly() == this.IsFriendly) return false;

        // Kui objekt on trigger, siis ei tee midagi
        if (collision.isTrigger) return false;

        else {

            if (Time.fixedTime > startTimeDoDmg + DmgFrequency)
            {
                if (AoE)
                {
                    for (int i = 0; i < collidedObjects.Keys.Count; i++)
                    {
                        GameObject collidedTroopN = collidedObjects.Keys.ElementAt(i);

                        if (collidedObjects[collidedTroopN] == false) continue;
                        //GameObject collidedTroopN = collidedObjects[i];
                        if (collidedTroopN.GetComponent<Destructables>() != null)
                        {
                            collidedTroopN.GetComponent<Destructables>().TakeDamage(this.DamageAmount);
                            Debug.LogWarning(this.gameObject.name + " damaged " + collidedTroopN.gameObject.name);
                        }
                    }
                    //collidedObjects.RemoveAll(item => item == null);
                }
                else
                {
                    destructable.TakeDamage(this.DamageAmount);
                    Debug.LogWarning(this.gameObject.name + " damaged " + destructable.gameObject.name);
                }
                startTimeDoDmg = Time.fixedTime;
            }
        }
        // Kui Objekt teeb esimesel kokkupõrkel kahju, siis
        // implementeeritakse kahjustamise loogika
        // Child klassides
        return true;
    }

}