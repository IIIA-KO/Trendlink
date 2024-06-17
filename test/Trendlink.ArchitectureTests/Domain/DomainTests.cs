﻿using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Trendlink.ArchitectureTests.Infrastructure;
using Trendlink.Domain.Abstraction;

namespace Trendlink.ArchitectureTests.Domain
{
    public class DomainTests : BaseTest
    {
        [Fact]
        public void DomainEvents_Should_BeSealed()
        {
            TestResult result = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEvent_ShouldHave_DomainEventPostfix()
        {
            TestResult result = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .Should()
                .HaveNameEndingWith("DomainEvent")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Entities_ShouldHave_PrivateParameterlessConstructor()
        {
            IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(IEntity))
                .GetTypes();

            var failingTypes = new List<Type>();
            foreach (Type entityType in entityTypes)
            {
                ConstructorInfo[] constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                                            BindingFlags.Instance);

                if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
                {
                    failingTypes.Add(entityType);
                }
            }

            failingTypes.Should().BeEmpty();
        }
    }
}
