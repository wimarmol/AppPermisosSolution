using DAL;
using Dapper;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Permiso
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }        
        public string TipoPermiso { get; set; }
        public int TipoPermisoId { get; set; }
        public DateTime FechaHoraInicioPermiso { get; set; }
        public DateTime FechaHoraFinPermiso { get; set; }
        
        // propiedadses auxiliares 
        public List<Empleado> Empleados { get; set; }
        public string Empleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public int Cantidad { get; set; }

        private readonly DataConnection dataConnection = new();

        public int Save(Permiso permiso)
        {
            var parameters = new DynamicParameters();
            parameters.Add("empleadoId", permiso.EmpleadoId);
            parameters.Add("tipoPermisoId", permiso.TipoPermisoId);
            parameters.Add("fechaHoraInicioPermiso", permiso.FechaHoraInicioPermiso.ToString(dataConnection.formatDateHour));
            parameters.Add("fechaHoraFinPermiso", permiso.FechaHoraFinPermiso.ToString(dataConnection.formatDateHour));
            return dataConnection.ExecuteGetId("savePermiso", parameters);
        }
        public List<Permiso> List()
        {
            return dataConnection.GetList<Permiso>("listPermiso");
        }
        public List<Permiso> ListPermisoEmpleadoCantidad(DateTime inicio, DateTime fin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("fechaHoraInicioPermiso", inicio.ToString(dataConnection.formatDateHour));
            parameters.Add("fechaHoraFinPermiso", fin.ToString(dataConnection.formatDateHour));
            return dataConnection.GetList<Permiso>("listPermisoEmpleadoCantidad",parameters);
        }
        public List<Permiso> List(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("empleadoId", id);
            return dataConnection.GetList<Permiso>("listPermisoEmpleado", parameters);
        }
        public Permiso Get(int id)
        {
            var permiso = new Permiso();
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var permisoList = dataConnection.GetList<Permiso>("getPermiso", parameters);
            permiso = (permisoList.Count > 0) ? permisoList[0] : null;
            return permiso;
        }
        public int Update(Permiso permiso)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", permiso.Id);
            parameters.Add("empleadoId", permiso.EmpleadoId);
            parameters.Add("tipoPermisoId", permiso.TipoPermisoId);
            parameters.Add("fechaHoraInicioPermiso", permiso.FechaHoraInicioPermiso.ToString(dataConnection.formatDateHour));
            parameters.Add("fechaHoraFinPermiso", permiso.FechaHoraFinPermiso.ToString(dataConnection.formatDateHour));
            return dataConnection.ExecuteGetId("updatePermiso", parameters);
        }
        public int Delete(Permiso permiso)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", permiso.Id);
            return dataConnection.ExecuteGetId("deletePermiso", parameters);
        }
    }
}
