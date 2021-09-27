using System;
using System.Threading.Tasks;
using MaximaTech.Core.DTOs;
using MaximaTech.Core.Entities;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTech.Web.Controllers
{
    [Route("products")]
    [Authorize(AuthenticationSchemes = "MaximaTechScheme")]
    public class ProductController : Controller
    {
        protected readonly IMediator _mediator;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mediator"></param>
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Products listing view
        /// </summary>
        /// <returns>View Result</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index([FromQuery] ProductListingRequest request)
        {
            Pagination<Product> products = await _mediator.Send(request);

            return View(products);
        }

        /// <summary>
        /// Shows the create page
        /// </summary>
        /// <returns>View Result</returns>
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("store")]
        public async Task<IActionResult> Store(ProductAddRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(request);
                    TempData["Success"] = "Produto adicionado com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Erro ao cadastrar produto";
            }

            return View("Create", request);
        }

        /// <summary>
        /// Show edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                Product product = await _mediator.Send(new ProductFindRequest()
                {
                    Id = id,
                });

                return View("Edit", new ProductUpdateRequest()
                {
                    Id = product.Id,
                    Code = product.Code,
                    Description = product.Description,
                    DepartmentId = product.DepartmentId,
                    Price = product.Price,
                });
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "Produto não encontrado.";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Update product</returns>
        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, ProductUpdateRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.Id = id;
                    await _mediator.Send(request);
                    TempData["Success"] = "Produto atualizado com sucesso!";
                    return RedirectToAction("Edit", new { id = id });
                }
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "Produto não encontrado.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Erro ao atualizar produto";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Shows the delete screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Product product = await _mediator.Send(new ProductFindRequest()
                {
                    Id = id,
                });

                ViewBag.Product = product;

                return View("Delete");
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "Produto não encontrado.";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Remove the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _mediator.Send(new ProductDeleteRequest()
                {
                    Id = id,
                });
                TempData["Success"] = "Produto removido com sucesso!";
                return RedirectToAction("Index");
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "Produto não encontrado.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Erro ao remover produto";
            }

            return RedirectToAction("Index");
        }
    }
}