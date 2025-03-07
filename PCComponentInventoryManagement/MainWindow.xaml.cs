using System.Windows;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Data.Repositories;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly ComponentRepository _componentRepository;

        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            _componentRepository = new ComponentRepository(_context);
            LoadComponents();
        }

        // Загрузка компонентов
        private async void LoadComponents()
        {
            var components = await _componentRepository.GetAllComponentsAsync();
            ComponentsDataGrid.ItemsSource = components;
        }

        // Обработчики событий
        private void AddComponentButton_Click(object sender, RoutedEventArgs e)
        {
            var addComponentWindow = new AddComponentWindow();
            addComponentWindow.ShowDialog();
            LoadComponents();
        }

        private void InventoryTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            var inventoryRepository = new InventoryRepository(_context);
            var transactionWindow = new InventoryTransactionWindow(inventoryRepository, _context);
            transactionWindow.ShowDialog();
            LoadComponents();
        }

        // Управление конфигурациями
        private void ManageConfigurationsButton_Click(object sender, RoutedEventArgs e)
        {
            var configManagementWindow = new ConfigurationsManagementWindow(_context);
            configManagementWindow.ShowDialog();
        }

        // Просмотр конфигураций
        private void ViewConfigurationsButton_Click(object sender, RoutedEventArgs e)
        {
            var viewConfigurationsWindow = new ConfigurationsManagementWindow(_context);
            viewConfigurationsWindow.ShowDialog();
        }
    }
}