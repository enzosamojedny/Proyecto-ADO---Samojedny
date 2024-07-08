using Microsoft.AspNetCore.Mvc;
using Proyecto_ADO.Models;
using System.Diagnostics;
using Capa_de_Datos.Repository;
using Capa_de_Datos.Entities;

namespace Proyecto_ADO.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmpleadosRepository _repository;
        private readonly IEmpleadoRepository _repositorio;
        private ICreateEmpleadoRepository _createEmpleadoRepository;
        public HomeController(IEmpleadosRepository repository, IEmpleadoRepository repositorio, ICreateEmpleadoRepository createEmpleadoRepository)
        {
            _repository = repository;
            _repositorio = repositorio;
            _createEmpleadoRepository = createEmpleadoRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> DisplayEmpleados()
        {
            List<Empleado> listaEmpleados = await _repository.GetAllEmpleados();
            return View(listaEmpleados);
        }
        public async Task<IActionResult> DisplayEmpleado(int id)
        {
            Empleado empleado = await _repositorio.GetEmpleado(id);
            return View(empleado);
        }

        [HttpGet]
        public IActionResult CreateEmpleado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }

            await _createEmpleadoRepository.CreateEmpleado(empleado.NombreCompleto, empleado.DNI, empleado.Edad, empleado.Casado, empleado.Salario.GetValueOrDefault());

            return RedirectToAction(nameof(DisplayEmpleados));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
