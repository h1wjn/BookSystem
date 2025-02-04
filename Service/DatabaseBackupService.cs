using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

public class DatabaseBackupService
{
    private readonly string server;
    private readonly string user;
    private readonly string password;
    private readonly string databaseName;

    public DatabaseBackupService(string server, string user, string password, string databaseName)
    {
        this.server = server;
        this.user = user;
        this.password = password;
        this.databaseName = databaseName;
    }

    public void BackupDatabase()
    {
        // 获取备份文件的路径
        string backupFilePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Backups",
            $"backup_{DateTime.Now:yyyyMMddHHmmss}.sql"
        );

        // 确保备份文件的目录存在
        string backupDirectory = Path.GetDirectoryName(backupFilePath);
        if (!Directory.Exists(backupDirectory))
        {
            Directory.CreateDirectory(backupDirectory);
        }

        // 连接到 MySQL 数据库
        string connectionString = $"Server={server};Database={databaseName};User ID={user};Password={password};";
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                
                // 获取所有表名
                var tables = GetTableNames(connection);

                // 创建一个 SQL 文件来存储备份数据
                using (var writer = new StreamWriter(backupFilePath))
                {
                    // 遍历每个表，备份数据
                    foreach (var table in tables)
                    {
                        writer.WriteLine($"-- Table: {table}");
                        var data = GetTableData(connection, table);
                        writer.WriteLine(data);
                    }
                }

                Console.WriteLine($"数据库备份成功，备份文件：{backupFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库备份失败，错误信息：{ex.Message}");
            }
        }
    }

    // 获取数据库中所有表名
    private List<string> GetTableNames(MySqlConnection connection)
    {
        var tableNames = new List<string>();
        string query = "SHOW TABLES";
        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableNames.Add(reader.GetString(0));
                }
            }
        }
        return tableNames;
    }

    // 获取指定表的数据，生成 SQL 插入语句
    private string GetTableData(MySqlConnection connection, string tableName)
    {
        var sqlData = "";
        string query = $"SELECT * FROM {tableName}";
        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var columns = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i).ToString().Replace("'", "''")}'");
                    }
                    var values = string.Join(", ", columns);
                    sqlData += $"INSERT INTO {tableName} VALUES ({values});\n";
                }
            }
        }
        return sqlData;
    }
}
