using Dapper;
using DAL;
using System.Collections.Generic;


namespace BLL
{
    public class TipoPermiso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        private readonly DataConnection dataConnection = new();

        public int Save(TipoPermiso tipoPermiso) {
            var parameters = new DynamicParameters();
            parameters.Add("descripcion", tipoPermiso.Descripcion);            
            return dataConnection.ExecuteGetId("saveTipoPermiso", parameters);
        }
        public List<TipoPermiso> List() {
            return dataConnection.GetList<TipoPermiso>("listTipoPermiso");
        }
        public int Update(TipoPermiso tipoPermiso) {
            var parameters = new DynamicParameters();
            parameters.Add("id", tipoPermiso.Id);
            parameters.Add("descripcion", tipoPermiso.Descripcion);
            return dataConnection.ExecuteGetId("updateTipoPermiso", parameters);
        }
        public int Delete(TipoPermiso tipoPermiso)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", tipoPermiso.Id);           
            return dataConnection.ExecuteGetId("deleteTipoPermiso", parameters);
        }
    }
}
