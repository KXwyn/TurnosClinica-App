# Sistema de Gestión de Turnos para Clínica (Taller POO con C#)

Este proyecto es una aplicación de consola desarrollada en .NET 8 que resuelve el caso de negocio de la gestión de turnos para el personal de enfermería. La solución está diseñada aplicando los cuatro pilares de la Programación Orientada a Objetos para garantizar un código limpio, mantenible y escalable.

---

### Decisiones de Diseño Clave

El diseño se centra en la separación de responsabilidades y el desacoplamiento entre capas, siguiendo los principios SOLID.

-   **Abstracción:** Se utilizó una clase `abstracta` **Turno** para modelar el concepto general de un turno, y se definieron interfaces como **`IRepositorioEnfermeras`** y **`IAsignadorTurnos`** para desacoplar la lógica de negocio de los detalles de implementación.
-   **Encapsulamiento:** La clase **Enfermera** protege su estado interno (la lista de turnos). Las reglas de validación (como el choque de horarios) se encuentran dentro de la propia clase, asegurando la integridad de los datos.
-   **Herencia:** Las clases **TurnoDia** y **TurnoNoche** heredan de la clase base **Turno**, especializando su comportamiento para establecer las horas correctas y reutilizando la lógica común.
-   **Polimorfismo:** La aplicación utiliza la interfaz **`IAsignadorTurnos`** para permitir diferentes estrategias de asignación, demostrando polimorfismo a través de contratos.

---

### Estructura del Proyecto

El código fuente está organizado en un único proyecto de consola, utilizando carpetas para simular una arquitectura por capas y mantener una clara separación de responsabilidades.

```
/TurnosApp
|-- /Domain/ # Entidades, reglas de negocio y contratos (interfaces)
|-- /Application/ # Servicios que orquestan la lógica del dominio
|-- /Infrastructure/ # Implementaciones de persistencia (repositorio en memoria)
|-- Program.cs # Punto de entrada y manejo de la UI de consola
```

---

### Requisitos Previos

-   .NET 8 SDK

---

### Cómo Compilar y Ejecutar

1.  Clona este repositorio en tu máquina local.
2.  Abre una terminal en la carpeta raíz del proyecto.
3.  Ejecuta la aplicación con el siguiente comando:

    ```bash
    dotnet run --project TurnosApp
    ```

---

### Evidencias de Ejecución y Pruebas

A continuación se presentan capturas de pantalla que demuestran el funcionamiento de la aplicación.

**1. Menú Principal de la Aplicación**
![Menú Principal](docs/evidencias/MenuPrincipal.png)

**2. Flujo Exitoso: Creación y Asignación**
![Flujo Exitoso](docs/evidencias/FlujoExitoso.png)

**3. Manejo de Error de Regla de Negocio**
![Error Controlado](docs/evidencias/ErrorControlado.png)