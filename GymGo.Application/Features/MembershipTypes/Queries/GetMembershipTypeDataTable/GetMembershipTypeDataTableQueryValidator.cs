using FluentValidation;
using GymGo.Application.Requests.DateTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetMembershipTypeDataTable
{
    public class GetMembershipTypeDataTableQueryValidator
        : AbstractValidator<GetMembershipTypeDataTableQuery>
    {
        // Define los campos válidos para MembershipType
        private static readonly HashSet<string> AllowedColumns = new()
        {
            "Id", "Name", "Description", "Price", "DurationInDays", "IsActive", "CreatedBy"
        };

        public GetMembershipTypeDataTableQueryValidator()
        {
            RuleFor(x => x.DataTableRequest)
                .NotNull()
                .WithMessage("DataTableRequest is required.");

            When(x => x.DataTableRequest != null, () =>
            {
                RuleFor(x => x.DataTableRequest.Draw)
                    .GreaterThan(0)
                    .WithMessage("Page number must be greater than 0.");

                RuleFor(x => x.DataTableRequest.Start)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Start index must be greater than or equal to 0.");

                RuleFor(x => x.DataTableRequest.Length)
                    .GreaterThan(0)
                    .WithMessage("Length must be greater than 0.");

                RuleFor(x => x.DataTableRequest.Columns)
                    .NotNull()
                    .WithMessage("Columns are required.")
                    .Must(cols => cols.Count > 0)
                    .WithMessage("At least one column must be defined.");

                RuleForEach(x => x.DataTableRequest.Order)
                    .SetValidator(new DataTableOrderValidator());

                // Aquí pasas el HashSet solo desde el validador principal
                RuleForEach(x => x.DataTableRequest.Columns)
                    .SetValidator(new DataTableColumnValidator(AllowedColumns));
            });
        }

        // Haz este validador internal o private para evitar registro en DI
        internal class DataTableColumnValidator : AbstractValidator<DataTableColumn>
        {
            public DataTableColumnValidator(HashSet<string> allowedColumns)
            {
                RuleFor(x => x.Data)
                    .NotEmpty()
                    .WithMessage("Column data property is required.")
                    .Must(col => allowedColumns.Contains(col))
                    .WithMessage("Column '{PropertyValue}' is not allowed.");
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Column name is required.");
            }
        }

        public class DataTableOrderValidator : AbstractValidator<DataTableOrder>
        {
            public DataTableOrderValidator()
            {
                RuleFor(x => x.Dir)
                    .Must(dir => dir == "asc" || dir == "desc")
                    .WithMessage("Order direction must be 'asc' or 'desc'.");
            }
        }
    }
}
