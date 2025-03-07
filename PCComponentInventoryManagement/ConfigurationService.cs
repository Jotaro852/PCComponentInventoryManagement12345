using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Data.Repositories;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement.Services
{
    public class ConfigurationService
    {
        private readonly ConfigurationRepository _repository;
        private readonly AppDbContext _context;

        public ConfigurationService(ConfigurationRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public (bool IsValid, List<string> Errors) ValidateConfiguration(PCConfiguration config)
        {
            var errors = new List<string>();
            
            foreach (var component in config.Components)
            {
                var dbComponent = _context.Components.Find(component.ComponentId);
                if (dbComponent == null)
                {
                    errors.Add($"Компонент ID {component.ComponentId} не найден");
                    continue;
                }

                if (component.QuantityRequired <= 0)
                    errors.Add($"Некорректное количество для {dbComponent.Name}");
            }

            return (errors.Count == 0, errors);
        }
    }
}