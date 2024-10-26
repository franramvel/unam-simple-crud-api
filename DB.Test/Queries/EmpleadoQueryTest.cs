using DB.Command;
using DB.Context;
using DB.Query;
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
    public class EmpleadoQueryTest : IClassFixture<MemoryContextDependencyFixture>
    {
        private MainDbContext _ctx;
        private Mock<ILogger<EmpleadoQueryHandler>> _mockLogger;


        public EmpleadoQueryTest(MemoryContextDependencyFixture fixture)
        {
            _ctx = fixture.Context;
            _mockLogger = new Mock<ILogger<EmpleadoQueryHandler>>();
        }

        //Escenario 1: Obtencion simple 
        [Fact]
        public async Task EmpleadoCommandHandlerTestObtencionSimple()
        {
            //Armado del escenario:
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


            var queryBaseClass = new GetEmpleadoQuery(empleadoInstance.Id);
            var handler = new EmpleadoQueryHandler(_ctx, _mockLogger.Object);
            
            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado insertado sin complicaciones

            var result = await handler.Handle(queryBaseClass, new CancellationToken());

            //Criterio 1: La respuesta no debe ser nula
            Assert.NotNull(result.Data);
            //Criterio 2: El objeto obtenido debe tener codigo 200
            Assert.Equal(200, result.HttpCode);

            //Limpieza del caso
            _ctx.Remove(result.Data);
            _ctx.SaveChanges();
        }

        //Escenario 2: Obtencion con error por objeto no encontrado
        [Fact]
        public async Task EmpleadoCommandHandlerTestObtencionNoEncontrada()
        {


            var queryBaseClass = new GetEmpleadoQuery(int.MaxValue); //Se busca un id inexistente
            var handler = new EmpleadoQueryHandler(_ctx, _mockLogger.Object);

            //Ejecucion de prueba
            //Resultado esperado: Obtencion del resultado con error 404

            var result = await handler.Handle(queryBaseClass, new CancellationToken());

            //Criterio 1: La respuesta debe ser nula
            Assert.Null(result.Data);
            //Criterio 2: El parametro de http code debe ser 404
            Assert.Equal(404, result.HttpCode);
        }




    }
}
