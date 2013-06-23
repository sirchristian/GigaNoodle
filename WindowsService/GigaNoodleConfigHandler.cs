using GigaNoodle.Library.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.WindowsService.Config
{
	public class GigaNoodleConfigHandler : ConfigurationSection
	{
		public static GigaNoodleConfigHandler CurrentConfig { get { return _currentConfig; } }
		private static GigaNoodleConfigHandler _currentConfig = ConfigurationManager.GetSection("gigaNoodleConfig") as GigaNoodleConfigHandler;

		/// <summary>
		/// Any known types that can go into the queue. Will be appended to any known type defined at the GigaNoodleConfig section
		/// </summary>
		[ConfigurationProperty("queues")]
		[ConfigurationCollection(typeof(Queue), AddItemName = "queue")]
		public Queues Queues
		{
			get
			{ return (Queues)this["queues"]; }
			set
			{ this["queues"] = value; }
		}

		/// <summary>
		/// Any known types that can go into the queue. Will be use for ALL defined queues
		/// </summary>
		[ConfigurationProperty("queueItemTypes")]
		[ConfigurationCollection(typeof(QueueItemTypes), AddItemName = "queueItemType")]
		public QueueItemKnownTypes KnownTypes
		{
			get
			{ return (QueueItemKnownTypes)this["queueItemTypes"]; }
			set
			{ this["queueItemTypes"] = value; }
		}
	}

	/// <summary>
	/// Defines a queue configuration.
	/// </summary>
	public class Queue : ConfigurationElement
	{
		/// <summary>
		/// The type of the queue
		/// </summary>
		[ConfigurationProperty("type", IsRequired = true)]
		public string TypeAssemblyQualifiedName
		{
			get { return (string)this["type"]; }
			set { this["type"] = value; }
		}

		public Type Type
		{
			get { return Type.GetType((string)this["type"]); }
			set { this["type"] = value.AssemblyQualifiedName; }
		}

		/// <summary>
		/// The name of the queue
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		/// <summary>
		/// The priority of the queue
		/// </summary>
		[ConfigurationProperty("priority", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
		public int Priority
		{
			get { return (int)this["priority"]; }
			set { this["priority"] = value; }
		}

		/// <summary>
		/// Any known types that can go into the queue. Will be appended to any known type defined at the GigaNoodleConfig section
		/// </summary>
		[ConfigurationProperty("queueItemTypes")]
		[ConfigurationCollection(typeof(QueueItemTypes), AddItemName = "queueItemType")]
		public QueueItemTypes KnownTypes
		{
			get
			{ return (QueueItemTypes)this["queueItemTypes"]; }
			set
			{ this["queueItemTypes"] = value; }
		}
	}

	/// <summary>
	/// Defines tne types of the QueueItems to use
	/// </summary>
	public class QueueItemTypes : ConfigurationElement
	{
		/// <summary>
		/// Name of the queue item type
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		/// <summary>
		/// A specific type of QueueItem
		/// </summary>
		[ConfigurationProperty("knownType", DefaultValue=null)]
		public string KnownTypeAssemblyQualifiedName
		{
			get { return (string)this["knownType"]; }
			set { this["knownType"] = value; }
		}

		public Type KnownType
		{
			get { return Type.GetType((string)this["knownType"]); }
			set { this["knownType"] = value.AssemblyQualifiedName; }
		}

		/// <summary>
		/// An assembly that contains QueueItems. All QueueItems found in assembly will be treated as a known type
		/// </summary>
		[ConfigurationProperty("inAssembly", DefaultValue = null)]
		public string InAssemblyFullName
		{
			get { return (string)this["inAssembly"]; }
			set { this["inAssembly"] = value; }
		}

		public Assembly InAssembly
		{
			get { return Assembly.Load((string)this["inAssembly"]); }
			set { this["inAssembly"] = value.FullName; }
		}

		public IEnumerable<Type> GetAllQueueItemType()
		{
			if (!String.IsNullOrWhiteSpace(InAssemblyFullName) && InAssembly != null)
				return InAssembly.GetTypes()
					.Where(t => typeof(QueueItem).IsAssignableFrom(t));

			else if (!String.IsNullOrWhiteSpace(KnownTypeAssemblyQualifiedName) && KnownType != null)
				return new List<Type>() { KnownType };

			else
				return new List<Type>();
		}
	}

	public class Queues : ConfigurationElementCollection
	{
		public Queue this[int index]
		{
			get
			{
				return (Queue)base.BaseGet(index) ;
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public new Queue this[string name]
		{
			get { return (Queue)BaseGet(name); }
		}

		protected override System.Configuration.ConfigurationElement CreateNewElement()
		{
			return new Queue();
		}

		protected override object GetElementKey(System.Configuration.ConfigurationElement element)
		{
			return ((Queue)element).Name;
		}
	}

	public class QueueItemKnownTypes : ConfigurationElementCollection
	{
		public QueueItemTypes this[int index]
		{
			get
			{
				return (QueueItemTypes)base.BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public new QueueItemTypes this[string name]
		{
			get { return (QueueItemTypes)BaseGet(name); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new QueueItemTypes();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((QueueItemTypes)element).Name;
		}
	}
}
