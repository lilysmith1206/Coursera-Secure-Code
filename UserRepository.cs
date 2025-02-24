using System;
using MySql.Data.MySqlClient;

public class UserRepository
{
	private string connectionString = "server=localhost;database=your_database;user=your_user;password=your_password;";

	/// <summary>
	/// Retrieves user information securely using parameterized queries.
	/// </summary>
	/// <param name="username">The username to search for.</param>
	public void GetUserByUsername(string username)
	{
		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			conn.Open();

			string query = "SELECT UserID, Username, Email FROM Users WHERE Username = @username";

			using (MySqlCommand cmd = new MySqlCommand(query, conn))
			{
				// Using parameterized query to avoid SQL Injection
				cmd.Parameters.AddWithValue("@username", username);

				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine($"UserID: {reader["UserID"]}, Username: {reader["Username"]}, Email: {reader["Email"]}");
					}
				}
			}
		}
	}

	/// <summary>
	/// Inserts a new user into the database securely.
	/// </summary>
	/// <param name="username">The username to insert.</param>
	/// <param name="email">The email to insert.</param>
	public void InsertUser(string username, string email)
	{
		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			conn.Open();

			string query = "INSERT INTO Users (Username, Email) VALUES (@username, @email)";

			using (MySqlCommand cmd = new MySqlCommand(query, conn))
			{
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@email", email);

				int rowsAffected = cmd.ExecuteNonQuery();
				Console.WriteLine($"{rowsAffected} user(s) inserted.");
			}
		}
	}
}
