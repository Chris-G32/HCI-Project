using HCI_Project.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCI_Project.MVVM.Model.Games
{
    public class GameAchievement:ObservableObject
    {
        public string Name { get; set; }
        public Uri Icon { get; set; }

        public int TimeAchieved;
    }
}
