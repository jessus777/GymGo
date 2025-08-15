using GymGo.Application.Dtos;
using GymGo.Application.Requests;
using GymGo.Application.Requests.DateTables;
using GymGo.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetMembershipTypeDataTable
{
    public sealed class GetMembershipTypeDataTableQuery
        : ApplicationRequest<DataTableResponse<MembershipTypeDto>>
    {
        public GetMembershipTypeDataTableQuery(DataTableRequest dataTableRequest)
        {
            DataTableRequest = dataTableRequest;
        }

        public DataTableRequest DataTableRequest { get; set; }
    }
}
