namespace GymGo.Persistence.Helpers.SqlFileLoader
{
    public interface ISqlFileLoader
    {
        Task<string> LoadSqlAsync(string folderPath, string fileName);

    }
}
