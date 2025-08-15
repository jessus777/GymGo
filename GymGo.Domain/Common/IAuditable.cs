using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Domain.Common
{
    public interface IAuditable
    {
        AuditableEntity Auditoria { get; set; }
    }
}
