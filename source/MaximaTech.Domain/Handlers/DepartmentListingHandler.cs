using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MediatR;

namespace MaximaTech.Domain.Handlers
{
    public class DepartmentListingHandler : IRequestHandler<DepartmentListingRequest, List<Department>>
    {
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public DepartmentListingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of Department</returns>
        public async Task<List<Department>> Handle(DepartmentListingRequest request, CancellationToken cancellationToken)
        {
            List<Department> departments = await _uow.DepartmentRepository.AllAsync();

            return departments;
        }
    }
}