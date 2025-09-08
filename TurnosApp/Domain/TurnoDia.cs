// TurnosApp/Domain/TurnoDia.cs

namespace TurnosApp.Domain;

// La clase 'sealed' no puede ser heredada por otras clases. Es una buena práctica para clases concretas.
// La sintaxis ': Turno' indica que TurnoDia ES-UN tipo de Turno.
// Esto es HERENCIA.
public sealed class TurnoDia : Turno
{
    // Este constructor toma una fecha y llama al constructor de la clase base 'Turno'
    // con las horas específicas del turno de día (7am a 7pm).
    public TurnoDia(DateTime fecha) : base(
        new DateTime(fecha.Year, fecha.Month, fecha.Day, 7, 0, 0),
        new DateTime(fecha.Year, fecha.Month, fecha.Day, 19, 0, 0))
    { }
}