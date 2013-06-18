using GigaNoodle.Library.Interfaces;
using GigaNoodle.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.MemoryQueue
{
    public class MemoryQueue : IQueue
    {
		/// <summary>
		/// Constructs a memory queue with name and priority
		/// </summary>
		/// <param name="name"></param>
		/// <param name="priority"></param>
		public MemoryQueue(string name, int priority)
		{
			_name = name;
			_priority = priority;
		}

		/// <summary>
		/// The name of the in memory queue
		/// </summary>
		string IQueue.Name
		{
			get { return _name; }
		}
		private string _name;

		/// <summary>
		/// The priority of the queue
		/// </summary>
		int IQueue.Priority
		{
			get { return _priority; }
		}
		private int _priority;

		/// <summary>
		/// Not implemented for MemoryQueue
		/// </summary>
		Type[] IQueue.KnownTypes
		{
			get { throw new NotImplementedException(); }
		}
		private Type[] _types;

		/// <summary>
		/// Pushes a work item onto the memory queue
		/// </summary>
		/// <param name="item"></param>
		void IQueue.Push(QueueItem item)
		{
			_memoryQueue.Enqueue(item);
		}

		/// <summary>
		/// Pops a work item from the memory queue
		/// </summary>
		/// <returns></returns>
		QueueItem IQueue.Pop()
		{
			if (_memoryQueue.Count > 0)
				return _memoryQueue.Dequeue();

			return null;
		}

		private Queue<QueueItem> _memoryQueue = new Queue<QueueItem>();
	}
}
