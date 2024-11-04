using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public Sprite image;
    public string name;
    [Range(0, 100)] public float speed;
    public int price;

    public bool isPurchased;
}
