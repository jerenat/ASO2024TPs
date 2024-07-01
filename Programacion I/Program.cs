using System;
using System.Globalization;
using System.IO;
using System.Threading;
using static Program;

public class Program
{


    // -- archivos donde se alojarán los menues
    public static string notasdb = "notasdb.txt";                       // base de datos de notas
    public static string materiadb = "materiadb.txt";                   // base de datos de materias
    public static string materiasiddb = "materias_id_db.txt";           // base de datos de los ID de las materias
    public static string alumnodb = "inscripcionesdb.txt";              // base de datos de las inscripciones de los alumnos

    // -- Cambiar COLOR de Texto
    public static void CambiarColorTexto(string texto, string color)
    {
        switch (color)
        {
            case "green":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(texto);
                Console.ResetColor();
                break;
            case "red":
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(texto);
                Console.ResetColor();
                break;
            case "blue":
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(texto);
                Console.ResetColor();
                break;
            case "yellow":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(texto);
                Console.ResetColor();
                break;
            case "magenta":
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(texto);
                Console.ResetColor();
                break;
            default:
                Console.WriteLine("Error!, ingrese los valores adecuados");
                break;
        }

    }


    // -- Back to Main Menu
    public static void BackToMenu()
    {
        Console.Clear();
        Main();
    }


    // -- Obtener FECHA ACTUAL
    public static DateTime obtenerFecha = DateTime.Now;


    // -- Menu GENÉRICO según opcion
    public static void SelectMenu(string menu)
    {
        // -- Pantalla principal
        Console.WriteLine($"=== Menu {menu} ===");
        Console.WriteLine($"1. Dar de alta un(a) {menu}");
        Console.WriteLine($"2. Dar de baja un(a) {menu}");
        Console.WriteLine($"3. Modificar un(a) {menu}");
        Console.WriteLine($"4. Volver");
    }

    // -- Menu Principal
    public static void MenuPrincipal()
    {
        // -- Pantalla de Inicio
        Console.WriteLine("==================== Aula Virtual 2.0 ===================");
        Console.WriteLine("=========================================================\n");

        // -- Fecha y Hora actual
        Console.WriteLine($"Fecha y Hora actual: {obtenerFecha}\n");

        // -- Menu Principal
        Console.WriteLine("=== Menu Principal ===");
        Console.WriteLine("1. Materias");
        Console.WriteLine("2. Alumnos");
        Console.WriteLine("3. Notas");
        Console.WriteLine("4. Salir\n");
        Console.Write("Por favor, elija una opción: ");
    }



    // -- Obtener el ID de LAS MATERIAS de la Universidad (o por lo menos, las ingresadas hasta el momento...

    // estructura
    public struct Listados
    {
        public int Indice;              // indice
        public string Nombre;    // nombre materia
    }

    // retorna menu
    public static List<Listados> RetornarListaIndiceMaterias()
    {
        List<Listados> listaMaterias = new List<Listados>();
        using (StreamReader sr = new StreamReader(materiasiddb))
        {
            string? linea = sr.ReadLine();

            while (linea != null)
            {
                string[] materiaArchivo = linea.Split(',');
                Listados materiaStruct = new Listados();
                materiaStruct.Indice = int.Parse(materiaArchivo[0]);                // indice
                materiaStruct.Nombre = materiaArchivo[1];                    // nombre materia
                listaMaterias.Add(materiaStruct);
                linea = sr.ReadLine();
            }
        }
        return listaMaterias;
    }


    /// ================== MATERIAS ====================

    // -- Datos de las materias
    public struct Materias
    {
        public int Indice;              // indice
        public int IndiceMateria;       // indice -> legajo
        public int NombreMateria;    // nombre materia
        public string MateriaActiva;    // actividad? -> activo / inactivo
    }


    // -- Enlistar Materias
    public static List<Materias> RetornarListaMaterias(string materia)
    {
        List<Materias> listaMaterias = new List<Materias>();
        using (StreamReader sr = new StreamReader(materia))
        {
            string? linea = sr.ReadLine();

            while (linea != null)
            {
                string[] materiaArchivo = linea.Split(',');
                Materias materiaStruct = new Materias();
                materiaStruct.Indice = int.Parse(materiaArchivo[0]);                // indice
                materiaStruct.IndiceMateria = int.Parse(materiaArchivo[1]);         // indice -> legajo
                materiaStruct.NombreMateria = int.Parse(materiaArchivo[2]);         // nombre materia
                materiaStruct.MateriaActiva = materiaArchivo[3];                    // activo?
                listaMaterias.Add(materiaStruct);
                linea = sr.ReadLine();
            }
        }
        return listaMaterias;
    }

