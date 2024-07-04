using AutoMapper;
using Desafio.API.Interfaces;
using Desafio.API.Models.Dtos;
using Desafio.API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_mapper.Map<List<EmployeeDto>>(await _unitOfWork.RepositoryEmployee.GetAllAsync()));
        }

        [HttpGet("{id:guid}")]
        [ActionName("BuscarEmploeeById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var employee = await _unitOfWork.RepositoryEmployee.GetByIdAsync(id);

            if (employee == null) return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var e = await _unitOfWork.RepositoryEmployee.CreateAsync(employee);
            await _unitOfWork.Commit();

            return CreatedAtAction("BuscarEmploeeById", new {id = e.Id}, e);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto.Id != id || employeeDto.Id == null) return BadRequest();

            var employee = await _unitOfWork.RepositoryEmployee.GetByIdAsync(id);

            if (employee == null) return NotFound();

            _mapper.Map(employeeDto, employee);
            _unitOfWork.RepositoryEmployee.Update(employee);
            await _unitOfWork.Commit();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var employee = await _unitOfWork.RepositoryEmployee.GetByIdAsync(id);

            if (employee == null) return NotFound();

            _unitOfWork.RepositoryEmployee.Delete(employee);
            await _unitOfWork.Commit();

            return NoContent();
        }
    }
}