using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModelSincronizador.Connection
{
    public class Conection
    {
        FbConnection _Conect;
        FbDataAdapter adaptador;
        FbCommand comando;
        DataTable tabla;
        string respuesta;
        string dato;
        private FbTransaction sqlTransaccion = null;
        
        
        public void FbConeccion()
        {
            _Conect = new FbConnection();
            //_Conect.ConnectionString = "User=SYSDBA;password=masterkey;DataSource=localhost;Database=192.168.0.38:D:\\Kaux\\Base de Datos\\KAUX.FDB ;Charset=WIN1252;Dialect=3";
            //_Conect.ConnectionString = "User=SYSDBA;password=masterkey;DataSource=localhost;Database=D:\\Bd Pruebas\\KAUX.FDB ;Charset=WIN1252;Dialect=3";
        }

        public void FbConeccion(string ConnectionString)
        {
            _Conect = new FbConnection();
            _Conect.ConnectionString = ConnectionString;
        }

        public bool FbConectionOpen()
        {
            try
            {
                _Conect.Open();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool FbConectionClose()
        {
            try
            {
                _Conect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public DataTable BuscaDatos(String str) //solo recibe un select
        {

            FbConectionOpen();

            try
            {

                adaptador = new FbDataAdapter(str, _Conect);

                tabla = new DataTable();

                adaptador.Fill(tabla); //llena datatable

                return tabla; //si hay error retorna nulo nada

            }

            catch
            {

                return null;

            }

        }


        public string reader(string consulta)
        {

            try
            {
                comando = new FbCommand(consulta, _Conect);
                comando.ExecuteNonQuery();
                respuesta = Convert.ToString(comando.ExecuteScalar());
                respuesta = respuesta.Trim();
                return respuesta;
            }
            catch
            {
                return null;
            }
        }

        public int readerint(string consulta)
        {
            int respuesta;
            try
            {

                comando = new FbCommand(consulta, _Conect);
                comando.ExecuteNonQuery();
                respuesta = Convert.ToInt32(comando.ExecuteScalar());
                return respuesta;
            }
            catch
            {
                return respuesta = 0;
            }
        }



        public FbCommand consulta(string consulta)
        {
            comando = new FbCommand(consulta, _Conect);
            comando.ExecuteNonQuery();
            return comando;
        }

        //public SqlDataAdapter consultaAdapter(string consulta)
        //{
        //    comando = new SqlCommand(consulta, conecta);
        //    comando.ExecuteNonQuery();
        //    SqlDataAdapter da = new SqlDataAdapter(comando);
        //    return da;
        //}

        public bool buscar(string consulta)
        {
            comando = new FbCommand(consulta, _Conect);
            dato = Convert.ToString(comando.ExecuteScalar());
            if (dato == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateComando(string consulta)
        {
            try
            {
                comando = new FbCommand(consulta, _Conect);
                
            }
            catch (Exception ex)
            {
                throw new Exception("No se creo Comando" + ex.Message);
            }


            this.comando = _Conect.CreateCommand();
            this.comando.Connection = _Conect;
            this.comando.CommandType = CommandType.Text;
            this.comando.CommandText = consulta;

            if (this.sqlTransaccion != null)
            {
                this.comando.Transaction = this.sqlTransaccion;
            }

          

        }

        public void CreateComandProcedure(string nameProcedure) {
            try
            {
                comando = new FbCommand(nameProcedure, _Conect);

                this.comando = _Conect.CreateCommand();
                this.comando.Connection = _Conect;
                this.comando.CommandType = CommandType.StoredProcedure;
                this.comando.CommandText = nameProcedure;
            }
            catch (Exception ex)
            {
                throw new Exception("No se creo Comando" + ex.Message);
            }
        }


        public void ComenzarTransaccion()
        {
            if (Object.Equals(this.sqlTransaccion, null) && !Equals(this._Conect, null))
            {
                this.sqlTransaccion = this._Conect.BeginTransaction();
            }
          
        }

        public void AsignarParametrosString(string parametro, string valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros String " + ex.Message);
            }

        }

        public void AsignarParametrosInt(string parametro, Int32 valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Entero " + ex.Message);
            }

        }


        public void AsignarParametrosIntLong(string parametro, Int64 valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Entero Largo" + ex.Message);
            }

        }


        public void AsignarParametrosFecha(string parametro, DateTime valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Entero " + ex.Message);
            }

        }

        public void AsignarParametrosFechaNull(string parametro, DateTime? valor)
        {
            try
            {
                if (valor != null)
                    comando.Parameters.AddWithValue(parametro, valor);
                else
                    comando.Parameters.AddWithValue(parametro, DBNull.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Entero " + ex.Message);
            }

        }

        public void AsignarParametrosBoolean(string parametro, bool valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Bool " + ex.Message);
            }

        }


        public void AsignarParametrosDouble(string parametro, Double valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Asignando Parametros Double " + ex.Message);
            }

        }

        public FbDataReader EjecutarConsulta()
        {
            try
            {
                //comando = new SqlCommand(consulta, conecta);
                return this.comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Problema Ejecutar Comando" + ex.Message);
            }
        }


        public FbDataAdapter consultaAdapter(string consulta)
        {
            //comando = new SqlCommand(consulta, conecta);
            comando.ExecuteNonQuery();
            FbDataAdapter da = new FbDataAdapter(comando);
            return da;
        }



        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }


        public void ConfirmarTransaccion()
        {
            if (this.sqlTransaccion != null)
            {
                this.sqlTransaccion.Commit();
            }
        }

        public void CancelarTransaccion()
        {
            if (this.sqlTransaccion != null)
            {
                try
                {
                    this.sqlTransaccion.Rollback();
                }
                catch
                {
                    return;
                }
            }
        }

        public void EjecutarComando()
        {
            try
            {
                    this.comando.ExecuteNonQuery();

            }
            catch (FbException ex)
            {

                throw new Exception("Comando SQL no valido : " + ex.Message, ex);
            }
            
        }

        public int EjecutarComandoProcedure() {
            try
            {
                int result = (int)this.comando.ExecuteScalar();
                return result;
            }
            catch (FbException ex)
            {

                throw new Exception("Comando SQL no valido : " + ex.Message, ex);
            }
        }

    }
}
