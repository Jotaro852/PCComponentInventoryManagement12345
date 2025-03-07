namespace PCComponentInventoryManagement.Models
{
    public class ConfigurationComponent
    {
        public int Id { get; set; }
        public int PCConfigurationId { get; set; }
        public PCConfiguration Configuration { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public int QuantityRequired { get; set; }
    }
}