using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("Instance", "_player_Inven_Content", "stat", "player_items", "slot", "onChangeItem", "m_CancellationTokenSource", "enabled", "name")]
	public class ES3UserType_PlayerInventory : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerInventory() : base(typeof(PlayerInventory)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerInventory)obj;
			
			writer.WritePropertyByRef("Instance", PlayerInventory.Instance);
			writer.WritePropertyByRef("_player_Inven_Content", instance._player_Inven_Content);
			writer.WritePrivateFieldByRef("stat", instance);
			writer.WriteProperty("player_items", instance.player_items, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Item>)));
			writer.WritePropertyByRef("slot", instance.slot);
			writer.WriteProperty("onChangeItem", instance.onChangeItem, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(PlayerInventory.OnChangeItem)));
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerInventory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Instance":
						PlayerInventory.Instance = reader.Read<PlayerInventory>(ES3UserType_PlayerInventory.Instance);
						break;
					case "_player_Inven_Content":
						instance._player_Inven_Content = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "stat":
					instance = (PlayerInventory)reader.SetPrivateField("stat", reader.Read<PlayerStat>(), instance);
					break;
					case "player_items":
						instance.player_items = reader.Read<System.Collections.Generic.List<Item>>();
						break;
					case "slot":
						instance.slot = reader.Read<Slot>();
						break;
					case "onChangeItem":
						instance.onChangeItem = reader.Read<PlayerInventory.OnChangeItem>();
						break;
					case "m_CancellationTokenSource":
					instance = (PlayerInventory)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
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


	public class ES3UserType_PlayerInventoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerInventoryArray() : base(typeof(PlayerInventory[]), ES3UserType_PlayerInventory.Instance)
		{
			Instance = this;
		}
	}
}