using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Infrastructure.DataSeeding
{
    public interface ICsvDataSeeder
    {
        Task SeedAsync<T>(string relativeCsvPath) where T : class;
    }
}
