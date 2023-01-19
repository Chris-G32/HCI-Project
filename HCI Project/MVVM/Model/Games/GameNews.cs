//DONE WITH RESTRUCTURE AND COMMENT
using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.Model.Games
{
    /// <summary>
    /// Class representing the news related to a game. Simply holds daya, no methods.
    /// </summary>
    public class GameNews:ObservableObject
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
    }
}
