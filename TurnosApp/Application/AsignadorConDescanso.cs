// TurnosApp/Application/AsignadorConDescanso.cs

using TurnosApp.Domain;

namespace TurnosApp.Application;

// Esta clase implementa la interfaz IAsignadorTurnos. 
// Esto significa que "promete" tener un método Asignar que coincide con la firma del contrato.
// Esto es POLIMORFISMO a través de interfaces.
public class AsignadorConDescanso : IAsignadorTurnos
{
    // El método 'Asignar' contiene la lógica específica de la regla de negocio del descanso.
    public void Asignar(Enfermera enfermera, Turno turno)
    {
        // 1. Obtenemos el último turno que tuvo la enfermera, ordenando por fecha de finalización.
        var ultimoTurno = enfermera.Turnos.OrderByDescending(t => t.Fin).FirstOrDefault();

        // 2. Aplicamos la regla de negocio de la aplicación.
        if (ultimoTurno != null && ultimoTurno.EsNocturno)
        {
            // Si el último turno fue nocturno, verificamos si la fecha de inicio del nuevo turno
            // es el mismo día en que terminó el turno nocturno.
            // (Ej: Turno nocturno del día 10 termina a las 7am del día 11. No puede trabajar el día 11).
            if (turno.Inicio.Date == ultimoTurno.Fin.Date)
            {
                throw new InvalidOperationException("La enfermera debe tener un día completo de descanso después de un turno nocturno.");
            }
        }

        // 3. Si todas las reglas de la aplicación se cumplen, delegamos la asignación final
        // a la propia entidad Enfermera. Ella se encargará de sus propias reglas internas,
        // como la validación de choques de horario.
        // ¡Esto es un excelente ejemplo de separación de responsabilidades!
        enfermera.AsignarTurno(turno);
    }
}