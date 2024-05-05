using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    GameObject troopPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // uue classi tegemine
        //TypeTroop defaultClass = new TypeTroop("default", 100, 1, 10, 1, troopPrefab);

        // uue troopi tegemine "default" classi põhjal
        //Troop defaultFighter = defaultClass.GetTroop();


        //Debug.Log(defaultFighter.getHealth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
