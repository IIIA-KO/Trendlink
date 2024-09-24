#pragma warning disable  CA1054
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users
{
    public sealed class UserResponse
    {
        public UserResponse() { }

        public UserResponse(
            Guid id,
            string Email,
            string firstName,
            string lastName,
            string? profilePhotoId,
            string? profilePhotoUri,
            DateOnly birthDate,
            string countryName,
            string stateName,
            string phoneNumber,
            string bio,
            AccountCategory accountCategory,
            int? followersCount,
            int? mediaCount
        )
        {
            this.Id = id;
            this.Email = Email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ProfilePhotoId = profilePhotoId;
            this.ProfilePhotoUri = profilePhotoUri;
            this.BirthDate = birthDate;
            this.CountryName = countryName;
            this.StateName = stateName;
            this.PhoneNumber = phoneNumber;
            this.Bio = bio;
            this.AccountCategory = accountCategory;
            this.FollowersCount = followersCount;
            this.MediaCount = mediaCount;
        }

        public Guid Id { get; init; }

        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string? ProfilePhotoId { get; init; }

        public string? ProfilePhotoUri { get; init; }

        public DateOnly BirthDate { get; init; }

        public string CountryName { get; init; }

        public string StateName { get; init; }

        public string PhoneNumber { get; init; }

        public string Bio { get; init; }

        public AccountCategory AccountCategory { get; init; }

        public int? FollowersCount { get; init; }

        public int? MediaCount { get; init; }
    }
}
