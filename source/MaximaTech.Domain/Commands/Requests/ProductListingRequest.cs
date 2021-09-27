using System.ComponentModel.DataAnnotations;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;
using MediatR;

namespace MaximaTech.Domain.Commands.Requests
{
    public class ProductListingRequest : IRequest<Pagination<Product>>
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }
    }
}