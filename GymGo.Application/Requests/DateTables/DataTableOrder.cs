namespace GymGo.Application.Requests.DateTables
{
    public class DataTableOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; } = "asc";
    }
}