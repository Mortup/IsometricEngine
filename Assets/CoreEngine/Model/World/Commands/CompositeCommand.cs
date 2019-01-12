using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class CompositeCommand : AbstractWorldCommand {

		List<IWorldCommand> commands;

		public CompositeCommand(Level level, List<IWorldCommand> commands) : base(level, -1, -1, -1) {
			this.commands = commands;
		}

		public override IWorldCommand Excecute ()
		{
			List<IWorldCommand> inverseCommands = new List<IWorldCommand> ();

			foreach (IWorldCommand cmd  in commands) {
                IWorldCommand inverseCmd = cmd.Excecute ();
				if (inverseCmd != NullCommand.instance)
					inverseCommands.Add(inverseCmd);
			}

			if (inverseCommands.Count == 0)
				return NullCommand.instance;

			return new CompositeCommand (level, inverseCommands);
		}
		
	}

}