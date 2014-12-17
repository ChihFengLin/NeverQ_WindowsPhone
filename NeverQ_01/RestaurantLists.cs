using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NeverQ_01
{
    public class RestaurantLists : ObservableCollection<RestaurantList>
    {
        public RestaurantLists()
        {
            Add(new RestaurantList("鼎泰豐", "台北市信義區信義路二段194號", "02-23218928", "Assets/dintaifeng.jpeg", "2013. 5. 28", "23", "12"));
            Add(new RestaurantList("Miacucina", "台北市士林區德行西路48號1樓", "02-88662658", "Assets/DSCN8737.JPG", "2014. 2. 20", "44", "20"));
            Add(new RestaurantList("杏子豬排", "台北市大安區復興南路二段271巷2號", "02-27010298", "Assets/normal_20070718_193208.JPG", "2013. 12. 25", "13", "12"));
            Add(new RestaurantList("瓦城-站前店", "台北市中正區忠孝西路一段66號12樓", "02-23111125", "Assets/P7131308.JPG", "2014. 2. 1", "25", "33"));
            Add(new RestaurantList("Dazzling Cafe", "台北市信義區忠孝東路四段205巷7弄11號", "02-87739238", "Assets/dazzling.jpg", "2013. 8. 31", "23", "10"));
        }
    }

}


