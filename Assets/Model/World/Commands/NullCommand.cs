using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.commands {

	public class NullCommand : CursorCommand {

		public static readonly NullCommand instance = new NullCommand ();

		public NullCommand() : base(null, -1, -1, -1) {
		}

		public override CursorCommand Excecute ()
		{
			return new NullCommand();
		}
		
	}

}