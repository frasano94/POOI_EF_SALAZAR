using POOI_EF_SALAZAR;
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
    public class ConsumidorController : Controller
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

        // 1. Método auxiliar para listar usando el Store Procedure
        IEnumerable<Consumidor> ListarConsumidores()
        {
            List<Consumidor> lista = new List<Consumidor>();
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listar_consumidor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Consumidor(
                        dr.GetString(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.IsDBNull(3) ? "" : dr.GetString(3),
                        dr.IsDBNull(4) ? "" : dr.GetString(4)
                    ));
                }
            }
            return lista;
        }

        // 2. Método auxiliar para guardar usando el SP y capturar el mensaje OUTPUT
        string GuardarConsumidor(Consumidor c)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_agrega_consumidor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@dnicont", c.Dnicont ?? "");
                cmd.Parameters.AddWithValue("@nomcont", c.Nomcont ?? "");
                cmd.Parameters.AddWithValue("@apecont", c.Apecont ?? "");
                cmd.Parameters.AddWithValue("@emailcont", c.Emailcont ?? "");
                cmd.Parameters.AddWithValue("@idpais", c.Idpais ?? "");

                SqlParameter pMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100);
                pMensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pMensaje);

                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = pMensaje.Value.ToString();
            }
            return mensaje;
        }

        // 3. Método auxiliar para eliminar por DNI
        string EliminarConsumidor(string dnicont)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("usp_eliminar_consumidor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@dnicont", dnicont);

                SqlParameter pMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 100);
                pMensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pMensaje);

                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = pMensaje.Value.ToString();
            }
            return mensaje;
        }

        // --- ACCIONES (ActionResult) ---

        public ActionResult Index()
        {
            return View(ListarConsumidores());
        }

        public ActionResult Create()
        {
            return View(new Consumidor());
        }

        [HttpPost]
        public ActionResult Create(Consumidor consumidor)
        {
            string mensaje = GuardarConsumidor(consumidor);
            ViewBag.mensaje = mensaje;
            return View(consumidor);
        }

        public ActionResult Delete(string id)
        {
            string mensaje = EliminarConsumidor(id);
            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }
    }
}