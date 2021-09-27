using System;
using MaximaTech.Core.Entities;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class ProductFindRequest : IRequest<Product>
    {
        public Guid Id { get; set; }
    }
}