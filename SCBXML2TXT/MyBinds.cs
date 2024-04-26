using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBXML2TXT
{
    internal class MyBinds
    {
        public string Category;
        public string DisplayCategory;
        public string Name;
        public string Subcategory;
        public string DisplayName;

        public MyBinds(string category, string name, string displaycategory, string subcatergory, string displaymame) 
        {
            Category = category;
            Name = name;
            DisplayCategory = displaycategory;
            Subcategory = subcatergory;
            DisplayName = displaymame;
        }

        public override string ToString()
        {
            return $"        new MyBinds(\"{Category}\", \"{Name}\", \"{DisplayCategory}\", \"{Subcategory}\", \"{DisplayName}\"),\n";
        }
    }
}
