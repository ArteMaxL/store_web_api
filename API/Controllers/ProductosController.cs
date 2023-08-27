using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoListDto>>> Get()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoListDto>>(productos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var productos = await _unitOfWork.Productos.GetByIdAsync(id);

        if (productos == null)
        {
            return NotFound();
        }
        return _mapper.Map<ProductoDto>(productos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Post(ProductoAddUpdateDto productoDto)
    {
        if (productoDto == null)
        {
            return BadRequest();
        }

        var producto = _mapper.Map<Producto>(productoDto);

        _unitOfWork.Productos.Add(producto);
        await _unitOfWork.SaveAsync();
        productoDto.Id = producto.Id;

        return CreatedAtAction(nameof(Post), new { id = productoDto.Id }, productoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoAddUpdateDto>> Put(int id, [FromBody] ProductoAddUpdateDto productoDto)
    {
        if (productoDto == null)
        {
            return NotFound();
        }

        var producto = _mapper.Map<Producto>(productoDto);
        _unitOfWork.Productos.Update(producto);
        await _unitOfWork.SaveAsync();

        return productoDto;
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
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
