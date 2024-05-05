using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
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
        for (int i = 0; i < 1; i++)
        {
            PooledObject archerTroopPO = Pools.Instance.GetPooledObject(Pools.Instance.pools.Keys.Last());
            GameObject archerTroopobject = archerTroopPO.go;
            archerTroopobject.transform.position = this.transform.position;  
        }
        
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}