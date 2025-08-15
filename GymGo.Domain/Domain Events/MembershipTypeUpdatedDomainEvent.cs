using GymGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Domain.Domain_Events
{
    public class MembershipTypeUpdatedDomainEvent(MembershipType membershipType)
                : IDomainEvent
    {
        
        public MembershipType MembershipType
        {
            get;
        } = membershipType ?? throw new ArgumentNullException(nameof(membershipType));

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}