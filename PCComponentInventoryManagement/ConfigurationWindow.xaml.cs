using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement
{
    public partial class ConfigurationWindow : Window
    {
        private readonly AppDbContext _context;
        public PCConfiguration Configuration { get; set; }
        public ObservableCollection<Component> AllComponents { get; set; }

        public ConfigurationWindow(PCConfiguration? config = null)
        {
            InitializeComponent();
            _context = new AppDbContext();
            Configuration = config ?? new PCConfiguration();
            
            // Инициализация списка компонентов
            if (Configuration.Components == null)
                Configuration.Components = new ObservableCollection<ConfigurationComponent>();
            else if (Configuration.Components.GetType() == typeof(List<ConfigurationComponent>))
                Configuration.Components = new ObservableCollection<ConfigurationComponent>(new List<ConfigurationComponent>(Configuration.Components));
            
            LoadData();
            DataContext = this;
        }

        private async void LoadData()
        {
            // Загрузка всех компонентов из базы данных
            AllComponents = new ObservableCollection<Component>(await _context.Components.ToListAsync());
            ComponentsGrid.ItemsSource = Configuration.Components;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка названия
                if (string.IsNullOrEmpty(Configuration.Name))
                {
                    MessageBox.Show("Введите название конфигурации!");
                    return;
                }

                // Сохранение новой или обновление существующей конфигурации
                if (Configuration.Id == 0)
                    await _context.PCConfigurations.AddAsync(Configuration);
                else
                    _context.PCConfigurations.Update(Configuration);

                // Сохранение компонентов
                foreach (var component in Configuration.Components)
                {
                    if (component.Id == 0)
                        await _context.ConfigurationComponents.AddAsync(component);
                    else
                        _context.ConfigurationComponents.Update(component);
                }

                await _context.SaveChangesAsync();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddComponentButton_Click(object sender, RoutedEventArgs e)
        {
            var addComponentWindow = new AddComponentWindow();
            if (addComponentWindow.ShowDialog() == true)
            {
                // Добавление нового компонента в список
                AllComponents.Add(addComponentWindow.Component);
                
                // Обновление DataGrid
                ComponentsGrid.Items.Refresh();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}