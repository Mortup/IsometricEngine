namespace com.gStudios.isometric.model.world.commands {

    public interface IWorldCommand {

        /// <summary>
		/// Excecutes the current command action.
		/// Returns the reversed command.
		/// </summary>
        IWorldCommand Excecute();

    }
}