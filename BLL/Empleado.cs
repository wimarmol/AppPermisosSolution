using DAL;
using Dapper;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<Permiso> Permisos { get; set; }

        private readonly DataConnection dataConnection = new();

        public int Save(Empleado empleado)
        {
            var parameters = new DynamicParameters();
            parameters.Add("nombreEmpleado", empleado.NombreEmpleado);
            parameters.Add("apellidosEmpleado", empleado.ApellidosEmpleado);
            parameters.Add("fechaNacimiento", empleado.FechaNacimiento.ToString(dataConnection.formatDate));
            parameters.Add("fechaIngreso", empleado.FechaIngreso.ToString(dataConnection.formatDate));
            return dataConnection.ExecuteGetId("saveEmpleado", parameters);
        }
        public List<Empleado> List()
        {
            return dataConnection.GetList<Empleado>("listEmpleado");
        }

        public List<Empleado> ListTodosPermisos(DateTime inicio, DateTime fin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("fechaHoraInicioPermiso", inicio.ToString(dataConnection.formatDateHour));
            parameters.Add("fechaHoraFinPermiso", fin.ToString(dataConnection.formatDateHour));
            return dataConnection.GetList<Empleado>("listEmpladoTodosPermisos", parameters);
        }

        public Empleado Get(int id) {
            var empleado = new Empleado();
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var empleadoList = dataConnection.GetList<Empleado>("getEmpleado", parameters);
            empleado = (empleadoList.Count > 0) ? empleadoList[0] : null;
            return empleado;
        }
        public int Update(Empleado empleado)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", empleado.Id);
            parameters.Add("nombreEmpleado", empleado.NombreEmpleado);
            parameters.Add("apellidosEmpleado", empleado.ApellidosEmpleado);
            parameters.Add("fechaNacimiento", empleado.FechaNacimiento.ToString(dataConnection.formatDate));
            parameters.Add("fechaIngreso", empleado.FechaIngreso.ToString(dataConnection.formatDate));
            return dataConnection.ExecuteGetId("updateEmpleado", parameters);
        }
        public int Delete(Empleado empleado)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", empleado.Id);
            return dataConnection.ExecuteGetId("deleteEmpleado", parameters);
        }
    }
}
