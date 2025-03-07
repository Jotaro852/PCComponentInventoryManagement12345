using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement.Data.Repositories
{
    public class ComponentRepository
    {
        private readonly AppDbContext _context;

        public ComponentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddComponentAsync(Component component)
        {
            await _context.Components.AddAsync(component);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Component>> GetAllComponentsAsync()
        {
            return await _context.Components.ToListAsync();
        }
        public async Task<List<PCConfiguration>> GetAllConfigurationsAsync()
        {
            return await _context.PCConfigurations
                .Include(c => c.Components)
                .ThenInclude(cc => cc.Component)
                .ToListAsync();
        }
    }
}