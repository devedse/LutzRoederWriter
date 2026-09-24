using System;
using System.ComponentModel;

namespace Writer
{
	internal interface IConfiguration
	{
		event PropertyChangedEventHandler PropertyChanged;
		string this[string name] { get; set; }
		void Clear();
	}
}
