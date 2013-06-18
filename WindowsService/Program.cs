﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
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

			if (args.Contains("action=debug"))
			{
				// run as a console app
				Console.WriteLine("Starting service as console app. Hit <enter> to stop.");

				var service = new Service();
				service.TheQueues = new List<Library.Interfaces.IQueue>();
				service.TheQueues.Add(new MemoryQueue.MemoryQueue("consolequeue", 1));
				service.Start();

				// block until <enter>
				Console.ReadLine();
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new Service() 
				};
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}