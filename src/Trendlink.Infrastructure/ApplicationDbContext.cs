﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Exceptions;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.BackgroundJobs.Outbox;

namespace Trendlink.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings =
            new()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        private readonly IDateTimeProvider _dateTimeProvider;

        public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            this._dateTimeProvider = dateTimeProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                this.AddDomainEventsAsOutboxMessages();

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new ConcurrencyException("Concurrency exception occurred.", exception);
            }
        }

        private void AddDomainEventsAsOutboxMessages()
        {
            var outboxMessages = this
                .ChangeTracker.Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage(
                    Guid.NewGuid(),
                    this._dateTimeProvider.UtcNow,
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)
                ))
                .ToList();

            this.AddRange(outboxMessages);
        }
    }
}
