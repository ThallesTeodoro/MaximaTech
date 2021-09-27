using System;
using System.ComponentModel.DataAnnotations;
using MaximaTech.Core.Entities;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class ProductAddRequest : IRequest<Product>
    {
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