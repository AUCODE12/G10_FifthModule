namespace OnlineVotingSystem.Repository.Settings;

public class SqlDBConnectionString
{
	private string _connectionString;

	public string ConnectionSting
	{
		get { return _connectionString; }
		set { _connectionString = value; }
	}

    public SqlDBConnectionString(string connectionString)
    {
        ConnectionSting = connectionString;
    }
}
