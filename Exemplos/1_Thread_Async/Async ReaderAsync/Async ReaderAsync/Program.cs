using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_ReaderAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            CarregaDados();
            CarregarDadosAsync();

            Console.ReadKey();
        }

        private static void CarregaDados()
        {
            StringBuilder txtDados = new StringBuilder();
            var connectionString = @"Data Source=localhost;Initial Catalog=Cadastro;Integrated Security=True";
            string sql = @"select Id,Nome from Alunos";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtDados.Append("\nId: ");
                    txtDados.Append(rdr.GetValue(0) + "\t\t" + rdr.GetValue(1));
                    txtDados.Append("\n");
                }

                Console.WriteLine(txtDados);
            }
        }



        private static async void CarregarDadosAsync()
        {

            StringBuilder txtDados = new StringBuilder();
            var connectionString = @"Data Source=localhost;Initial Catalog=Cadastro;Integrated Security=True";
            string sql = @"select Id,Nome from Alunos";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                await conn.OpenAsync();

                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        txtDados.Append("\nId: ");
                        txtDados.Append(await rdr.GetFieldValueAsync<int>(0) + "\t\t" + await rdr.GetFieldValueAsync<string>(1));
                        txtDados.Append("\n");
                    }

                    Console.WriteLine(txtDados);
                }
            }
        }
    }
}
