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

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/empleados";
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            string request = "api/empleados/" + idEmpleado;
            Empleado empleado = await CallApiAsync<Empleado>(request);
            return empleado;
        }

        public async Task<List<string>> GetOficioAsync()
        {
            string request = "api/empleados/Oficios";
            List<string> oficios = await CallApiAsync<List<string>>(request);
            return oficios;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "api/empleados/EmpleadosByOficio/" + oficio;
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<Empleado>> GetEmpleadosSalarioDepartamentoAsync(int salario, int departamento)
        {
            string request = "api/empleados/EmpleadosBySalarioDepartamento/" + salario + "/" + departamento;
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }
    }
}
