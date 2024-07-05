using System.Reflection;
using Trendlink.Api;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure;

namespace Trendlink.ArchitectureTests.Infrastructure
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(IEntity).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
        protected static readonly Assembly InfrastructureAssembly =
            typeof(ApplicationDbContext).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }
}
