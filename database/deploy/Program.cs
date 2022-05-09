using System;
using Microsoft.Data.SqlClient;
using DbUp;
using DotNetEnv;

// This will load the content of .env file and create related environment variables
DotNetEnv.Env.Load();

// Connection string for deploying the database (high-privileged account as it needs to be able to CREATE/ALTER/DROP)
var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

var csb = new SqlConnectionStringBuilder(connectionString);
Console.WriteLine($"Deploying database: {csb.InitialCatalog}");

Console.WriteLine("Testing connection...");
var conn = new SqlConnection(csb.ToString());
conn.Open();
conn.Close();

Console.WriteLine("Starting deployment...");
var dbup = DeployChanges.To
    .SqlDatabase(csb.ConnectionString)
    .WithScriptsFromFileSystem("../sql")
    .JournalToSqlTable("dbo", "$__dbup_journal")
    .LogToConsole()
    .Build();

var result = dbup.PerformUpgrade();

if (!result.Successful)
{
    Console.WriteLine(result.Error);
    return -1;
}

Console.WriteLine("Success");
return 0;