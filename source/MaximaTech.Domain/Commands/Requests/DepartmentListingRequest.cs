using System.Collections.Generic;
using MaximaTech.Core.Entities;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class DepartmentListingRequest : IRequest<List<Department>> { }
}