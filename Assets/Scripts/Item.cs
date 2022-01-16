using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public GameObject itemObject;
    public Sprite sprite;
}
