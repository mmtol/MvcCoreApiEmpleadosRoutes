using Microsoft.AspNetCore.Mvc;
using MvcCoreApiEmpleadosRoutes.Services;
using NugetApiModelsMMT.Models;

namespace MvcCoreApiEmpleadosRoutes.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Servidor()
        {
            List<string> oficios = await service.GetOficioAsync();
            List<Empleado> empleados = await service.GetEmpleadosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Servidor(string oficio)
        {
            List<string> oficios = await service.GetOficioAsync();
            List<Empleado> empleados = await service.GetEmpleadosOficioAsync(oficio);
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }

        public async Task<IActionResult> Details(int idempleado)
        {
            Empleado emp = await service.FindEmpleadoAsync(idempleado);
            return View(emp);
        }
    }
}
