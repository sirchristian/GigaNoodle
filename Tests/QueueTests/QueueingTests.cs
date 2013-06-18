using GigaNoodle.Library.Interfaces;
using GigaNoodle.Tests.TestImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace GigaNoodle.Tests.QueueTests
{
	public class QueueingTests : BaseTest
	{
		[TestMethod]
		public void TestQueueName()
		{
			IQueue queue = TestingKernel.Get<IQueue>();
			Assert.IsTrue(queue.Name == "Test");
		}


	}
}
