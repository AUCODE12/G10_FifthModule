namespace MusicManagement.Repository.Settings;

public class SqlDBConnectionString
{
	private string connectionString;

	public string ConnectionString
    {
		get { return connectionString; }
		set { connectionString = value; }
	}

    public SqlDBConnectionString(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
