using System.Collections.Generic;
using MaximaTech.Core.Entities;
using MaximaTech.Domain.Commands.Responses;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class DepartmentListingRequest : IRequest<List<Department>> { }
}