using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Base : Destructibles
{
    // Start is called before the first frame update
    void Start()
    {
        //initialHealth = this.Health;
        //animator = GetComponent<Animator>();
        InitialHealth = this.Health;
        healthBar = GetComponentInChildren<HealthBarUI>();
        animator = GetComponent<Animator>();

        string path = "Animation/base/" + PlayerData.selectedClassTypeTroops[0].ClassName + "/" + (this.IsFriendly ? "" : "enemy ") + PlayerData.selectedClassTypeTroops[0].ClassName +  " Base Animator";
        this.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);

        healthBar.SetMaxHealth(this.InitialHealth);
        healthBar.SetHealth(this.Health);
        if (PlayerData.Difficulty == 2 && IsFriendly)
        {
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D collider in colliders)
            {
                if (collider.isTrigger)
                {
                    collider.size = new Vector2(1.5f, 1f);
                    collider.offset = new Vector2(1.25f, 0f);
                    break;
                }
            }
            DamageAmount = 2.5f;
        }
        else if (PlayerData.Difficulty == 1 && IsFriendly)
        {
            DamageAmount = 15f;
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Destroyed");
        yield return new WaitForSeconds(0.8f);
        NotifyObservers();
        base.NotifyHitlistOnDisable(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.TriggervTrigger(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        base.TriggerStay(other, this.DamageFrequency);
        
    }

    private void OnDisable()
    {
        
    }
    /*public int getInitialHealth()
    {
        return (int) this.initialHealth;
    }*/


}