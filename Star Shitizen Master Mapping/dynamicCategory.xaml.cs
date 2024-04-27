using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for dynamicCategory.xaml
    /// </summary>
    public partial class dynamicCategory : UserControl
    {
        public string Category { get; set; }
        public bool selected { get; set; }  
        public List<dynamicBindingsSubCategory> Subcategories { get; set; } = new();
        private dynamicBindingsSubCategory? selectedSubcategory = null;

        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        SolidColorBrush StrokeDefault = new SolidColorBrush();

        public dynamicCategory(string category)
        {
            Category = category;

            InitializeComponent();
            DataContext = this;

            CloseHoverFill.Color = Color.FromArgb(127, 0, 0, 0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
            StrokeDefault.Color = Color.FromArgb(255, 58, 113, 135);
        }

        // Add at creation time (in MainWindow)
        public void AddSubcategory(dynamicBindingsSubCategory subCategory)
        {
            subCategory.CategoryParent = this;
            Subcategories.Add(subCategory);
        }

        private void eventBindingCategoryClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.categoryPage(true, this);
            MainWindow.Instance.categorySelect(this);

            if (selectedSubcategory == null)
            {
                selectedSubcategory = Subcategories.FirstOrDefault();
                subcategorySelect(selectedSubcategory);
            }
        }

        private void eventBindingCategoryEnter(object sender, MouseEventArgs e)
        {
            uiBindingCategoryRRect.Fill = CloseHoverFill;
            uiBindingCategoryRRect.Stroke = CloseFontHover;
            uiBindingCategoryLabel.Foreground = CloseFontHover;
        }

        private void eventBindingCategoryLeave(object sender, MouseEventArgs e)
        {
            if (!selected)
            {
                uiBindingCategoryRRect.Fill = CloseDefaultFill;
                uiBindingCategoryRRect.Stroke = StrokeDefault;
                uiBindingCategoryLabel.Foreground = CloseFontDefault;
            }

        }

        public void setSelected(bool selected)
        {
            if (!selected)
            {
                uiBindingCategoryRRect.Fill = CloseDefaultFill;
                uiBindingCategoryRRect.Stroke = StrokeDefault;
                uiBindingCategoryLabel.Foreground = CloseFontDefault;
                this.selected = false;

            }
            else
            {
                uiBindingCategoryRRect.Fill = CloseHoverFill;
                uiBindingCategoryRRect.Stroke = CloseFontHover;
                uiBindingCategoryLabel.Foreground = CloseFontHover;
                this.selected = true;

            }
        }

        public void subcategorySelect(dynamicBindingsSubCategory? subCategory)
        {
            foreach (var i in Subcategories)
            {
                if (i == null)
                    continue;

                if (i == subCategory)
                {
                    i.setSelected(true);
                }
                else
                {
                    i.setSelected(false);
                }
            }
        }
    }
}
