using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace RepositorioGastos
{
    internal class GastoRepositorio
    {
        private const string conexion = "Server=(localdb)\\MSSQLLocalDB;Database=GastosBD;Trusted_Connection=True;";

        public void Insertar(Gasto gasto)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "INSERT INTO dbo.gastos (descripcion, monto, categoria) VALUES (@descripcion, @monto, @categoria)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@descripcion", gasto.Descripcion);
                    cmd.Parameters.AddWithValue("@monto", gasto.Monto);
                    cmd.Parameters.AddWithValue("@categoria", gasto.Categoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Gasto> ListarTodos()
        {
            List<Gasto> lista = new List<Gasto>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT * FROM dbo.gastos ORDER BY ID";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Gasto gasto = new Gasto();

                            gasto.Id = Convert.ToInt32(lector["ID"]);
                            gasto.Descripcion = lector["descripcion"].ToString();
                            gasto.Monto = Convert.ToDecimal(lector["monto"]);
                            gasto.Categoria = lector["categoria"].ToString();

                            lista.Add(gasto);
                        }
                    }
                }
            }

            return lista;
        }
    }
}