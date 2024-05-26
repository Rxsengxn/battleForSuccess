using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Destructibles : MonoBehaviour, ISubject
{
	[field: SerializeField] public float Health { protected set; get; }
	[field: SerializeField] public float DamageAmount { protected set; get; }
	[field: SerializeField] public float DamageFrequency { protected set; get; }
	[field: SerializeField] public bool IsFriendly { protected set; get; }
	[field: SerializeField] public bool AoE { protected set; get; }
	[field: SerializeField] public float InitialHealth { protected set; get; }

	protected Animator animator;

	public HealthBarUI healthBar;

	private void Awake()
	{
		//collidedObjects = new Dictionary<GameObject, bool>();
		/*initialHealth = this.Health;
		healthBar = GetComponent<HealthBarUI>();*/
		
	}

	/*protected virtual void SetRefs()
	{
		initialHealth = this.Health;
		healthBar = GetComponent<HealthBarUI>();
		animator = GetComponent<Animator>();
	}*/

	private void Start()
	{
	}

	//public virtual bool getIsFriendly() { return this.IsFriendly; }

	//public virtual float getHealth() { return this.Health; }

	public virtual void TakeDamage(float amount)
	{
		if (this.Health <= 0) return;
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
		else return;//throw new Exception("Object not found in collidedObjects");
	}

	public virtual void RestoreMovementSpeed()
	{
		//Debug.Log("Restoring movement speed");
	}

	//public List<GameObject> collidedObjects;

	public Dictionary<GameObject, bool> collidedObjects = new Dictionary<GameObject, bool>();
	public List<string> collidedObjectsList = new List<string>();

	public virtual void NotifyHitlistOnDisable(GameObject self)
	{
		//RemoveMeFromDamageTakers(gameObject);
		/*foreach (GameObject destructables in collidedObjects)
		{
			Debug.Log(destructables.name);
		}*/
		//Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
		if (!GameManager.Instance.GameOn) return;
		for (int i = 0; i < collidedObjects.Keys.Count; i++)//GameObject collidedObject in collidedObjects.Keys)
		{
			GameObject collidedObject = collidedObjects.Keys.ElementAt(i);
			if (collidedObject != null)
			{
				collidedObject.GetComponent<Destructibles>().RemoveMeFromDamageTakers(self);
				collidedObject.GetComponent<Destructibles>().RestoreMovementSpeed();
			}
		}
		//Debug.LogWarning("Clear list");
		collidedObjects.Clear();
		collidedObjectsList.Clear();
		//Debug.LogWarning("list cleared");
		//Debug.Log("List has " + collidedObjects.Count + " elements");
	}


	protected float startTimeDoDmg;
	protected bool startTimeInit = false;
	// OnTriggerEnter
	protected virtual void TriggervTrigger(Collider2D collision)
	{

		if (Health <= 0) return;

		GameObject collisionObj = collision.gameObject;

		// Kui objekt ei ole Destructable tyypi, siis ei tee midagi
		if (collisionObj.GetComponent<Destructibles>() == null) return;

		Destructibles destructable = collisionObj.GetComponent<Destructibles>();

		// Kui objekt on sõbralik, siis ei tee midagi
		if (destructable.IsFriendly == this.IsFriendly) return;

		if (collision.isTrigger)
		{
			// Kui vastase baasi triggerisse siseneb troop, kes pole sõbralik,
			// siis see troop lisatakse listi, keda teavitada, et uuesti liikuma hakata,
			// kui baas saab "surma"

			if (!collidedObjects.Keys.Contains(collisionObj))
			{
				collidedObjects[collisionObj] = false;
				collidedObjectsList.Add(collisionObj.name + " trigger");
			}
		}
		else
		{
			if (collidedObjects.Keys.Contains(collisionObj))
			{
				collidedObjects[collisionObj] = true;
				collidedObjectsList.Add(collisionObj.name + " collider");
			}
			// Kui pole trigger ja esimene vaenlane ilmus range'i siis
			// algv22rtusta kahjustamise algusaeg
			int x = 0;
			foreach (var value in collidedObjects.Values)
			{
				if (value) x++;
			}
			if (x == 1)
			{
				// algv22rtustamine
				startTimeDoDmg = Time.timeSinceLevelLoad;
			}
		}
		//collisionObj.GetComponent<Troop>().TakeDamage(damageAmount);
		
		

		// Kui Objekt teeb esimesel kokkupõrkel kahju, siis
		// implementeeritakse kahjustamise loogika
		// Child klassides

	}

	protected virtual bool TriggerStay(Collider2D collision, float DmgFrequency)
	{
		if (Health <= 0) return false;

		GameObject collisionObj = collision.gameObject;

		// Kui objekt ei ole Destructable tyypi, siis ei tee midagi
		if (collisionObj.GetComponent<Destructibles>() == null) return false;

		Destructibles destructable = collisionObj.GetComponent<Destructibles>();

		// Kui objekt on sõbralik, siis ei tee midagi
		if (destructable.IsFriendly == this.IsFriendly) return false;

		// Kui objekt on trigger, siis ei tee midagi
		if (collision.isTrigger) return false;


		if (Time.timeSinceLevelLoad > startTimeDoDmg + DmgFrequency)
		{

			if (AoE)
			{
				for (int i = 0; i < collidedObjects.Keys.Count; i++)
				{
					GameObject collidedTroopN = collidedObjects.Keys.ElementAt(i);

					if (collidedObjects[collidedTroopN] == false) continue;
					//GameObject collidedTroopN = collidedObjects[i];
					if (collidedTroopN.GetComponent<Destructibles>() != null)
					{
						// Kui objekt on juba surnud, siis ei tee midagi
						//if (collidedTroopN.GetComponent<Destructables>().getHealth() <= 0) continue;
						collidedTroopN.GetComponent<Destructibles>().TakeDamage(this.DamageAmount);
						Debug.LogWarning(this.gameObject.name + " damaged " + collidedTroopN.gameObject.name);
					}
				}
				//collidedObjects.RemoveAll(item => item == null);
			}
			else
			{
				// Kui objekt on juba surnud, siis ei tee midagi
				//if (destructable.GetComponent<Destructables>().getHealth() <= 0) return true;
				destructable.TakeDamage(this.DamageAmount);
				Debug.LogWarning(this.gameObject.name + " damaged " + destructable.gameObject.name);
			}
			startTimeDoDmg = Time.timeSinceLevelLoad;
		}
		
		// Kui Objekt teeb esimesel kokkupõrkel kahju, siis
		// implementeeritakse kahjustamise loogika
		// Child klassides
		return true;
	}
	protected List<IObserver> observers = new List<IObserver>();
	public void Attach(IObserver observer)
	{
		observers.Add(observer);
	}

	public void Detach(IObserver observer)
	{
		observers.Remove(observer);
	}

	public void NotifyObservers()
	{
		observers.ForEach(observer => observer.OnDeath(this));
	}

	/*public int getInitialHealth()
	{
		return (int) this.InitialHealth;
	}*/
}