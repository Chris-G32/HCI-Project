using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.Model.Games
{
    public class GameNews:ObservableObject
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
    }
}
