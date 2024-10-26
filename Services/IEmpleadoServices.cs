using DB;

using Model.DB.Responses;

namespace Services
{
    public interface IEmpleadoServices
    {
        Task<InternalResponse<Empleado>> GetAsync(int id, CancellationToken cancellationToken);
        Task<InternalResponse<object>> DeleteAsync(int id);
        Task<InternalResponse<Empleado>> UpdateAsync(Empleado moneda);
        Task<InternalResponse<Empleado>> CreateAsync(Empleado moneda);
    }
}