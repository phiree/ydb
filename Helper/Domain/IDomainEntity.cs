using System;

namespace Helpers.Domain
{
    public interface IDomainEntity
    {
        Guid Id { get; }
    }
}
