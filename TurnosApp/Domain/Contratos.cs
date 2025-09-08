// TurnosApp/Domain/Contratos.cs

namespace TurnosApp.Domain;

// Interfaz para el servicio de asignación de turnos.
// Define un contrato que dice: "Cualquier clase que quiera asignar turnos
// DEBE tener un método Asignar que reciba una Enfermera y un Turno".
// Esto prepara el terreno para el POLIMORFISMO.
public interface IAsignadorTurnos
{
    void Asignar(Enfermera enfermera, Turno turno);
}

// Interfaz para el Repositorio de Enfermeras.
// Abstrae la lógica de persistencia. Define las operaciones básicas que se
// pueden realizar con los datos de las enfermeras, sin especificar si se
// guardan en memoria, en un archivo JSON o en una base de datos.
// Esto es ABSTRACCIÓN pura.
public interface IRepositorioEnfermeras
{
    void Agregar(Enfermera enfermera);
    Enfermera? ObtenerPorId(string id);
    IEnumerable<Enfermera> Listar();
}