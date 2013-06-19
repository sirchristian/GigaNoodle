using GigaNoodle.Library.Interfaces;
using GigaNoodle.WindowsService;
using GigaNoodle.Tests.TestImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace GigaNoodle.Tests.QueueTests
{
	[TestClass]
	public class QueueingTests : BaseTest
	{
		[TestMethod]
		public void TestQueueName()
		{
			var queue = TestingKernel.Get<IQueue>();
			Assert.IsTrue(queue.Name == "Test");
		}

		[TestMethod]
		public void TestWorkItemDoWork()
		{
			var queue = TestingKernel.Get<IQueue>();
			var queueItem = new TestQueueItem();
			queueItem.WillChangeOnDoWork = "NoWorkYet";
			var service = new Service();
			service.TheQueues = new List<IQueue>() { queue };
			queue.Push(queueItem);
			service.Start();
			Assert.AreEqual(queueItem.WillChangeOnDoWork, TestQueueItem.DidWorkString);
			service.Stop();
		}

	}
}
