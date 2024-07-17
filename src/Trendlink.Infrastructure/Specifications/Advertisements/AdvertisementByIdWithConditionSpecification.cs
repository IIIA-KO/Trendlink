using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Advertisements
{
    internal sealed class AdvertisementByIdWithConditionSpecification
        : Specification<Advertisement, AdvertisementId>
    {
        public AdvertisementByIdWithConditionSpecification(AdvertisementId advertisementId)
            : base(advertisement => advertisement.Id == advertisementId)
        {
            this.AddInclude(advertisement => advertisement.Condition);
        }
    }
}
