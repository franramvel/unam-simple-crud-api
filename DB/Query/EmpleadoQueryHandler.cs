using DB.Context;
using DB.Query.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public EmpleadoQueryHandler(MainDbContext ctx)
        {
            _ctx = ctx;
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
