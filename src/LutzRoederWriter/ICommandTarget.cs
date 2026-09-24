using System;

namespace Writer
{
	/// <summary>
	/// Handles command execution and supplies current command state.
	/// </summary>
	public interface ICommandTarget
	{
		/// <summary>
		/// Attempts to execute a command.
		/// </summary>
		/// <param name="commandState">The command and its active control state.</param>
		/// <returns><see langword="true"/> when the command was handled.</returns>
		bool Execute(CommandState commandState);

		/// <summary>
		/// Attempts to update the state of a command.
		/// </summary>
		/// <param name="commandState">The command state to update.</param>
		/// <returns><see langword="true"/> when the target supplied the state.</returns>
		bool QueryStatus(CommandState commandState);
	}
}
