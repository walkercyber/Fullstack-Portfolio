using FullstackLabb3.Models;
using FullstackLabb3.Data;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3.Data
{
    public class SkillService
    {
        private readonly DataContext _db;

        public SkillService(DataContext context)
        {
            _db = context;
        }

        public async Task AddSkill(Skill skill)
        {
            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();
        }

        public async Task<Skill> GetSkillById(int id)
        {
            return await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Skill> UpdateSkill(int id, Skill updatedSkill)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            if (skill == null)
                return null;

            skill.Technology = updatedSkill.Technology;
            skill.Yearsofexperience = updatedSkill.Yearsofexperience;
            skill.SkillLevel = updatedSkill.SkillLevel;

            await _db.SaveChangesAsync();
            return skill;
        }

        public async Task<Skill> DeleteSkill(int id)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            if (skill == null)
                return null;

            _db.Skills.Remove(skill);
            await _db.SaveChangesAsync();
            return skill;
        }
    }
}
