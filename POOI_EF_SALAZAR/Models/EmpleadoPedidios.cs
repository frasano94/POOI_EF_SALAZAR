using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_EF_SALAZAR.Models
{
    public class EmpleadoPedidos
    {
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public int CantidadPedidos { get; set; }

        public EmpleadoPedidos() { }

        public EmpleadoPedidos(int idEmpleado, string empleado, int cantidadPedidos)
        {
            IdEmpleado = idEmpleado;
            Empleado = empleado;
            CantidadPedidos = cantidadPedidos;
        }
    }
}