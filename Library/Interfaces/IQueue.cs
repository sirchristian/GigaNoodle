using GigaNoodle.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Library.Interfaces
{
	/// <summary>
	/// A queue
	/// </summary>
	public interface IQueue
	{
		/// <summary>
		/// Name of the queue
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The relative priority of the queue
		/// </summary>
		int Priority { get; }

		/// <summary>
		/// Types of items that can get pushed on the queue
		/// </summary>
		Type[] KnownTypes { get; }

		/// <summary>
		/// Pushes an item on the queue
		/// </summary>
		/// <param name="item"></param>
		void Push(QueueItem item);

		/// <summary>
		/// Gets the next item in the queue
		/// </summary>
		/// <returns></returns>
		QueueItem Pop();
	}
}
