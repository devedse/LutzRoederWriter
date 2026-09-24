using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Writer.Forms
{
	public class CommandBarTypeConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			object obj;
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = typeof(CommandBar).GetConstructor(Type.EmptyTypes);
				obj = new InstanceDescriptor(constructor, null, false);
			}
			else if (destinationType == typeof(string))
			{
				CommandBar commandBar = (CommandBar)value;
				obj = ((commandBar != null) ? commandBar.Style.ToString() : string.Empty);
			}
			else
			{
				obj = base.ConvertTo(context, culture, value, destinationType);
			}
			return obj;
		}
	}
}