    // -- Obtener SOLO EL INDICE de las materias y RETORNARLOS
    public static int ObtenerIndice()
    {
        int materiaid = 0;

        List<Materias> listaMaterias = new List<Materias>();
        using (StreamReader sr = new StreamReader(materiadb))
        {
            string? linea = sr.ReadLine();
            bool found = false;

            while (linea != null)
            {
                string[] materiaArchivo = linea.Split(',');
                Materias materiaStruct = new Materias();
                if (materiaArchivo.Length > 0 && int.TryParse(materiaArchivo[0], out int indice))
                {
                    materiaStruct.Indice = indice;  // indice
                    found = true;
                    materiaid = indice;
                }
                linea = sr.ReadLine();
            }

            if (!found)
            {
                materiaid = 0;
            }
        }
        return materiaid;
    }


    // -- Escribir los parámetros en el archivo
    public static void EscribirMaterias(List<Materias> materias, bool concat, string accion)
    {

        try
        {
            using (StreamWriter escritor = new StreamWriter(materiadb, concat))
            {
                foreach (var mat in materias)
                {

                    // --                   1,              22486,                      Matematica,             activo?
                    escritor.WriteLine(mat.Indice + "," + mat.IndiceMateria + "," + mat.NombreMateria + "," + mat.MateriaActiva);
                }
            }

            Console.WriteLine($"Materia {accion} exitosamente.");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    // -- retornar Menu
    public static void RetornarMenuMaterias()
    {

        Thread.Sleep(3000); // espera 3segs
        Console.Clear();    // limpiar pantalla
        MenuMaterias();     // vuelve a llamar al menu

    }




    // -- Menu de Materia
    public static void MenuMaterias()
    {
        // -- variable global
        bool existe;

        // -- Pantalla principal
        SelectMenu("Materia");


        // -- variable de opcion
        int materiaOpcion;
        materiaOpcion = Convert.ToInt32(Console.ReadLine());

        switch (materiaOpcion)
        {

            // -- Dar de alta materia
            case 1:

                Materias materias = new Materias();

                // -- indice
                materias.Indice = (ObtenerIndice() + 1);

                // -- indice materia -> legajo
                Console.WriteLine("Ingrese un legajo:");
                string legajo = Console.ReadLine();

                // -- verifica que se haya ingresado un digito o digitos
                while (!int.TryParse(legajo, out materias.IndiceMateria))
                {
                    Console.WriteLine("No ingresó un número entero, Ingrese un número de legajo:");
                    legajo = Console.ReadLine()!;
                }

                // -- Nombre de la materia
                Console.WriteLine("Ingrese el ID de la materia:\n");


                // -- Obtiene el listado de las materias según ID
                List<Listados> listados = RetornarListaIndiceMaterias();
                foreach (Listados materiasEnLista in listados)
                {
                    Console.WriteLine("ID: "+materiasEnLista.Indice+". "+ materiasEnLista.Nombre);
                }
                // -- ingresa el ID
                Console.Write("\nIngrese ID: ");
                materias.NombreMateria = Convert.ToInt32(Console.ReadLine());


                // -- actividad de la materia
                Console.WriteLine("Esta activa?");
                Console.WriteLine("[1. Si / 2. No]");
                string actividadMateria = Console.ReadLine();

                // verifica si lo ingresado es un digito
                while(!int.TryParse(actividadMateria, out int esUnDigito))
                {
                    Console.WriteLine("Por favor, ingrese un número:");
                    Console.WriteLine("[1. Si / 2. No]");
                    actividadMateria = Console.ReadLine();
                }

                // si todo salio bien, lo convierte a entero:
                int actividadDigitoMateria = Convert.ToInt32(actividadMateria);


                // si todo salio bien
                if (actividadDigitoMateria == 1)
                {
                    materias.MateriaActiva = "activa";
                }
                else if(actividadDigitoMateria == 2)
                {
                    materias.MateriaActiva = "inactiva";
                }

                Materias nuevaMateria = materias;
                List<Materias> agregarNuevaMateria = new List<Materias> { nuevaMateria };

                EscribirMaterias(agregarNuevaMateria, true, "anotada");
                RetornarMenuMaterias(); // retorna al menu de materias
                break;

            // -- Dar de baja materia
            case 2:

                // limpiar pantalla
                Console.Clear();

                // el usuario ingresa el legajo
                Console.WriteLine("Por favor, ingrese legajo:");

                int getIndice = Convert.ToInt32(Console.ReadLine());

                // booleano: si existe las materias
                existe = false;

                // retorna lista de materias
                List<Materias> lmateria = RetornarListaMaterias(materiadb);

                // enlista todas las materias
                foreach (Materias materiasEnLista in lmateria)
                {
                    // si la materia coincide con el indice -> materia
                    if (getIndice == materiasEnLista.IndiceMateria)
                    {

                        if (materiasEnLista.MateriaActiva == "activa")
                        {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine(materiasEnLista.Indice);
                            Console.WriteLine(materiasEnLista.IndiceMateria);
                            Console.WriteLine(materiasEnLista.NombreMateria);
                            CambiarColorTexto(materiasEnLista.MateriaActiva, "green");
                            existe = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existe la materia!");
                        existe = false;
                    }
                }

                if (existe)
                {
                    Console.WriteLine("Seleccione el ID de la materia que desea dar de baja:");
                    int materiaASeleccionar = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Seguro que desea dar de baja esta materia?");
                    Console.WriteLine("[1. Si / 2. No]");
                    int DeseaDarBaja = Convert.ToInt32(Console.ReadLine());

                    if (DeseaDarBaja == 1)
                    {
                        if (lmateria.Exists(alu => alu.Indice == materiaASeleccionar))
                        {
                            for (int i = 0; i < lmateria.Count; i++)
                            {
                                if (lmateria[i].Indice == materiaASeleccionar)
                                {
                                    Materias mats = lmateria[i];
                                    mats.Indice = lmateria[i].Indice;
                                    mats.IndiceMateria = lmateria[i].IndiceMateria;
                                    mats.NombreMateria = lmateria[i].NombreMateria;
                                    mats.MateriaActiva = "inactiva";
                                    lmateria[i] = mats;
                                }
                            }


                            EscribirMaterias(lmateria, false, "dada de baja");
                            RetornarMenuMaterias(); // retornar al menu
                        }
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada.");
                        RetornarMenuMaterias();     // retorna al menu
                        
                    }

                }
                else
                {
                    Console.WriteLine($"El legajo Nro {getIndice} no contiene Materias.");
                    RetornarMenuMaterias();
                }

                break;

            // -- Editar una materia
            case 3:
                // -- limpiando pantalla
                Console.Clear();
                //-- el usuario ingresa el legajo
                Console.WriteLine("Por favor, ingrese legajo:");
                int getIndiceEdit = Convert.ToInt32(Console.ReadLine());

                existe = false; // formateamos existe

                List<Materias> emateria = RetornarListaMaterias(materiadb);

                foreach (Materias materiasEnLista in emateria)
                {

                    if (getIndiceEdit == materiasEnLista.IndiceMateria)
                    {

                        if (materiasEnLista.MateriaActiva == "activa")
                        {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine(materiasEnLista.Indice);
                            Console.WriteLine(materiasEnLista.IndiceMateria);
                            Console.WriteLine(materiasEnLista.NombreMateria);
                            CambiarColorTexto(materiasEnLista.MateriaActiva, "green");
                            existe = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existe la materia!");
                        existe = false;
                    }
                }

                if (existe)
                {
                    Console.WriteLine("Seleccione el ID de la materia que desea editar");
                    int IndiceAEditar = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Ingrese un legajo:");
                    int elegajo = Convert.ToInt32(Console.ReadLine());

                    // -- Nombre de la materia
                    Console.WriteLine("Ingrese el nombre de la materia:");
                    int xmateria = Convert.ToInt32(Console.ReadLine());

                    // -- actividad de la materia
                    Console.WriteLine("Esta activa?");
                    Console.WriteLine("[1. Si / 2. No]");
                    int eactividadMateria = Convert.ToInt32(Console.ReadLine());
                    string eActiva;

                    if (eactividadMateria == 1)
                    {
                        eActiva = "activa";
                    }
                    else
                    {
                        eActiva = "inactiva";
                    }

                    if (emateria.Exists(emat => emat.Indice == IndiceAEditar))
                    {
                        for (int i = 0; i < emateria.Count; i++)
                        {
                            if (emateria[i].Indice == IndiceAEditar)
                            {
                                Materias emt = new Materias();
                                emt.Indice = IndiceAEditar;
                                emt.IndiceMateria = elegajo;
                                emt.NombreMateria = xmateria;
                                emt.MateriaActiva = eActiva;
                                emateria[i] = emt;
                            }
                        }


                        EscribirMaterias(emateria, false, "editada");
                    }
                }

                break;

            // -- Volver al menu
            case 4:
                BackToMenu();
                break;

            // -- error
            default:
                Console.WriteLine("Por favor, ingrese una opcion del menu.");
                MenuMaterias();
                break;

        }

    }
    /// =============== END MATERIAS ===================





















    /// ====================INSCRIPCIONES & ALUMNOS ===========================


    public struct Alumno
    {
        public int Legajo;              // indice
        public string Nombre;           // nombre(s)
        public string Apellido;         // apellido(s)
        public int DNI;                 // DNI
        public string Nacimiento;       // fecha de nacimiento
        public string Domicilio;        // domicilio
        public string Activo;           // actividad
    }

    // -- Enlistar Alumnos
    public static List<Alumno> RetornarListaAlumnos(string inscripciones)
    {
        List<Alumno> listaAlumnos = new List<Alumno>();
        using (StreamReader sr = new StreamReader(inscripciones))
        {
            string? linea = sr.ReadLine();

            while (linea != null)
            {
                string[] alumnoArchivo = linea.Split(',');
                Alumno alumnoStruct = new Alumno();
                alumnoStruct.Legajo = int.Parse(alumnoArchivo[0]);          // legajo
                alumnoStruct.Nombre = alumnoArchivo[1];                     // nombre(s)
                alumnoStruct.Apellido = alumnoArchivo[2];                   // apellido(s)
                alumnoStruct.DNI = int.Parse(alumnoArchivo[3]);             // dni
                alumnoStruct.Nacimiento = alumnoArchivo[4];                 // fecha nacimiento
                alumnoStruct.Domicilio = alumnoArchivo[5];                  // domicilio
                alumnoStruct.Activo = alumnoArchivo[6];                     // activo?
                listaAlumnos.Add(alumnoStruct);
                linea = sr.ReadLine();
            }
        }
        return listaAlumnos;
    }


    //-- Menu de Materias
    public static void MenuInscripciones()
    {

        // -- Pantalla principal
        SelectMenu("Alumno(s)");


        // -- variable de opcion
        int alumnoOpcion;
        alumnoOpcion = Convert.ToInt32(Console.ReadLine());

        switch(alumnoOpcion) {

            // -- Dar de alta un alumno
            case 1:

                // invocar estructura nuevo alumno
                Alumno alumno = new Alumno();

                // legajo
                Console.WriteLine("Ingrese Legajo");
                string legajo = Console.ReadLine()!;
                while (!int.TryParse(legajo, out alumno.Legajo))
                {
                    Console.WriteLine("No ingresó un número entero, Ingrese un número de legajo:");
                    legajo = Console.ReadLine()!;
                }

                // nombre
                Console.WriteLine("Ingrese Nombre");
                alumno.Nombre = Console.ReadLine()!;

                // apellido
                Console.WriteLine("Ingrese Apellido");
                alumno.Apellido = Console.ReadLine()!;

                // DNI
                Console.WriteLine("Ingrese DNI");
                string dni = Console.ReadLine()!;
                while (!int.TryParse(dni, out alumno.DNI))
                {
                    Console.WriteLine("No ingresó un número entero, Ingrese un número de DNI:");
                    dni = Console.ReadLine()!;
                }

                // nacimiento
                Console.WriteLine("Ingrese una fecha de nacimiento");
                alumno.Nacimiento = Console.ReadLine()!;

                // domicilio
                Console.WriteLine("Ingrese un domicilio");
                alumno.Domicilio = Console.ReadLine()!;

                // actividad?
                Console.WriteLine("Esta activo?");
                Console.WriteLine("[1. Si / 2. No]");

                int alumnoActivo = Convert.ToInt32(Console.ReadLine()!);

                if(alumnoActivo == 1)
                {
                    alumno.Activo = "activo";
                }
                else
                {
                    alumno.Activo = "inactivo";
                }



                Alumno nuevoAlumno = alumno;
                List<Alumno> agregarNuevoAlumno = new List<Alumno> { nuevoAlumno };

                try
                {
                    using (StreamWriter escritor = new StreamWriter(alumnodb, true))
                    {
                        foreach (var alu in agregarNuevoAlumno)
                        {
                                               // 22486,            jeremias,           geminiani,          42884472,       19/08/2000              av. indep 4270,         activo/inactivo?
                            escritor.WriteLine(alu.Legajo + ", " + alu.Nombre + ", " + alu.Apellido + ", " + alu.DNI + ", " + alu.Nacimiento + "," + alu.Domicilio + "," + alu.Activo);
                        }
                    }

                    Console.WriteLine("Alumno inscripto exitosamente.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }



                break;
            // -- Dar de baja un alumno
            case 2:

                Console.Clear();
                Console.WriteLine("Por favor, ingrese legajo:");
                int getIndice = Convert.ToInt32(Console.ReadLine());
                bool existe = false;
                List<Alumno> lalumno = RetornarListaAlumnos(alumnodb);


                foreach (Alumno alumnosEnLista in lalumno)
                {

                    if (getIndice == alumnosEnLista.Legajo)
                    {

                        if (alumnosEnLista.Activo == "activo")
                        {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine(alumnosEnLista.Legajo);
                            Console.WriteLine(alumnosEnLista.Nombre);
                            Console.WriteLine(alumnosEnLista.Apellido);
                            Console.WriteLine(alumnosEnLista.DNI);
                            Console.WriteLine(alumnosEnLista.Nacimiento);
                            Console.WriteLine(alumnosEnLista.Domicilio);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(alumnosEnLista.Activo);
                            Console.ResetColor();
                            existe = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existe el alumno");
                        existe = false;
                    }
                }

                if (existe)
                {

                    Console.WriteLine("Seguro que desea dar de baja este alumno?");
                    Console.WriteLine("[1. Si/ 2. No]");
                    int DeseaDarBaja = Convert.ToInt32(Console.ReadLine());

                    if (DeseaDarBaja == 1)
                    {
                        if (lalumno.Exists(alu => alu.Legajo == getIndice))
                        {
                            for (int i = 0; i < lalumno.Count; i++)
                            {
                                if (lalumno[i].Legajo == getIndice)
                                {
                                    Alumno alum = lalumno[i];
                                    alum.Legajo = lalumno[i].Legajo;
                                    alum.Nombre = lalumno[i].Nombre;
                                    alum.Apellido = lalumno[i].Apellido;
                                    alum.DNI = lalumno[i].DNI;
                                    alum.Nacimiento = lalumno[i].Nacimiento;
                                    alum.Domicilio = lalumno[i].Domicilio;
                                    alum.Activo = "inactivo";
                                    lalumno[i] = alum;
                                }
                            }

                            try
                            {
                                using (StreamWriter escritor = new StreamWriter(alumnodb, false))
                                {
                                    foreach (var alm in lalumno)
                                    {

                                        // 22486,            jeremias,           geminiani,          42884472,       19/08/2000              av. indep 4270,         activo/inactivo?
                                        escritor.WriteLine(alm.Legajo + ", " + alm.Nombre + ", " + alm.Apellido + ", " + alm.DNI + ", " + alm.Nacimiento + "," + alm.Domicilio + "," + alm.Activo);

                                    }
                                }

                                Console.WriteLine("Alumno dado de baja exitosamente.");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }


                        }
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada.");
                    }

                }



                break;

            // -- Editar un alumno
            case 3:

                Console.Clear();
                Console.WriteLine("Por favor, ingrese legajo:");
                int getAlumno = Convert.ToInt32(Console.ReadLine());
                bool existeAlm = false;
                List<Alumno> ealumno = RetornarListaAlumnos(alumnodb);


                foreach (Alumno alumnosEnLista in ealumno)
                {

                    if (getAlumno == alumnosEnLista.Legajo)
                    {

                        if (alumnosEnLista.Activo == "activo")
                        {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine(alumnosEnLista.Legajo);
                            Console.WriteLine(alumnosEnLista.Nombre);
                            Console.WriteLine(alumnosEnLista.Apellido);
                            Console.WriteLine(alumnosEnLista.DNI);
                            Console.WriteLine(alumnosEnLista.Nacimiento);
                            Console.WriteLine(alumnosEnLista.Domicilio);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(alumnosEnLista.Activo);
                            Console.ResetColor();
                            existeAlm = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existe el alumno");
                        existeAlm = false;
                    }
                }

                if (existeAlm)
                {

                    // nombre
                    Console.WriteLine("Ingrese Nombre");
                    string enombre = Console.ReadLine()!;

                    // apellido
                    Console.WriteLine("Ingrese Apellido");
                    string eapellido = Console.ReadLine()!;

                    // DNI
                    Console.WriteLine("Ingrese DNI");
                    string edni = Console.ReadLine()!;
                    while (!int.TryParse(edni, out alumno.DNI))
                    {
                        Console.WriteLine("No ingresó un número entero, Ingrese un número de DNI:");
                        edni = Console.ReadLine()!;
                    }

                    // nacimiento
                    Console.WriteLine("Ingrese una fecha de nacimiento");
                    string enacimiento = Console.ReadLine()!;

                    // domicilio
                    Console.WriteLine("Ingrese un domicilio");
                    string edomicilio = Console.ReadLine()!;

                    // actividad?
                    Console.WriteLine("Esta activo?");
                    Console.WriteLine("[1. Si / 2. No]");

                    int ealumnoActivo = Convert.ToInt32(Console.ReadLine()!);
                    string eactivo;

                    if (ealumnoActivo == 1)
                    {
                        eactivo = "activo";
                    }
                    else
                    {
                        eactivo = "inactivo";
                    }

                    if (ealumno.Exists(ealm => ealm.Legajo == getAlumno))
                    {
                        for (int i = 0; i < ealumno.Count; i++)
                        {
                            if (ealumno[i].Legajo == getAlumno)
                            {
                                Alumno ealum = new Alumno();
                                ealum.Legajo = getAlumno;
                                ealum.Nombre = enombre;
                                ealum.Apellido = eapellido;
                                ealum.DNI = Convert.ToInt32(edni);
                                ealum.Nacimiento = enacimiento;
                                ealum.Domicilio = edomicilio;
                                ealum.Activo = eactivo;
                                ealumno[i] = ealum;
                            }
                        }

                        try
                        {
                            using (StreamWriter escritor = new StreamWriter(alumnodb, false))
                            {
                                foreach (var alm in ealumno)
                                {

                                    // 22486,            jeremias,           geminiani,          42884472,       19/08/2000              av. indep 4270,         activo/inactivo?
                                    escritor.WriteLine(alm.Legajo + ", " + alm.Nombre + ", " + alm.Apellido + ", " + alm.DNI + ", " + alm.Nacimiento + "," + alm.Domicilio + "," + alm.Activo);

                                }
                            }

                            Console.WriteLine("Alumno editado exitosamente.");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No existe el alumno!");
                }

                break;

            // -- error
            default:
                Console.WriteLine("Por favor, ingrese una opcion del menu.");
                MenuInscripciones();
                break;
        
        }

    }


    /// ====================END INSCRIPCIONES & ALUMNOS =======================




















    /// ====================== NOTAS ================================

    public struct Notas
    {
        public int Indice;              // indice
        public int IndiceAlumno;        // indice -> legajo
        public int IndiceMateria;       // indice -> materia
        public string Cursado;          // cursado?: cursada / no cursada / recuperatorio / final /  etc...
        public string Estado;           // estado: aprobado / desaprobado
        public string Nota;             // nota
        public string Fecha;          // fecha
    }

    // -- Obtener SOLO EL INDICE de las notas y RETORNARLOS
    public static int ObtenerIndiceNotas()
    {
        int notasid = -1;

        List<Notas> listaNotas = new List<Notas>();
        using (StreamReader sr = new StreamReader(notasdb))
        {
            string? linea = sr.ReadLine();

            while (linea != null)
            {
                string[] notaArchivo = linea.Split(',');
                Notas notaStruct = new Notas();
                notaStruct.Indice = int.Parse(notaArchivo[0]);                // indice
                linea = sr.ReadLine();

                notasid = int.Parse(notaArchivo[0]);
            }
        }
        return notasid;
    }


    // -- Enlistar Notas
    public static List<Notas> RetornarListaNotas(string notasdb)
    {
        List<Notas> listaNotas = new List<Notas>();
        using (StreamReader sr = new StreamReader(notasdb))
        {
            string? linea = sr.ReadLine();

            while (linea != null)
            {
                string[] notasArchivo = linea.Split(',');
                Notas notasStruct = new Notas();
                notasStruct.Indice = int.Parse(notasArchivo[0]);            // indice
                notasStruct.IndiceAlumno = int.Parse(notasArchivo[1]);      // indice -> Legajo
                notasStruct.IndiceMateria = int.Parse(notasArchivo[2]);     // indice -> Materia
                notasStruct.Cursado = notasArchivo[3].Trim();               // cursado?
                notasStruct.Estado = notasArchivo[4].Trim();                // aprobado?
                notasStruct.Nota = notasArchivo[5];                         // nota
                notasStruct.Fecha = notasArchivo[6];                        // fecha

                listaNotas.Add(notasStruct);
                linea = sr.ReadLine();
            }
        }
        return listaNotas;
    }



    // -- Menu principal: Notas
    public static void MenuNotas()
    {
        // -- Menu Principal
        SelectMenu("Nota");


        // -- variable de opcion
        int notasOpcion;
        notasOpcion = Convert.ToInt32(Console.ReadLine());


        switch(notasOpcion) {

            // -- dar de alta una nota
            case 1:
                Console.Clear();
                Notas nota = new Notas();

                // -- indice
                nota.Indice = (ObtenerIndiceNotas() + 1);

                // -- legajo
                Console.WriteLine("Ingrese Legajo");
                string legajo = Console.ReadLine()!;
                while (!int.TryParse(legajo, out nota.IndiceAlumno))
                {
                    Console.WriteLine("No ingresó un número entero, Ingrese un número de legajo:");
                    legajo = Console.ReadLine()!;
                }

                // -- indice de materia
                Console.WriteLine("Ingrese ID de Materia");
                string materia = Console.ReadLine()!;
                while (!int.TryParse(materia, out nota.IndiceMateria))
                {
                    Console.WriteLine("No ingresó un número entero, Ingrese un ID de materia:");
                    materia = Console.ReadLine()!;
                }

                // -- cursado?
                Console.WriteLine("Ingrese el estado del cursado");
                Console.WriteLine("1. Cursado");
                Console.WriteLine("2. No cursado");
                Console.WriteLine("3. Recuperatorio");
                Console.WriteLine("4. Final");

                int cursado = Convert.ToInt32(Console.ReadLine()!);
                

                switch (cursado)
                {
                    // cursado
                    case 1:
                        nota.Cursado = "cursado";
                        break;
                    // no cursado
                    case 2:
                        nota.Cursado = "no cursado";
                        break;
                    // recuperatorio
                    case 3:
                        nota.Cursado = "recuperatorio";
                        break;
                    // final
                    case 4:
                        nota.Cursado = "final";
                        break;
                    default:
                        Console.WriteLine("Por favor, ingrese un estado de cursado");
                        Console.WriteLine("1. Cursado");
                        Console.WriteLine("2. No cursado");
                        Console.WriteLine("3. Recuperatorio");
                        Console.WriteLine("4. Final");
                        cursado = Convert.ToInt32(Console.ReadLine()!);
                        break;
                }


                // -- nota
                Console.WriteLine("Ingrese la nota:");
                string getNota = Console.ReadLine();

                try
                {
                    string getNotaDot = getNota.Replace(",", ".");
                    double totalNota = double.Parse(getNotaDot, CultureInfo.InvariantCulture);
                    nota.Nota = getNotaDot;

                    // -- estado? aprobado : desaprobado
                    if (totalNota >= 4.00)
                    {
                        nota.Estado = "aprobado";
                    }
                    else if(totalNota < 4.00)
                    {
                        nota.Estado = "desaprobado";
                    }

                }
                catch(FormatException)
                {

                    Console.WriteLine("La cadena no tiene el formato correcto.");
                }


                // -- fecha 
                DateTime fecha = DateTime.Now;
                nota.Fecha = fecha.ToString();


                Notas nuevaNota = nota;
                List<Notas> agregarNuevaNota = new List<Notas> { nuevaNota };

                try
                {                 
                    using (StreamWriter escritor = new StreamWriter(notasdb, true))
                    {
                        foreach (var not in agregarNuevaNota)
                        {
                                                // 1,               22486,                      1,                      cursado/...         aprobado/...        9.81            19/08/2000
                            escritor.WriteLine(not.Indice + ", "+  not.IndiceAlumno + ", " + not.IndiceMateria + ", " + not.Cursado + ", " + not.Estado + ", " + not.Nota + ", ~~~" + not.Fecha);
                        }
                    }

                    Console.WriteLine("Nota inscripta exitosamente.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }


                break;
            // -- eliminar una nota
            case 2:

                Console.Clear();
                Console.WriteLine("Por favor, ingrese legajo:");
                int getIndice = Convert.ToInt32(Console.ReadLine());
                bool existe = false;
                List<Notas> lnotas = RetornarListaNotas(notasdb);


                foreach (Notas notasEnLista in lnotas)
                {

                    if (getIndice == notasEnLista.IndiceAlumno)
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(notasEnLista.Indice);
                        Console.WriteLine(notasEnLista.IndiceAlumno);
                        Console.WriteLine(notasEnLista.IndiceMateria);

                        switch (notasEnLista.Cursado)
                        {

                            // -- cursado -> verde
                            case "cursado":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- no cursado -> rojo
                            case "no cursado":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- recuperatorio -> amarillo
                            case "recuperatorio":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- final -> violeta
                            case "final":
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- error
                            default:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("error!");
                                Console.ResetColor();
                                break;
                        }


                        if(String.Compare(notasEnLista.Estado,"aprobado") == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(notasEnLista.Estado);
                            Console.ResetColor();

                        }else if(String.Compare(notasEnLista.Estado, "desaprobado") == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(notasEnLista.Estado);
                            Console.ResetColor();
                        }

                        Console.WriteLine(notasEnLista.Nota);
                        Console.WriteLine(notasEnLista.Fecha);

                        existe = true;
                    }
                    else
                    {
                        Console.WriteLine($"No hay notas en el registro del alumno {getIndice}");
                        existe = false;
                    }
                }


                if (existe)
                {
                    Console.WriteLine("Seleccione el ID de la nota que desea eliminar:");
                    int notaASeleccionar = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Seguro que desea eliminar esta nota?");
                    Console.WriteLine("[1. Si/ 2. No]");
                    int DeseaDarBaja = Convert.ToInt32(Console.ReadLine());

                    if (DeseaDarBaja == 1)
                    {

                        Notas borrar = new Notas();
                        bool existeNota = false;
                        
                        foreach(var note in lnotas)
                        {
                            if(notaASeleccionar == note.Indice)
                            {
                                borrar = note;
                                existeNota = true;
                            }
                        }
                        if (existeNota)
                        {
                            lnotas.Remove(borrar);
                        }


                        try
                        {
                            using (StreamWriter escritor = new StreamWriter(notasdb, false))
                            {
                                foreach (var not in lnotas)
                                {
                                    // 1,               22486,                      1,                      cursado/...         aprobado/...        9.81            19/08/2000
                                    escritor.WriteLine(not.Indice + ", " + not.IndiceAlumno + ", " + not.IndiceMateria + ", " + not.Cursado + ", " + not.Estado + ", " + not.Nota + ", " + not.Fecha);
                                }
                            }

                            Console.WriteLine("Nota eliminada exitosamente.");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }


                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada.");
                    }

                }



                break;
            // -- editar una nota
            case 3:


                Console.Clear();
                Console.WriteLine("Por favor, ingrese legajo:");
                int getNotaEditar = Convert.ToInt32(Console.ReadLine());
                bool existeEditar = false;
                List<Notas> enotas = RetornarListaNotas(notasdb);


                foreach (Notas notasEnLista in enotas)
                {

                    if (getNotaEditar == notasEnLista.IndiceAlumno)
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(notasEnLista.Indice);
                        Console.WriteLine(notasEnLista.IndiceAlumno);
                        Console.WriteLine(notasEnLista.IndiceMateria);

                        switch (notasEnLista.Cursado)
                        {

                            // -- cursado -> verde
                            case "cursado":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- no cursado -> rojo
                            case "no cursado":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- recuperatorio -> amarillo
                            case "recuperatorio":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- final -> violeta
                            case "final":
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(notasEnLista.Cursado);
                                Console.ResetColor();
                                break;
                            // -- error
                            default:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("error!");
                                Console.ResetColor();
                                break;
                        }


                        if (String.Compare(notasEnLista.Estado, "aprobado") == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(notasEnLista.Estado);
                            Console.ResetColor();

                        }
                        else if (String.Compare(notasEnLista.Estado, "desaprobado") == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(notasEnLista.Estado);
                            Console.ResetColor();
                        }

                        Console.WriteLine(notasEnLista.Nota);
                        Console.WriteLine(notasEnLista.Fecha);

                        existeEditar = true;
                    }
                    else
                    {
                        Console.WriteLine($"No hay notas en el registro del alumno: {getNotaEditar}");
                        existeEditar = false;
                    }
                }

                if (existeEditar)
                {

                    Console.WriteLine("Seleccione el ID de la nota que desea editar:");
                    int notaASeleccionar = Convert.ToInt32(Console.ReadLine());


                    if (enotas.Exists(enot => enot.Indice == notaASeleccionar))
                    {

                        // -- legajo
                        Console.WriteLine("Ingrese Legajo");
                        string elegajo = Console.ReadLine()!;
                        while (!int.TryParse(elegajo, out nota.IndiceAlumno))
                        {
                            Console.WriteLine("No ingresó un número entero, Ingrese un número de legajo:");
                            elegajo = Console.ReadLine()!;
                        }

                        // -- indice de materia
                        Console.WriteLine("Ingrese ID de Materia");
                        string emateria = Console.ReadLine()!;
                        while (!int.TryParse(emateria, out nota.IndiceMateria))
                        {
                            Console.WriteLine("No ingresó un número entero, Ingrese un ID de materia:");
                            emateria = Console.ReadLine()!;
                        }


                        // -- cursado?
                        Console.WriteLine("Ingrese el estado del cursado");
                        Console.WriteLine("1. Cursado");
                        Console.WriteLine("2. No cursado");
                        Console.WriteLine("3. Recuperatorio");
                        Console.WriteLine("4. Final");

                        int ecursado = Convert.ToInt32(Console.ReadLine()!);


                        switch (ecursado)
                        {
                            // cursado
                            case 1:
                                nota.Cursado = "cursado";
                                break;
                            // no cursado
                            case 2:
                                nota.Cursado = "no cursado";
                                break;
                            // recuperatorio
                            case 3:
                                nota.Cursado = "recuperatorio";
                                break;
                            // final
                            case 4:
                                nota.Cursado = "final";
                                break;
                            default:
                                Console.WriteLine("Por favor, ingrese un estado de cursado");
                                Console.WriteLine("1. Cursado");
                                Console.WriteLine("2. No cursado");
                                Console.WriteLine("3. Recuperatorio");
                                Console.WriteLine("4. Final");
                                ecursado = Convert.ToInt32(Console.ReadLine()!);
                                break;
                        }


                        // -- nota
                        Console.WriteLine("Ingrese la nota:");
                        string getENota = Console.ReadLine();

                        string getNotaDot = getENota.Replace(",", ".");
                        double totalNota = double.Parse(getNotaDot, CultureInfo.InvariantCulture);
                        string eestado;

                            // -- estado? aprobado : desaprobado
                            if (totalNota >= 4.00)
                            {
                                eestado = "aprobado";
                            }
                            else if (totalNota < 4.00)
                            {
                                eestado = "desaprobado";
                            }


                        // -- fecha 
                        DateTime efecha = DateTime.Now;
                        nota.Fecha = efecha.ToString();




                        for (int i = 0; i < enotas.Count; i++)
                        {
                            if (enotas[i].Indice == notaASeleccionar)
                            {
                                Notas ent = new Notas();
                                ent.Indice = notaASeleccionar;
                                ent.IndiceAlumno = nota.IndiceAlumno;
                                ent.IndiceMateria = nota.IndiceMateria;
                                ent.Cursado = enotas[i].Cursado;
                                ent.Nota = getNotaDot;
                                ent.Estado = enotas[i].Estado;
                                ent.Fecha = nota.Fecha;
                                enotas[i] = ent;
                            }
                        }

                    try
                    {
                        using (StreamWriter escritor = new StreamWriter(notasdb, false))
                        {
                            foreach (var not in enotas)
                            {
                                // 1,               22486,                      1,                      cursado/...         aprobado/...        9.81            19/08/2000
                                escritor.WriteLine(not.Indice + ", " + not.IndiceAlumno + ", " + not.IndiceMateria + ", " + not.Cursado + ", " + not.Estado + ", " + not.Nota + ", " + not.Fecha);
                            }
                        }

                        Console.WriteLine("Nota editada exitosamente.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }


                }
                else
                    {
                        Console.WriteLine("No existe la nota o el ID");
                    }

                }



                break;
            // -- error
            default:
                Console.WriteLine("Por favor, elija una opcion del menu.");
                MenuNotas();
                break;
        }


    }




    /// ====================== END NOTAS ============================









    // -- MENU PRINCIPAL
    public static void Main()
    {

        // -- elegir opcion
        int menuOpcion;

        // -- pantalla de menu principal
        MenuPrincipal();

        // -- capturar teclado
        menuOpcion = Convert.ToInt32(Console.ReadLine());

        // -- si los números se encuentran en el menu
        switch(menuOpcion)
        {
            // materias
            case 1:
                Console.Clear();
                MenuMaterias();
                break;

            // inscripciones 
            case 2:
                Console.Clear();
                MenuInscripciones();
                break;

            // notas
            case 3:
                Console.Clear();
                MenuNotas();
                break;

            // salir
            case 4:
                Console.WriteLine("Hasta Luego.");
                break;

            // error
            default:
                Console.WriteLine("Por favor, elija una opcion valida.");
                Main();
                break;
        }

        

    }
}