//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DALPermisos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permiso
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoPermisoId { get; set; }
        public System.DateTime FechaHoraInicioPermiso { get; set; }
        public System.DateTime FechaHoraFinPermiso { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual TipoPermiso TipoPermiso { get; set; }
    }
}