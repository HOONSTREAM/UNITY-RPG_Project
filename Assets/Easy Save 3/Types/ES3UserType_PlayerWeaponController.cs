using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("Equip_Weapon", "One_Hand_Wepaon_Long_Sword", "Two_Hand_Weapon_Great_Sword", "m_CancellationTokenSource", "enabled", "name")]
	public class ES3UserType_PlayerWeaponController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerWeaponController() : base(typeof(PlayerWeaponController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerWeaponController)obj;
			
			writer.WriteProperty("Equip_Weapon", instance.Equip_Weapon, ES3UserType_Item.Instance);
			writer.WritePropertyByRef("One_Hand_Wepaon_Long_Sword", instance.One_Hand_Wepaon_Long_Sword);
			writer.WritePropertyByRef("Two_Hand_Weapon_Great_Sword", instance.Two_Hand_Weapon_Great_Sword);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerWeaponController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Equip_Weapon":
						instance.Equip_Weapon = reader.Read<Item>(ES3UserType_Item.Instance);
						break;
					case "One_Hand_Wepaon_Long_Sword":
						instance.One_Hand_Wepaon_Long_Sword = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "Two_Hand_Weapon_Great_Sword":
						instance.Two_Hand_Weapon_Great_Sword = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "m_CancellationTokenSource":
					instance = (PlayerWeaponController)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
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


	public class ES3UserType_PlayerWeaponControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerWeaponControllerArray() : base(typeof(PlayerWeaponController[]), ES3UserType_PlayerWeaponController.Instance)
		{
			Instance = this;
		}
	}
}