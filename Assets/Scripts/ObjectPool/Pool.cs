using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    public List<PooledObject> pool;
    public int index;
    public GameObject objectPool;

    private GameObject prefab;
    private int count;

    private TypeTroop typeTroop;

    public Pool(GameObject prefab, int count, TypeTroop typeTroop)
    {
        pool = new List<PooledObject>();
        objectPool = new GameObject("ObjectPool " + typeTroop.TroopName);
        this.prefab = prefab;
        this.count = count;
        this.typeTroop = typeTroop;

        AddNewObjects(0, count);

        index = 0;
    }

    public PooledObject GetPooledObject()
    {
        if (index >= pool.Count)
        {
            Debug.Log($"There was no more objects in the pool so 10 new {prefab.name} objects were made.");

            AddNewObjects(count, 10);
            this.count += 10;
        }

        //If the index of the pointer is between the list's elements, revive next and return it.
        if (index < pool.Count && index > -1)
        {
            if (pool.Count != 0)
            {
                PooledObject po = pool[index];
                po.Revive();
                index++;
                return po;
            }
        }
        else throw new System.Exception("There are no more pooled objects to revive!\nindex was: " + index.ToString() + " and pool count was: " + pool.Count.ToString());

        return null;
    }

    public void KillPooledObject(PooledObject po)
    {
        if (index > 0)
        {
            po.Kill();
            Debug.Log("Killed: " + po.go.name + " index: " + index.ToString());
            int i = pool.IndexOf(po);
            if (i != -1)
            {
                //If the po is not the first, swap this po's place with the next dead object's
                //to keep the structure of the pool.
                if (index > 1)
                {
                    PooledObject temp = po;
                    PooledObject temp2 = pool[index - 1];
                    pool[pool.IndexOf(po)] = temp2;
                    pool[index - 1] = temp;
                }
                index--;
            }
            else Debug.LogWarning("The object you are trying to kill has already been killed!!");
        }
        else throw new System.Exception("There are no more pooled objects to kill!");
        return;
    }

    private void AddNewObjects(int from, int count)
    {
        for (int i = from; i < from+count; i++)
        {
            GameObject go = Object.Instantiate<GameObject>(prefab);
            //pool.Add(new PooledObject(new GameObject("copy_of_"+prefab.name), prefab, i));
            PooledObject po = new PooledObject(go, prefab, i);//, prefab.GetComponent<Troop>());
            

            go.GetComponent<Troop>().SetTroopType(typeTroop);
            go.GetComponent<Troop>().SetPooledObject(po);
            po.Kill();
            go.transform.SetParent(objectPool.transform);
            go.name = typeTroop.TroopName + " no: " + (i).ToString();
            pool.Add(po);

        }
    }

    public override string ToString()
    {
        string output = "";
        foreach (PooledObject po in pool)
        {
            if (po.go.Equals(null))
            {
                output += "null, ";
            }
            else output += po.go.name + "  " + po.go.activeSelf.ToString() + ", ";

        }
        return output + "pointer on kohal indeksiga: " + index;
    }

}
