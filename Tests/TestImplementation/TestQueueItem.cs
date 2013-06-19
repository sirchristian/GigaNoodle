using GigaNoodle.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Tests.TestImplementation
{
	[DataContract]
	public class TestQueueItem : QueueItem
	{
		[DataMember]
		public string WillChangeOnDoWork { get; set; }

		[DataMember]
		public int SerializableInt { get; set; }

		[DataMember]
		public string SerializableString { get; set; }

		public int NonSerializableInt { get; set; }
		public string NonSerializableString { get; set; }

		public override void DoWork()
		{
			Console.WriteLine("Doing Test work");
			WillChangeOnDoWork = DidWorkString;
		}

		public static string DidWorkString = "### Did work ###";
	}
}
