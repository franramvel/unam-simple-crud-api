using DB.Context;
using DB.Query.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DB.Responses;
using System.Linq.Dynamic.Core;

namespace DB.Query
{
    public record GetEmpleadoQuery(int Id);

    public interface IEmpleadoQueryHandler :
    IQueryHandler<GetEmpleadoQuery, InternalResponse<Empleado>>
    {
    }

    public class EmpleadoQueryHandler : IEmpleadoQueryHandler
    {

        private readonly MainDbContext _ctx;
        private readonly ILogger<EmpleadoQueryHandler> _logger;

        public EmpleadoQueryHandler(MainDbContext ctx, ILogger<EmpleadoQueryHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }


  
        public async Task<InternalResponse<Empleado>> Handle(GetEmpleadoQuery query, CancellationToken cancellation)
        {
            var result = await _ctx.Empleados.FirstOrDefaultAsync(x => x.Id == query.Id);
            if (result ==null)
            {
                return new InternalResponse<Empleado>(404, "Registro no encontrado", null);
            }
            return new InternalResponse<Empleado>(200,"Registro encontrado",result);

        }


        
    }
}
