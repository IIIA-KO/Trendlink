namespace Trendlink.Application.Countries.GetStates
{
    public class StateResponse
    {
        public StateResponse() { }

        public StateResponse(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
