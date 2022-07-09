using FirebirdSql.Data.FirebirdClient;
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
    public class CatalogoContrato
    {
        string _cadenaconexion;
        string _cadenaconexionWeb;


        public CatalogoContrato()
        {
        }

        public CatalogoContrato(string cadenaConexion)
        {
            this._cadenaconexion = cadenaConexion;

        }

        public CatalogoContrato(string cadenaConexion ,string cadenaconexionweb)
        {
            this._cadenaconexionWeb = cadenaconexionweb;
            this._cadenaconexion = cadenaConexion;
        }


        public ContratoDTO ConsultarContrato(string NroContrato)
        {
            ContratoDTO contrato = new ContratoDTO();
            Conection db = new Conection();

            try
            {
                string sql = "SELECT C.idcontrato, C.fechaingreso, C.pagohasta, C.estado, P.IDPERSONA , " +
                             "P.nombres || ' ' || P.nombre2 || ' ' || P.apellidos || ' ' || P.apellido2 AS TITULAR " +
                              "    , P.direccion, P.telefono, P.telefamiliar , PL.nombreplan, C.valorinicial, C.FECHAULTIMOPAGO," +
                              "'ND' AS NRORECIBO, '0' AS VALOR  , P.depto, P.ciudad, P.boxcontratante ,C.deptoc, C.mpioc , c.barrioc, c.direccioncobro, p.Parentesco " +
                            "FROM TBLCONTRATO C " +
                            "INNER JOIN TBLPERSONA P ON C.idpersona = P.idpersona " +
                            "INNER JOIN TBLPLANES PL ON c.idplan = PL.idplan " +
                            "WHERE C.idcontrato=@Idcontrato ";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                db.AsignarParametrosString("@Idcontrato", NroContrato);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    try
                    {
                        contrato.IdContrato= datos.GetString(0);
                        if (datos[1].ToString() != "")
                            contrato.FechaAfiliacion = datos.GetDateTime(1);

                        if (datos[2].ToString() != "")
                            contrato.FechaCobertura = datos.GetDateTime(2);
                        else
                            contrato.FechaCobertura = null;
                        contrato.EstadoContrato = datos.GetString(3);
                        contrato.Cedula = datos.GetString(4);
                        contrato.Titular = datos.GetString(5);
                        contrato.Direccion = datos.GetString(6);
                        contrato.Telefono = datos.GetString(7);
                        contrato.Celular = datos.GetString(8);
                        contrato.Plan = datos.GetString(9);
                        contrato.Cuota = datos.GetDouble(10);
                        if (datos[11].ToString() != "")
                            contrato.FechaUltimoPago = datos.GetDateTime(11);
                        else
                            contrato.FechaUltimoPago = null;
                        contrato.NoRecibo = datos.GetString(12);
                        contrato.Valor = datos.GetDouble(13);
                        contrato.Departamento = datos.GetString(14);
                        contrato.Municipio = datos.GetString(15);

                        contrato.Barrio = datos.GetString(16);
                        contrato.DepartamentoCobro = datos.GetString(17);
                        contrato.MunicipioCobro = datos.GetString(18);
                        contrato.BarrioCobro = datos.GetString(19);
                        contrato.DireccionCobro = datos.GetString(20);
                        contrato.Email = datos.GetString(21);

                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Error Consultando Contrato por NroContrato" + ex.Message);
                    }
                    
                }


                return contrato;
            }
            catch(Exception ex)
            {
                throw new Exception("Error Consultando Contrato por NroContrato" + ex.Message); 
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public ContratoDTO ConsultarContratoSP(string NroContrato)
        {

            List<string> lstBeneficiarios = new List<string>();
            ContratoDTO contrato = new ContratoDTO();
            Conection db = new Conection();

            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARCONTRATO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroContrato;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            try
                            {
                                contrato.IdContrato = datos.GetString(0);
                                if (datos[1].ToString() != "")
                                    contrato.FechaAfiliacion = datos.GetDateTime(1);

                                if (datos[2].ToString() != "")
                                    contrato.FechaCobertura = datos.GetDateTime(2);
                                else
                                    contrato.FechaCobertura = null;
                                contrato.EstadoContrato = datos.GetString(3);
                                contrato.Cedula = datos.GetString(4);
                                contrato.Titular = datos.GetString(5);
                                contrato.Direccion = datos.GetString(6);
                                contrato.Telefono = datos.GetString(7);
                                contrato.Celular = datos.GetString(8);
                                contrato.Plan = datos.GetString(9);
                                contrato.Cuota = datos.GetDouble(10);
                                if (datos[11].ToString() != "")
                                    contrato.FechaUltimoPago = datos.GetDateTime(11);
                                else
                                    contrato.FechaUltimoPago = null;
                                contrato.NoRecibo = datos.GetString(12);
                                contrato.Valor = datos.GetDouble(13);
                                contrato.Departamento = datos.GetString(14);
                                contrato.Municipio = datos.GetString(15);

                                contrato.Barrio = datos.GetString(16);
                                contrato.DepartamentoCobro = datos.GetString(17);
                                contrato.MunicipioCobro = datos.GetString(18);
                                contrato.BarrioCobro = datos.GetString(19);
                                contrato.DireccionCobro = datos.GetString(20);
                                contrato.Email = datos.GetString(21);
                                contrato.Nota1 = datos.GetString(22);

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error Consultando Contrato por NroContrato" + ex.Message);
                            }

                        }

                    }
                    conn.Close();
                }

                return contrato;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Beneficiarios por NroCedula" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }

        public List<ContratoDTO> ConsultarContratoxCedula(string NroCedula)
        {

            List<ContratoDTO> lstContratos = new List<ContratoDTO>();
            
            Conection db = new Conection();

            try
            {
                string sql = "SELECT C.idcontrato, C.fechaingreso, C.pagohasta, C.estado, P.IDPERSONA , " +
                             "P.nombres || ' ' || P.nombre2 || ' ' || P.apellidos || ' ' || P.apellido2 AS TITULAR " +
                              "    , P.direccion, P.telefono, P.telefamiliar , PL.nombreplan, C.valorinicial, C.FECHAULTIMOPAGO, 'ND' AS NRORECIBO, '0' AS VALOR  , P.depto, P.ciudad, P.boxcontratante ,C.deptoc, C.mpioc , c.barrioc, c.direccioncobro, p.Parentesco " +
                            "FROM TBLCONTRATO C " +
                            "INNER JOIN TBLPERSONA P ON C.idpersona = P.idpersona " +
                            "INNER JOIN TBLPLANES PL ON c.idplan = PL.idplan " +
                            "WHERE C.idpersona=@Idcedula ";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                db.AsignarParametrosString("@Idcedula", NroCedula + 'A');
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    ContratoDTO contrato = new ContratoDTO();
                    try
                    {
                        
                        contrato.IdContrato = datos.GetString(0);
                        if(datos[1].ToString() != "")
                            contrato.FechaAfiliacion = datos.GetDateTime(1);

                        if (datos[2].ToString() != "")
                            contrato.FechaCobertura = datos.GetDateTime(2);
                        else
                            contrato.FechaCobertura = null;

                        contrato.EstadoContrato = datos.GetString(3);
                        contrato.Cedula = datos.GetString(4);
                        contrato.Titular = datos.GetString(5);
                        contrato.Direccion = datos.GetString(6);
                        contrato.Telefono = datos.GetString(7);
                        contrato.Celular = datos.GetString(8);
                        contrato.Plan = datos.GetString(9);
                        contrato.Cuota = datos.GetDouble(10);
                        if(datos[11].ToString() != "")
                            contrato.FechaUltimoPago = datos.GetDateTime(11);
                        else
                            contrato.FechaUltimoPago = null;

                        contrato.NoRecibo = datos.GetString(12);
                        contrato.Valor = datos.GetDouble(13);
                        contrato.Departamento = datos.GetString(14);
                        contrato.Municipio = datos.GetString(15);

                        contrato.Barrio = datos.GetString(16);
                        contrato.DepartamentoCobro = datos.GetString(17);
                        contrato.MunicipioCobro = datos.GetString(18);
                        contrato.BarrioCobro = datos.GetString(19);
                        contrato.DireccionCobro = datos.GetString(20);
                        contrato.Email = datos.GetString(21);
                        
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Consultando Contrato por NroCedula" + ex.Message);
                    }

                    lstContratos.Add(contrato);
                }


                return lstContratos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Contrato por NroContrato" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<ContratoDTO> ConsultarContratoxCedulaSP(string NroCedula)
        {

            List<ContratoDTO> lstContratos = new List<ContratoDTO>();

            Conection db = new Conection();

            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARCONTRATOCEDULA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCEDULA", FbDbType.VarChar).Value = NroCedula;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            ContratoDTO contrato = new ContratoDTO();
                            try
                            {

                                contrato.IdContrato = datos.GetString(0);
                                if (datos[1].ToString() != "")
                                    contrato.FechaAfiliacion = datos.GetDateTime(1);

                                if (datos[2].ToString() != "")
                                    contrato.FechaCobertura = datos.GetDateTime(2);
                                else
                                    contrato.FechaCobertura = null;

                                contrato.EstadoContrato = datos.GetString(3);
                                contrato.Cedula = datos.GetString(4);
                                contrato.Titular = datos.GetString(5);
                                contrato.Direccion = datos.GetString(6);
                                contrato.Telefono = datos.GetString(7);
                                contrato.Celular = datos.GetString(8);
                                contrato.Plan = datos.GetString(9);
                                contrato.Cuota = datos.GetDouble(10);
                                if (datos[11].ToString() != "")
                                    contrato.FechaUltimoPago = datos.GetDateTime(11);
                                else
                                    contrato.FechaUltimoPago = null;

                                contrato.NoRecibo = datos.GetString(12);
                                contrato.Valor = datos.GetDouble(13);
                                contrato.Departamento = datos.GetString(14);
                                contrato.Municipio = datos.GetString(15);

                                contrato.Barrio = datos.GetString(16);
                                contrato.DepartamentoCobro = datos.GetString(17);
                                contrato.MunicipioCobro = datos.GetString(18);
                                contrato.BarrioCobro = datos.GetString(19);
                                contrato.DireccionCobro = datos.GetString(20);
                                contrato.Email = datos.GetString(21);
                                contrato.Nota1 = datos.GetString(22);

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error Consultando Contrato por NroCedula" + ex.Message);
                            }

                            lstContratos.Add(contrato);
                        }

                    }
                    conn.Close();
                }

                return lstContratos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Contrato por NroCedula" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }


        //public List<string> ConsultarBeneficiarios(string NroCedula)
        //{

        //    List<string> lstBeneficiarios = new List<string>();

        //    Conection db = new Conection();

        //    try
        //    {
        //        string sql = "select p.nombres || ' ' || p.apellidos ||'(B) - ' || p.tipo || '- Edad('|| p.edadactual ||')' from tblpersona p where p.NROCONTRATO=@Idcedula ";

        //        db.FbConeccion(_cadenaconexion);
        //        db.FbConectionOpen();
        //        db.ComenzarTransaccion();
        //        db.CreateComando(sql);
        //        db.AsignarParametrosString("@Idcedula", NroCedula);
        //        FbDataReader datos = db.EjecutarConsulta();

        //        while (datos.Read())
        //        {
        //            string bene = string.Empty;
        //            try
        //            {

        //                bene = datos.GetString(0);

        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error Consultando Beneficiarios por NroCedula" + ex.Message);
        //            }

        //            lstBeneficiarios.Add(bene);
        //        }


        //        return lstBeneficiarios;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Consultando Beneficiarios" + ex.Message);
        //    }
        //}


        public List<string> ConsultarBeneficiarios(string NroCedula)
        {

            List<string> lstBeneficiarios = new List<string>();

            Conection db = new Conection();

            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARBENEFICIARIOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroCedula;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string bene = string.Empty;
                            try
                            {
                                bene = datos.GetString(0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando beneficiarios por nrocedula" + ex.Message);
                            }

                            lstBeneficiarios.Add(bene);
                        }

                    }
                    conn.Close();
                }

                return lstBeneficiarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Beneficiarios por NroCedula" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }

        //public List<TipoNovedadDTO> ConsultarTipoNovedades()
        //{

        //    List<TipoNovedadDTO> lsttiponovedad = new List<TipoNovedadDTO>();

        //    Conection db = new Conection();

        //    try
        //    {
        //        string sql = "select idnovedad , novedad from TBLNOVEDADES ";

        //        db.FbConeccion(_cadenaconexionWeb);
        //        db.FbConectionOpen();
        //        db.ComenzarTransaccion();
        //        db.CreateComando(sql);
        //        FbDataReader datos = db.EjecutarConsulta();

        //        while (datos.Read())
        //        {
        //            TipoNovedadDTO tiponovedad = new TipoNovedadDTO();
        //            tiponovedad.Idnovedad = datos.GetInt32(0);
        //            tiponovedad.Novedad = datos.GetString(1);

        //            lsttiponovedad.Add(tiponovedad);
        //        }


        //        return lsttiponovedad;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Consultando Tipo de Novedades" + ex.Message);
        //    }
        //    finally
        //    {
        //        db.FbConectionClose();
        //    }
        //}

        public List<TipoNovedadDTO> ConsultarTipoNovedades()
        {

            List<TipoNovedadDTO> lsttiponovedad = new List<TipoNovedadDTO>();

            Conection db = new Conection();

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
                        cmd.CommandText = "WS_CONSULTARTIPONOVEDADES";
                        cmd.CommandType = CommandType.StoredProcedure;

                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            TipoNovedadDTO tiponovedad = new TipoNovedadDTO();

                            tiponovedad.Idnovedad = datos.GetInt32(0);
                            tiponovedad.Novedad = datos.GetString(1);

                            lsttiponovedad.Add(tiponovedad);
                        }

                    }
                    conn.Close();
                }
                return lsttiponovedad;
            }

            catch (Exception ex)
            {
                throw new Exception("Error Consultando Tipo de Novedades" + ex.Message);
            }
            finally {
                db.FbConectionClose();
            }
        }

        public List<string> ConsultaDepartamentos()
        {

            List<string> lstDepartamentos = new List<string>();

            Conection db = new Conection();

            try
            {
                string sql = "select departamento from tbldepartamentos ";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    string bene = string.Empty;
                    try
                    {

                        bene = datos.GetString(0);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Consultando Beneficiarios por NroCedula" + ex.Message);
                    }

                    lstDepartamentos.Add(bene);
                }


                return lstDepartamentos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Departamentos" + ex.Message);
            }
            finally {
                db.FbConectionClose();
            }
        }

        public List<string> ConsultaDepartamentosSP()
        {

            List<string> lstDepartamentos = new List<string>();

            Conection db = new Conection();

            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARDEPTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroCedula;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string depa = string.Empty;
                            try
                            {
                                depa = datos.GetString(0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando Departamentos" + ex.Message);
                            }

                            lstDepartamentos.Add(depa);
                        }

                    }
                    conn.Close();
                }


                return lstDepartamentos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Departamentos" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<string> ConsultarMunicipios(String dpto)
        {
            List<string> lstMunicipios = new List<string>();

            Conection db = new Conection();

            try
            {
                string sql = "select m.municipio from tblmunicipios  m inner join tbldepartamentos d on d.coddane = m.coddepartamento  where d.departamento = @Depto ";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                db.AsignarParametrosString("@Depto", dpto);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    string bene = string.Empty;
                    try
                    {

                        bene = datos.GetString(0);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Consultando Municipios por Departemento" + ex.Message);
                    }

                    lstMunicipios.Add(bene);
                }


                return lstMunicipios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Municipios" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<string> ConsultarMunicipiosSP(String dpto)
        {
            List<string> lstMunicipios = new List<string>();

            Conection db = new Conection();

            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                DataTable table = new DataTable();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTARCIUDAD";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DEPTO", FbDbType.VarChar).Value = dpto;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string muni = string.Empty;
                            try
                            {
                                muni = datos.GetString(0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando Municipios" + ex.Message);
                            }

                            lstMunicipios.Add(muni);
                        }

                    }
                    conn.Close();
                    return lstMunicipios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Municipios" + ex.Message);
            }
            finally {
                db.FbConectionClose();
            }
        }

        public string ActualizarContrato(UpdateContratoRequest contrato)
        {
            Conection db = new Conection();


            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_ACTUALIZARDATOSCTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCONTRATO", FbDbType.VarChar).Value = contrato.contrato == null ? "" : contrato.contrato;
                        cmd.Parameters.Add("@CEDULA", FbDbType.VarChar).Value = contrato.CEDULA == null ? "" : contrato.CEDULA;
                        cmd.Parameters.Add("@EMAIL", FbDbType.VarChar).Value = contrato.email == null ? "" : contrato.email;
                        cmd.Parameters.Add("@DPTO", FbDbType.VarChar).Value = contrato.departamento == null ? "" : contrato.departamento;
                        cmd.Parameters.Add("@MUNICIPIO", FbDbType.VarChar).Value = contrato.Ciudad == null ? "" : contrato.Ciudad;
                        cmd.Parameters.Add("@BARRIO", FbDbType.VarChar).Value = contrato.BARRIO == null ? "" : contrato.BARRIO;
                        cmd.Parameters.Add("@DIRECCION", FbDbType.VarChar).Value = contrato.DIRECCION == null ? "" : contrato.DIRECCION;
                        cmd.Parameters.Add("@TELEFONO", FbDbType.VarChar).Value = contrato.telefono == null ? "" : contrato.telefono;
                        cmd.Parameters.Add("@CELULAR", FbDbType.VarChar).Value = contrato.movil == null ? "" : contrato.movil;
                        cmd.Parameters.Add("@DPTOCOBRO", FbDbType.VarChar).Value = contrato.departamentocobro == null ? "" : contrato.departamentocobro;
                        cmd.Parameters.Add("@MUNICIPIOCOBRO", FbDbType.VarChar).Value = contrato.CiudadCobro == null ? "" : contrato.CiudadCobro;
                        cmd.Parameters.Add("@BARRIOCOBRO", FbDbType.VarChar).Value = contrato.BARRIOCOBRO == null ? "" : contrato.BARRIOCOBRO;
                        cmd.Parameters.Add("@DIRCOBRO", FbDbType.VarChar).Value = contrato.DIRECCIONCobro == null ? "" : contrato.DIRECCIONCobro;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = contrato.USUARIO == null ? "" : contrato.USUARIO;
                        cmd.Parameters.Add("@POSX", FbDbType.VarChar).Value = contrato.POSX == null ? "" : contrato.POSX;
                        cmd.Parameters.Add("@POSY", FbDbType.VarChar).Value = contrato.POSY == null ? "" : contrato.POSY;
                        
                        
                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);


                        return dt.Rows[0][0].ToString();
                        //Console.WriteLine(result);
                    }
                    conn.Close();
                }
                //db.EjecutarComandoProcedure();
                //db.ConfirmarTransaccion();

            }
            catch (Exception ex)
            {
                return "-1";
            }
            finally
            {
                db.FbConectionClose();
            }
        }


    }
}
