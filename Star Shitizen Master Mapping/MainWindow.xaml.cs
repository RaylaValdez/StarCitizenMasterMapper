using Microsoft.Win32;
using SharpDX.DirectInput;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SCBXML2TXT;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Define MainWindow Instance
        public static MainWindow Instance { get; private set; }

        // Define Brushs
        SolidColorBrush TglHoverCol = new SolidColorBrush();
        SolidColorBrush TglDefaultCol = new SolidColorBrush();
        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        SolidColorBrush StrokeDefault = new SolidColorBrush();

        // Animations
        ThicknessAnimation toggleAnimationOff = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(0.05)));
        ThicknessAnimation toggleAnimationOn = new ThicknessAnimation(new Thickness(20, 0, 0, 0), new Duration(TimeSpan.FromSeconds(0.05)));

        // Variables
        enum uiElement { Devices, Binding, Visual, Toggle, vJoy }
        static uiElement Selected = 0;
        static bool isCorrupt = false;
        static bool vJoyOverride = false;
        public static bool vJoySelected = false;

        // Dynamic Device List
        List<dynamicDevices> directInputDevices = new List<dynamicDevices>();
        List<dynamicCategory> bindingCategories = new List<dynamicCategory>();

        // Define devicesConfig.ini
        public static IniFile devicesConfig = new IniFile("devicesConfig.ini");

        public dynamicCategory ?selectedCategory;

        public MainWindow()
        {
            var directInput = new DirectInput();

            foreach (var deviceInstance in directInput.GetDevices().Where(x => IsWanted(x)))
            {
                if (!deviceInstance.ProductName.Contains("vJoy Device"))
                {
                    directInputDevices.Add(new dynamicDevices() { deviceName = deviceInstance.ProductName, deviceID = deviceInstance.ProductGuid.ToString() });
                    if (!devicesConfig.KeyExists("Physical_ID", deviceInstance.ProductName))
                    {
                        devicesConfig.Write("Physical_ID", deviceInstance.ProductGuid.ToString(), deviceInstance.ProductName);
                        devicesConfig.WriteText("");
                        if (devicesConfig.KeyExists("Physical_ID", deviceInstance.ProductName))
                        {
                            devicesConfig.Write("X_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("X_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("X_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("X_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("X_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("X_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("Y_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("Z_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("RX_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("RY_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("RZ_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("Slider_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_Saturation", "1", deviceInstance.ProductName);

                            devicesConfig.Write("Dial_Axis_Mode", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_Curve", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_DZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_LDZ", "0", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_Saturation", "1", deviceInstance.ProductName);


                            devicesConfig.WriteText("");

                        }
                    }

                }
            }


            Instance = this;

            TglDefaultCol.Color = Color.FromArgb(255, 147, 188, 199);
            CloseHoverFill.Color = Color.FromArgb(127, 0, 0, 0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
            StrokeDefault.Color = Color.FromArgb(255, 58, 113, 135);
            TglHoverCol.Color = Colors.Blue;
            Selected = uiElement.Devices;
            vJoySelected = false;
            vJoyOverride = true;

            InitializeComponent();

            uiVjoySettingsGrid.IsEnabled = false;
            uiVjoySettingsGrid.Visibility = Visibility.Hidden;
            uiBindingPageScroll.Visibility = Visibility.Hidden;
            dynamicDeviceConfig.Visibility = Visibility.Hidden;
            uiBindingsForCategory.Visibility = Visibility.Hidden;


            if (IsSoftwareInstalled("vjoy"))
            {
                uiVjoySettingsInstalledLabel.Content = "vJoy is detected on your system.\r\nYou're good to go.";
                uiVjoySettingsDownloadRRect.IsEnabled = false;
                uiVjoySettingsDownloadRRect.Visibility = Visibility.Hidden;
                uiVjoySettingsDownloadLabel.IsEnabled = false;
                uiVjoySettingsDownloadLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                uiVjoySettingsInstalledLabel.Content = "vJoy isn't present on your system.\r\nThe button below will open a browser window.";
                uiVjoySettingsDownloadRRect.IsEnabled = true;
                uiVjoySettingsDownloadRRect.Visibility = Visibility.Visible;
                uiVjoySettingsDownloadLabel.IsEnabled = true;
                uiVjoySettingsDownloadLabel.Visibility = Visibility.Visible;
            }




            switch (Selected)
            {
                case uiElement.Devices:
                {
                    // Highlight Devices
                    uiDevicesRoundRect.Fill = CloseHoverFill;
                    uiDevicesRoundRect.Stroke = CloseFontHover;
                    uiDevicesLabel.Foreground = CloseFontHover;
                    // Default Everything Else
                    defaultColor(uiElement.Binding);
                    defaultColor(uiElement.Visual);
                    break;
                }
                case uiElement.Binding:
                {
                    // Highlight Binding
                    uiBindingRoundRect.Fill = CloseHoverFill;
                    uiBindingRoundRect.Stroke = CloseFontHover;
                    uiBindingLabel.Foreground = CloseFontHover;
                    // Defualt everything else
                    defaultColor(uiElement.Devices);
                    defaultColor(uiElement.Visual);
                    break;
                }
                case uiElement.Visual:
                {
                    // Highlight Visual
                    uiVisualRoundRect.Fill = CloseHoverFill;
                    uiVisualRoundRect.Stroke = CloseFontHover;
                    uiVisualLabel.Foreground = CloseFontHover;
                    // Default everything else
                    defaultColor(uiElement.Devices);
                    defaultColor(uiElement.Binding);
                    break;
                }
            }

            foreach (dynamicDevices i in directInputDevices)
            {
                uiDeviceStackPanel.Children.Add(i);
            }

            // i guess right here fill the binding categories from my binds
            // stack panel - uiBindingStackPanel

            // Category (index into CategoryOrder) : Subcategory
            SortedDictionary<string, SortedList<string, string>> sortedCategories = new(new CategorySorter());

            // Loop through each MyBinds object in the BindingsList.Bindings list
            foreach (MyBinds binding in BindingsList.Bindings)
            {
                // Check if the DisplayCategory of the binding does not contain "PH_"
                if (!binding.DisplayCategory.Contains("PH_"))
                {
                    // Check if the sortedCategories dictionary does not already contain the DisplayCategory
                    if (!sortedCategories.ContainsKey(binding.DisplayCategory))
                    {
                        // Add a new SortedList to the sortedCategories dictionary with the DisplayCategory as the key and a new subcategory sorter that checks each subcategory based on the main category
                        sortedCategories.Add(binding.DisplayCategory, new SortedList<string, string>(new SubcategorySorter(binding.DisplayCategory)));
                    }

                    // Check if the Subcategory of the binding is not already present in the SortedList for the DisplayCategory - SortedList only allows one key
                    if (!sortedCategories[binding.DisplayCategory].ContainsKey(binding.Subcategory))
                    {
                        // Add the Subcategory to the SortedList for the DisplayCategory
                        sortedCategories[binding.DisplayCategory].Add(binding.Subcategory, binding.Subcategory);
                    }
                }
            }

            foreach (var categoryPair in sortedCategories)
            {
                string category = categoryPair.Key;
                var subcategories = categoryPair.Value;

                // Create category and add it to stuff
                dynamicCategory uiCategory = new(category);
                bindingCategories.Add(uiCategory);
                uiBindingStackPanel.Children.Add(uiCategory);

                foreach (var subcategoryPair in subcategories)
                {
                    string subcategory = subcategoryPair.Key;

                    // Create subcategory
                    dynamicBindingsSubCategory uiSubcategory = new dynamicBindingsSubCategory(subcategory);
                    uiCategory.AddSubcategory(uiSubcategory);

                    // Add each binding which matches this category and subcategory
                    foreach (MyBinds binding in BindingsList.Bindings)
                    {
                        if (binding.DisplayCategory == category && binding.Subcategory == subcategory)
                        {
                            uiSubcategory.AddBind(binding);
                        }
                    }
                }
            }
        }


    }

    // Sorts sorted containers by category order (defined in BindingsList)
    // If the category doesn't exist in the CategoryOrder, then it moves to the end
    class CategorySorter : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            // Big number to put at the end of the list
            int indexX = 10000;
            int indexY = 10000;
            if (x != null)
            {
                indexX = BindingsList.CategoryOrder.Keys.Contains(x) ? BindingsList.CategoryOrder.Keys.ToList().IndexOf(x) : indexX;
            }
            if (y != null)
            {
                indexY = BindingsList.CategoryOrder.Keys.Contains(y) ? BindingsList.CategoryOrder.Keys.ToList().IndexOf(y) : indexY;
            }
            return indexX.CompareTo(indexY);
        }
    }

    // Sorts sorted containers by a category's subcategory order (defined in BindingsList)
    // If the subcategory doesn't exist in the CategoryOrder[DisplayCategory], then it moves to the end
    class SubcategorySorter : IComparer<string>
    {
        public string DisplayCategory;
        public SubcategorySorter(string displayCategory)
        {
            DisplayCategory = displayCategory;
        }

        public int Compare(string? x, string? y)
        {
            // If category does not exist in CategoryOrder then x & y are equal
            if (!BindingsList.CategoryOrder.ContainsKey(DisplayCategory))
            {
                return 0;
            }

            int indexX = 10000;
            int indexY = 10000;
            if (x != null)
            {
                indexX = BindingsList.CategoryOrder[DisplayCategory].Contains(x) ? BindingsList.CategoryOrder.Keys.ToList().IndexOf(x) : indexX;
            }
            if (y != null)
            {
                indexY = BindingsList.CategoryOrder[DisplayCategory].Contains(y) ? BindingsList.CategoryOrder.Keys.ToList().IndexOf(y) : indexY;
            }
            return indexX.CompareTo(indexY);
        }
    }
}