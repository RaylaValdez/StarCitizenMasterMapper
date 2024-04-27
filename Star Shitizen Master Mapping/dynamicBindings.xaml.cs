using SCBXML2TXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for dynamicBindings.xaml
    /// </summary>
    public partial class dynamicBindings : UserControl
    {
        public List<dynamicBindingsSubCategory> subCategories = new();

        public static dynamicBindings? Instance = null;

        public dynamicBindings()
        {
            Instance = this;

            InitializeComponent();
        }

        public void Update(dynamicCategory dynamicCategory)
        {
            // Clear bindings >:D send the kids to the orphanage!

            uiCatergoryBindingsStack.Children.Clear();

            // Clear subcategories
            uiSubCatergoryBindingsWrapPanel.Children.Clear();
            subCategories.Clear();
            foreach (var subcategory in dynamicCategory.Subcategories)
            {
                subCategories.Add(subcategory);
                uiSubCatergoryBindingsWrapPanel.Children.Add(subcategory);
            }
        }

        public void AddBinds(List<MyBinds> binds)
        {
            // Clear bindings >:D send the kids to the orphanage!
            uiCatergoryBindingsStack.Children.Clear();
            foreach(MyBinds bind in binds)
            {
                uiCatergoryBindingsStack.Children.Add(new dynamicBind(bind));
            }
        }
    }
}
