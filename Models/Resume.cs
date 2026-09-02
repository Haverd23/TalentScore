namespace TalentScore.Models
{
    public class Resume
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public int Score { get; private set; }

        public Resume(string name, string email, string? phone, int score)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Score = score;
        }




    }
}
