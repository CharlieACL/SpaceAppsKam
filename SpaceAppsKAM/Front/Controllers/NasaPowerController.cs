using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Front.Service;

namespace Front.Controllers
{
    public class NasaPowerController : Controller
    {
        private readonly NasaPowerApi_Service _nasaPowerService;

        public NasaPowerController(NasaPowerApi_Service nasaPowerService)
        {
            _nasaPowerService = nasaPowerService;
        }

        // Vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para obtener los datos climáticos
        [HttpPost]
        public async Task<IActionResult> GetClimateData(string location)
        {
            // Crear los parámetros necesarios para la API
            string parameters = $"parameters=RH2M,T2M_MAX,T2M_MIN,WS10M&community=AG&longitude={location}&latitude={location}&format=JSON&start=2024&end=20241004";


            // Llamar al servicio de la API
            string data = await _nasaPowerService.GetNasaPowerDataAsync(parameters);

            // Procesar y generar recomendaciones (esto puedes mejorar con lógica propia)
            if (data != null)
            {
                ViewBag.Data = data;
                ViewBag.Recommendation = "Recomendación: Basado en los datos actuales, la mejor época para sembrar es la próxima semana, con un riego moderado cada 3 días.";
            }
            else
            {
                ViewBag.ErrorMessage = "No se pudieron obtener los datos climáticos. Intenta nuevamente.";
            }

            return View("Index");
        }
    }
}