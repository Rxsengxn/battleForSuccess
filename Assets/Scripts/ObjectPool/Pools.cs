using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{

    public static Pools Instance;

    public Dictionary<TypeTroop, Pool> pools;
    public GameObject troopPrefab;
    //public GameObject riverRightPrefab;
    //public GameObject riverForwardPrefab;
    //public GameObject portalPrefab;

    private GameObject objectPools;

    void Awake()
    {
        Instance = this;
        objectPools = new GameObject("ObjectPools");
        pools = new Dictionary<TypeTroop, Pool>();
    }

    void Start()
    {
        /*pools.Add(troopPrefab, new Pool(troopPrefab, 10));


        //pools.Add(riverRightPrefab, new Pool(riverRightPrefab, 5));
        //pools.Add(riverForwardPrefab, new Pool(riverForwardPrefab, 5));
        //pools.Add(portalPrefab, new Pool(portalPrefab, 2));

        foreach (Pool pool in pools.Values)
        {
            pool.objectPool.transform.SetParent(objectPools.transform);
        }*/
    }


    public void AddPool(TypeTroop typeTroop, GameObject prefab, int count)
    {

        Pool pool = new Pool(prefab, count, typeTroop);

        //Debug.Log("Pool: " + pool);


        pools.Add(typeTroop, pool);
        pools[typeTroop].objectPool.transform.SetParent(objectPools.transform);
    }


    public PooledObject GetPooledObject(TypeTroop typeTroop)
    {
        //For debugging purposes
        //Debug.Log("Enne reive: " + pools[prefab].ToString());
        PooledObject po = pools[typeTroop].GetPooledObject();
        po.go.GetComponent<Troop>().SetTroopType(typeTroop);
        //Debug.Log("Peale revive: " + pools[prefab].ToString());
        return po;
    }

    public void KillPooledObject(PooledObject po)
    {
        // Print the getName function and enumerator index for all keys of the dictionary
        /*int i = 0;
        foreach (TypeTroop key in pools.Keys)
        {
            Debug.Log("Key " + i + " " + key.getName());
            i++;
        }*/


        /*for (int i = 0; i < )
        {
            Debug.Log("Key " + key.getName());
        }*/

        TypeTroop troopKey = po.go.GetComponent<Troop>().getTypeTroop();

        //Debug.Log(pools.Keys.ToString());


        //Debug.Log(troopKey.getName().ToString());


        po.go.transform.parent = pools[troopKey].objectPool.transform;
        po.go.transform.position = pools[troopKey].objectPool.transform.position;

        pools[troopKey].KillPooledObject(po);
        Debug.Log("Killed: " + po.go.name);
        
        return;
    }
}