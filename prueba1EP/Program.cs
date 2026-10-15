using System;
using System.IO;
using System.Threading;

namespace prueba1EP
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxExpedientes = 100;

            // Arreglos paralelos para los datos de la posta
            string[] codigos = new string[maxExpedientes];
            string[] tiposDocumento = new string[maxExpedientes];
            string[] remitentes = new string[maxExpedientes];
            string[] gremiosDestino = new string[maxExpedientes];
            string[] fechas = new string[maxExpedientes];

            int totalExpedientes = 0;
            string nombreArchivo = "expedientes_posta.txt";

            // Cargar datos automáticamente al iniciar el programa[cite: 1]
            cargar_desde_archivo(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, ref totalExpedientes, nombreArchivo);

            int opcion;

            do
            {
                Console.Clear();
                mostrar_menu(); //[cite: 1]
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        registrar_expediente(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, ref totalExpedientes, maxExpedientes); //[cite: 1]
                        guardar_en_archivo(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, totalExpedientes, nombreArchivo); //[cite: 1]
                        break;

                    case 2:
                        listar_expedientes(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, totalExpedientes); //[cite: 1]
                        break;

                    case 3:
                        buscar_por_codigo(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, totalExpedientes); //[cite: 1]
                        break;

                    case 4:
                        ordenar_expedientes(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, totalExpedientes); //[cite: 1]
                        break;

                    case 0:
                        guardar_en_archivo(codigos, tiposDocumento, remitentes, gremiosDestino, fechas, totalExpedientes, nombreArchivo); //[cite: 1]
                        Console.WriteLine("\nSaliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla...");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 0);
        }

        static void mostrar_menu()
        {
            Console.Clear();

            // Caja superior con el título del sistema en color Azul
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\t┌--------------------------------------------┐");
            Console.WriteLine("\t\t\t\t|                                            |");
            Console.WriteLine("\t\t\t\t|         SISTEMA - POSTA MÉDICA             |");
            Console.WriteLine("\t\t\t\t|                                            |");
            Console.WriteLine("\t\t\t\t└--------------------------------------------┘");
            Console.ResetColor();

            // Caja inferior con las opciones del menú en color Cian (Opción 0 para salir)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t┌--------------------------------------------┐");
            Console.WriteLine("\t\t\t\t|                                            |");
            Console.WriteLine("\t\t\t\t|  1) Registrar expediente                   |");
            Console.WriteLine("\t\t\t\t|  2) Listar expedientes                     |");
            Console.WriteLine("\t\t\t\t|  3) Buscar por código                      |");
            Console.WriteLine("\t\t\t\t|  4) Ordenar expedientes                    |");
            Console.WriteLine("\t\t\t\t|  0) Salir                                  |");
            Console.WriteLine("\t\t\t\t|                                            |");
            Console.WriteLine("\t\t\t\t└--------------------------------------------┘");
            Console.ResetColor();

            Console.Write("\n\t\t\t\tSeleccione una opción: ");
         
        }

        static bool validar_datos(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                Console.WriteLine($"Error: El campo '{nombreCampo}' no puede estar vacío[cite: 1].");
                return false;
            }
            return true;
        }

        static void registrar_expediente(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, ref int totalExpedientes, int maxExpedientes)
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRAR EXPEDIENTE ---");

            if (totalExpedientes >= maxExpedientes)
            {
                Console.WriteLine("Capacidad máxima alcanzada.");
            }
            else
            {
                string tipoDoc, remitente, gremio, fecha;

                // Validación de campos obligatorios[cite: 1]
                do
                {
                    /* Diferencia:
                    Oficio: Comunicación oficial entre instituciones o autoridades.
                    Solicitud: Petición personal que hace un ciudadano a una entidad.
                    */
                    Console.Write("Ingrese tipo de documento (ej. Oficio, Solicitud): ");
                    tipoDoc = Console.ReadLine();
                } while (!validar_datos(tipoDoc, "Tipo de Documento"));

                do
                {
                    Console.Write("Ingrese remitente: ");
                    remitente = Console.ReadLine();
                } while (!validar_datos(remitente, "Remitente"));

                do
                {
                    Console.Write("Ingrese gremio de destino: ");
                    gremio = Console.ReadLine();
                } while (!validar_datos(gremio, "Gremio de Destino"));

                do
                {
                    Console.Write("Ingrese fecha de registro (DD/MM/AAAA): ");
                    fecha = Console.ReadLine();
                } while (!validar_datos(fecha, "Fecha"));
                // Genera un código correlativo automático (ej: EXP-1, EXP-2) sumando 1 al total actual.
                string codigo = "EXP-" + (totalExpedientes + 1);
                // Almacena cada dato ingresado en la posición libre actual de los arreglos paralelos.
                codigos[totalExpedientes] = codigo;
                tiposDoc[totalExpedientes] = tipoDoc;
                remitentes[totalExpedientes] = remitente;
                gremios[totalExpedientes] = gremio;
                fechas[totalExpedientes] = fecha;
                // Incrementa en 1 el contador global para indicar que hay un trámite más registrado.
                totalExpedientes++;

                Console.WriteLine("\n¡Expediente registrado con éxito!");
                Console.WriteLine("Código asignado: " + codigo);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void listar_expedientes(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, int totalExpedientes)
        {
            Console.Clear();
            Console.WriteLine("--- LISTAR EXPEDIENTES ---");
            if (totalExpedientes == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
            }
            else
            {
                for (int i = 0; i < totalExpedientes; i++)
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Código: " + codigos[i]);
                    Console.WriteLine("Tipo de Documento: " + tiposDoc[i]);
                    Console.WriteLine("Remitente: " + remitentes[i]);
                    Console.WriteLine("Gremio de Destino: " + gremios[i]);
                    Console.WriteLine("Fecha: " + fechas[i]);
                }
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void buscar_por_codigo(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, int totalExpedientes)
        {
            Console.Clear();
            Console.WriteLine("--- BUSCAR POR CÓDIGO ---");
            Console.Write("Ingrese el código a buscar (ej. EXP-1): ");
            string buscar = Console.ReadLine();

            bool encontrado = false;
            for (int i = 0; i < totalExpedientes; i++)
            {
                if (codigos[i].Equals(buscar, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n¡Expediente encontrado!");
                    Console.WriteLine("Código: " + codigos[i]);
                    Console.WriteLine("Tipo de Documento: " + tiposDoc[i]);
                    Console.WriteLine("Remitente: " + remitentes[i]);
                    Console.WriteLine("Gremio de Destino: " + gremios[i]);
                    Console.WriteLine("Fecha: " + fechas[i]);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\nNo se encontró ningún expediente con ese código.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void ordenar_expedientes(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, int totalExpedientes)
        {
            Console.Clear();
            Console.WriteLine("--- ORDENAR EXPEDIENTES ---");
            if (totalExpedientes < 2)
            {
                Console.WriteLine("No hay suficientes expedientes para ordenar.");
            }
            else
            {
                Console.WriteLine("1. Ordenar por Código");
                Console.WriteLine("2. Ordenar por Fecha");
                Console.Write("Seleccione criterio: ");
                string criterio = Console.ReadLine();

                for (int i = 0; i < totalExpedientes - 1; i++)
                {
                    for (int j = 0; j < totalExpedientes - i - 1; j++)
                    {
                        bool condicion = false;
                        if (criterio == "1")
                            condicion = string.Compare(codigos[j], codigos[j + 1]) > 0;
                        else if (criterio == "2")
                            condicion = string.Compare(fechas[j], fechas[j + 1]) > 0;

                        if (condicion)
                        {
                            Intercambiar(ref codigos[j], ref codigos[j + 1]);
                            Intercambiar(ref tiposDoc[j], ref tiposDoc[j + 1]);
                            Intercambiar(ref remitentes[j], ref remitentes[j + 1]);
                            Intercambiar(ref gremios[j], ref gremios[j + 1]);
                            Intercambiar(ref fechas[j], ref fechas[j + 1]);
                        }
                    }
                }

                Console.WriteLine("\n¡Expedientes ordenados con éxito!\n");
                for (int i = 0; i < totalExpedientes; i++)
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Código: " + codigos[i]);
                    Console.WriteLine("Tipo de Documento: " + tiposDoc[i]);
                    Console.WriteLine("Remitente: " + remitentes[i]);
                    Console.WriteLine("Gremio de Destino: " + gremios[i]);
                    Console.WriteLine("Fecha: " + fechas[i]);
                }
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

//        static void Intercambiar(ref string a, ref string b)
//        {
//            string temp = a;
//            a = b;
//            b = temp;
//        }

//        static void guardar_en_archivo(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, int totalExpedientes, string nombreArchivo)
//        {
//            try
//            {
//                using (StreamWriter sw = new StreamWriter(nombreArchivo))
//                {
//                    sw.WriteLine(totalExpedientes);
//                    for (int i = 0; i < totalExpedientes; i++)
//                    {
//                        sw.WriteLine(codigos[i]);
//                        sw.WriteLine(tiposDoc[i]);
//                        sw.WriteLine(remitentes[i]);
//                        sw.WriteLine(gremios[i]);
//                        sw.WriteLine(fechas[i]);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error al guardar en archivo: " + ex.Message);
//            }
//        }

//        static void cargar_desde_archivo(string[] codigos, string[] tiposDoc, string[] remitentes, string[] gremios, string[] fechas, ref int totalExpedientes, string nombreArchivo)
//        {
//            if (File.Exists(nombreArchivo))
//            {
//                try
//                {
//                    using (StreamReader sr = new StreamReader(nombreArchivo))
//                    {
//                        string linea = sr.ReadLine();
//                        if (linea != null)
//                        {
//                            totalExpedientes = int.Parse(linea);
//                            for (int i = 0; i < totalExpedientes; i++)
//                            {
//                                codigos[i] = sr.ReadLine();
//                                tiposDoc[i] = sr.ReadLine();
//                                remitentes[i] = sr.ReadLine();
//                                gremios[i] = sr.ReadLine();
//                                fechas[i] = sr.ReadLine();
//                            }
//                        }
//                    }
//                }
//                catch (Exception)
//                {
//                    totalExpedientes = 0;
//                }
//            }
//        }
//    }
//}