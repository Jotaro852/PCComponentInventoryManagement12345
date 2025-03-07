using System.Collections.ObjectModel;

namespace PCComponentInventoryManagement.Models
{
    public class PCConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ConfigurationComponent> Components { get; set; } = new();
    }
}