// TurnosApp/Domain/Enfermera.cs

namespace TurnosApp.Domain;

public class Enfermera
{
    // Propiedades p�blicas para acceder a los datos de forma controlada.
    public string Id { get; } // C�dula
    public string Nombre { get; private set; }

    // Esta es la lista de turnos. Es PRIVADA para protegerla de modificaciones externas.
    // Solo la clase Enfermera puede agregar o quitar elementos de esta lista.
    // Esto es ENCAPSULAMIENTO.
    private readonly List<Turno> _turnos = new();

    // Para permitir que el mundo exterior VEA los turnos pero no los MODIFIQUE,
    // exponemos una copia de solo lectura de la lista.
    public IReadOnlyCollection<Turno> Turnos => _turnos.AsReadOnly();

    public Enfermera(string id, string nombre)
    {
        // Validaciones en el constructor para asegurar que un objeto Enfermera
        // siempre se cree en un estado v�lido.
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("El Id es requerido.");
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El Nombre es requerido.");

        Id = id;
        Nombre = nombre;
    }

    // Este es el m�todo "guardi�n". Es la �nica puerta de entrada para modificar la lista de turnos.
    // Contiene la l�gica de negocio que protege el estado del objeto.
    public void AsignarTurno(Turno turno)
    {
        // Regla de negocio: No se pueden asignar turnos que se superpongan.
        if (_turnos.Any(t => t.ChocaCon(turno)))
        {
            throw new InvalidOperationException("La enfermera ya tiene un turno que choca con este horario.");
        }

        _turnos.Add(turno);
        // Podr�amos a�adir m�s l�gica aqu� en el futuro, y el resto del programa no se enterar�a.
        // Ese es el poder del encapsulamiento.
    }
}