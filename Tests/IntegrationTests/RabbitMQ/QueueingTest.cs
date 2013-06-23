using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigaNoodle.Tests.TestImplementation;
using GigaNoodle.WindowsService;
using GigaNoodle.Library.Interfaces;
using System.Collections.Generic;

namespace GigaNoodle.Tests.IntegrationTests.RabbitMQ
{
	[TestClass]
	public class QueueingTest : BaseTest
	{
		[TestMethod]
		public void TestWorkItemDoWork()
		{
			var queue = (IQueue) new RabbitMQueue.RabbitMQueue("TestQueue", 1, typeof(TestQueueItem));
			var queueItem = new TestQueueItem();
			queueItem.WillChangeOnDoWork = "NoWorkYet";
			var service = new Service();
			service.TheQueues = new List<IQueue>() { queue };
			queue.Push(queueItem);
			service.Start();

			// Basically this is an integration test. Not doing any assertions because of that. 
		}
	}
}
