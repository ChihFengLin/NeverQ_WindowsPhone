using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverQ_01
{
    class BackgroundLists : ObservableCollection<BackgroundList>
    {
        public BackgroundLists()
        {
            Add(new BackgroundList("Assets/bg.png"));
        }
        
    }
}
