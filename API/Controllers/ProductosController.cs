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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Post(Producto producto)
    {
        if (producto == null)
        {
            return BadRequest();
        }

        _unitOfWork.Productos.Add(producto);
        _unitOfWork.Save();

        return CreatedAtAction(nameof(Post), new { id = producto.Id }, producto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Producto>> Put(int id, [FromBody] Producto producto)
    {
        if (producto == null)
        {
            return NotFound();
        }

        _unitOfWork.Productos.Update(producto);
        _unitOfWork.Save();

        return Ok(producto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete (int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);

        if (producto == null)
        {
            return NotFound();
        }

        _unitOfWork.Productos.Remove(producto);
        _unitOfWork.Save();

        return NoContent();
    }
}
