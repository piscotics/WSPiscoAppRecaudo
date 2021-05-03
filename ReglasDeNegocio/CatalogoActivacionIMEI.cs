using ModelSincronizador;
using ModelSincronizador.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace ReglasDeNegocio
{
    public class CatalogoActivacionIMEI
    {
        string _cadenaconexion  ;
    
        
        public CatalogoActivacionIMEI() {
        }

        public CatalogoActivacionIMEI(string cadenaConexion)
        {
            this._cadenaconexion = cadenaConexion;
            
        }

        public  void GrabarActivacionImei(TblActivacionIMEI ActivacionImei)
        {
            Conection db = new Conection();

            try
            {
                db.FbConeccion();
                db.FbConectionOpen();
                

                string sql = "INSERT TBLACTIVACIONIMEI (IDACTIVACIONIMEI,FECHAREGISTRO,IMEI" +
                    ",ESTADO,USUARIO) VALUES (@Codigo,@Fecha,@Imei" +
                    ",@Estado,@Usuario";

                db.ComenzarTransaccion();
                db.CreateComando(sql);
                db.AsignarParametrosInt("@Codigo", ActivacionImei.IDACTIVACIONIMEI);
                db.AsignarParametrosString("@Imei", ActivacionImei.IMEI);
                db.AsignarParametrosFecha("@Fecha", ActivacionImei.FECHAREGISTRO);
                db.AsignarParametrosString("@Estado", ActivacionImei.ESTADO);
                db.AsignarParametrosString("@Usuario", ActivacionImei.USUARIO);
                db.EjecutarComando();
                db.ConfirmarTransaccion();
                
            }
            catch (Exception ex)
            {
                db.CancelarTransaccion();
                throw new Exception("Error Creando Imei " + ex.Message);
                
            }
            finally
            {
                db.FbConectionClose();
            }
        }



        public List<TblActivacionIMEI> ObtenerImeis()
        {
            List<TblActivacionIMEI> imeis = new List<TblActivacionIMEI>();
            Conection db = new Conection();
            try
            {
                string sql = "SELECT IDACTIVACIONIMEI,FECHAREGISTRO,IMEI,ESTADO,USUARIO FROM TBLACTIVACIONIMEI";

                db.FbConeccion();
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                TblActivacionIMEI imei = null;
                while (datos.Read())
                {
                    try
                    {
                        imei = new TblActivacionIMEI();
                        imei.IDACTIVACIONIMEI = datos.GetInt32(0);
                        imei.FECHAREGISTRO = datos.GetDateTime(1);
                        imei.IMEI = datos.GetString(2);
                        imei.ESTADO = datos.GetString(3);
                        imei.USUARIO = datos.GetString(4);

                        imeis.Add(imei);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new Exception("Los tipos no coinciden.", ex);
                    }
                    catch (DataException ex)
                    {
                        throw new Exception("Error de ADO.NET.", ex);
                    }
                }
                datos.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al acceder a la base de datos para obtener los imeis."  +  ex.Message);
            }
           finally
            {
                db.FbConectionClose();
            }

            return imeis;
        }



        public void ActualizarImei(TblActivacionIMEI ActivacionImei)
        {
            Conection db = new Conection();
            try
            {
                db.FbConeccion();
                db.FbConectionOpen();
                db.ComenzarTransaccion();

                string sql = "UPDATE TBLACTIVACIONIMEI " +
                    "SET IMEI=@Imei,ESTADO=@Estado,USUARIO=@Usuario " +
                    "WHERE IDACTIVACIONIMEI=@Codigo";
                db.CreateComando(sql);
                db.AsignarParametrosInt("@Codigo", ActivacionImei.IDACTIVACIONIMEI);
                db.AsignarParametrosString("@Imei", ActivacionImei.IMEI);
                db.AsignarParametrosString("@Estado", ActivacionImei.ESTADO);
                db.AsignarParametrosString("@Usuario", ActivacionImei.USUARIO);
                db.EjecutarComando();
                db.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                db.CancelarTransaccion();
                throw new Exception("Error al actualizar la activacion imei :\n" + ex.Message, ex);
            }
            finally { db.FbConectionClose(); }

        }






    }
}
