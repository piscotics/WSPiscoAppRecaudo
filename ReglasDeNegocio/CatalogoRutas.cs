using FirebirdSql.Data.FirebirdClient;
using ModelSincronizador.Connection;
using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio
{
    public class CatalogoRutas
    {
        string _cadenaconexionWeb;
        string _cadenaconexion;


        public CatalogoRutas()
        {
        }

        public CatalogoRutas(string cadenaConexion, string Cadenaconexionweb)
        {
            this._cadenaconexion = cadenaConexion;
            this._cadenaconexionWeb = Cadenaconexionweb;
        }

        public List<string> ConsultaLstRutas(RutaRequest consulta)
        {
            Conection db = new Conection();
           // CuadreCajaDTO cuadre = new CuadreCajaDTO();
            List<string> lstRutas = new List<string>();

            try
            {

                db.FbConeccion(this._cadenaconexionWeb);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexionWeb))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SW_LISTADORUTAS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = consulta.Dato;
                        cmd.Parameters.Add("@FECHA", FbDbType.Date).Value = consulta.Fecha;

                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string rutas = string.Empty;
                            try
                            {
                                rutas += "Persona: " + datos.GetString(1);
                                rutas += " - Nro Documento: " + datos.GetString(0);
                                rutas += " - Fecha Pago:  " + (datos.GetDateTime(2)).ToString("dd/MMM/yyyy");
                                rutas += " - Nro Recibo: " + datos.GetString(3);
                                rutas += " - Descuento: " + datos.GetString(4);
                                rutas += " - Valor Pagado: " + (datos.GetString(5)).ToString();
                                rutas += " - Observaciones: " + (datos.GetString(6)).ToString();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando pagos por contrato" + ex.Message);
                            }

                            lstRutas.Add(rutas);
                        }
                    }
                }


                return lstRutas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Sacando Lista Pagos ");
            }
            finally
            {
                db.FbConectionClose();
            }
        }
    }
}
