using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Eduipable,   // 장착 가능한 아이템 (예: 무기, 방어구)
    Consumable,  // 소모형 아이템 (예: 회복 물약)
    Resource     // 자원형 아이템 (예: 재료, 아이템 제작용)
}

public enum ConsumbleType
{
    Health       // 체력 회복용 아이템
}

[Serializable] // 이 클래스를 인스펙터에서 볼 수 있게 만듦 (직렬화)
public class ItemDataConsumbale
{
    public ConsumbleType Type; // 소모 아이템의 종류
    public float value;        // 회복 수치 등 수치 값
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] // ScriptableObject 생성 메뉴에 "New Item"으로 추가됨
public class ItemData : ScriptableObject
{
    [Header("Info")] 
    public string displayName;    // 아이템 이름
    public string description;    // 아이템 설명
    public ItemType type;         // 아이템 종류 (장비, 소모, 자원)
    public Sprite icon;           // 아이템 아이콘
    public GameObject dropPrefab; // 드롭 시 생성될 프리팹
}
