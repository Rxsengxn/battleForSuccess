using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField]
    
    public GameObject SpawnTroop(TypeTroop typeTroop)
    {
        PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(typeTroop);
        GameObject fighterTroopobject = fighterTroopPO.go;
        fighterTroopobject.transform.position = this.transform.position;
        return fighterTroopobject;
    }

    public void SpawnFighterTroop()
    {
        for (int i = 0; i<1;i++)
        {
            PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(Pools.Instance.pools.Keys.First());
            GameObject fighterTroopobject = fighterTroopPO.go;
            fighterTroopobject.transform.position = this.transform.position;
        }
    }

    public void SpawnArcherTroop()
    {
        PooledObject archerTroopPO = Pools.Instance.GetPooledObject(Pools.Instance.pools.Keys.ElementAt(1));
        GameObject archerTroopobject = archerTroopPO.go;
        archerTroopobject.transform.position = this.transform.position;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}