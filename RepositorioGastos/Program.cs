using System;

namespace RepositorioGastos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GastoRepositorio repositorio = new GastoRepositorio();
            int opcion = 0;

            while (opcion != 3)
            {
                Console.WriteLine("\n--- MENU DE GASTOS ---");
                Console.WriteLine("1. Registrar gasto");
                Console.WriteLine("2. Listar gastos");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opcion invalida.");
                    opcion = 0;
                    continue;
                }

                if (opcion == 1)
                {
                    Gasto gasto = new Gasto();

                    Console.Write("Descripcion: ");
                    gasto.Descripcion = Console.ReadLine();

                    Console.Write("Monto: ");
                    decimal monto;

                    if (!decimal.TryParse(Console.ReadLine(), out monto) ||
                        monto <= 0 ||
                        string.IsNullOrWhiteSpace(gasto.Descripcion))
                    {
                        Console.WriteLine("Datos inválidos.");
                        continue;
                    }

                    gasto.Monto = monto;

                    Console.Write("Categoria: ");
                    gasto.Categoria = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(gasto.Categoria))
                    {
                        Console.WriteLine("Datos inválidos.");
                        continue;
                    }

                    repositorio.Insertar(gasto);

                    Console.WriteLine("Gasto registrado correctamente.");
                }
                else if (opcion == 2)
                {
                    var gastos = repositorio.ListarTodos();
                    decimal total = 0;

                    Console.WriteLine("\n--- LISTA DE GASTOS ---");

                    if (gastos.Count == 0)
                    {
                        Console.WriteLine("No hay gastos registrados.");
                    }
                    else
                    {
                        foreach (Gasto gasto in gastos)
                        {
                            Console.WriteLine(
                                "ID: " + gasto.Id +
                                " | Descripcion: " + gasto.Descripcion +
                                " | Monto: " + gasto.Monto.ToString("0.00") +
                                " | Categoria: " + gasto.Categoria);

                            total = total + gasto.Monto;
                        }

                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("TOTAL GASTADO: Q " + total.ToString("0.00"));
                    }
                }
                else if (opcion == 3)
                {
                    Console.WriteLine("Programa finalizado.");
                }
                else
                {
                    Console.WriteLine("Opcion invalida.");
                }
            }
        }
    }
}