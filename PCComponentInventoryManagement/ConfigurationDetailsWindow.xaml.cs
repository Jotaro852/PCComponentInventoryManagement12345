using System.Windows;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement
{
    public partial class ConfigurationDetailsWindow : Window
    {
        public PCConfiguration Configuration { get; set; }

        public ConfigurationDetailsWindow(PCConfiguration configuration)
        {
            InitializeComponent();
            Configuration = configuration;
            DataContext = Configuration;
        }
    }
}