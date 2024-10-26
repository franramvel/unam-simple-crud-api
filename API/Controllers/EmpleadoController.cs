using AutoMapper;
using DB;
using Microsoft.AspNetCore.Mvc;


using Services;

namespace ParrillaReportes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : Controller
    {
        //El controlador usa el standard mediante verbos de http, por lo que se puede usar el mismo nombre de url, y cambiar unicamente la accion para poder realizar las operaciones
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoServices _empleadoServices;
        private readonly IMapper _mapper;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoServices empleadoServices)
        {
            _logger = logger;
            _empleadoServices = empleadoServices;
        }


        //Obtener el valor por Id, usarse para obtener el valor de una moneda en especifico
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id,CancellationToken cancellationToken)
        {
            var result = await _empleadoServices.GetAsync(id, cancellationToken);
            switch (result.HttpCode) 
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result.Message);
                default:
                    return StatusCode(500, result.Message);
            }
        }




        //Insercion de un nuevo registro
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empleado model)
        {
            var result = await _empleadoServices.CreateAsync(model);
            switch (result.HttpCode)
            {
                case 200:
                    return Ok(result);
                case 409:
                    return Conflict(result.Message);
                default:
                    return StatusCode(500, result.Message);
            }
        }

        //Actualizacion de un registro
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Empleado model)
        {
            var mapperInstance = _mapper.Map<Empleado>(model);
            var result = await _empleadoServices.UpdateAsync(mapperInstance);
            switch (result.HttpCode)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result.Message);
                case 409:
                    return Conflict(result.Message);
                default:
                    return StatusCode(500, result.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empleadoServices.DeleteAsync(id);
            switch (result.HttpCode)
            {
                case 200:
                    return Ok(result.Message);
                case 404:
                    return NotFound(result.Message);
                default:
                    return StatusCode(500, result.Message);
            }
        }
    }
}
