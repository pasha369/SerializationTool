using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SerializationTool.ViewModels.Abstract
{
    /// <summary>
    /// Represents observable object.
    /// </summary>
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
