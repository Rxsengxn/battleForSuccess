using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]

    public int Difficulty;

    public GameObject SpawnTroop(TypeTroop typeTroop)
    {
        PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(typeTroop);
        GameObject fighterTroopobject = fighterTroopPO.go;
        fighterTroopobject.transform.position = this.transform.position;
        return fighterTroopobject;
    }

    public void SpawnFirstGradeTroop(int typeOfTroop)
    {
        // Type + 0
        for (int i = 0; i < 1; i++)
        {
            PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(PlayerData.selectedEnemyTypeTroops[typeOfTroop*3]);
            GameObject fighterTroopobject = fighterTroopPO.go;
            fighterTroopobject.transform.position = this.transform.position;  
        }
        
    }

    public void SpawnSecondOrFirstGradeTroop(int typeOfTroop)
    {
        // Type + 1
        for (int i = 0; i < 1; i++)
        {
            PooledObject archerTroopPO = Pools.Instance.GetPooledObject(PlayerData.selectedEnemyTypeTroops[(typeOfTroop * 3)+Random.Range(0, 2)]);
            GameObject archerTroopobject = archerTroopPO.go;
            archerTroopobject.transform.position = this.transform.position;
        }
    }

    public void SpawnThirdGradeTroop(int typeOfTroop)
    {
        // Type + 2
        for (int i = 0; i < 1; i++)
        {
            PooledObject toughTroopPO = Pools.Instance.GetPooledObject(PlayerData.selectedEnemyTypeTroops[(typeOfTroop * 3) + 2]);
            GameObject toughTroopobject = toughTroopPO.go;
            toughTroopobject.transform.position = this.transform.position;
        }
    }   

    public void SpawnRandomGradeTroop(int typeOfTroop)
    {
        for (int i = 0; i < 1; i++)
        {
            PooledObject toughTroopPO = Pools.Instance.GetPooledObject(PlayerData.selectedEnemyTypeTroops[typeOfTroop]);
            GameObject toughTroopobject = toughTroopPO.go;
            toughTroopobject.transform.position = this.transform.position;
        }
    }

    private void Start()
    {
        this.Difficulty = PlayerData.Difficulty;
    }

    private bool RandomChanceNotToSpawn()
    {
        return Random.Range(0, 100) < 80;
    }

    private void Update()
    {
        
        if (SpawnEnemyCurve(3f))
        {
            // Can only spawn first grade troops
            if (RandomChanceNotToSpawn()) SpawnFirstGradeTroop((int) Random.Range(0, Difficulty));
        }
        if (SpawnEnemyCurve(0.9f))
        {
            // Can spawn first and second grade troops
            if (RandomChanceNotToSpawn()) SpawnSecondOrFirstGradeTroop((int) Random.Range(0, Difficulty));
        }
        if (SpawnEnemyCurve(0.05f))
        {
            // Can spawn first, second and third grade troops
            if (RandomChanceNotToSpawn()) SpawnRandomGradeTroop((int) Random.Range(0, (Difficulty*3)));
        }
    }

    private void FixedUpdate()
    {
        
    }

    Dictionary<float, float> timeToSpawnDict = new Dictionary<float, float>();


    public bool SpawnEnemyCurve(float troopGrade)
    {
        if (timeToSpawnDict.ContainsKey(troopGrade))
        {
            float tempTime = Time.timeSinceLevelLoad;

            float timeToSpawnNext = Mathf.Floor((Mathf.Pow(tempTime, 1.4f) * troopGrade / 130f)+0.75f);
            bool returnVal = timeToSpawnNext != timeToSpawnDict[troopGrade];
            timeToSpawnDict[troopGrade] = timeToSpawnNext;
            return returnVal;
        }
        else
        {
            timeToSpawnDict.Add(troopGrade, Time.timeSinceLevelLoad);
            return false;
        }
    }

    public bool SpawnEnemyLinear(float troopGrade)
    {
        if (timeToSpawnDict.ContainsKey(troopGrade))
        {
            float tempTime = Time.timeSinceLevelLoad;

            float timeToSpawnNext = Mathf.Floor(tempTime * (troopGrade*10f));
            bool returnVal = timeToSpawnNext != timeToSpawnDict[troopGrade];
            timeToSpawnDict[troopGrade] = timeToSpawnNext;
            return returnVal;
        }
        else
        {
            timeToSpawnDict.Add(troopGrade, Time.timeSinceLevelLoad);
            return false;
        }
    }
}