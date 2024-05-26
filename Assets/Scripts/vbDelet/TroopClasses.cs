using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopClasses : ScriptableObject
{
    public string TroopName;
    public float Health;
    public float DamageAmount;
    public float MovingSpeed;
    public float DamageRange;
    public GameObject gameObject;
    public bool IsFriendly;
    public bool AoE;
}
