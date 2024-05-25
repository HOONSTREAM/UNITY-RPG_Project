using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ItemID", "itemrank", "equiptype", "weapontype", "itemtype", "itemname", "stat_1", "stat_2", "num_1", "num_2", "stat_3", "stat_4", "num_3", "num_4", "Description", "itemImage", "Equip", "amount", "buyprice", "sellprice", "efts")]
	public class ES3UserType_Item : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Item() : base(typeof(Item)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (Item)obj;
			
			writer.WriteProperty("ItemID", instance.ItemID, ES3Type_int.Instance);
			writer.WriteProperty("itemrank", instance.itemrank, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ItemRank)));
			writer.WriteProperty("equiptype", instance.equiptype, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(EquipType)));
			writer.WriteProperty("weapontype", instance.weapontype, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(WeaponType)));
			writer.WriteProperty("itemtype", instance.itemtype, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ItemType)));
			writer.WriteProperty("itemname", instance.itemname, ES3Type_string.Instance);
			writer.WriteProperty("stat_1", instance.stat_1, ES3Type_string.Instance);
			writer.WriteProperty("stat_2", instance.stat_2, ES3Type_string.Instance);
			writer.WriteProperty("num_1", instance.num_1, ES3Type_int.Instance);
			writer.WriteProperty("num_2", instance.num_2, ES3Type_int.Instance);
			writer.WriteProperty("stat_3", instance.stat_3, ES3Type_string.Instance);
			writer.WriteProperty("stat_4", instance.stat_4, ES3Type_string.Instance);
			writer.WriteProperty("num_3", instance.num_3, ES3Type_int.Instance);
			writer.WriteProperty("num_4", instance.num_4, ES3Type_int.Instance);
			writer.WriteProperty("Description", instance.Description, ES3Type_string.Instance);
			writer.WritePropertyByRef("itemImage", instance.itemImage);
			writer.WriteProperty("Equip", instance.Equip, ES3Type_bool.Instance);
			writer.WriteProperty("amount", instance.amount, ES3Type_int.Instance);
			writer.WriteProperty("buyprice", instance.buyprice, ES3Type_int.Instance);
			writer.WriteProperty("sellprice", instance.sellprice, ES3Type_int.Instance);
			writer.WriteProperty("efts", instance.efts, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<ItemEffect>)));
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (Item)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ItemID":
						instance.ItemID = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "itemrank":
						instance.itemrank = reader.Read<ItemRank>();
						break;
					case "equiptype":
						instance.equiptype = reader.Read<EquipType>();
						break;
					case "weapontype":
						instance.weapontype = reader.Read<WeaponType>();
						break;
					case "itemtype":
						instance.itemtype = reader.Read<ItemType>();
						break;
					case "itemname":
						instance.itemname = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "stat_1":
						instance.stat_1 = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "stat_2":
						instance.stat_2 = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "num_1":
						instance.num_1 = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "num_2":
						instance.num_2 = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "stat_3":
						instance.stat_3 = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "stat_4":
						instance.stat_4 = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "num_3":
						instance.num_3 = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "num_4":
						instance.num_4 = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Description":
						instance.Description = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "itemImage":
						instance.itemImage = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "Equip":
						instance.Equip = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "amount":
						instance.amount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "buyprice":
						instance.buyprice = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "sellprice":
						instance.sellprice = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "efts":
						instance.efts = reader.Read<System.Collections.Generic.List<ItemEffect>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new Item();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_ItemArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ItemArray() : base(typeof(Item[]), ES3UserType_Item.Instance)
		{
			Instance = this;
		}
	}
}