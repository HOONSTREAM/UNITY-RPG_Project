using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("instance", "Weapon_oneHand", "Weapon_TwoHand", "Weapon_Bow", "Weapon_Staff", "Head_Decoration", "Head", "Chest", "Pants", "Outter_Plate", "Shield", "Necklace", "Ring", "Shoes", "Vehicle", "Cape", "Consumable", "Etcs", "SkillBook", "m_CancellationTokenSource", "enabled", "name")]
	public class ES3UserType_ItemDataBase : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ItemDataBase() : base(typeof(ItemDataBase)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ItemDataBase)obj;
			
			writer.WritePropertyByRef("instance", ItemDataBase.instance);
			writer.WritePrivateField("Weapon_oneHand", instance);
			writer.WritePrivateField("Weapon_TwoHand", instance);
			writer.WritePrivateField("Weapon_Bow", instance);
			writer.WritePrivateField("Weapon_Staff", instance);
			writer.WritePrivateField("Head_Decoration", instance);
			writer.WritePrivateField("Head", instance);
			writer.WritePrivateField("Chest", instance);
			writer.WritePrivateField("Pants", instance);
			writer.WritePrivateField("Outter_Plate", instance);
			writer.WritePrivateField("Shield", instance);
			writer.WritePrivateField("Necklace", instance);
			writer.WritePrivateField("Ring", instance);
			writer.WritePrivateField("Shoes", instance);
			writer.WritePrivateField("Vehicle", instance);
			writer.WritePrivateField("Cape", instance);
			writer.WritePrivateField("Consumable", instance);
			writer.WritePrivateField("Etcs", instance);
			writer.WritePrivateField("SkillBook", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ItemDataBase)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "instance":
						ItemDataBase.instance = reader.Read<ItemDataBase>(ES3UserType_ItemDataBase.Instance);
						break;
					case "Weapon_oneHand":
					instance = (ItemDataBase)reader.SetPrivateField("Weapon_oneHand", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Weapon_TwoHand":
					instance = (ItemDataBase)reader.SetPrivateField("Weapon_TwoHand", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Weapon_Bow":
					instance = (ItemDataBase)reader.SetPrivateField("Weapon_Bow", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Weapon_Staff":
					instance = (ItemDataBase)reader.SetPrivateField("Weapon_Staff", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Head_Decoration":
					instance = (ItemDataBase)reader.SetPrivateField("Head_Decoration", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Head":
					instance = (ItemDataBase)reader.SetPrivateField("Head", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Chest":
					instance = (ItemDataBase)reader.SetPrivateField("Chest", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Pants":
					instance = (ItemDataBase)reader.SetPrivateField("Pants", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Outter_Plate":
					instance = (ItemDataBase)reader.SetPrivateField("Outter_Plate", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Shield":
					instance = (ItemDataBase)reader.SetPrivateField("Shield", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Necklace":
					instance = (ItemDataBase)reader.SetPrivateField("Necklace", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Ring":
					instance = (ItemDataBase)reader.SetPrivateField("Ring", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Shoes":
					instance = (ItemDataBase)reader.SetPrivateField("Shoes", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Vehicle":
					instance = (ItemDataBase)reader.SetPrivateField("Vehicle", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Cape":
					instance = (ItemDataBase)reader.SetPrivateField("Cape", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Consumable":
					instance = (ItemDataBase)reader.SetPrivateField("Consumable", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "Etcs":
					instance = (ItemDataBase)reader.SetPrivateField("Etcs", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "SkillBook":
					instance = (ItemDataBase)reader.SetPrivateField("SkillBook", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (ItemDataBase)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ItemDataBaseArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ItemDataBaseArray() : base(typeof(ItemDataBase[]), ES3UserType_ItemDataBase.Instance)
		{
			Instance = this;
		}
	}
}