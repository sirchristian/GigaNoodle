using GigaNoodle.Library.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.WindowsService
{
	internal partial class Service : ServiceBase
	{
		public List<IQueue> TheQueues { get; set; }

		public Service()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			Start();
		}

		protected override void OnStop()
		{
			// we are stopping
			stopping = true;

			// Wait for all the tasks to finish
			Task.WaitAll(_currentRunningTasks.ToArray());
		}
		bool stopping = false;

		internal void Start()
		{
			// Queue up a task
			var task = Task
				.Run(() => WorkOnItem())
				.ContinueWith(t => _currentRunningTasks.Remove(t));

			_currentRunningTasks.Add(task);
		}

		private void WorkOnItem()
		{
			// do the work
			var item = TheQueues[0].Pop();

			if (item != null)
				item.DoWork();
			else
				System.Threading.Thread.Sleep(300);

			// if not stopping queue up another task
			if (!stopping)
			{
				var task = Task
					.Run(() => WorkOnItem())
					.ContinueWith(t => _currentRunningTasks.Remove(t));

				_currentRunningTasks.Add(task);
			}
		}

		private List<Task> _currentRunningTasks = new List<Task>();
	}
}
