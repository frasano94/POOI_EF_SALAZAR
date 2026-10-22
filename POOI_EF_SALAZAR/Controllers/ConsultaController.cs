using POOI_EF_SALAZAR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_EF_SALAZAR.Controllers
{
    public class ConsultaController : Controller
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

        // Método conectado para obtener la consulta agrupada filtrada por año
        IEnumerable<EmpleadoPedidos> ConsultarPedidosPorAnio(int anio)
        {
            List<EmpleadoPedidos> lista = new List<EmpleadoPedidos>();

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_empleados_cantidad_pedidos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@anio", anio);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new EmpleadoPedidos(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetInt32(2)
                    ));
                }
            }
            return lista;
        }

        // GET: Consulta/Index
        public ActionResult Index(int? anio)
        {
            // Si el usuario no ha ingresado un año, colocamos un año por defecto (ej. 2024 o el año actual)
            int anioConsulta = anio ?? DateTime.Now.Year;
            ViewBag.anioActual = anioConsulta;

            return View(ConsultarPedidosPorAnio(anioConsulta));
        }
    }
}
