using Microsoft.Data.SqlClient;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
	public class UserDataManagementService
	{
		private readonly string connectionString = "Server=localhost;Database=UserManangementDb;Trusted_Connection=True;TrustServerCertificate=True";
		private SqlConnection connection = null!;

		public IEnumerable<DeleteUser> GetAllUserDetails()
		{
			connection = new SqlConnection(connectionString);
			string commandString = "select * from users;";
			SqlCommand command = new SqlCommand(commandString, connection);
			List<DeleteUser> users = new();

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					DeleteUser user = new();
					user.Id = (int)reader[0];
					user.FirstName = (string)reader[1];
					user.LastName = (string)reader[2];
					user.Email = (string)reader[3];
					user.Password = (string)reader[4];
					user.Role = (string)reader[5];

					users.Add(user);
				}

				reader.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return users;
		}

		public DetailsUser GetDetailsById(int? id)
		{
			connection = new SqlConnection(connectionString);
			string commandString = "select * from users where id = @id;";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@id", id);
			DetailsUser detailsUser = null!;

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					detailsUser = new DetailsUser();
					detailsUser.Id = (int)reader[0];
					detailsUser.FirstName = (string)reader[1];
					detailsUser.LastName = (string)reader[2];
					detailsUser.Email = (string)reader[3];
					detailsUser.Password = (string)reader[4];
					detailsUser.Role = (string)reader[5];
				}

				reader.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return detailsUser;
		}

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

				reader.Close();
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

		public bool EmailExistsAlready(string newEmail, EditUser model)
		{
			connection = new SqlConnection(connectionString);
			string emailCommand = "select email from users where id = @id";
			SqlCommand command = new SqlCommand(emailCommand, connection);
			command.Parameters.AddWithValue("@id", model.Id);
			string currentEmail = String.Empty;
			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					currentEmail = (string)reader[0];
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

			string checkCommand = "select count(*) from users where email = @newemail and email != @currmail";
			command = new SqlCommand(checkCommand, connection);
			command.Parameters.AddWithValue("@newemail", newEmail);
			command.Parameters.AddWithValue("@currmail", currentEmail);
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
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return success;
		}

		public bool AddUserToDbWithRole(CreateUser user)
		{
			bool success = false;
			connection = new SqlConnection(connectionString);
			string commandString = "insert into users values (@first_name, @last_name, @email, @password, @user);";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@first_name", user.FirstName);
			command.Parameters.AddWithValue("@last_name", user.LastName);
			command.Parameters.AddWithValue("@email", user.Email);
			command.Parameters.AddWithValue("@password", user.Password);
			command.Parameters.AddWithValue("@user", user.Role);

			try
			{
				connection.Open();
				success = 1 == command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				connection.Close();
			}

			return success;
		}

		public bool DeleteUser(int id)
		{
			connection = new SqlConnection(connectionString);
			string commandString = "delete from users where id = @id";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@id", id);
			bool success = false;

			try
			{
				connection.Open();
				success = 1 == command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
			finally
			{
				connection.Close();
			}

			return success;
		}

		public bool UpdateUser(EditUser user)
		{
			connection = new SqlConnection(connectionString);
			string commandString = "update users set first_name = @fname, last_name = @lname, email = @email, password = @password, role = @role where id = @id";
			SqlCommand command = new SqlCommand(commandString, connection);
			command.Parameters.AddWithValue("@fname", user.FirstName);
			command.Parameters.AddWithValue("@lname", user.LastName);
			command.Parameters.AddWithValue("@email", user.Email);
			command.Parameters.AddWithValue("@password", user.Password);
			command.Parameters.AddWithValue("@role", user.Role);
			command.Parameters.AddWithValue("@id", user.Id);
			bool success = false;

			try
			{
				connection.Open();
				success = 1 == command.ExecuteNonQuery();
			}
			catch (Exception e)
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
