using Tareas;
using System.Text.Json;

//creo instancia HttpClient
HttpClient client = new HttpClient(); 

// Enviar Solicitud GET: Se envía una solicitud GET a la URL especificada y se verifica que la respuesta sea exitosa.
HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/"); 
response.EnsureSuccessStatusCode();

//Leer y Deserializar la Respuesta
string responseBody = await response.Content.ReadAsStringAsync();
List<Tarea> listTarea = JsonSerializer.Deserialize<List<Tarea>>(responseBody);

//muestro los datos
foreach (var tarea in listTarea)
{
    Console.WriteLine($"ID de Usuario: {tarea.userId} \n ID de tarea: {tarea.id} \n Tarea: {tarea.title} \n Estado de completada: {tarea.completed}");
    Console.WriteLine("/-------------------------------/");
}

//Ordena la lista segun pendientes
var listTareaOrdenada = listTarea.OrderBy(t => t.completed).ToList(); 

Console.WriteLine("/---------------/ Aqui las tareas ordenadas segun completadas /---------------/");

foreach (var tarea in listTareaOrdenada)
{

    Console.WriteLine($"ID de Usuario: {tarea.userId} \n ID de tarea: {tarea.id} \n Tarea: {tarea.title} \n Estado de completada: {tarea.completed}");
    Console.WriteLine("/-------------------------------/");

}

//Pongo el JSON con formato indentado
var opciones = new JsonSerializerOptions
{
    WriteIndented = true
};  

//Serializa la lista de tareas a JSON
string jsonTareas = JsonSerializer.Serialize(listTarea, opciones);
string rutaArchivo = "tareas.json";

//Guarda el JSON en un archivo
File.WriteAllText(rutaArchivo, jsonTareas);

//Muestro el json
Console.WriteLine(jsonTareas);