using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaNoodle.WindowsService
{
	enum ArgAction
	{
		None,
		Debug, 
		Help,
		Install
	}

	class Args
	{
		[Option('a', "action", DefaultValue=ArgAction.None)]
		public ArgAction Action { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			this.Action = ArgAction.Help;

			var help = new HelpText();
			help.AddOptions(this);
			return help;
		}
	}
}
