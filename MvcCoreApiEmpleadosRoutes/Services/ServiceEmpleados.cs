using Newtonsoft.Json;
using NugetApiModelsMMT.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiEmpleadosRoutes.Services
{
    public class ServiceEmpleados
    {
        private string url;

        //necesitamos indicar el tipo de datos que vamos a leer
        private MediaTypeWithQualityHeaderValue header;

        public ServiceEmpleados(IConfiguration conf)
        {
            url = conf.GetValue<string>("ApiUrls:ApiEmpleados");
            header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados";
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //recuperamos el contenido en json
                    string json = await response.Content.ReadAsStringAsync();
                    //mediante newton serializamos
                    empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            Empleado empleado = new Empleado();

            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/" + idEmpleado;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    empleado = JsonConvert.DeserializeObject<Empleado>(json);
                    return empleado;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<string>> GetOficioAsync()
        {
            List<string> oficios = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/Oficios";
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    oficios = JsonConvert.DeserializeObject<List<string>>(json);
                    return oficios;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            List<Empleado> empleados = new List<Empleado>();

            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/EmpleadosByOficio/" + oficio;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
