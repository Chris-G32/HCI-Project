//DONE WITH RESTRUCTURE AND COMMENT
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HCI_Project.Core
{
    /// <summary>
    /// Inherit from this to implement <see cref="System.ComponentModel.INotifyPropertyChanged"/> on a class.
    /// NOTE: Classes that need to inherit from more than one class need to implement the interface seperately
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
