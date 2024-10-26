using DB.Command;
using DB.Context;
using DB.Test.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DB.Test.Commands
{
    public class EmpleadoCommandTest : IClassFixture<MemoryContextDependencyFixture>
    {
        private MainDbContext _ctx;
        private Mock<ILogger<EmpleadoCommandHandler>> _mockLogger;


        public EmpleadoCommandTest(MemoryContextDependencyFixture fixture)
        {
            _ctx = fixture.Context;
            _mockLogger = new Mock<ILogger<EmpleadoCommandHandler>>();
        }

        //Escenario 1: Insercion simple sin errores de duplicidad
        [Fact]
        public async Task EmpleadoCommandHandlerTestInsercionSimple()
        {
            //Armado del escenario:
            var empleadoInstance = new Empleado {
            Email="franramvel@gmail.com",
            Direccion = "Rio Nilo Num. 87 Col. San Salvador",
            Telefono = "56784455"
            };
            var commandBaseClass = new InsertEmpleadoCommand(empleadoInstance);
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);
            
            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado insertado sin complicaciones

            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta no debe ser nula
            Assert.NotNull(result.Data);
            //Criterio 2: El objeto insertado debe tener un id asignado y el mensaje debe ser 200
            Assert.Equal(200, result.HttpCode);
            Assert.True(result.Data.Id>0);

            //Limpieza del caso
            _ctx.Remove(result.Data);
            _ctx.SaveChanges();
        }

        //Escenario 2: Insercion simple con errores de duplicidad en email
        [Fact]
        public async Task EmpleadoCommandHandlerTestInsercionEmailDuplicado()
        {
            //Armado del escenario:
            var empleadoInstance = new Empleado
            {
                Email = "franciscoramjive@gmail.com",
                Direccion = "Rio Nilo Num. 87 Col. San Salvador",
                Telefono = "56784455"
            };
            var commandBaseClass = new InsertEmpleadoCommand(empleadoInstance);

            var empleadoInstanceEmailDuplicado = new Empleado
            {
                Email = "franciscoramjive@gmail.com",
                Direccion = "Otra direccion",
                Telefono = "777845245"
            };
            var commandBaseClassEmailDuplicado = new InsertEmpleadoCommand(empleadoInstanceEmailDuplicado);


            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado insertado sin complicaciones en la primera insercion, pero en la segunda obtener error 409 de conflicto
            var result = await handler.Handle(commandBaseClass);
            var resultDuplicado = await handler.Handle(commandBaseClassEmailDuplicado);

            //Criterio 1: La respuesta no debe ser nula
            Assert.NotNull(resultDuplicado.Data);
            //Criterio 2: El objeto insertado debe tener un error 409
            Assert.Equal(409, resultDuplicado.HttpCode);

            //Limpieza del caso
            _ctx.Remove(result.Data!);
            _ctx.Remove(resultDuplicado.Data!);
            _ctx.SaveChanges();
        }


        //Escenario 3: Actualizacion simple sin errores de duplicidad
        [Fact]
        public async Task EmpleadoCommandHandlerTestActualizacionSimple()
        {
            //Armado del escenario:
            var empleadoInstance = new Empleado
            {
                Email = $"franram{Guid.NewGuid()}@gmail.com",
                Direccion = "Rio Nilo Num. 87 Col. San Salvador",
                Telefono = "56784455"
            };
            //Se agrega directamente a la base de datos, como se genera con guid, nunca habra errores de duplicidad
            _ctx.Add(empleadoInstance);
            _ctx.SaveChanges();

            //Se modifica el empleado
            empleadoInstance.Direccion = "Nueva Direccion";
            var commandBaseClass = new UpdateEmpleadoCommand(empleadoInstance);
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado insertado sin complicaciones

            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta no debe ser nula
            Assert.NotNull(result.Data);
            //Criterio 2: El objeto actualizado debe tener la nueva direccion y el mensaje debe ser 200
            Assert.Equal(200, result.HttpCode);
            Assert.Equal("Nueva Direccion",result.Data.Direccion);

            //Limpieza del caso
            _ctx.Remove(result.Data);
            _ctx.SaveChanges();
        }

        //Escenario 4: Actualizacion simple con errores de duplicidad en email
        [Fact]
        public async Task EmpleadoCommandHandlerTestActualizacionEmailDuplicado()
        {
            //Armado del escenario:
            var empleadoInstance = new Empleado
            {
                Email = $"franram{Guid.NewGuid()}@gmail.com",
                Direccion = "Rio Nilo Num. 87 Col. San Salvador",
                Telefono = "56784455"
            };
            var empleadoPreExistenteInstance = new Empleado
            {
                Email = $"franram{Guid.NewGuid()}@gmail.com",
                Direccion = "Otra Direccion",
                Telefono = "56784455"
            };
            //Se agrega directamente a la base de datos, como se genera con guid, nunca habra errores de duplicidad
            _ctx.Add(empleadoInstance);
            _ctx.Add(empleadoPreExistenteInstance);
            _ctx.SaveChanges();

            //Se modifica el empleado instance con el mismo email que el empleado pre existente
            empleadoInstance.Email = empleadoPreExistenteInstance.Email;
            var commandBaseClass = new UpdateEmpleadoCommand(empleadoInstance);
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado actualizado con error

            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta no debe ser nula
            Assert.NotNull(result.Data);
            //Criterio 2: El objeto actualizado debe tener error de conflicto 409
            Assert.Equal(409, result.HttpCode);

            //Limpieza del caso
            _ctx.Remove(empleadoInstance);
            _ctx.Remove(empleadoPreExistenteInstance);
            _ctx.SaveChanges();
        }

        //Escenario 5: Actualizacion simple con errores de objeto no encontrado
        [Fact]
        public async Task EmpleadoCommandHandlerTestActualizacionNoEncontrado()
        {
            //Armado del escenario:
            var empleadoInstance = new Empleado
            {
                Id = int.MaxValue, //Se toma un entero que se sabe, no existe en la base de datos, en este caso, el maximo valor de un int
                Email = $"franram{Guid.NewGuid()}@gmail.com",
                Direccion = "Rio Nilo Num. 87 Col. San Salvador",
                Telefono = "56784455"
            };

            //Se agrega directamente a la base de datos, como se genera con guid, nunca habra errores de duplicidad
            var commandBaseClass = new UpdateEmpleadoCommand(empleadoInstance);
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado actualizado con error

            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta debe ser nula
            Assert.Null(result.Data);
            //Criterio 2: El objeto actualizado debe tener error de no encontrado 404
            Assert.Equal(404, result.HttpCode);

        }

        //Escenario 8: Eliminación simple 
        [Fact]
        public async Task EmpleadoCommandHandlerTestEliminacionSimple()
        {

            //Armado del escenario:
            var empleadoInstance = new Empleado
            {
                Email = $"franram{Guid.NewGuid()}@gmail.com",
                Direccion = "Rio Nilo Num. 87 Col. San Salvador",
                Telefono = "56784455"
            };
            //Se agrega directamente a la base de datos, como se genera con guid, nunca habra errores de duplicidad
            _ctx.Add(empleadoInstance);
            _ctx.SaveChanges();

            var commandBaseClass = new DeleteEmpleadoCommand(empleadoInstance.Id);
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado eliminado
            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta debe ser nula
            Assert.Null(result.Data);
            //Criterio 2: El objeto actualizado debe tener un mensaje 200
            Assert.Equal(200, result.HttpCode);

        }


        //Escenario 7: Eliminación simple con errores de objeto no encontrado
        [Fact]
        public async Task EmpleadoCommandHandlerTestEliminacionNoEncontrado()
        {


            //Se agrega directamente a la base de datos, como se genera con guid, nunca habra errores de duplicidad
            var commandBaseClass = new DeleteEmpleadoCommand(int.MaxValue);//Se toma un entero que se sabe, no existe en la base de datos, en este caso, el maximo valor de un int
            var handler = new EmpleadoCommandHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado eliminado con error
            var result = await handler.Handle(commandBaseClass);

            //Criterio 1: La respuesta debe ser nula
            Assert.Null(result.Data);
            //Criterio 2: El objeto actualizado debe tener error de no encontrado 404
            Assert.Equal(404, result.HttpCode);

        }


    }
}
