using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("name")]
	public class ES3UserType_ScriptableObject : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ScriptableObject() : base(typeof(UnityEngine.ScriptableObject)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.ScriptableObject)obj;
			
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.ScriptableObject)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "name":
						instance.name = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ScriptableObjectArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ScriptableObjectArray() : base(typeof(UnityEngine.ScriptableObject[]), ES3UserType_ScriptableObject.Instance)
		{
			Instance = this;
		}
	}
}