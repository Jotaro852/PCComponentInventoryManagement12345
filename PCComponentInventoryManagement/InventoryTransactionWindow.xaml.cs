using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Data.Repositories;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement
{
    public partial class InventoryTransactionWindow : Window
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly AppDbContext _context;

        public InventoryTransactionWindow(InventoryRepository repository, AppDbContext context)
        {
            InitializeComponent();
            _inventoryRepository = repository;
            _context = context;
            LoadComponentsAsync();
        }

        private async Task LoadComponentsAsync()
        {
            var components = await _context.Components.ToListAsync();
            ComponentComboBox.ItemsSource = components;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypeComboBox.SelectedItem is not ComboBoxItem selectedType || 
                ComponentComboBox.SelectedItem is not Component selectedComponent || 
                !int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Проверьте введенные данные!");
                return;
            }

            var transaction = new InventoryTransaction
            {
                Type = selectedType.Content.ToString(),
                ComponentId = selectedComponent.Id,
                Quantity = quantity
            };

            // Обновление остатка
            selectedComponent.Quantity += transaction.Type == "Поступление" ? quantity : -quantity;
            _context.Components.Update(selectedComponent);

            await _inventoryRepository.AddTransactionAsync(transaction);
            Close();
        }
    }
}