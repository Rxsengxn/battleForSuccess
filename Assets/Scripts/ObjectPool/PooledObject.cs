using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject
{
    public GameObject go;
    //public GameObject prefab;
    public Troop troop;
    public int index;
    public PooledObject(GameObject go, GameObject prefab, int index)//, Troop troop)
    {
        this.go = go;
        //this.prefab = prefab;
        this.index = index;
        //this.troop = troop;
    }

    public void Revive()
    {
        if (go.Equals(null))
        {
            Debug.Log("the gameobject, you are trying to access is null!");
        }
        go.SetActive(true);
        return;
    }

    public void Kill()
    {
        go.SetActive(false);
        return;
    }
}
