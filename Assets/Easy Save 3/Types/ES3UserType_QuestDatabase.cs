using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("instance", "QuestDB", "m_CancellationTokenSource", "enabled", "name")]
	public class ES3UserType_QuestDatabase : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_QuestDatabase() : base(typeof(QuestDatabase)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (QuestDatabase)obj;
			
			writer.WritePropertyByRef("instance", QuestDatabase.instance);
			writer.WriteProperty("QuestDB", instance.QuestDB, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Quest>)));
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (QuestDatabase)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "instance":
						QuestDatabase.instance = reader.Read<QuestDatabase>();
						break;
					case "QuestDB":
						instance.QuestDB = reader.Read<System.Collections.Generic.List<Quest>>();
						break;
					case "m_CancellationTokenSource":
					instance = (QuestDatabase)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
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


	public class ES3UserType_QuestDatabaseArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_QuestDatabaseArray() : base(typeof(QuestDatabase[]), ES3UserType_QuestDatabase.Instance)
		{
			Instance = this;
		}
	}
}