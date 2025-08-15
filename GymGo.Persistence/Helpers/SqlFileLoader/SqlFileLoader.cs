namespace GymGo.Persistence.Helpers.SqlFileLoader
{
    public class SqlFileLoader
        : ISqlFileLoader
    {
        private readonly string _baseFolder;

        public SqlFileLoader(string baseFolder)
        {
            _baseFolder = baseFolder;
        }
        public async Task<string> LoadSqlAsync(string folderPath, string fileName)
        {
            var basePath = Path.Combine(_baseFolder, folderPath);
            var fullPath = Path.Combine(basePath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Archivo SQL no encontrado: {fullPath}");

            return await File.ReadAllTextAsync(fullPath);
        }
    }
}
