using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Producto>>> Get()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return Ok(productos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Producto>>> Get(int id)
    {
        var productos = await _unitOfWork.Productos.GetByIdAsync(id);
        return Ok(productos);
    }
}
