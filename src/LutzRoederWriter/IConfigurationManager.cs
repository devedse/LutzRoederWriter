using System;

namespace Writer
{
	internal interface IConfigurationManager
	{
		IConfiguration this[string name] { get; }
	}
}
