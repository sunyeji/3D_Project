using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Eduipable,
    Consumable,
    Resource
}

public enum ConsumbleType
{
    Health
}

[Serializable]
public class ItemDataConsumbale
{
    public ConsumbleType Type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] //파일이름은 아이템, 메뉴이름은 뉴 아이템
public class ItemData : ScriptableObject
{
    [Header("Info")] 
    public string displayName; //이름
    public string description; //설명
    public ItemType type; 
    public Sprite icon;
    public GameObject dropPrefab;
}
