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
    /// Interaction logic for dynamicBindingsSubCategory.xaml
    /// </summary>
    public partial class dynamicBindingsSubCategory : UserControl
    {
        public string Subcategory { get; set; }

        public dynamicCategory? CategoryParent { get; set; } = null;

        public bool selected { get; set; }

        public List<MyBinds> Binds { get; set; } = new();

        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        SolidColorBrush StrokeDefault = new SolidColorBrush();

        public dynamicBindingsSubCategory(string subcategory)
        {
            Subcategory = subcategory;

            InitializeComponent();

            DataContext = this;

            CloseHoverFill.Color = Color.FromArgb(127, 0, 0, 0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
            StrokeDefault.Color = Color.FromArgb(255, 58, 113, 135);
        }

        public void AddBind(MyBinds bind)
        {
            Binds.Add(bind);
        }

        private void eventBindSubCategoryClick(object sender, MouseButtonEventArgs e)
        {
            CategoryParent?.subcategorySelect(this);
        }

        private void eventBindSubCategoryEnter(object sender, MouseEventArgs e)
        {
            uiBindingSubCategoryRectangle.Fill = CloseHoverFill;
            uiBindingSubCategoryRectangle.Stroke = CloseFontHover;
            uiBindingSubCategoryLabel.Foreground = CloseFontHover;
        }

        private void eventBindSubCategoryLeave(object sender, MouseEventArgs e)
        {
            if (!selected)
            {
                uiBindingSubCategoryRectangle.Fill = CloseDefaultFill;
                uiBindingSubCategoryRectangle.Stroke = StrokeDefault;
                uiBindingSubCategoryLabel.Foreground = CloseFontDefault;
            }
        }

        private void populateBindings()
        {
            dynamicBindings.Instance?.AddBinds(Binds);
        }

        public void setSelected(bool selected)
        {
            if (!selected)
            {
                uiBindingSubCategoryRectangle.Fill = CloseDefaultFill;
                uiBindingSubCategoryRectangle.Stroke = StrokeDefault;
                uiBindingSubCategoryLabel.Foreground = CloseFontDefault;
                this.selected = false;

            }
            else
            {
                uiBindingSubCategoryRectangle.Fill = CloseHoverFill;
                uiBindingSubCategoryRectangle.Stroke = CloseFontHover;
                uiBindingSubCategoryLabel.Foreground = CloseFontHover;
                this.selected = true;

                // Add binds to binding screen when selected
                populateBindings();
            }
        }
    }
}
