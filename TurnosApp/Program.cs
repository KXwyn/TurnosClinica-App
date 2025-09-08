// TurnosApp/Program.cs

// 1. Importamos todos los namespaces que contienen las clases y contratos que vamos a usar.
using System.Globalization;
using TurnosApp.Application;
using TurnosApp.Domain;
using TurnosApp.Infrastructure;

// 2. Creamos las instancias de nuestras implementaciones concretas.
//    Fíjate que las variables son del tipo de la INTERFAZ, no de la clase.
//    Esto nos permitiría cambiar 'RepositorioEnMemoria' por 'RepositorioEnJson'
//    en una sola línea, y el resto del programa seguiría funcionando.
IRepositorioEnfermeras repo = new RepositorioEnMemoria();
IAsignadorTurnos asignador = new AsignadorConDescanso();

Console.WriteLine("--- Bienvenido al Sistema de Gestión de Turnos ---");

// 3. Bucle principal de la aplicación. Se ejecutará infinitamente hasta que el usuario elija salir.
while (true)
{
    Console.WriteLine("\n--- Menú Principal ---");
    Console.WriteLine("1) Crear enfermera");
    Console.WriteLine("2) Asignar turno");
    Console.WriteLine("3) Listar todas las enfermeras");
    Console.WriteLine("4) Ver turnos de una enfermera");
    Console.WriteLine("0) Salir");
    Console.Write("Seleccione una opción: ");
    var op = Console.ReadLine();

    if (op == "0") break;

    // 4. Bloque try-catch para manejar errores de forma centralizada.
    //    Cualquier excepción lanzada desde nuestras capas (Domain, App, Infra)
    //    será capturada aquí, mostrando un mensaje amigable sin cerrar el programa.
    try
    {
        switch (op)
        {
            case "1":
                CrearEnfermera();
                break;
            case "2":
                AsignarTurno();
                break;
            case "3":
                ListarEnfermeras();
                break;
            case "4":
                VerTurnosDeEnfermera();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    catch (Exception ex)
    {
        // Mostramos el mensaje de la excepción. Nuestros mensajes de error son claros y descriptivos.
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
        Console.ResetColor();
    }
}

Console.WriteLine("\n¡Gracias por usar el sistema!");

// --- Métodos de ayuda para mantener el switch limpio ---

void CrearEnfermera()
{
    Console.Write("Ingrese ID de la enfermera: ");
    var id = Console.ReadLine()!;
    Console.Write("Ingrese Nombre de la enfermera: ");
    var nom = Console.ReadLine()!;

    var nuevaEnfermera = new Enfermera(id, nom);
    repo.Agregar(nuevaEnfermera);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("¡Enfermera creada con éxito!");
    Console.ResetColor();
}

void AsignarTurno()
{
    Console.Write("Ingrese ID de la enfermera: ");
    var idE = Console.ReadLine()!;
    var enfermera = repo.ObtenerPorId(idE) ?? throw new Exception($"La enfermera con ID '{idE}' no fue encontrada.");

    Console.Write("Ingrese Fecha (formato yyyy-MM-dd): ");
    var fechaStr = Console.ReadLine()!;
    var fecha = DateTime.ParseExact(fechaStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

    Console.Write("Ingrese Tipo de turno (D=día / N=noche): ");
    var tipo = Console.ReadLine()?.ToUpperInvariant();

    Turno nuevoTurno = tipo switch
    {
        "D" => new TurnoDia(fecha),
        "N" => new TurnoNoche(fecha),
        _ => throw new Exception("Tipo de turno no válido. Use 'D' para día o 'N' para noche.")
    };

    asignador.Asignar(enfermera, nuevoTurno);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Turno asignado a {enfermera.Nombre} con éxito.");
    Console.ResetColor();
}

void ListarEnfermeras()
{
    Console.WriteLine("\n--- Listado de Enfermeras Registradas ---");
    var todas = repo.Listar();
    if (!todas.Any())
    {
        Console.WriteLine("No hay enfermeras registradas.");
        return;
    }

    foreach (var enfermera in todas)
    {
        Console.WriteLine($"ID: {enfermera.Id}, Nombre: {enfermera.Nombre}, Turnos Asignados: {enfermera.Turnos.Count}");
    }
}

void VerTurnosDeEnfermera()
{
    Console.Write("Ingrese ID de la enfermera a consultar: ");
    var idC = Console.ReadLine()!;
    var enfermera = repo.ObtenerPorId(idC) ?? throw new Exception($"La enfermera con ID '{idC}' no fue encontrada.");

    Console.WriteLine($"\n--- Turnos de {enfermera.Nombre} (ID: {enfermera.Id}) ---");

    if (!enfermera.Turnos.Any())
    {
        Console.WriteLine("Esta enfermera no tiene turnos asignados.");
        return;
    }

    // Ordenamos los turnos por fecha de inicio para una mejor visualización
    foreach (var turno in enfermera.Turnos.OrderBy(t => t.Inicio))
    {
        string tipoTurno = turno.EsNocturno ? "Noche" : "Día";
        Console.WriteLine($" - Tipo: {tipoTurno}, Inicio: {turno.Inicio:yyyy-MM-dd HH:mm}, Fin: {turno.Fin:yyyy-MM-dd HH:mm}");
    }
}