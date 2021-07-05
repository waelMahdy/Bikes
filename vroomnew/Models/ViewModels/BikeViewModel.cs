using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Models.ViewModels
{
    public class BikeViewModel
    {
        public Bike Bike { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public List<currency> Currencies { get; set; }

        private List<currency> Clist = new List<currency>();
      
      
        public BikeViewModel()
        {
            Currencies=CreateList();
        }
        public List<currency> CreateList()
        {
            Clist.Add(new currency("USD", "USD"));
            Clist.Add(new currency("INR", "INR"));
            Clist.Add(new currency("EUR", "EUR"));
            return Clist;
        }

    }
    public class currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public currency(string id,string name)
        {
            Id = id;
            Name = name;


        }
    }
}
