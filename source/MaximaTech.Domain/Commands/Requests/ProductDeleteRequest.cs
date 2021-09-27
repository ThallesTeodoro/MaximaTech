using System;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class ProductDeleteRequest : IRequest<object>
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Department { get; set; }

        public double Price { get; set; }
    }
}