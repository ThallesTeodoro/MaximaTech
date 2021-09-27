using System.Collections.Generic;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Domain.Commands.Responses;
using MaximaTech.Web.Filters;
using MaximaTech.Web.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTech.Web.Controllers.Api
{
    [ApiController]
    [Area("Api")]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMediator _mediator;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mediator"></param>
        public DepartmentController(IUnitOfWork uow, IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<JsonResponse<List<Department>, object>> Index()
        {
            JsonResponse<List<Department>, object> response = new JsonResponse<List<Department>, object>();

            response.Data = await _mediator.Send(new DepartmentListingRequest());

            return response;
        }
    }
}