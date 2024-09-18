using PersonalInfo.Model;

namespace PersonalInfo.DAO
{
    public interface IPersonalInfoDao
    {
        Task<List<PersonalInformationData>> GetAllPersonalInfo(string baseUri);
        Task<int> InsertPersonalDetails(InsertPersonalDetails personalInfo, string imageName);


    }
}
