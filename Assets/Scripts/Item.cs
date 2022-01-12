using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public GameObject itemObject;
    public Sprite sprite;
}
