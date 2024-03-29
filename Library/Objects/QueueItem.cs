﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.Library.Objects
{
	/// <summary>
	/// An item that can be queued
	/// </summary>
	[Serializable, DataContract(Namespace="http://giganoodle.com/xml")]
	public abstract class QueueItem
	{
		/// <summary>
		/// An id that can track multiple related items in the queue
		/// </summary>
		[DataMember]
		public Guid CorrelationId { get; set; }

		/// <summary>
		/// Work to do once off the queue
		/// </summary>
		public abstract void DoWork();
	}
}
