namespace com.gStudios.isometric.model.world.commands {

	public class NullCommand : IWorldCommand {

		public static readonly NullCommand instance = new NullCommand ();

		public IWorldCommand Excecute ()
		{
			return new NullCommand();
		}
		
	}

}