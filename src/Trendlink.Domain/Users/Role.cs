namespace Trendlink.Domain.Users
{
    public sealed class Role
    {
        public static readonly Role Administrator = new(1, "Administrator");
        public static readonly Role Registered = new(2, "Registered");

        private Role(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; init; }

        public string Name { get; init; }

        public ICollection<User> Users { get; init; } = [];
    }
}
