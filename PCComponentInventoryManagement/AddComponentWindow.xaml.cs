using System.Windows;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement
{
    public partial class AddComponentWindow : Window
    {
        public Component Component { get; set; }

        public AddComponentWindow()
        {
            InitializeComponent();
            Component = new Component();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PriceTextBox.Text, out decimal price) &&
                int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                // Сохранение данных
                Component.Name = NameTextBox.Text;
                Component.Type = TypeTextBox.Text;
                Component.Manufacturer = ManufacturerTextBox.Text;
                Component.Model = ModelTextBox.Text;
                Component.Price = price;
                Component.Quantity = quantity;

                // Добавление в БД
                using (var context = new AppDbContext())
                {
                    context.Components.Add(Component);
                    context.SaveChanges();
                }

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Проверьте введенные данные!");
            }
        }
    }
}