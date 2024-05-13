using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// 전체 아이템을 저장하고있을 데이터베이스 
public class ItemDataBase : MonoBehaviour
{
    
   public static ItemDataBase instance;


    #region Weapon
    [SerializeField]
    private List<Item> Weapon_oneHand = new List<Item>();
    [SerializeField]
    private List<Item> Weapon_TwoHand = new List<Item>();
    [SerializeField]
    private List<Item> Weapon_Bow = new List<Item>();
    [SerializeField]
    private List<Item> Weapon_Staff = new List<Item>();
    #endregion

    [SerializeField]
    private List<Item> Head_Decoration = new List<Item>();
    [SerializeField]
    private List<Item> Head = new List<Item>();
    [SerializeField]
    private List<Item> Chest = new List<Item>(); // 내부상의
    [SerializeField]
    private List<Item> Pants = new List<Item>(); // 내부바지
    [SerializeField]
    private List<Item> Outter_Plate = new List<Item>(); // 겉갑옷
    [SerializeField]
    private List<Item> Shield = new List<Item>();
    [SerializeField]
    private List<Item> Necklace = new List<Item>();
    [SerializeField]
    private List<Item> Ring = new List<Item>();
    [SerializeField]
    private List<Item> Shoes = new List<Item>();
    [SerializeField]
    private List<Item> Vehicle = new List<Item>();
    [SerializeField]
    private List<Item> Cape = new List<Item>();
    [SerializeField]
    private List<Item> Consumable = new List<Item>();
    [SerializeField]
    private List<Item> Etcs = new List<Item>();
    [SerializeField]
    private List<Item> SkillBook = new List<Item>();



    private void Awake()
    {
        instance = this;      
    }

    public Dictionary<string, ReadOnlyCollection<Item>> GetAllItems()
    {
        var allItems = new Dictionary<string, ReadOnlyCollection<Item>>
        {
            {"Weapon_oneHand", new ReadOnlyCollection<Item>(Weapon_oneHand)},
            {"Weapon_TwoHand", new ReadOnlyCollection<Item>(Weapon_TwoHand)},
            {"Weapon_Bow", new ReadOnlyCollection<Item>(Weapon_Bow)},
            {"Weapon_Staff", new ReadOnlyCollection<Item>(Weapon_Staff)},
            {"Head_Decoration", new ReadOnlyCollection<Item>(Head_Decoration)},
            {"Head", new ReadOnlyCollection<Item>(Head)},
            {"Chest", new ReadOnlyCollection<Item>(Chest)},
            {"Pants", new ReadOnlyCollection<Item>(Pants)},
            {"Outter_Plate", new ReadOnlyCollection<Item>(Outter_Plate)},
            {"Shield", new ReadOnlyCollection<Item>(Shield)},
            {"Necklace", new ReadOnlyCollection<Item>(Necklace)},
            {"Ring", new ReadOnlyCollection<Item>(Ring)},
            {"Shoes", new ReadOnlyCollection<Item>(Shoes)},
            {"Vehicle", new ReadOnlyCollection<Item>(Vehicle)},
            {"Cape", new ReadOnlyCollection<Item>(Cape)},
            {"Consumable", new ReadOnlyCollection<Item>(Consumable) },
            {"Etcs", new ReadOnlyCollection<Item>(Etcs) },
            {"SkillBook" , new ReadOnlyCollection<Item>(SkillBook)}
        };

        return allItems;
    }



}
