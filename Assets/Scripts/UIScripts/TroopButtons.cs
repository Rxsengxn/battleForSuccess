using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TroopButtons : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public GameObject ButtonParent;
    public GameObject spawner;

    public void CreateButtons()
    {
        foreach (var item in Pools.Instance.pools.Keys)
        {
            GameObject button = Instantiate(ButtonPrefab, ButtonParent.transform);
            button.GetComponentsInChildren<TextMeshProUGUI>()[0].text = item.TroopName;
            button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = item.Cost.ToString();
            button.GetComponent<Button>().onClick.AddListener(() => SpawnTroop(item));
            button.GetComponent<Button>().onClick.AddListener(() => ClickCooldown());
        }
    }

    private void ClickCooldown()
    {
        return;
        //StartCoroutine(Cooldown());
    }

    public void DestroyButtons()
    {
        foreach (Transform child in ButtonParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnTroop(TypeTroop item)
    {
        if (GameManager.Instance.Gold < item.Cost)
        {
            //Debug.Log("Not enough gold");
            // Play sound
            // Play deny animation

            EventSystem.current.currentSelectedGameObject.GetComponent<Animator>().SetTrigger("Deny");

            return;
        }
        GameManager.Instance.RemoveGold(item.Cost);
        PooledObject fighterTroopPO = Pools.Instance.GetPooledObject(item);
        GameObject fighterTroopobject = fighterTroopPO.go;
        fighterTroopobject.transform.position = spawner.transform.position;
    }
}
