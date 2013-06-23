using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigaNoodle.Library.Objects;
using GigaNoodle.Tests.TestImplementation;
using GigaNoodle.Library;

namespace GigaNoodle.Tests.QueueTests
{
	[TestClass]
	public class QueueItemTests
	{
		[TestMethod]
		public void Serialization()
		{
			var item = new TestQueueItem();
			item.NonSerializableInt = 0;
			item.NonSerializableString = "Non";
			item.SerializableInt = 1;
			item.SerializableString = "Oui";
			string xml = Utility.Serialize<QueueItem>(item, typeof(TestQueueItem));
			Assert.IsTrue(xml.Contains("SerializableInt"));
			Assert.IsTrue(xml.Contains("SerializableString"));
			Assert.IsFalse(xml.Contains("NonSerializableInt"));
			Assert.IsFalse(xml.Contains("NonSerializableString"));
		}
	}
}
