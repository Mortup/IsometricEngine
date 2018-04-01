using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class CompositeCommand : CursorCommand {

		List<CursorCommand> commands;

		public CompositeCommand(Level level, List<CursorCommand> commands) : base(level, -1, -1, -1) {
			this.commands = commands;
		}

		public override CursorCommand Excecute ()
		{
			List<CursorCommand> inverseCommands = new List<CursorCommand> ();

			foreach ( CursorCommand cmd  in commands) {
				CursorCommand inverseCmd = cmd.Excecute ();
				if (inverseCmd != NullCommand.instance)
					inverseCommands.Add(inverseCmd);
			}

			if (inverseCommands.Count == 0)
				return NullCommand.instance;

			return new CompositeCommand (level, inverseCommands);
		}
		
	}

}