using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Data.Repositories;
using PCComponentInventoryManagement.Models;
using System.Threading.Tasks;

namespace PCComponentInventoryManagement
{
    public partial class ConfigurationsManagementWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly ConfigurationRepository _repository;

        public ConfigurationsManagementWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            _repository = new ConfigurationRepository(_context);
            _ = LoadConfigurationsAsync(); // Запуск асинхронной загрузки
        }

        // Загрузка конфигураций (возвращает Task)
        private async Task LoadConfigurationsAsync()
        {
            var configurations = await _repository.GetAllConfigurationsAsync();
            ConfigurationsGrid.ItemsSource = configurations;
        }

        // Добавление конфигурации
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new ConfigurationWindow();
            if (configWindow.ShowDialog() == true)
            {
                await _repository.AddConfigurationAsync(configWindow.Configuration);
                await LoadConfigurationsAsync();
            }
        }

        // Удаление конфигурации
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigurationsGrid.SelectedItem is PCConfiguration selectedConfig)
            {
                await _repository.DeleteConfigurationAsync(selectedConfig.Id);
                await LoadConfigurationsAsync();
            }
        }

        // Редактирование конфигурации (по двойному клику)
        private async void ConfigurationsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ConfigurationsGrid.SelectedItem is PCConfiguration selectedConfig)
            {
                var configWindow = new ConfigurationWindow(selectedConfig);
                if (configWindow.ShowDialog() == true)
                {
                    await _context.SaveChangesAsync();
                    await LoadConfigurationsAsync();
                }
            }
        }
    }
}