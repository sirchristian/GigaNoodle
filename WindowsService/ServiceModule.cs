using GigaNoodle.Library.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.WindowsService
{
	class ServiceModule : NinjectModule
	{
		/// <summary>
		/// Initializes the bindings.
		/// </summary>
		public override void Load()
		{
			Bind<IQueue>().To<MemoryQueue.MemoryQueue>();
		}
	}
}
