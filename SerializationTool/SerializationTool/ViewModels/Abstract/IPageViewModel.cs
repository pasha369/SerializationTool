namespace SerializationTool.ViewModels.Abstract
{
    /// <summary>
    /// Represents page view model interface.
    /// </summary>
    public interface IPageViewModel
    {
        /// <summary>
        /// Page name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets icon path.
        /// </summary>
        string IconPath { get;  }
    }
}
