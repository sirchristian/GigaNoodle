using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Library
{
	/// <summary>
	/// Utility methods
	/// </summary>
	public class Utility
	{
		/// <summary>
		/// Serializes a data contract item to a string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Serialize<T>(T item, params Type[] knownTypes)
		{
			using (var stream = new MemoryStream())
			{
				var serializer = new DataContractSerializer(typeof(T), knownTypes);
				serializer.WriteObject(stream, item);

				stream.Position = 0;
				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}
		
		/// <summary>
		/// Deserializes string to an object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <param name="knownTypes"></param>
		/// <returns></returns>
		public static T Deserialize<T>(string o, params Type[] knownTypes)
		{
			return Deserialize<T>(System.Text.Encoding.UTF8.GetBytes(o));
		}

		/// <summary>
		/// Deserializes bytes to an object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <param name="knownTypes"></param>
		/// <returns></returns>
		public static T Deserialize<T>(byte[] o, params Type[] knownTypes)
		{
			using (var stream = new MemoryStream(o))
			{
				var serializer = new DataContractSerializer(typeof(T), knownTypes);
				return (T)serializer.ReadObject(stream);
			}
		}
	}
}