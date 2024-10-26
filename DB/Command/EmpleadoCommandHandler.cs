﻿using DB.Command.Interfaces;
using DB.Context;
using Microsoft.EntityFrameworkCore;

using Model.DB.Responses;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DB.Command
{
    //En caso de que se requiera mas informacion en el request de la base de datos para 
    public record InsertEmpleadoCommand(Empleado instance);
    public record UpdateEmpleadoCommand(Empleado instance);
    public record DeleteEmpleadoCommand(int Id);

    public interface IEmpleadoCommandHandler :
    ICommandHandler<InsertEmpleadoCommand, InternalResponse<Empleado>>,
    ICommandHandler<UpdateEmpleadoCommand, InternalResponse<Empleado>>,
    ICommandHandler<DeleteEmpleadoCommand, InternalResponse<object>>
    {
    }

    public class EmpleadoCommandHandler : IEmpleadoCommandHandler
    {

        private readonly MainDbContext _ctx;

        public EmpleadoCommandHandler(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<InternalResponse<Empleado>> Handle(InsertEmpleadoCommand command)
        {
            _ctx.Empleados.Add(command.instance);
            _ctx.SaveChanges();
            return Task.FromResult(new InternalResponse<Empleado>(200, "Registro guardado", command.instance));
        }

        public async Task<InternalResponse<Empleado>> Handle(UpdateEmpleadoCommand command)
        {
            var result = await _ctx.Empleados.FirstOrDefaultAsync(x => x.Id == command.instance.Id);
            if (result == null)
            {
                return new InternalResponse<Empleado>(404, "Registro no encontrado", null);
            }
            //Segun la logica, actualizar los campos necesarios
            result.Direccion = command.instance.Direccion;
            result.Telefono = command.instance.Telefono;
            result.Email = command.instance.Email;
            _ctx.Update(result);
            await _ctx.SaveChangesAsync();
            return new InternalResponse<Empleado>(200, "Registro actualizado", result);

        }

        public async Task<InternalResponse<object>> Handle(DeleteEmpleadoCommand command)
        {
            var result = await _ctx.Empleados.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (result == null)
            {
                return new InternalResponse<object>(404, "Registro no encontrado", null);
            }
            _ctx.Remove(result);
            await _ctx.SaveChangesAsync();
            return new InternalResponse<object>(200, "Registro eliminado", result);

        }
    }
}
