using Microsoft.Data.SqlClient;
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
			UserDbModel dbModel = new UserDbModel();

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
	}
}
