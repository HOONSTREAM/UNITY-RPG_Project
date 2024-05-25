using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_exp", "_gold", "_str", "_int", "_vit", "_agi", "one_hand_sword_AbilityAttack", "two_hand_sword_AbilityAttack", "improvement_Ability_attack", "buff_damage", "buff_DEFENSE", "equipment", "onchangestat", "_level", "_hp", "_maxhp", "_mp", "_maxMp", "_movespeed", "_attack", "_defense", "Fielditem", "m_CancellationTokenSource", "EXP", "Gold", "STR", "INT", "VIT", "AGI", "LEVEL", "Hp", "MAXHP", "Mp", "MaxMp", "ATTACK", "DEFENSE", "MOVESPEED", "enabled", "name")]
	public class ES3UserType_PlayerStat : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerStat() : base(typeof(PlayerStat)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerStat)obj;
			
			writer.WritePrivateField("_exp", instance);
			writer.WritePrivateField("_gold", instance);
			writer.WritePrivateField("_str", instance);
			writer.WritePrivateField("_int", instance);
			writer.WritePrivateField("_vit", instance);
			writer.WritePrivateField("_agi", instance);
			writer.WriteProperty("one_hand_sword_AbilityAttack", instance.one_hand_sword_AbilityAttack, ES3Type_int.Instance);
			writer.WriteProperty("two_hand_sword_AbilityAttack", instance.two_hand_sword_AbilityAttack, ES3Type_int.Instance);
			writer.WriteProperty("improvement_Ability_attack", instance.improvement_Ability_attack, ES3Type_int.Instance);
			writer.WriteProperty("buff_damage", instance.buff_damage, ES3Type_int.Instance);
			writer.WriteProperty("buff_DEFENSE", instance.buff_DEFENSE, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("equipment", instance);
			writer.WriteProperty("onchangestat", instance.onchangestat, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(PlayerStat.OnChangePlayerStat)));
			writer.WritePrivateField("_level", instance);
			writer.WritePrivateField("_hp", instance);
			writer.WritePrivateField("_maxhp", instance);
			writer.WritePrivateField("_mp", instance);
			writer.WritePrivateField("_maxMp", instance);
			writer.WritePrivateField("_movespeed", instance);
			writer.WritePrivateField("_attack", instance);
			writer.WritePrivateField("_defense", instance);
			writer.WritePropertyByRef("Fielditem", instance.Fielditem);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("EXP", instance.EXP, ES3Type_int.Instance);
			writer.WriteProperty("Gold", instance.Gold, ES3Type_int.Instance);
			writer.WriteProperty("STR", instance.STR, ES3Type_int.Instance);
			writer.WriteProperty("INT", instance.INT, ES3Type_int.Instance);
			writer.WriteProperty("VIT", instance.VIT, ES3Type_int.Instance);
			writer.WriteProperty("AGI", instance.AGI, ES3Type_int.Instance);
			writer.WriteProperty("LEVEL", instance.LEVEL, ES3Type_int.Instance);
			writer.WriteProperty("Hp", instance.Hp, ES3Type_int.Instance);
			writer.WriteProperty("MAXHP", instance.MAXHP, ES3Type_int.Instance);
			writer.WriteProperty("Mp", instance.Mp, ES3Type_int.Instance);
			writer.WriteProperty("MaxMp", instance.MaxMp, ES3Type_int.Instance);
			writer.WriteProperty("ATTACK", instance.ATTACK, ES3Type_int.Instance);
			writer.WriteProperty("DEFENSE", instance.DEFENSE, ES3Type_int.Instance);
			writer.WriteProperty("MOVESPEED", instance.MOVESPEED, ES3Type_float.Instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerStat)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_exp":
					instance = (PlayerStat)reader.SetPrivateField("_exp", reader.Read<System.Int32>(), instance);
					break;
					case "_gold":
					instance = (PlayerStat)reader.SetPrivateField("_gold", reader.Read<System.Int32>(), instance);
					break;
					case "_str":
					instance = (PlayerStat)reader.SetPrivateField("_str", reader.Read<System.Int32>(), instance);
					break;
					case "_int":
					instance = (PlayerStat)reader.SetPrivateField("_int", reader.Read<System.Int32>(), instance);
					break;
					case "_vit":
					instance = (PlayerStat)reader.SetPrivateField("_vit", reader.Read<System.Int32>(), instance);
					break;
					case "_agi":
					instance = (PlayerStat)reader.SetPrivateField("_agi", reader.Read<System.Int32>(), instance);
					break;
					case "one_hand_sword_AbilityAttack":
						instance.one_hand_sword_AbilityAttack = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "two_hand_sword_AbilityAttack":
						instance.two_hand_sword_AbilityAttack = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "improvement_Ability_attack":
						instance.improvement_Ability_attack = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "buff_damage":
						instance.buff_damage = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "buff_DEFENSE":
						instance.buff_DEFENSE = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "equipment":
					instance = (PlayerStat)reader.SetPrivateField("equipment", reader.Read<PlayerEquipment>(), instance);
					break;
					case "onchangestat":
						instance.onchangestat = reader.Read<PlayerStat.OnChangePlayerStat>();
						break;
					case "_level":
					instance = (PlayerStat)reader.SetPrivateField("_level", reader.Read<System.Int32>(), instance);
					break;
					case "_hp":
					instance = (PlayerStat)reader.SetPrivateField("_hp", reader.Read<System.Int32>(), instance);
					break;
					case "_maxhp":
					instance = (PlayerStat)reader.SetPrivateField("_maxhp", reader.Read<System.Int32>(), instance);
					break;
					case "_mp":
					instance = (PlayerStat)reader.SetPrivateField("_mp", reader.Read<System.Int32>(), instance);
					break;
					case "_maxMp":
					instance = (PlayerStat)reader.SetPrivateField("_maxMp", reader.Read<System.Int32>(), instance);
					break;
					case "_movespeed":
					instance = (PlayerStat)reader.SetPrivateField("_movespeed", reader.Read<System.Single>(), instance);
					break;
					case "_attack":
					instance = (PlayerStat)reader.SetPrivateField("_attack", reader.Read<System.Int32>(), instance);
					break;
					case "_defense":
					instance = (PlayerStat)reader.SetPrivateField("_defense", reader.Read<System.Int32>(), instance);
					break;
					case "Fielditem":
						instance.Fielditem = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "m_CancellationTokenSource":
					instance = (PlayerStat)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					case "EXP":
						instance.EXP = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Gold":
						instance.Gold = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "STR":
						instance.STR = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "INT":
						instance.INT = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "VIT":
						instance.VIT = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "AGI":
						instance.AGI = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "LEVEL":
						instance.LEVEL = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Hp":
						instance.Hp = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "MAXHP":
						instance.MAXHP = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Mp":
						instance.Mp = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "MaxMp":
						instance.MaxMp = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "ATTACK":
						instance.ATTACK = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "DEFENSE":
						instance.DEFENSE = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "MOVESPEED":
						instance.MOVESPEED = reader.Read<System.Single>(ES3Type_float.Instance);
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


	public class ES3UserType_PlayerStatArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerStatArray() : base(typeof(PlayerStat[]), ES3UserType_PlayerStat.Instance)
		{
			Instance = this;
		}
	}
}