using GigaNoodle.Library.Interfaces;
using GigaNoodle.WindowsService;
using GigaNoodle.Tests.TestImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using GigaNoodle.Library.Objects;

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
			// kinda crappy, but wait for work to be done. Should be fast since test queues are in memory
			System.Threading.Thread.Sleep(100);
			Assert.AreEqual(queueItem.WillChangeOnDoWork, TestQueueItem.DidWorkString);
			service.Stop();
		}

		[TestMethod]
		public void TestQueueComparer()
		{
			var queue1 = new MemoryQueue.MemoryQueue("MemoryQueue", 5);
			var queue2 = TestingKernel.Get<IQueue>();
			
			// Add queues to the list
			List<IQueue> queues = new List<IQueue>() 
			{
				queue1, 
				queue2
			};
			// sort
			queues.Sort(QueueComparer.Comparer);
			// TEST: first queue in the list should have higher priority
			Assert.IsTrue(queues[0].Priority > queues[1].Priority);

			// Create the list with items in different order
			queues = new List<IQueue>() 
			{
				queue2,
				queue1
			};
			// sort
			queues.Sort(QueueComparer.Comparer);
			// TEST: first queue in the list should have higher priority
			Assert.IsTrue(queues[0].Priority > queues[1].Priority);
		}
	}
}
