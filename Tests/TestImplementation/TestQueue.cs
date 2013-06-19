using GigaNoodle.Library.Interfaces;
using GigaNoodle.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Tests.TestImplementation
{
	/// <summary>
	/// Test queue implementation
	/// </summary>
	public class TestQueue : IQueue
	{
		/// <summary>
		/// Test
		/// </summary>
		public string Name
		{
			get { return "Test"; }
		}

		/// <summary>
		/// 1
		/// </summary>
		public int Priority
		{
			get { return 1; }
		}

		/// <summary>
		/// Known types of items that can go in the test queue
		/// </summary>
		public Type[] KnownTypes
		{
			get { return _knownTypes; }
		}
		private static readonly Type[] _knownTypes = new Type[] { typeof(TestQueueItem) };

		/// <summary>
		/// Pushes onto a memory queue
		/// </summary>
		/// <param name="item"></param>
		public void Push(QueueItem item)
		{
			_memoryQueue.Enqueue(item);
		}

		/// <summary>
		/// Pops from a memory queue
		/// </summary>
		/// <returns></returns>
		public QueueItem Pop()
		{
			if (_memoryQueue.Count > 0)
				return _memoryQueue.Dequeue();

			return null;
		}

		/// <summary>
		/// An in memory queue we use for testing
		/// </summary>
		private Queue<QueueItem> _memoryQueue = new Queue<QueueItem>();
	}
}
