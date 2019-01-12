namespace com.gStudios.isometric.model.world.commands {

    /// <summary>
    /// Base for most world commands that use a pair of coordinates and an index.
    /// </summary>
	public abstract class AbstractWorldCommand : IWorldCommand{

		protected Level level;
		protected int posX, posY;
		protected int index;

		public AbstractWorldCommand(Level level, int posX, int posY, int index) {
			this.level = level;
			this.posX = posX;
			this.posY = posY;
			this.index = index;
		}

		public abstract IWorldCommand Excecute ();

	}

}