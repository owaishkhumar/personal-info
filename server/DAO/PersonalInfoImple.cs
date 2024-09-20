using Npgsql;
using System.Data.Common;
using System.Data;
using PersonalInfo.Model;
using PersonalInfo.Helper;
using System.Net.NetworkInformation;

namespace PersonalInfo.DAO
{

    public class PersonalInfoImple : IPersonalInfoDao
    {
        NpgsqlConnection _connection;

        public PersonalInfoImple(NpgsqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<List<PersonalInformationData>> GetAllPersonalInfo(string baseUri)
        {
            string query = "select * from personal_info.userData;";
            List<PersonalInformationData> personalInfoList = new List<PersonalInformationData>();
            PersonalInformationData personalInfo = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            personalInfo = new PersonalInformationData();
                            personalInfo.Id = Convert.ToInt32(reader["id"]);

                            personalInfo.Name = reader["name"].ToString();
                            personalInfo.DateOfBirth = reader["date_of_birth"].ToString();
                            personalInfo.ResidentialAddress = reader["residential_address"].ToString();
                            personalInfo.PermanentAddress = reader["permanent_address"].ToString();
                            personalInfo.MobileNumber = reader["phone_number"].ToString();
                            personalInfo.EmailAddress = reader["email_address"].ToString();
                            personalInfo.MaritalStatus = reader["marital_status"].ToString();
                            personalInfo.Gender = reader["gender"].ToString();
                            personalInfo.Occupation = reader["occupation"].ToString();
                            personalInfo.AadharCardNumber = reader["aadhar_card_number"].ToString();
                            personalInfo.PANNumber = reader["pan_number"].ToString();
                            personalInfo.ImageUrl = baseUri + reader["image"].ToString();
                            personalInfoList.Add(personalInfo);
                        }
                    }
                    reader.Close();
                    return personalInfoList;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("-------------Exception Get All Personal Information---------------" + e.Message);
            }
            return personalInfoList;
        }

        public async Task<int> InsertPersonalDetails(InsertPersonalDetails personalInfo, string imageName)
        {
            int rowInserted = 0;
            string message;
            string insertQuery = $"INSERT INTO personal_info.userdata ( name, date_of_birth, residential_address, permanent_address, phone_number, email_address, marital_status, gender, occupation, aadhar_card_number, pan_number, image ) VALUES ( '{personalInfo.Name}', '{personalInfo.DateOfBirth}', '{personalInfo.ResidentialAddress}', '{personalInfo.PermanentAddress}', '{personalInfo.MobileNumber}', '{personalInfo.EmailAddress}', '{personalInfo.MaritalStatus}', '{personalInfo.Gender}', '{personalInfo.Occupation}', '{personalInfo.AadharCardNumber}', '{personalInfo.PANNumber}', 'static/images/{imageName}');";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, _connection);
                    insertCommand.CommandType = CommandType.Text;
                    rowInserted = await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
                Console.WriteLine("---------Exception Insert Player--------------\n" + message);
            }
            return rowInserted;
        }

        public async Task<PersonalInformationData> GetPersonalInfoById(int id, string baseUri)
        {
            string query = $"select * from personal_info.userData where id = {id};";
            PersonalInformationData? personalInfo = null;
            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = query;
                await _connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            personalInfo = new PersonalInformationData();
                            personalInfo.Id = Convert.ToInt32(reader["id"]);

                            personalInfo.Name = reader["name"].ToString();
                            personalInfo.DateOfBirth = reader["date_of_birth"].ToString();
                            personalInfo.ResidentialAddress = reader["residential_address"].ToString();
                            personalInfo.PermanentAddress = reader["permanent_address"].ToString();
                            personalInfo.MobileNumber = reader["phone_number"].ToString();
                            personalInfo.EmailAddress = reader["email_address"].ToString();
                            personalInfo.MaritalStatus = reader["marital_status"].ToString();
                            personalInfo.Gender = reader["gender"].ToString();
                            personalInfo.Occupation = reader["occupation"].ToString();
                            personalInfo.AadharCardNumber = reader["aadhar_card_number"].ToString();
                            personalInfo.PANNumber = reader["pan_number"].ToString();
                            personalInfo.ImageUrl = baseUri + reader["image"].ToString();
                        }
                    }
                    reader.Close();
                    await _connection.CloseAsync();
                    return personalInfo;    
                
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("-------------Exception Get Personal Information by Id---------------" + e.Message);
            }
            return personalInfo;
        }
            
        public async Task<string> DeletePersonalDetails(int id)
        {
            int rowDeleted = 0;
            string message;
            string deleteQuery = $"DELETE FROM personal_info.userdata WHERE id = {id} returning image;";

            string imageUrl = "";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(deleteQuery, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        Console.WriteLine(reader);
                        while (reader.Read())
                        {
                            imageUrl = reader["image"].ToString();  
                        }
                    }

                    imageUrl = imageUrl.Substring(14);

                    UploadHandler.DeleteImage(imageUrl);

                    reader.Close();
                    return imageUrl;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("-------------Exception Get Personal Information by Id---------------" + e.Message);
            }
            return imageUrl;
        }

        public async Task<int> UpdatePersonalInfo(InsertPersonalDetails personalInfo, int id, string imageName)
        {
            int rowUpdated = 0;
            string message;
            string updateQuery = $"UPDATE personal_info.userdata set name = '{personalInfo.Name}', date_of_birth = '{personalInfo.DateOfBirth}', residential_address = '{personalInfo.ResidentialAddress}', permanent_address = '{personalInfo.PermanentAddress}', phone_number = '{personalInfo.MobileNumber}', email_address = '{personalInfo.EmailAddress}', marital_status = '{personalInfo.MaritalStatus}', gender = '{personalInfo.Gender}', occupation = '{personalInfo.Occupation}', aadhar_card_number = '{personalInfo.AadharCardNumber}', pan_number = '{personalInfo.PANNumber}', image = 'static/images/{imageName}' where id = {id};";
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, _connection);
                    updateCommand.CommandType = CommandType.Text;
                    rowUpdated = await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
                Console.WriteLine("---------NpsqlExpection Exception Update Personal--------------\n" + message);
            }
            catch (Exception e)
            {
                message = e.Message;
                Console.WriteLine("---------Exception Update Personal--------------\n" + message);
            }
            return rowUpdated;
        }
    }
}
