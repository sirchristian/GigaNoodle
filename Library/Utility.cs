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
		/// Deserializes a data contract item to a string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Deserialize<T>(T item, params Type[] knownTypes)
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
	}
}
