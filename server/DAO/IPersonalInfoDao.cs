using PersonalInfo.Model;

namespace PersonalInfo.DAO
{
    public interface IPersonalInfoDao
    {
        Task<List<PersonalInformationData>> GetAllPersonalInfo(string baseUri);
        Task<int> InsertPersonalDetails(InsertPersonalDetails personalInfo, string imageName);
        Task<string> DeletePersonalDetails(int id);

        Task<PersonalInformationData> GetPersonalInfoById(int id, string baseUri);
        Task<int> UpdatePersonalInfo(InsertPersonalDetails personalInfo, int id, string imageName);




    }
}
