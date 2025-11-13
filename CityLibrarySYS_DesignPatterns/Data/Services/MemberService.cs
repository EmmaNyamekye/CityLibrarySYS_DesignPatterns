using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public class MemberService : IMemberService
    {
        private readonly LibraryDatabaseContext _context;

        public MemberService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddMember(Member member)
        {
            member.Status = "A";

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _context.Members.OrderBy(m => m.Surname).ToListAsync();
        }

        public async Task<Member?> GetMemberById(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.MemberID == id);
        }

        public async Task UpdateMember(int id, Member newMemberData)
        {
            var existingMember = await GetMemberById(id);
            if (existingMember == null) return;

            existingMember.Forename = newMemberData.Forename;
            existingMember.Surname = newMemberData.Surname;
            existingMember.DoB = newMemberData.DoB;
            existingMember.Street = newMemberData.Street;
            existingMember.Town = newMemberData.Town;
            existingMember.CountyCode = newMemberData.CountyCode;
            existingMember.Eircode = newMemberData.Eircode;
            existingMember.Phone = newMemberData.Phone;
            existingMember.Email = newMemberData.Email;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMember(int id)
        {
            var memberToDelete = await GetMemberById(id); 
            if (memberToDelete == null) return;

            _context.Members.Remove(memberToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberStatus(int id, string status, int daysInactive = 0)
        {
            var member = await GetMemberById(id); 
            if (member == null) return;

            member.Status = status;

            await _context.SaveChangesAsync();
        }
    }
}