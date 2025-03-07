using System.Windows;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PCComponentInventoryManagement
{
    public partial class ConfigurationWindow : Window
    {
        public PCConfiguration Configuration { get; set; }
        public ObservableCollection<Component> AllComponents { get; set; }

        private readonly AppDbContext _context;

        public ConfigurationWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            Configuration = new PCConfiguration();
            LoadComponents();
            DataContext = this;
        }

        public ConfigurationWindow(PCConfiguration configuration)
        {
            InitializeComponent();
            _context = new AppDbContext();
            Configuration = configuration;
            LoadComponents();
            DataContext = this;
        }

        private async void LoadComponents()
        {
            AllComponents = new ObservableCollection<Component>(await _context.Components.ToListAsync());
            ComponentsGrid.ItemsSource = Configuration.Components;
        }

        private void AddComponentButton_Click(object sender, RoutedEventArgs e)
        {
            var newComponent = new ConfigurationComponent
            {
                ComponentId = AllComponents.FirstOrDefault()?.Id ?? 0, // Выбор первого компонента по умолчанию
                QuantityRequired = 1
            };
            Configuration.Components.Add(newComponent);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Configuration.Name))
            {
                MessageBox.Show("Введите название конфигурации!");
                return;
            }

            if (Configuration.Components.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один компонент!");
                return;
            }

            // Сохранение конфигурации и её компонентов
            if (Configuration.Id == 0) // Новая конфигурация
            {
                _context.PCConfigurations.Add(Configuration);
            }
            else // Редактирование существующей
            {
                _context.PCConfigurations.Update(Configuration);
            }

            await _context.SaveChangesAsync();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}