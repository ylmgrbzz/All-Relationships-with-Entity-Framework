using System.Text.Json.Serialization;

namespace EFCoreRelationShips
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RpgClass { get; set; } = "Knight";

        [JsonIgnore]
        public User User { get; set; } = null!;

        public int UserId { get; set; }
        public Weapon Weapon { get; set; } = null!;
        public int WeaponId { get; set; }
    }
}