// TurnosApp/Infrastructure/RepositorioEnMemoria.cs

using TurnosApp.Domain;

namespace TurnosApp.Infrastructure;

// Esta clase implementa la interfaz IRepositorioEnfermeras.
// Gracias a la interfaz, si en el futuro queremos guardar los datos en un archivo JSON
// o en una base de datos, solo tendríamos que crear una nueva clase que implemente
// la misma interfaz, y el resto de la aplicación no necesitaría cambios.
public class RepositorioEnMemoria : IRepositorioEnfermeras
{
    // Usamos un Diccionario para simular una base de datos en memoria.
    // La clave (Key) será el ID de la enfermera (string), y el valor (Value) será el objeto Enfermera completo.
    // Esto hace que la búsqueda por ID sea extremadamente rápida y eficiente.
    // Es 'private' para que nadie fuera de esta clase pueda acceder directamente a la "base de datos".
    private readonly Dictionary<string, Enfermera> _db = new();

    // Implementación del método 'Agregar' del contrato.
    public void Agregar(Enfermera enfermera)
    {
        // Antes de agregar, validamos una regla de negocio de persistencia: no permitir IDs duplicados.
        if (_db.ContainsKey(enfermera.Id))
        {
            throw new InvalidOperationException($"Ya existe una enfermera con la ID {enfermera.Id}");
        }
        _db[enfermera.Id] = enfermera;
    }

    // Implementación del método 'ObtenerPorId' del contrato.
    public Enfermera? ObtenerPorId(string id)
    {
        // TryGetValue es una forma segura de buscar en un diccionario.
        // Si encuentra la clave 'id', devuelve 'true' y asigna el valor a la variable 'enfermera'.
        // Si no, devuelve 'false'.
        // Usamos un operador ternario para devolver la enfermera encontrada o null si no existe.
        return _db.TryGetValue(id, out var enfermera) ? enfermera : null;
    }

    // Implementación del método 'Listar' del contrato.
    public IEnumerable<Enfermera> Listar()
    {
        // Devolvemos todos los valores (los objetos Enfermera) almacenados en el diccionario.
        return _db.Values;
    }
}