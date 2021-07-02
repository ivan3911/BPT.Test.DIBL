using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.Client
{
    class Program
    {
        static HttpClient client = new HttpClient();
        public static readonly string pathStudent= "https://localhost:44304/api/estudiantes/";
        public static readonly string pathAssignment = "https://localhost:44304/api/asignaciones/";
        public static readonly string pathAssignmentStudent = "https://localhost:44304/api/asignacionesestudiante/"; 


        static void Main()
        {
            
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            int id_e1=0;
            int id_e2 = 0;

            int id_a1 = 0;
            int id_a2 = 0;

            int id_ae1 = 0;
            int id_ae2= 0;

            //Se crean Estudiantes.
            Estudiante e1 = new Estudiante() { Nombre = "Guillermo Perez", FechaNacimiento = new DateTime(2000, 01, 01, 12, 20, 30, 000) };
            id_e1 = await PostStudentsAsync(e1, pathStudent);
            Estudiante e2 = new Estudiante() { Nombre = "Luis Sanchez", FechaNacimiento = new DateTime(1995, 05, 02, 08, 20, 30, 000) };
            id_e2 = await PostStudentsAsync(e2, pathStudent);

            //Se crean asignaciones
            Asignacion a1 = new Asignacion() { Nombre = "Asignacion 1" };
            id_a1 = await PostAssignmentsAsync(a1, pathAssignment);
            Asignacion a2 = new Asignacion() { Nombre = "Asignacion 2" };
            id_a2 = await PostAssignmentsAsync(a2, pathAssignment);

            //Se crean las AsignacionesEstudiantes
            AsignacionesEstudiante ae1 = new AsignacionesEstudiante() { IdEstudiante = id_e1, IdAsignacion = id_a1 };
            id_ae1 = await PostAssignmentStudentAsync(ae1, pathAssignmentStudent);

            AsignacionesEstudiante ae2 = new AsignacionesEstudiante() { IdEstudiante = id_e2, IdAsignacion = id_a2 };
            id_ae2 = await PostAssignmentStudentAsync(ae2, pathAssignmentStudent);


            //Se listan estudiantes
            Console.WriteLine("\nListado de todos los estudiantes:");
            await GetAllStudentsAsync(pathStudent);

            //Se listan asignaciones
            Console.WriteLine("\nListado de todas las asignaciones ");
            await GetAllAssignmentsAsync(pathAssignment);

            //Borrado de estudiante
            await DeleteStudentsAsync(pathStudent + id_e1);
            await DeleteStudentsAsync(pathStudent + id_e2);

            ////Borrado de asignacion
            await DeleteAssignmentsAsync(pathAssignment + id_a1);
            await DeleteAssignmentsAsync(pathAssignment + id_a2);


            //Se actualiza estudiante
            Estudiante eA1 = new Estudiante() { Nombre = "Juan Perez Hernandez Hernandez", FechaNacimiento = new DateTime(1989, 01, 01, 12, 20, 30, 000) };
            await PutStudentsAsync(eA1, pathStudent + id_e1);

            //Se actualiza Asignacion
            Asignacion upd_a1 = new Asignacion() { Nombre = "Actualizacion de asignacion" };
            await PutAssignmentsAsync(upd_a1, pathAssignment + id_a1);

        }
        static async Task GetAllStudentsAsync(string path)
        {
            List<Estudiante> students = new List<Estudiante>();
            using (var Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode) 
                {
                    var data = await response.Content.ReadAsStringAsync();
                   students = JsonConvert.DeserializeObject<List<Estudiante>>(data);
                }
            }
            foreach (Estudiante e in students) 
            {
                Console.WriteLine($"Nombre: [{e.Nombre}]\nFecha Nacimiento: [{e.FechaNacimiento}]");
            }
        }

        static async Task<int> PostStudentsAsync( Estudiante student, string path)
        {
            using (var Client = new HttpClient())
            {
                var id = 0;
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PostAsJsonAsync(path, student);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Regristro agregado correctamente con id [{result}]");
                    int.TryParse(result, out id);
                }
                return id;
            }
        }

        static async Task PutStudentsAsync( Estudiante student, string path)
        {
            using (var Client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(path, student);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Regristro actualizado correctamente con id [{result}]");
                }
            }
        }

        static async Task DeleteStudentsAsync(string path)
        {
            using (var Client = new HttpClient())
            {
                var response = await client.DeleteAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Se eliminó registro correctamente");
                }
            }
        }


        static async Task GetAllAssignmentsAsync(string path)
        {
            List<Asignacion> Assignments = new List<Asignacion>();
            using (var Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                     Assignments = JsonConvert.DeserializeObject<List<Asignacion>>(data);
                }
            }
            foreach (Asignacion e in Assignments)
            {
                Console.WriteLine($"Nombre: [{e.Nombre}]");
            }
        }

        static async Task<int> PostAssignmentsAsync(Asignacion Assignment, string path)
        {
            using (var Client = new HttpClient())
            {
                var id = 0;
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PostAsJsonAsync(path, Assignment);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Regristro agregado correctamente con id [{result}]");
                    int.TryParse(result, out id);
                }
                return id;
            }
        }

        static async Task PutAssignmentsAsync(Asignacion assignment, string path)
        {
            using (var Client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(path, assignment);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Regristro actualizado correctamente con id [{result}]");
                }
            }
        }

        static async Task DeleteAssignmentsAsync(string path)
        {
            using (var Client = new HttpClient())
            {
                var response = await client.DeleteAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Se eliminó registro correctamente");
                }
            }
        }


        static async Task<int> PostAssignmentStudentAsync(AsignacionesEstudiante assignmentStudent, string path)
        {
            using (var Client = new HttpClient())
            {
                var id = 0;
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PostAsJsonAsync(path, assignmentStudent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Regristro agregado correctamente con id [{result}]");
                    int.TryParse(result, out id);
                }
                return id;
            }
        }

        static async Task DeleteAssignmentStudentAsync(string path)
        {
            using (var Client = new HttpClient())
            {
                var response = await client.DeleteAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\nSe eliminó registro correctamente");
                }
            }
        }
    }
}
