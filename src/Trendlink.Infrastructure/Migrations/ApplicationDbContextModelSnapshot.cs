﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Trendlink.Infrastructure;

#nullable disable

namespace Trendlink.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer")
                        .HasColumnName("roles_id");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_role_user");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_role_user_users_id");

                    b.ToTable("role_user", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Conditions.Advertisements.Advertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ConditionId")
                        .HasColumnType("uuid")
                        .HasColumnName("condition_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("LastCooperatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_cooperated_on_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_advertisements");

                    b.HasIndex("ConditionId")
                        .HasDatabaseName("ix_advertisements_condition_id");

                    b.ToTable("advertisements", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Conditions.Condition", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_conditions");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_conditions_user_id");

                    b.ToTable("conditions", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Cooperations.BlockedDates.BlockedDate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_blocked_dates");

                    b.HasIndex("UserId", "Date")
                        .IsUnique()
                        .HasDatabaseName("ix_blocked_dates_user_id_date");

                    b.ToTable("blocked_dates", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Cooperations.Cooperation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AdvertisementId")
                        .HasColumnType("uuid")
                        .HasColumnName("advertisement_id");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("buyer_id");

                    b.Property<DateTime?>("CancelledOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("cancelled_on_utc");

                    b.Property<DateTime?>("CompletedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_on_utc");

                    b.Property<DateTime?>("ConfirmedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("confirmed_on_utc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("DoneOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("done_on_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("PendedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("pended_on_utc");

                    b.Property<DateTime?>("RejectedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rejected_on_utc");

                    b.Property<DateTimeOffset>("ScheduledOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("scheduled_on_utc");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid")
                        .HasColumnName("seller_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_cooperations");

                    b.HasIndex("AdvertisementId")
                        .HasDatabaseName("ix_cooperations_advertisement_id");

                    b.HasIndex("BuyerId")
                        .HasDatabaseName("ix_cooperations_buyer_id");

                    b.HasIndex("SellerId")
                        .HasDatabaseName("ix_cooperations_seller_id");

                    b.ToTable("cooperations", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Notifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean")
                        .HasColumnName("is_read");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<int>("NotificationType")
                        .HasColumnType("integer")
                        .HasColumnName("notification_type");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_notifications");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_notifications_user_id");

                    b.ToTable("notifications", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Reviews.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AdvertisementId")
                        .HasColumnType("uuid")
                        .HasColumnName("advertisement_id");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("buyer_id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<Guid>("CooperationId")
                        .HasColumnType("uuid")
                        .HasColumnName("cooperation_id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid")
                        .HasColumnName("seller_id");

                    b.HasKey("Id")
                        .HasName("pk_reviews");

                    b.HasIndex("AdvertisementId")
                        .HasDatabaseName("ix_reviews_advertisement_id");

                    b.HasIndex("BuyerId")
                        .HasDatabaseName("ix_reviews_buyer_id");

                    b.HasIndex("CooperationId")
                        .HasDatabaseName("ix_reviews_cooperation_id");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_reviews_id");

                    b.HasIndex("SellerId")
                        .HasDatabaseName("ix_reviews_seller_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.Countries.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_countries");

                    b.ToTable("countries", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.InstagramBusinessAccount.InstagramAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AdvertisementAccountId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("advertisement_account_id");

                    b.Property<string>("FacebookPageId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("facebook_page_id");

                    b.Property<DateTime?>("LastUpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_at_utc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_instagram_accounts");

                    b.HasIndex("LastUpdatedAtUtc")
                        .HasDatabaseName("ix_instagram_accounts_last_updated_at_utc");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_instagram_accounts_user_id");

                    b.ToTable("instagram_accounts", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Registered"
                        });
                });

            modelBuilder.Entity("Trendlink.Domain.Users.States.State", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid")
                        .HasColumnName("country_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_states");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_states_country_id");

                    b.ToTable("states", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.Token.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("access_token");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTimeOffset>("ExpiresAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at_utc");

                    b.Property<DateTime?>("LastCheckedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_checked_on_utc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_tokens");

                    b.HasIndex("ExpiresAtUtc")
                        .HasDatabaseName("ix_user_tokens_expires_at_utc");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_tokens_user_id");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccountCategory")
                        .HasColumnType("integer")
                        .HasColumnName("account_category");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("bio");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("email_verified");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uuid")
                        .HasColumnName("state_id");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_identity_id");

                    b.HasIndex("StateId")
                        .HasDatabaseName("ix_users_state_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Trendlink.Domain.Users.VerificationTokens.EmailVerificationToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at_utc");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_email_verification_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_email_verification_tokens_user_id");

                    b.ToTable("email_verification_tokens", (string)null);
                });

            modelBuilder.Entity("Trendlink.Infrastructure.BackgroundJobs.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occured_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_role_roles_id");

                    b.HasOne("Trendlink.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_users_users_id");
                });

            modelBuilder.Entity("Trendlink.Domain.Conditions.Advertisements.Advertisement", b =>
                {
                    b.HasOne("Trendlink.Domain.Conditions.Condition", "Condition")
                        .WithMany("Advertisements")
                        .HasForeignKey("ConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_advertisements_conditions_condition_id");

                    b.OwnsOne("Trendlink.Domain.Conditions.Advertisements.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("AdvertisementId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency");

                            b1.HasKey("AdvertisementId");

                            b1.ToTable("advertisements");

                            b1.WithOwner()
                                .HasForeignKey("AdvertisementId")
                                .HasConstraintName("fk_advertisements_advertisements_id");
                        });

                    b.Navigation("Condition");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Trendlink.Domain.Conditions.Condition", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithOne("Condition")
                        .HasForeignKey("Trendlink.Domain.Conditions.Condition", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_conditions_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Cooperations.BlockedDates.BlockedDate", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_blocked_dates_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Cooperations.Cooperation", b =>
                {
                    b.HasOne("Trendlink.Domain.Conditions.Advertisements.Advertisement", "Advertisement")
                        .WithMany()
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cooperations_advertisements_advertisement_id");

                    b.HasOne("Trendlink.Domain.Users.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_cooperations_users_buyer_id");

                    b.HasOne("Trendlink.Domain.Users.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_cooperations_users_seller_id");

                    b.OwnsOne("Trendlink.Domain.Conditions.Advertisements.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("CooperationId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency");

                            b1.HasKey("CooperationId");

                            b1.ToTable("cooperations");

                            b1.WithOwner()
                                .HasForeignKey("CooperationId")
                                .HasConstraintName("fk_cooperations_cooperations_id");
                        });

                    b.Navigation("Advertisement");

                    b.Navigation("Buyer");

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Trendlink.Domain.Notifications.Notification", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notifications_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Reviews.Review", b =>
                {
                    b.HasOne("Trendlink.Domain.Conditions.Advertisements.Advertisement", null)
                        .WithMany()
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_advertisements_advertisement_id");

                    b.HasOne("Trendlink.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_users_buyer_id");

                    b.HasOne("Trendlink.Domain.Cooperations.Cooperation", null)
                        .WithMany()
                        .HasForeignKey("CooperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_cooperations_cooperation_id");

                    b.HasOne("Trendlink.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_users_seller_id");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.InstagramBusinessAccount.InstagramAccount", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithOne("InstagramAccount")
                        .HasForeignKey("Trendlink.Domain.Users.InstagramBusinessAccount.InstagramAccount", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_instagram_accounts_users_user_id");

                    b.OwnsOne("Trendlink.Domain.Users.InstagramBusinessAccount.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("InstagramAccountId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("FollowersCount")
                                .HasColumnType("integer")
                                .HasColumnName("metadata_followers_count");

                            b1.Property<string>("Id")
                                .HasColumnType("text")
                                .HasColumnName("metadata_id");

                            b1.Property<long>("IgId")
                                .HasColumnType("bigint")
                                .HasColumnName("metadata_ig_id");

                            b1.Property<int>("MediaCount")
                                .HasColumnType("integer")
                                .HasColumnName("metadata_media_count");

                            b1.Property<string>("UserName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("metadata_user_name");

                            b1.HasKey("InstagramAccountId");

                            b1.ToTable("instagram_accounts");

                            b1.WithOwner()
                                .HasForeignKey("InstagramAccountId")
                                .HasConstraintName("fk_instagram_accounts_instagram_accounts_id");
                        });

                    b.Navigation("Metadata")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.States.State", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.Countries.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_states_countries_country_id");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.Token.UserToken", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithOne("Token")
                        .HasForeignKey("Trendlink.Domain.Users.Token.UserToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_user_tokens_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.User", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.States.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_states_state_id");

                    b.OwnsOne("Trendlink.Domain.Users.Photo", "ProfilePhoto", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Id")
                                .HasColumnType("text")
                                .HasColumnName("profile_photo_id");

                            b1.Property<string>("Uri")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("profile_photo_uri");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("ProfilePhoto");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.VerificationTokens.EmailVerificationToken", b =>
                {
                    b.HasOne("Trendlink.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_email_verification_tokens_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trendlink.Domain.Conditions.Condition", b =>
                {
                    b.Navigation("Advertisements");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.Countries.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Trendlink.Domain.Users.User", b =>
                {
                    b.Navigation("Condition");

                    b.Navigation("InstagramAccount");

                    b.Navigation("Notifications");

                    b.Navigation("Token");
                });
#pragma warning restore 612, 618
        }
    }
}
