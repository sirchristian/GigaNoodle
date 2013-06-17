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

		public int Priority
		{
			get { return 1; }
		}

		public void Push(QueueItem item)
		{
			throw new NotImplementedException();
		}

		public QueueItem Pop()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// An in memory queue we use for testing
		/// </summary>
		private Queue<string> _memoryQueue = new Queue<string>();
	}
}
