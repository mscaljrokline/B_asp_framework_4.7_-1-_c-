using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OPERACION_AMHCH_5_OK.Models
{
    public class DbCuenta
    {
        public static readonly string _connectionString = "Data Source=(local);Initial Catalog=BD_TRANSACCIONES_AMHCH_5;User ID=USUARIO_5;Password=PASSWORD"; // Ajusta con tu cadena de conexión
        public static string Guardar(Cuenta cuenta)
        {
            string a = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_guardar_cuenta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada al procedimiento almacenado
                        cmd.Parameters.AddWithValue("@nro_cuenta", cuenta.NRO_CUENTA);
                        cmd.Parameters.AddWithValue("@tipo", cuenta.TIPO);
                        cmd.Parameters.AddWithValue("@moneda", cuenta.MONEDA);
                        cmd.Parameters.AddWithValue("@nombre", cuenta.NOMBRE);
                        cmd.Parameters.AddWithValue("@saldo", cuenta.SALDO);

                        // Parámetros de salida
                        SqlParameter outputResultado = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        SqlParameter outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outputResultado);
                        cmd.Parameters.Add(outputMensaje);

                        // Abrir conexión y ejecutar el procedimiento
                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Verificar el resultado
                        int resultado = (int)outputResultado.Value;



                        if (resultado != 1)
                        {
                            a = (string)outputMensaje.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                a = ex.Message;
            }
            return a;

        }
        public static IEnumerable<Cuenta2> listar_cuenta()
        {
            List<Cuenta2> cuentas = new List<Cuenta2>();


            // Ejecuta el procedimiento almacenado
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("pro_listar_cuenta", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    // Usamos un objeto anónimo para incluir la nueva propiedad sin modificar el modelo
                    cuentas.Add(new Cuenta2
                    {
                        NRO_CUENTA = reader["NRO_CUENTA"].ToString(),
                        TIPO = reader["TIPO"].ToString(),
                        NOMBRE = reader["NOMBRE"].ToString(),
                        MONEDA = reader["MONEDA"].ToString(),
                        SALDO = (double)Convert.ToDecimal(reader["SALDO"]),
                        NRO_CUENTA2 = FormatearNumeroDeCuenta(reader["NRO_CUENTA"].ToString()) // Campo adicional con el formato 11-22-33333
                    });
                }

                conn.Close();
            }

            return cuentas;

        }
        public static string FormatearNumeroDeCuenta(string nro)
        {
            string ret = "";
            if (nro.Length >= 10)
            {
                int n = nro.Length;
                return $"{nro.Substring(0, 3)}-{nro.Substring(4, n - 6)}-{nro.Substring(n - 5, 2)} -{nro.Substring(n - 2, 1)}";
            }
            return ret;
        }
        public static bool Operacion(string CUENTA, decimal MONTO, string proc)
        {
            bool sw = false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada al procedimiento almacenado
                        cmd.Parameters.AddWithValue("@nro_cuenta", CUENTA);
                        cmd.Parameters.AddWithValue("@monto", MONTO);
                        cmd.Parameters.AddWithValue("@fecha", Convert.ToDateTime(DateTime.Now.ToString("F")));

                        if (proc == "pro_retiro_cuenta")
                        {
                            cmd.Parameters.Add("sw", SqlDbType.Int).Direction = ParameterDirection.Output;

                        }


                        // Abrir conexión y ejecutar el procedimiento
                        con.Open();
                        cmd.ExecuteNonQuery();



                        if (proc == "pro_retiro_cuenta")
                        {
                            if (Convert.ToInt32(cmd.Parameters["sw"].Value) == 1)
                            {
                                sw = true;
                            }
                        }
                        else
                        {
                            sw = true;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                sw=false;
            }
            return sw;
        }
        /*
        public static double consulta_saldo(string nro)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_obt_saldo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nro_cuenta", nro);
                    // Abrir conexión y ejecutar el procedimiento
                    con.Open();
                    // Agregar el parámetro de salida para obtener el saldo
                    SqlParameter saldoParam = new SqlParameter("@saldo", SqlDbType.Decimal);
                    saldoParam.Direction = ParameterDirection.Output; // Definir como parámetro de salida
                    cmd.Parameters.Add(saldoParam);

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    return Convert.ToDouble(cmd.Parameters["saldo"].Value);

                }
            }
        }
        */
        public static string obtener_saldo(string nro)
        {
            string saldo = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_obt_saldo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        // Agregar el parámetro de entrada para el número de cuenta
                        cmd.Parameters.AddWithValue("@nro_cuenta", nro);

                        // Agregar el parámetro de salida para obtener el saldo
                        SqlParameter saldoParam = new SqlParameter("@saldo", SqlDbType.Decimal);
                        saldoParam.Direction = ParameterDirection.Output; // Definir como parámetro de salida
                        cmd.Parameters.Add(saldoParam);

                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();

                        // Recuperar el valor del parámetro de salida
                        saldo = Convert.ToString(saldoParam.Value);
                    }

                }
        

            }
            catch (Exception ex)
            {
                // Manejar la excepción (puedes mostrar un mensaje o registrar el error)
                saldo=ex.Message;
            }

            return saldo;
        }
        public static IEnumerable<Movimiento> listar_mov(string nro)
        {
            List<Movimiento> cuentas = new List<Movimiento>();


            // Ejecuta el procedimiento almacenado
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("pro_listar_mov", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                // Agregar el parámetro de entrada para el número de cuenta
                cmd.Parameters.AddWithValue("@nro_cuenta", nro);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    // Usamos un objeto anónimo para incluir la nueva propiedad sin modificar el modelo
                    cuentas.Add(new Movimiento
                    {
                        FECHA = Convert.ToDateTime(reader["FECHA"]),
                        IMPORTE = (double)Convert.ToDecimal(reader["IMPORTE"]),
                        TIPO = reader["TIPO"].ToString()
                    });
                }

                conn.Close();
            }

            return cuentas;

        }

    }
}