using Npgsql;
using System.Data.Common;
using System.Data;
using PersonalInfo.Model;

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
            string insertQuery = $"INSERT INTO personal_info.userdata ( name, date_of_birth, residential_address, permanent_address, phone_number, email_address, marital_status, gender, occupation, aadhar_card_number, pan_number, image ) VALUES ( '{personalInfo.Name}', '{personalInfo.DateOfBirth}', '{personalInfo.ResidentialAddress}', '{personalInfo.PermanentAddress}', '{personalInfo.MobileNumber}', '{personalInfo.EmailAddress}', '{personalInfo.MaritalStatus}', '{personalInfo.Gender}', '{personalInfo.Occupation}', '{personalInfo.AadharCardNumber}', '{personalInfo.PANNumber}', 'image/{imageName}');";

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
    }


}
