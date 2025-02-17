using FullstackLabb3.Models;
using FullstackLabb3.Data;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3.Data
{

    public class PortfolioService
    {
        private readonly PortfolioDbContext _db;


        public PortfolioService(PortfolioDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddPortfolio(Portfolio portfolio)
        {
            await _db.Portfolios.AddAsync(portfolio);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Portfolio>> GetPortfolio()
        {
            return await _db.Portfolios.ToListAsync();
        }

        public async Task<Portfolio> UpdatePortfolio(int id, Portfolio updatedPortfolio)
        {
            var portfolio = await _db.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            if (portfolio == null) return null;
            portfolio.Technology = updatedPortfolio.Technology;
            portfolio.Yearsofexperience = updatedPortfolio.Yearsofexperience;
            portfolio.Skills = updatedPortfolio.Skills;
            await _db.SaveChangesAsync();
            return portfolio;

        }

        public async Task<Portfolio> DeletePortfolio(int id)
        {
            var deletePortfolio = await _db.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            _db.Portfolios.Remove(deletePortfolio);
            await _db.SaveChangesAsync();
            return deletePortfolio;
        }
    }
}
