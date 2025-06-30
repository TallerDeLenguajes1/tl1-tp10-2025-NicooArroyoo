using System.Text.Json;
using EspacioClases;

HttpClient ClienteConsumo = new HttpClient(); 
var UrlApi = "https://jsonplaceholder.typicode.com/users/"; 

HttpResponseMessage respuesta = await ClienteConsumo.GetAsync(UrlApi);  

respuesta.EnsureSuccessStatusCode(); // Comprueba que la respuesta de la api sea valida

string CuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();
List<Usuario> listUsuariotemp = JsonSerializer.Deserialize<List<Usuario>>(CuerpoRespuesta); 

Usuario[] listUsuario = new Usuario[5]; // declaro un arreglo de la clase usuario para asi tener los 5 que necesito 


for (int i = 0; i < 5; i++)
{
    listUsuario[i] = listUsuariotemp[i];
    Console.WriteLine($"Nombre: {listUsuario[i].name} \n Correo Electronico: {listUsuario[i].email} \n Domicilio: {listUsuario[i].address.city} {listUsuario[i].address.street}");
    Console.WriteLine("/-----------/");

}

var opciones = new JsonSerializerOptions
{
    WriteIndented = true
};


string ListaUsuariosJsoneada = JsonSerializer.Serialize(listUsuario, opciones);
string ruta = "usuarios.json";

await File.WriteAllTextAsync(ruta, ListaUsuariosJsoneada);
Console.WriteLine("json guardado correctamente");