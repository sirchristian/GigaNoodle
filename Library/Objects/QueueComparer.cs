using GigaNoodle.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Library.Objects
{
	public class QueueComparer : IComparer<IQueue>
	{
		int IComparer<IQueue>.Compare(IQueue q1, IQueue q2)
		{
			return q2.Priority.CompareTo(q1.Priority);
		}

		public static QueueComparer Comparer
		{
			get
			{
				return new QueueComparer();
			}
		}
	}
}
