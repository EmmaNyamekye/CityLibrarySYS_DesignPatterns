using CityLibrarySYS_DesignPatterns.Models;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface IMemberService
    {
        Task AddMember(Member member);

        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member?> GetMemberById(int id);

        Task UpdateMember(int id, Member newMemberData);

        Task DeleteMember(int id);

        Task UpdateMemberStatus(int id, string status, int daysInactive = 0);
    }
}