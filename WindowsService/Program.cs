using GigaNoodle.Library.Interfaces;
using GigaNoodle.Library.Objects;
using GigaNoodle.WindowsService.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.WindowsService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			var kernel = new StandardKernel();
			kernel.Load(new ServiceModule());

			Args a = new Args();
			CommandLine.Parser.Default.ParseArguments(args, a);

			var queues = new List<IQueue>();

			// Get the config 
			var config = GigaNoodleConfigHandler.CurrentConfig;
			var allBaseKnownTypes = new List<Type>();
			foreach (QueueItemTypes knownTypes in config.KnownTypes)
				allBaseKnownTypes.AddRange(knownTypes.GetAllQueueItemType());

			// Go through the queues and add them
			foreach (Queue queue in config.Queues)
			{
				var knownTypes = new List<Type>();
				knownTypes.AddRange(allBaseKnownTypes);
				knownTypes.AddRange(queue.KnownTypes.GetAllQueueItemType());

				queues.Add((IQueue)Activator.CreateInstance(queue.Type, queue.Name, queue.Priority, knownTypes.ToArray()));
			}

			if (a.Action == ArgAction.Debug)
			{
				// run as a console app
				Console.WriteLine("Starting service as console app. Hit <enter> to stop.");

				var service = new Service();
				service.TheQueues = queues;
				service.Start();

				// block until <enter>
				Console.ReadLine();
			}
			else if (a.Action == ArgAction.Help)
			{
				Console.WriteLine();
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new Service() { TheQueues = queues }
				};
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
