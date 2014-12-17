using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeverQ_01
{
    public class RestaurantList
    {
        private string p1;
        private string p2;

        public String Name { get; set; }
        public String Address { get; set; }
        public String Telephone { get; set; }
        public String ImageSource { get; set; }
        public String Date { get; set; }
        public String CurrentNo { get; set; }
        public String ExpectTime { get; set; }

        public RestaurantList(String name, String address, String telephone, String image, String date, String currentno, String expecttime)
        {
            this.Name = name;
            this.Address = address;
            this.Telephone = telephone;
            this.ImageSource = image;
            this.Date = date;
            this.CurrentNo = currentno;
            this.ExpectTime = expecttime;

        }
      
    }
    
    
}