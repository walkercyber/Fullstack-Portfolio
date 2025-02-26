using System.ComponentModel.DataAnnotations;

namespace FullstackLabb3.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Technology { get; set; }
        public int Yearsofexperience { get; set; }

        [Range(1, 5)]
        public int SkillLevel { get; set; }
    }
}
