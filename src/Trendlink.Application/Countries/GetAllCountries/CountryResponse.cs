namespace Trendlink.Application.Countries.GetAllCountries
{
    public class CountryResponse
    {
        public CountryResponse() { }

        public CountryResponse(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; init; }

        public string Name { get; init; }
    }
}
