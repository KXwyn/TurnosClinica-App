// TurnosApp/Domain/Turno.cs

namespace TurnosApp.Domain;

// Usamos 'abstract' porque un 'Turno' es un concepto. 
// No se puede crear un 'Turno' genérico, solo turnos concretos como TurnoDia o TurnoNoche.
// Esto es ABSTRACCIÓN.
public abstract class Turno
{
    // Propiedades de solo lectura (get) para asegurar que una vez creado el turno, no se pueda modificar.
    // Esto promueve la inmutabilidad y hace el código más seguro.
    public DateTime Inicio { get; }
    public DateTime Fin { get; }

    // El constructor es 'protected', lo que significa que solo puede ser llamado por esta clase
    // o por clases que hereden de ella (como TurnoDia y TurnoNoche).
    protected Turno(DateTime inicio, DateTime fin)
    {
        // Validación básica para proteger la integridad del objeto.
        if (fin <= inicio)
            throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");

        Inicio = inicio;
        Fin = fin;
    }

    // Propiedad calculada para saber si el turno es nocturno.
    public bool EsNocturno => Inicio.Hour == 19;

    // Lógica para detectar si dos turnos se solapan en el tiempo.
    // Este método es crucial para la regla de negocio de no permitir choques de horarios.
    public bool ChocaCon(Turno otro)
    {
        // Dos turnos NO chocan si uno termina antes de que el otro empiece.
        // Si esa condición NO se cumple, entonces sí chocan.
        return !(this.Fin <= otro.Inicio || otro.Fin <= this.Inicio);
    }
}