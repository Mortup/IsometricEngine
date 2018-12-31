using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class NullCommand : CursorCommand {

		public static readonly NullCommand instance = new NullCommand ();

		private NullCommand() : base(null, -1, -1, -1) {
		}

		public override CursorCommand Excecute ()
		{
			return new NullCommand();
		}
		
	}

}