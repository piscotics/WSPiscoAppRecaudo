using FirebirdSql.Data.FirebirdClient;
using ModelSincronizador;
using ModelSincronizador.Connection;
using ModelSincronizador.DTO;
using ReglasDeNegocio.DTO;
using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio
{
    public class CatalogoUsuarios
    {

        string _cadenaconexion;
        string _cadenaconexionp;


        public CatalogoUsuarios()
        {
        }

        public CatalogoUsuarios(string cadenaConexion)
        {
            this._cadenaconexion = cadenaConexion;

        }


        public CatalogoUsuarios(string cadenaConexion, string cadenaconexionp)
        {
            this._cadenaconexion = cadenaConexion;
            this._cadenaconexionp = cadenaconexionp;
        }


        public List<PosicisionDTO> ListaUbicaciones(UbicationRequest ubi)
        {
            Conection db = new Conection();
            PosicisionDTO posicion = new PosicisionDTO();
            List<PosicisionDTO> lst = new List<PosicisionDTO>();

            try
            {

                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTAPOSICIONXY";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ubi.Usuario == "9999")
                            cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = "";
                        else
                            cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = ubi.Usuario;

                        cmd.Parameters.Add("@FECHA", FbDbType.Date).Value = ubi.Fecha;




                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            posicion = new PosicisionDTO();
                            posicion.Id = row[0].ToString();
                            posicion.PosX = row[1].ToString();
                            posicion.Posy = row[2].ToString();
                            posicion.Tipo = row[3].ToString();


                            //CatalogoContrato catalogocontrato = new CatalogoContrato(this._cadenaconexionp);
                            //ContratoDTO contrato = catalogocontrato.ConsultarContrato(row[5].ToString());

                            posicion.IdContrato = row[5].ToString();
                            posicion.Nombre = row[6].ToString(); ;
                            posicion.Valor = Convert.ToDouble(row[7].ToString());

                            lst.Add(posicion);
                        }
                    }

                    return lst;
                }
            }
            catch (Exception ex)
            {
                return new List<PosicisionDTO>();
            }
            finally {
                db.FbConectionClose();
            }

            
        }

      

        public List<TblUsuarios> ConsultarUsuario()
        {

            List<TblUsuarios> lst = new List<TblUsuarios>();
            TblUsuarios usuarioBd = new TblUsuarios();
            Conection db = new Conection();
            try
            {
                string sql = "SELECT ID,USERNAME,CLAVE,ESTADO,FECHAINICIAL,FECHAFINAL,HORAINICIAL, HORAFINAL,IDCOBRADOR, NOMBRES,APELLIDOS,MAQUINA,NIT,PREFIJO,IDCAJAIND,IDCAJAEMP, IDCAJAPAR,IDCAJA, IDCAJAANT  FROM TBLUSUARIOS ORDER BY NOMBRES || ' ' || APELLIDOS, ESTADO ";


                
                //Se agrega usuario todos
                usuarioBd = new TblUsuarios();
                usuarioBd.ID = 1;
                usuarioBd.USERNAME = "9999";
                usuarioBd.CLAVE = "";
                usuarioBd.ESTADO = "";
                usuarioBd.FECHAINICIAL = DateTime.Now;
                usuarioBd.FECHAFINAL = DateTime.Now;
                usuarioBd.HORAINICIAL = DateTime.Now;
                usuarioBd.HORAFINAL = DateTime.Now;
                usuarioBd.IDCOBRADOR = "";
                usuarioBd.NOMBRES = "TODOS";
                usuarioBd.APELLIDOS = "";
                usuarioBd.MAQUINA = "";
                usuarioBd.NIT = "";
                usuarioBd.PREFIJO = "";
                usuarioBd.IDCAJAIND = 0;
                usuarioBd.IDCAJAEMP = 0;
                usuarioBd.IDCAJAPAR = 0;
                usuarioBd.IDCAJA = 0;
                usuarioBd.IDCAJAANT = 0;
                lst.Add(usuarioBd);








                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    try
                    {
                        usuarioBd = new TblUsuarios();
                        usuarioBd.ID = datos.GetInt16(0);
                        usuarioBd.USERNAME = datos.GetString(1);
                        usuarioBd.CLAVE = datos.GetString(2);
                        usuarioBd.ESTADO = datos.GetString(3);
                        usuarioBd.FECHAINICIAL = datos.GetDateTime(4);
                        usuarioBd.FECHAFINAL = datos.GetDateTime(5);
                        usuarioBd.HORAINICIAL = datos.GetDateTime(6);
                        usuarioBd.HORAFINAL = datos.GetDateTime(7);
                        usuarioBd.IDCOBRADOR = datos.GetString(8);
                        usuarioBd.NOMBRES = datos.GetString(9);
                        usuarioBd.APELLIDOS = datos.GetString(10);
                        usuarioBd.MAQUINA = datos.GetString(11);
                        usuarioBd.NIT = datos.GetString(12);
                        usuarioBd.PREFIJO = datos.GetString(13);
                        usuarioBd.IDCAJAIND = datos.GetInt16(14);
                        usuarioBd.IDCAJAEMP = datos.GetInt16(15);
                        usuarioBd.IDCAJAPAR = datos.GetInt16(16);
                        usuarioBd.IDCAJA = datos.GetInt16(17);
                        if(datos[18].GetType().Name != "DBNull")
                            usuarioBd.IDCAJAANT = datos.GetInt16(18);

                        lst.Add(usuarioBd);
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
                throw new Exception("Error al acceder a la base de datos para obtener los imeis." + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            return lst;
        }


        public TblUsuarios ConsultarUsuarioLogin(string usuario, string clave)
        {
            TblUsuarios usuarioBd = new TblUsuarios();
            Conection db = new Conection();
            try
            {
                string sql = "SELECT ID,USERNAME,CLAVE,ESTADO,FECHAINICIAL,FECHAFINAL,HORAINICIAL, HORAFINAL,IDCOBRADOR, NOMBRES,APELLIDOS,MAQUINA,NIT,PREFIJO,IDCAJAIND,IDCAJAEMP, IDCAJAPAR,IDCAJA, IDCAJAANT  FROM TBLUSUARIOS "+
                             "  WHERE USERNAME='"+ usuario +"'";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    try
                    {
                        usuarioBd = new TblUsuarios();
                        usuarioBd.ID = datos.GetInt16(0);
                        usuarioBd.USERNAME = datos.GetString(1);
                        usuarioBd.CLAVE = datos.GetString(2);
                        usuarioBd.ESTADO = datos.GetString(3);
                        usuarioBd.FECHAINICIAL = datos.GetDateTime(4);
                        usuarioBd.FECHAFINAL = datos.GetDateTime(5);
                        usuarioBd.HORAINICIAL = datos.GetDateTime(6);
                        usuarioBd.HORAFINAL = datos.GetDateTime(7);
                        usuarioBd.IDCOBRADOR = datos.GetString(8);
                        usuarioBd.NOMBRES = datos.GetString(9);
                        usuarioBd.APELLIDOS = datos.GetString(10);
                        usuarioBd.MAQUINA = datos.GetString(11);
                        usuarioBd.NIT = datos.GetString(12);
                        usuarioBd.PREFIJO = datos.GetString(13);
                        usuarioBd.IDCAJAIND = datos.GetInt16(14);
                        usuarioBd.IDCAJAEMP = datos.GetInt16(15);
                        usuarioBd.IDCAJAPAR = datos.GetInt16(16);
                        usuarioBd.IDCAJA = datos.GetInt16(17);
                        if (datos[18].GetType().Name != "DBNull")
                            usuarioBd.IDCAJAANT = datos.GetInt16(18);

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
                throw new Exception("Error al acceder a la base de datos para obtener los imeis." + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            return usuarioBd;
        }


        public bool ConsultarLicencia(string Licencia, string Usuario)
        {
            if (Usuario != "PISCO")
            {
                Conection db = new Conection();
           
                try
                {
                    string sql = "SELECT *  FROM TBLLICENCIAS " +
                                 "  WHERE CODIGOLICENCIA='" + Licencia + "'";

                    db.FbConeccion(_cadenaconexion);
                    db.FbConectionOpen();
                    db.ComenzarTransaccion();
                    db.CreateComando(sql);
                    FbDataReader datos = db.EjecutarConsulta();

                    while (datos.Read())
                    {

                        Conection db2 = new Conection();

                        db2.FbConeccion(_cadenaconexion);
                        db2.FbConectionOpen();
                        db2.ComenzarTransaccion();

                        string sql2 = "UPDATE TBLLICENCIAS SET USUARIO='" + Usuario + "'" +
                                  "  WHERE CODIGOLICENCIA='" + Licencia + "'";

                        db2.CreateComando(sql2);
                        db2.EjecutarComando();
                        db2.ConfirmarTransaccion();

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    db.FbConectionClose();
                }
            }
            else
            {
                return true;
            }
            
        }
               
        public TblUsuarios ConsultarUsuarioLoginSP(string usuario, string clave)
        {
            TblUsuarios usuarioBd = new TblUsuarios();
            Conection db = new Conection();
            try
            {

                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_VALIDARUSUARIOSF";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", FbDbType.VarChar).Value = "USUARIO";
                        cmd.Parameters.Add("@PARAMETRO", FbDbType.Date).Value = usuario;




                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);


                        usuarioBd = dt.AsEnumerable().Select(m => new TblUsuarios()
                        {
                            ID = 0,
                            USERNAME = m.Field<string>("USERNAME"),
                            CLAVE = m.Field<string>("CLAVE"),
                            ESTADO = m.Field<string>("ESTADO"),
                            IDCOBRADOR = m.Field<string>("IDCOBRADOR"),
                            NOMBRES = m.Field<string>("COBRADOR"),
                            APELLIDOS = m.Field<string>("CLASEDOC"),
                            NIT = m.Field<string>("NIT"),
                            PREFIJO = m.Field<string>("PREFIJO"),
                            IDCAJAIND =0,
                            IDCAJAEMP=0,
                            IDCAJAPAR  =0,
                            IDCAJA = 0,
                            IDCAJAANT=0
                        }).FirstOrDefault();

                       
                    }

                }
            }
            catch (Exception ex)
            {
                return new TblUsuarios();
            }
            finally
            {
                db.FbConectionClose();
            }

            return usuarioBd;
        }

        public bool AgregarLicencia(string Licencia)
        {

            int CantidadLicencias = 0;
            Conection db = new Conection();
            if (ConsultarLicenciaEnUso(Licencia))
            {
                try
                {
                    string sql = "select licencias - (select count(*) from tbllicencias) from tblfuneraria ";

                    db.FbConeccion(_cadenaconexion);
                    db.FbConectionOpen();
                    db.ComenzarTransaccion();
                    db.CreateComando(sql);
                    FbDataReader datos = db.EjecutarConsulta();


                    while (datos.Read())
                    {
                        CantidadLicencias = datos.GetInt32(0);

                    }



                    if (CantidadLicencias > 0)
                    {

                        string SqlInsert = "insert into TBLLICENCIAS ( CODIGOLICENCIA ) values ('" + Licencia + "')";

                        Conection db2 = new Conection();
                        db2.FbConeccion(_cadenaconexion);
                        db2.FbConectionOpen();
                        db2.ComenzarTransaccion();
                        db2.CreateComando(SqlInsert);
                        db2.EjecutarComando();
                        db2.ConfirmarTransaccion();
                        db2.FbConectionClose();
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    db.FbConectionClose();
                }
            }
            else
                return false;
        }


        public bool ConsultarLicenciaEnUso(string Licencia)
        {
            Conection db = new Conection();
            try
            {
                string sql = "SELECT *  FROM TBLLICENCIAS " +
                             "  WHERE CODIGOLICENCIA='" + Licencia + "'";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    return false;
                }

                return true;
            }
            catch{
                return false;
            }
        }

        public bool RemoverLicencia(string Licencia) {

            Conection db = new Conection();

            try
            {
                string sql = "DELETE FROM TBLLICENCIAS WHERE CODIGOLICENCIA='"+ Licencia +"' ";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                db.EjecutarComando();
                db.ConfirmarTransaccion();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<String> ConsultarLicencias()
        {

            List<String> lst = new List<String>();
            String licencia = string.Empty;
            Conection db = new Conection();
            try
            {
                string sql = "SELECT CODIGOLICENCIA FROM TBLLICENCIAS ";



                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    try
                    {
                        licencia = string.Empty;
                        licencia = datos.GetString(0);
                        lst.Add(licencia);
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
                throw new Exception("Error al acceder a la base de datos para obtener las licencias." + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            return lst;
        }

        public List<TblRutas> LstRuta()
        {
            List<TblRutas> lst = new List<TblRutas>();
            TblRutas ruta = new TblRutas();
            Conection db = new Conection();

            try
            {
                string sql = "select USUARIO, IDCOBRADOR, IDCONTRATO, CEDULA, TITULAR, DIRECCION , " +
                            "TELEFONO, CIUDAD, DIACOBRO1, DIACOBRO2, ESTADO, NOVEDAD, POSTFECHADODIA, " +
                            "INDICE, CUOTA, PENDIENTE, ESTADOCONTRATO,FECHAR, BASEDATOS, '' AS MODULO, " +
                            " EMPRESA, NIT, DIRECCIONCOBRO, BOXCONTRATANTE, VALORCARTERA, VALORSEGURO, " +
                            "CELULAR, PAGOHASTA, DEPTOC, MPIOC, BARRIOC, MOTIVO, FECHAPROGRAMADA, " +
                            "CODBARRIO, COBERTURA from TBLRUTAS R ";

                db.FbConeccion(_cadenaconexionp);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    try
                    {
                        ruta = new TblRutas();
                        ruta.USUARIO = datos.GetString(0);
                        ruta.IDCOBRADOR = datos.GetString(1);
                        ruta.IDCONTRATO = datos.GetString(2);
                        ruta.CEDULA = datos.GetString(3);
                        ruta.TITULAR = datos.GetString(4);
                        ruta.DIRECCION = datos.GetString(5);
                        ruta.TELEFONO = datos.GetString(6);
                        ruta.CIUDAD = datos.GetString(7);
                        ruta.DIACOBRO1 = datos.GetInt16(8);
                        ruta.DIACOBRO2 = datos.GetInt16(9);
                        ruta.ESTADO = datos.GetString(10);
                        ruta.NOVEDAD = datos.GetInt16(11);
                        ruta.POSTFECHADODIA = datos.GetInt16(12);
                        ruta.INDICE = datos.GetInt16(13);
                        ruta.CUOTA = datos.GetFloat(14);
                        ruta.PENDIENTE = datos.GetInt16(15);
                        ruta.ESTADOCONTRATO = datos.GetString(16);
                        ruta.FECHAR = datos.GetDateTime(17);
                        ruta.BASEDATOS = datos.GetString(18);
                        ruta.MODULO = datos.GetString(19);
                        ruta.EMPRESA = datos.GetString(20);
                        ruta.NIT = datos.GetString(21);
                        ruta.DIRECCIONCOBRO = datos.GetString(22);
                        ruta.BOXCONTRATANTE = datos.GetString(23);
                        ruta.VALORCARTERA = datos.GetFloat(24);
                        ruta.VALORSEGURO = datos.GetFloat(25);
                        ruta.CELULAR = datos.GetString(26);
                        ruta.PAGOHASTA = datos.GetDateTime(27);
                        ruta.DEPTOC = datos.GetString(28);
                        ruta.MPIOC = datos.GetString(29);
                        ruta.BARRIOC = datos.GetString(30);
                        ruta.MOTIVO = datos.GetString(31);
                        ruta.FECHAPROGRAMADA = datos.GetString(32);
                        ruta.CODBARRIO = datos.GetString(33);
                        ruta.COBERTURA = datos.GetString(34);
                        lst.Add(ruta);
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
                throw new Exception("Error al acceder a la base de datos para obtener las rutas." + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            return lst;
        }

        public List<TblRutas> LstRutaSP(String Cobrador)
        {
            List<TblRutas> lst = new List<TblRutas>();
            TblRutas ruta = new TblRutas();
            Conection db = new Conection();

            try
            {

                using (FbConnection conn = new FbConnection(this._cadenaconexionp))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARRUTA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NUMCOBRADOR", FbDbType.VarChar).Value = Cobrador;
                        cmd.Parameters.Add("@FECHARUTA", FbDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");



                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            ruta = new TblRutas();
                            ruta.USUARIO = row[1].ToString();
                            ruta.IDCOBRADOR = row[2].ToString();
                            ruta.IDCONTRATO = row[3].ToString();
                            ruta.CEDULA = row[4].ToString();
                            ruta.TITULAR = row[5].ToString();
                            ruta.DIRECCION = row[6].ToString();
                            ruta.TELEFONO = row[7].ToString();
                            ruta.CIUDAD = row[8].ToString();
                            ruta.DIACOBRO1 = Convert.ToInt16(row[9].ToString());
                            ruta.DIACOBRO2 = Convert.ToInt16(row[10].ToString());
                            ruta.ESTADO = row[11].ToString();
                            ruta.NOVEDAD = Convert.ToInt16(row[12].ToString());
                            ruta.POSTFECHADODIA =Convert.ToInt16( row[13].ToString());
                            ruta.INDICE = Convert.ToInt16(row[14].ToString());
                            ruta.CUOTA = Convert.ToInt64(row[15].ToString());
                            ruta.PENDIENTE = Convert.ToInt16(row[16].ToString());
                            ruta.ESTADOCONTRATO = row[17].ToString(); ;
                            ruta.FECHAR =Convert.ToDateTime(row[18].ToString()); 
                            ruta.BASEDATOS = row[22].ToString(); ;
                            //ruta.MODULO = row[19].ToString(); ;
                            ruta.EMPRESA = row[23].ToString(); ;
                            ruta.NIT = row[24].ToString(); ;
                            ruta.DIRECCIONCOBRO = row[25].ToString(); ;
                            ruta.BOXCONTRATANTE = row[26].ToString(); ;
                            ruta.VALORCARTERA = float.Parse(row[27].ToString());
                            ruta.VALORSEGURO = float.Parse(row[28].ToString()) ;
                            ruta.CELULAR = row[29].ToString();
                            if (row[30].ToString() != "")
                            {
                                ruta.PAGOHASTA = Convert.ToDateTime(row[30].ToString());
                            }
                            else
                            {
                                ruta.PAGOHASTA = new DateTime(1999,01,01);
                            }
                            ruta.DEPTOC = row[31].ToString();
                            ruta.MPIOC = row[32].ToString();
                            ruta.BARRIOC = row[33].ToString();
                            ruta.MOTIVO = row[34].ToString();
                            ruta.FECHAPROGRAMADA = row[35].ToString();
                            ruta.CODBARRIO = row[36].ToString();
                            ruta.COBERTURA = row[38].ToString();
                            ruta.ULTIMOSPAGOS = row[39].ToString();
                            ruta.BENEFICIARIOS = row[40].ToString();
                            ruta.FECHAAFILIACION = Convert.ToDateTime(row[41].ToString());
                            ruta.PLAN = row[42].ToString();
                            ruta.NOTA1 = row[43].ToString();
                            lst.Add(ruta);
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error al acceder a la base de datos para obtener las rutas." + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            return lst;
        }

    }
}
