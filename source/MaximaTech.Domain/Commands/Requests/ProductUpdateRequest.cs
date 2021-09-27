using System;
using System.ComponentModel.DataAnnotations;
using MaximaTech.Core.Entities;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class ProductUpdateRequest : IRequest<Product>
    {
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public double Price { get; set; }
    }
}