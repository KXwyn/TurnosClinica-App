// TurnosApp/Application/AsignadorConDescanso.cs

using TurnosApp.Domain;

namespace TurnosApp.Application;

// Esta clase implementa la interfaz IAsignadorTurnos. 
// Esto significa que "promete" tener un m�todo Asignar que coincide con la firma del contrato.
// Esto es POLIMORFISMO a trav�s de interfaces.
public class AsignadorConDescanso : IAsignadorTurnos
{
    // El m�todo 'Asignar' contiene la l�gica espec�fica de la regla de negocio del descanso.
    public void Asignar(Enfermera enfermera, Turno turno)
    {
        // 1. Obtenemos el �ltimo turno que tuvo la enfermera, ordenando por fecha de finalizaci�n.
        var ultimoTurno = enfermera.Turnos.OrderByDescending(t => t.Fin).FirstOrDefault();

        // 2. Aplicamos la regla de negocio de la aplicaci�n.
        if (ultimoTurno != null && ultimoTurno.EsNocturno)
        {
            // Si el �ltimo turno fue nocturno, verificamos si la fecha de inicio del nuevo turno
            // es el mismo d�a en que termin� el turno nocturno.
            // (Ej: Turno nocturno del d�a 10 termina a las 7am del d�a 11. No puede trabajar el d�a 11).
            if (turno.Inicio.Date == ultimoTurno.Fin.Date)
            {
                throw new InvalidOperationException("La enfermera debe tener un d�a completo de descanso despu�s de un turno nocturno.");
            }
        }

        // 3. Si todas las reglas de la aplicaci�n se cumplen, delegamos la asignaci�n final
        // a la propia entidad Enfermera. Ella se encargar� de sus propias reglas internas,
        // como la validaci�n de choques de horario.
        // �Esto es un excelente ejemplo de separaci�n de responsabilidades!
        enfermera.AsignarTurno(turno);
    }
}