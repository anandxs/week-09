using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.Data.SqlClient;
using System.Configuration;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
	public class UserDataManagementService
	{
		private readonly string connectionString = "Server=localhost;Database=UserManangementDb;Trusted_Connection=True;TrustServerCertificate=True";
		private SqlConnection connection = null!;

		public UserDbModel CheckCredentials(LoginUser model)
		{
			connection = new SqlConnection(connectionString);
			string commandString = "select first_name, last_name, email, password, role from users where email=@email and password=@password;";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@email", model.Email);
			command.Parameters.AddWithValue("@password", model.Password);
			UserDbModel dbModel = new();

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					dbModel.FirstName = (string)reader[0];
					dbModel.LastName = (string)reader[1];
					dbModel.Email = (string)reader[2];
					dbModel.Password = (string)reader[3];
					dbModel.Role = (string)reader[4];
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return dbModel;
		}

		public bool EmailExistsAlready(string email)
		{
			connection = new SqlConnection(connectionString);
			string commandString = "select count(*) from users where email = @email";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@email", email);
			UserDbModel dbModel = new();
			bool exists = false;

			try
			{
				connection.Open();
				exists = 1 == (int)command.ExecuteScalar();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return exists;
		}

		public bool AddUserToDb(RegisterUser user)
		{
			bool success = false;
			connection = new SqlConnection(connectionString);
			string commandString = "insert into users values (@first_name, @last_name, @email, @password, 'User');";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@first_name", user.FirstName);
			command.Parameters.AddWithValue("@last_name", user.LastName);
			command.Parameters.AddWithValue("@email", user.Email);
			command.Parameters.AddWithValue("@password", user.Password);

			try
			{
				connection.Open();
				success = 1 == command.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return success;
		}
	}
}
