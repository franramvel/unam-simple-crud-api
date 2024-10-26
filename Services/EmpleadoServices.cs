using DB;
using DB.Command;
using DB.Command.Interfaces;
using DB.Query;
using DB.Query.Interfaces;

using Model.DB.Responses;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Services
{
    public class EmpleadoServices : IEmpleadoServices
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public EmpleadoServices(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }
        public async Task<InternalResponse<Empleado>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetEmpleadoQuery(id);
            var result = await _queryDispatcher.Dispatch<GetEmpleadoQuery, InternalResponse<Empleado>>(query, cancellationToken);
            return result;
        }

        public async Task<InternalResponse<Empleado>> CreateAsync(Empleado moneda)
        {
            var command = new InsertEmpleadoCommand(moneda);
            var result = await _commandDispatcher.Dispatch<InsertEmpleadoCommand, InternalResponse<Empleado>>(command);
            return result;
        }

        public async Task<InternalResponse<Empleado>> UpdateAsync(Empleado moneda)
        {
            var command = new UpdateEmpleadoCommand(moneda);
            var result = await _commandDispatcher.Dispatch<UpdateEmpleadoCommand, InternalResponse<Empleado>>(command);
            return result;
        }

        public async Task<InternalResponse<object>> DeleteAsync(int id)
        {
            var command = new DeleteEmpleadoCommand(id);
            var result = await _commandDispatcher.Dispatch<DeleteEmpleadoCommand, InternalResponse<object>>(command);
            return result;
        }
    }
}
