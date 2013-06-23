using GigaNoodle.Library;
using GigaNoodle.Library.Interfaces;
using GigaNoodle.Library.Objects;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.RabbitMQueue
{
	public class RabbitMQueue : IQueue
	{

		/// <summary>
		/// Construct a Queue that uses RabbitMQ. 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="priority"></param>
		/// <param name="knownTypes"></param>
		public RabbitMQueue(string name, int priority, params Type[] knownTypes)
		{
			_name = name;
			_exchangeName = String.Format("{0}.Direct", _name);
			_priority = priority;
			_knownTypes = knownTypes;
		}

		/// <summary>
		/// Gets the name of the queue
		/// </summary>
		string IQueue.Name
		{
			get { return _name; }
		}
		private string _name;
		private string _exchangeName;

		/// <summary>
		/// Gets the priority of the queue
		/// </summary>
		int IQueue.Priority
		{
			get { return _priority; }
		}
		private int _priority;

		/// <summary>
		/// The types this queue know's how to serialize
		/// </summary>
		Type[] IQueue.KnownTypes
		{
			get { return _knownTypes; }
		}
		private Type[] _knownTypes;

		/// <summary>
		/// Push an item onto the queue
		/// </summary>
		/// <param name="item"></param>
		void IQueue.Push(QueueItem item)
		{
			try
			{
				// Get our message bytes
				byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(Utility.Serialize(item, _knownTypes));

				// publish the message on the queue
				RabbitMQChannel.BasicPublish(_exchangeName, routingKey: null, basicProperties: null, body: messageBodyBytes);
			}
			finally
			{
				// Cleanup
				CleanupChannel();
			}
		}

		/// <summary>
		/// Pops a message from the queue
		/// </summary>
		/// <returns></returns>
		QueueItem IQueue.Pop()
		{
			bool noAck = false;
			BasicGetResult result = RabbitMQChannel.BasicGet(_name, noAck);
			try
			{
				// If we have something return it, otherwise return null
				if (result == null)
					return null;
				else
					return Utility.Deserialize<QueueItem>(result.Body, _knownTypes);
			}
			finally
			{
				// acknowledge receipt of the message
				RabbitMQChannel.BasicAck(result.DeliveryTag, false);
			}
		}

		private IModel RabbitMQChannel
		{
			get
			{
				// TODO: How expensive is it to keep connection open? 
				//       How expensive is it to open/close a connetion?
				//  Need to find out best practices. Should we open/close 
				//  every get or keep a persistent connection?
				// TODO (CH): Read this better: http://www.rabbitmq.com/releases/rabbitmq-dotnet-client/v3.1.1/rabbitmq-dotnet-client-3.1.1-user-guide.pdf
				if (_channel == null)
				{
					ConnectionFactory factory = new ConnectionFactory();
					factory.Uri = "amqp://user:pass@hostName:port/vhost";
					_conn = factory.CreateConnection();
					_channel = _conn.CreateModel();

					// Setup the exchange and queue and bind them together
					_channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
					_channel.QueueDeclare(_name, durable: true, exclusive: false, autoDelete: false, arguments: null);
					_channel.QueueBind(_name, _exchangeName, routingKey: null, arguments: null);
				}

				return _channel;
			}
		}

		private void CleanupChannel()
		{
			_channel.Close(200, "Goodbye");
			_conn.Close();
			_conn = null;
			_channel = null;
		}
		private IModel _channel = null;
		private IConnection _conn = null;

	}
}
