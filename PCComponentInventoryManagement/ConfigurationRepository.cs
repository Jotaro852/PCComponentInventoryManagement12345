using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement.Data.Repositories
{
    public class ConfigurationRepository
    {
        private readonly AppDbContext _context;

        public ConfigurationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddConfigurationAsync(PCConfiguration config)
        {
            await _context.PCConfigurations.AddAsync(config);
            await _context.SaveChangesAsync();
        }

        // Добавленный метод удаления
        public async Task DeleteConfigurationAsync(int id)
        {
            var config = await _context.PCConfigurations
                .Include(c => c.Components)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            if (config != null)
            {
                _context.PCConfigurations.Remove(config);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PCConfiguration>> GetAllConfigurationsAsync()
        {
            return await _context.PCConfigurations
                .Include(c => c.Components)          // Загружаем компоненты конфигурации
                .ThenInclude(cc => cc.Component)     // Загружаем связанные компоненты (Component)
                .ToListAsync();
        }
    }
}