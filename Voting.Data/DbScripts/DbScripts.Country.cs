namespace Voting.Data.DbScripts
{
    public partial class DbScripts
    {
        public class Country
        {
            public static string Select = @"SELECT * FROM Country WHERE Id = @Id";

            public static string FindByName = @"SELECT * FROM Country WHERE Name = @Name";

            public static string FindAll = @"SELECT * FROM Country";
        }
    }
}
