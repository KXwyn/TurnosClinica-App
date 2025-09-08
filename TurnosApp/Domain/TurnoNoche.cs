// TurnosApp/Domain/TurnoNoche.cs

namespace TurnosApp.Domain;

// TurnoNoche también hereda de Turno.
public sealed class TurnoNoche : Turno
{
    // El constructor de TurnoNoche llama al constructor base con las horas de 7pm a 7am del día siguiente.
    public TurnoNoche(DateTime fecha) : base(
        new DateTime(fecha.Year, fecha.Month, fecha.Day, 19, 0, 0),
        new DateTime(fecha.Year, fecha.Month, fecha.Day, 7, 0, 0).AddDays(1)) // Importante: el turno termina al día siguiente.
    { }
}