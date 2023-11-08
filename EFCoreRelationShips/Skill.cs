namespace EFCoreRelationShips
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; } = 10;
        public List<Character> Character { get; set; } = null!;
    }
}