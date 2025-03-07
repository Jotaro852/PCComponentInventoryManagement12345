using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Data.Repositories;
using PCComponentInventoryManagement.Models;
using System.Threading.Tasks;

namespace PCComponentInventoryManagement
{
    public partial class ViewConfigurationsWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly ConfigurationRepository _repository;

        public ViewConfigurationsWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            _repository = new ConfigurationRepository(_context);
            _ = LoadConfigurationsAsync();
        }

        private async Task LoadConfigurationsAsync()
        {
            var configurations = await _repository.GetAllConfigurationsAsync();
            ConfigurationsGrid.ItemsSource = configurations;
        }

        private void ConfigurationsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ConfigurationsGrid.SelectedItem is PCConfiguration selectedConfig)
            {
                var detailsWindow = new ConfigurationDetailsWindow(selectedConfig);
                detailsWindow.ShowDialog();
            }
        }
    }
}