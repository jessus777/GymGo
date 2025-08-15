using CsvHelper;
using GymGo.Application.Contracts.Infrastructure;
using GymGo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace GymGo.Infrastructure.DataSeeding
{
    public class CsvDataSeeder
        : ICsvDataSeeder
    {
        private readonly IFileLoggerService _fileLoggerService;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public CsvDataSeeder(IFileLoggerService fileLoggerService,  IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _fileLoggerService = fileLoggerService;
            _dbContextFactory = dbContextFactory;
        }

        public async Task SeedAsync<T>(string relativeCsvPath) where T : class
        {
            using var _applicationDbContext = _dbContextFactory.CreateDbContext();

            var dbSet = _applicationDbContext.Set<T>();
            

            var existingRecords = await dbSet.ToListAsync();
            if (existingRecords.Any())
            {
                dbSet.RemoveRange(existingRecords);
                //await _applicationDbContext.SaveChangesAsync();
            }
            var filePath = Path.Combine(AppContext.BaseDirectory, relativeCsvPath);

            if (!File.Exists(filePath))
            {
                _fileLoggerService.Warning(string.Format("Archivo CSV no encontrado en: {FilePath}", filePath));
                return;
            }
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>().ToList();

            await _applicationDbContext.Set<T>().AddRangeAsync(records);
            await _applicationDbContext.SaveChangesAsync();

            _fileLoggerService.LogInformation($"{records.Count} registros insertados en {typeof(T).Name}");
        }
    }
}
