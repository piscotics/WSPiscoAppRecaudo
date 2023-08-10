using FirebirdSql.Data.FirebirdClient;
using ModelSincronizador;
using ModelSincronizador.Connection;
using ReglasDeNegocio.Clases;
using ReglasDeNegocio.DTO;
using ReglasDeNegocio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReglasDeNegocio
{
    public class CatalogoPagos
    {
        string _cadenaconexionWeb;
        string _cadenaconexion;


        public CatalogoPagos()
        {
        }

        public CatalogoPagos(string cadenaConexion, string Cadenaconexionweb)
        {
            this._cadenaconexion = cadenaConexion;
            this._cadenaconexionWeb = Cadenaconexionweb;
        }

        public NotificarReciboDTO NotificaRecibo(String NoRecibo )
        {
            
            NotificarReciboDTO Respuesta = new NotificarReciboDTO();
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
                        cmd.CommandText = "WS_NOTIFICARPAGO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NORECIBO", FbDbType.VarChar).Value = NoRecibo;
                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);

                        //FbDataAdapter adapter = new FbDataAdapter();
                        //adapter.SelectCommand = cmd;
                        //DataTable dt = new DataTable();
                        //adapter.Fill(dt);

                        Respuesta.IDCONTRATO = dt.Rows[0][0].ToString();
                        Respuesta.RESPUESTA = dt.Rows[0][1].ToString();

                    }
                    conn.Close();
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Creando pago " + ex.Message);

                return new NotificarReciboDTO();
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public  PagoResultDTO GrabarPago(PaysRequest pagos)
        {
            Conection db = new Conection();
            PagoDTO pago = new PagoDTO();
            PagoResultDTO Respuesta = new PagoResultDTO();
            int IdAlterna = 0;
            bool tienealterna = false;

            pago.ANULADO = 0;
            pago.CUOTAMENSUAL = pagos.CUOTAMENSUAL;
            pago.DESCUENTO = pagos.DESCUENTO;
            pago.DESDE = DateTime.Now;
            pago.HASTA = DateTime.Now;

            if (pagos.IDCONTRATO.Contains("|"))
            {
                
                string[] contrato = pagos.IDCONTRATO.Split('|');
                if (contrato[1] == "Amp")
                {
                    tienealterna = true;
                    pago.IDCONTRATO = contrato[0];
                    string[] plan = pagos.PLAN.Split('-');
                    plan[0] = plan[0].Remove(0, 11);
                    plan[0] = plan[0].Trim();
                    IdAlterna =Convert.ToInt32( plan[0]);
                }
                else
                {
                    pago.IDCONTRATO = contrato[0];
                }
            }
            else
                pago.IDCONTRATO = pagos.IDCONTRATO;

            pago.IDENTIFICADORBASE = pagos.IDENTIFICADORBASE;
            pago.IDPERSONA = pagos.IDPERSONA;
            pago.MAQUINA = pagos.MAQUINA;
            pago.OBSERVACIONES = pagos.OBSERVACIONES;
            pago.POSX = pagos.POSX;
            pago.POSY = pagos.POSY;
            pago.PUNTOS = 0;
            pago.SALDOC = 0;
            pago.TIPOPAGO = "RECAUDO";
            pago.FORMAPAGO = pagos.FORMAPAGO;
            pago.TITULAR = pagos.TITULAR;
            pago.TPAGO = "M";
            pago.TRANSAC = pagos.TRANSAC;
            pago.USUARIO = pagos.USUARIO;
            pago.VALOR = pagos.VALOR;
            pago.ESTADO = "ACTIVO";
            pago.FECHA = DateTime.Now;
            pago.FECHAPAGOR = Convert.ToDateTime( pagos.FECHAPAGOR);

            pago.NROREF = pagos.NROREF;


            try
            {
                db.FbConeccion(this._cadenaconexionWeb);
                db.FbConectionOpen();


                //string sql = "INSERT TBLPAGOS (IDGENERADOR,IDGENERADORT,FECHA,IDCONTRATO,IDPERSONA,VALOR,DESCUENTO,ANULADO,MAQUINA,TRANSAC,USUARIO,OBSERVACIONES,ESTADO,PUNTOS,CUOTAMENSUAL,IDENTIFICADORBASE, NORECIBO,DESDE,HASTA,IDCOBRADOR,DETALLE," + 
                //            "TM,RIFA,TITULAR,ESTADOCONTRATO,TIPOPAGO,TOTAL,FORMAPAGO,MODULO,SALDO,CODIGOBANCO,NROREF,FECHAPAGOF,IDCAJAIND,IDCAJAEMP,IDGENERADORC, TPAGO,ABONOCONTRATO,CLASEDOC,IDCAJA,POSICIONX, POSICIONY) "  +
                //    " VALUES (@Idgenerador,@IdGeneradorT,@Fecha,@IdContrato,@IdPersona,@Valor,@Descuento,@Anulado,@Maquina,@Transac,@Usuario,@Observaciones,@Estado,@Puntos,@CuotaMensual,@IdentificadorBase,@NoRecibo,@Desde,@Hasta,@IdCobrador,@Detalle," +
                //    ",@Tm,@Rifa,@Titular,@EstadoContrato,@TipoPago,@Total,@FormaPago,@Modulo,@Saldo,@CodigoBanco,@NroRef,@FechaPagoF,@IdCajaInd,@IdCajaEmp,@IdGeneradorC,@Tpago,@AbonoContrato,@ClaseDoc,@IdCaja,@PosicionX,@PosicionY) ";


                //db.ComenzarTransaccion();
                //db.CreateComandProcedure("WS_GUARDAPAGO");
                using (FbConnection conn = new FbConnection(this._cadenaconexionWeb))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_GUARDAPAGO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FECHA", FbDbType.Date).Value = pago.FECHA;
                        cmd.Parameters.Add("@IDCONTRATO", FbDbType.VarChar).Value = pago.IDCONTRATO;
                        cmd.Parameters.Add("@IDPERSONA", FbDbType.VarChar).Value = pago.IDPERSONA;
                        cmd.Parameters.Add("@VALOR", FbDbType.Float).Value = pago.VALOR;
                        cmd.Parameters.Add("@DESCUENTO", FbDbType.Float).Value = pago.DESCUENTO;
                        cmd.Parameters.Add("@ANULADO", FbDbType.Integer).Value = pago.ANULADO;
                        cmd.Parameters.Add("@MAQUINA", FbDbType.VarChar).Value = pago.MAQUINA;
                        cmd.Parameters.Add("@TRANSAC", FbDbType.VarChar).Value = pago.TRANSAC;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = pago.USUARIO;
                        cmd.Parameters.Add("@OBSERVACIONES", FbDbType.VarChar).Value = pago.OBSERVACIONES;
                        
                        cmd.Parameters.Add("@CUOTAMENSUAL", FbDbType.Float).Value = pago.CUOTAMENSUAL;
                        cmd.Parameters.Add("@DESDE", FbDbType.Date).Value = pago.DESDE;
                        cmd.Parameters.Add("@HASTA", FbDbType.Date).Value = pago.HASTA;
                        cmd.Parameters.Add("@IDENTIFICADORBASE", FbDbType.VarChar).Value = pago.IDENTIFICADORBASE;
                        cmd.Parameters.Add("@PUNTOS", FbDbType.Integer).Value = pago.PUNTOS;
                        cmd.Parameters.Add("@TITULAR", FbDbType.VarChar).Value = pago.TITULAR;
                        cmd.Parameters.Add("@ESTADO", FbDbType.VarChar).Value = pago.ESTADO;
                        cmd.Parameters.Add("@TIPOPAGO", FbDbType.VarChar).Value = pago.TIPOPAGO;
                        cmd.Parameters.Add("@FORMAPAGO", FbDbType.VarChar).Value = pago.FORMAPAGO;
                        cmd.Parameters.Add("@FECHAPAGOR", FbDbType.Date).Value = pago.FECHAPAGOR;
                        cmd.Parameters.Add("@TPAGO", FbDbType.VarChar).Value = pago.TPAGO;
                        cmd.Parameters.Add("@SALDOC", FbDbType.Float).Value = pago.SALDOC;
                        cmd.Parameters.Add("@POSX", FbDbType.VarChar).Value = pago.POSX;
                        cmd.Parameters.Add("@POSY", FbDbType.VarChar).Value = pago.POSY;
                        if (tienealterna)
                        {
                            cmd.Parameters.Add("@IDALTERNA", FbDbType.Integer).Value = IdAlterna;
                        }
                        else
                        {
                            cmd.Parameters.Add("@IDALTERNA", FbDbType.Integer).Value = null;
                        }

                        cmd.Parameters.Add("@NROREF", FbDbType.VarChar).Value = pago.NROREF;

                        
                        /*int result = (int)*///
                        ///cmd.ExecuteNonQuery();
                        //cmd.ExecuteScalar();

                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);
                        ValorenLetras letras = new ValorenLetras();

                        Respuesta.NroRecibo = dt.Rows[0][0].ToString();
                        if (dt.Rows[0][1].ToString() != "")
                        {
                            Respuesta.Desde = Convert.ToDateTime(dt.Rows[0][1]).ToString("yyyy-MM-dd");
                            
                        }
                        if (dt.Rows[0][2].ToString() != "") {
                            Respuesta.Hasta = Convert.ToDateTime(dt.Rows[0][2]).ToString("yyyy-MM-dd");
                        }



                        if (dt.Rows[0][3].ToString() != "")
                        {
                            Respuesta.Concepto = dt.Rows[0][3].ToString();
                        }

                        if (dt.Rows[0][4].ToString() != "")
                        {
                            Respuesta.DetallePago = dt.Rows[0][4].ToString();
                        }

                        if (dt.Rows[0][5].ToString() != "")
                        {
                            Respuesta.Respuesta = dt.Rows[0][5].ToString();
                        }
                        if (dt.Rows[0][6].ToString() != "")
                        {
                            Respuesta.Anulado = dt.Rows[0][6].ToString();
                        }

                        if (dt.Rows[0][7].ToString() != "")
                        { 
                            Respuesta.PVisita = Convert.ToDateTime(dt.Rows[0][7]).ToString("yyyy-MM-dd");
                        }


                        if (dt.Rows[0][8].ToString() != "")
                        {
                            Respuesta.Vdesde = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][8]).ToString("yyyy-MM-dd"));
                        }


                        if (dt.Rows[0][9].ToString() != "")
                        {
                            Respuesta.Vhasta = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][9]).ToString("yyyy-MM-dd"));
                        }



                        if (dt.Rows[0][10].ToString() != "")
                        {
                            Respuesta.VlrCto = Convert.ToDouble(dt.Rows[0][10]);
                        }


                        if (dt.Rows[0][11].ToString() != "")
                        {
                            Respuesta.VlrSaldo = Convert.ToDouble(dt.Rows[0][11]);
                        }

                        if (dt.Rows[0][12].ToString() != "")
                        {
                            Respuesta.VlrDctoPago = Convert.ToDouble(dt.Rows[0][12]);
                        }

                        if (dt.Rows[0][13].ToString() != "")
                        {
                            Respuesta.VlrIva = Convert.ToDouble(dt.Rows[0][13]);
                        }

                        if (dt.Rows[0][14].ToString() != "")
                        {
                            Respuesta.IdContrato = dt.Rows[0][14].ToString();
                        }

                        if (dt.Rows[0][15].ToString() != "")
                        {
                            Respuesta.IdPersona = dt.Rows[0][15].ToString();
                        }


                  

                        //datos de la empresa
                        if (dt.Rows[0][16].ToString() != "")
                        {
                            Respuesta.NitEmpresa = dt.Rows[0][16].ToString();
                        }

                        if (dt.Rows[0][17].ToString() != "")
                        {
                            Respuesta.Empresa = dt.Rows[0][17].ToString();
                        }

                        if (dt.Rows[0][18].ToString() != "")
                        {
                            Respuesta.TelefonoEmpresa = dt.Rows[0][18].ToString();
                        }

                        if (dt.Rows[0][19].ToString() != "")
                        {
                            Respuesta.DireccionEmpresa = dt.Rows[0][19].ToString();
                        }

                        if (dt.Rows[0][20].ToString() != "")
                        {
                            Respuesta.CiudadEmpresa = dt.Rows[0][20].ToString();
                        }


                        float total = pago.VALOR - pago.DESCUENTO;
                        
                        Respuesta.Valorenletras = letras.enletras(total.ToString());

                        //Respuesta = dt.Rows[0][0].ToString();

                        //if (Respuesta != "")
                        //    return Respuesta;
                        //else
                        //    return dt.Rows[0][1].ToString();




                        //Console.WriteLine(result);
                    }
                    conn.Close();
                }
                //db.EjecutarComandoProcedure();
                //db.ConfirmarTransaccion();
                return Respuesta;
            }
            catch (Exception ex)
            {
                //db.CancelarTransaccion();
                throw new Exception("Error Creando pago " + ex.Message);
                return new PagoResultDTO();

            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public GuardaHistoricoImpresionDTO GuardaHistoricoImpresion(string idContrato, string noRecibo, string usuario, string terminal)
        {
            GuardaHistoricoImpresionDTO Respuesta = new GuardaHistoricoImpresionDTO();
            Conection db = new Conection();
            try
            {
                db.FbConeccion(this._cadenaconexion);
                db.FbConectionOpen();

                using (FbConnection conn = new FbConnection(this._cadenaconexionWeb))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_GUARDAHISTORICOIMPRESION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCONTRATO", FbDbType.VarChar).Value = idContrato;
                        cmd.Parameters.Add("@NORECIBO", FbDbType.VarChar).Value = noRecibo;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = usuario;
                        cmd.Parameters.Add("@TERMINAL", FbDbType.VarChar).Value = terminal;
                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);
                    }
                    conn.Close();
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Creando historico impresion " + ex.Message);

                return new GuardaHistoricoImpresionDTO();
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public GuardaMovilEstadoDTO GuardaMovilEstado(string usuario, string estado, string terminal)
        {
            GuardaMovilEstadoDTO Respuesta = new GuardaMovilEstadoDTO();
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
                        cmd.CommandText = "WS_GUARDAMOVILESTADO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = usuario;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = estado;
                        cmd.Parameters.Add("@TERMINAL", FbDbType.VarChar).Value = terminal;
                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);
                    }
                    conn.Close();
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Creando historico estado usurio " + ex.Message);

                return new GuardaMovilEstadoDTO();
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public ConsulPagoDTO ConsultarPagoImpresion(String Dato)
        {
            Conection db = new Conection();
            ConsulPagoDTO pago = new ConsulPagoDTO();
            ValorenLetras letras = new ValorenLetras();

            try
            {
                db.FbConeccion(this._cadenaconexionWeb);
                db.FbConectionOpen();

                using (FbConnection conn = new FbConnection(this._cadenaconexionWeb))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_CONSULTAPAGOGUAR";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DATO", FbDbType.VarChar).Value = Dato;
                        //cmd.ExecuteScalar();


                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);



                        pago = dt.AsEnumerable().Select(m => new ConsulPagoDTO()
                        {
                            Contrato = m.Field<string>("IDCONTRATO"),
                            Cuota = m.Field<float>("CUOTA"),
                            NumeroDocumento = m.Field<string>("IDGENERADOR"),
                            Cedula = m.Field<string>("IDPERSONA"),
                            Nombre = m.Field<string>("TITULAR"),
                            FechaPago = m.Field<DateTime>("FECHA").ToString("yyyy-MM-dd"),//.ToString("dd/MM/yyyy"),
                            Total = m.Field<float>("VALOR"),
                            Usuario = m.Field<string>("USUARIO"),
                            Concepto = m.Field<string>("CONCEPTO"),
                            Valorenletras = letras.enletras(m.Field<float>("VALOR").ToString()),
                        }).FirstOrDefault();




                        if (dt.Rows[0][5].ToString() != "")
                            pago.Anulado = Convert.ToString(dt.Rows[0][5]);
                        else
                            pago.Anulado = null;

                        if (dt.Rows[0][6].ToString() != "")
                            pago.Terminal = Convert.ToString(dt.Rows[0][6]);
                        else
                            pago.Terminal = null;


                        if (dt.Rows[0][9].ToString() != "")
                            pago.Observaciones = Convert.ToString(dt.Rows[0][9]);
                        else
                            pago.Observaciones = null;


                        if (dt.Rows[0][14].ToString() != "")
                        {
                            pago.PagoDesde = Convert.ToString(dt.Rows[0][14]);
                        }
                        else
                            pago.PagoDesde = null;


                        if (dt.Rows[0][15].ToString() != "")
                        {
                            pago.PagoHasta = Convert.ToString(dt.Rows[0][15]);
                        }
                        else
                            pago.PagoHasta = null;



                        if (dt.Rows[0][17].ToString() != "")
                        {
                            pago.PVisita =(Convert.ToDateTime(dt.Rows[0][17]).ToString("yyyy-MM-dd")); 
                            
                        }

                        if (dt.Rows[0][18].ToString() != "")
                        {
                            pago.Vdesde = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][18]).ToString("yyyy-MM-dd"));
                        }


                        if (dt.Rows[0][19].ToString() != "")
                        {
                            pago.Vhasta = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][19]).ToString("yyyy-MM-dd"));
                        }



                        if (dt.Rows[0][20].ToString() != "")
                        {
                            pago.VlrCto = Convert.ToDouble(dt.Rows[0][20]);
                        }


                        if (dt.Rows[0][21].ToString() != "")
                        {
                            pago.VlrSaldo = Convert.ToDouble(dt.Rows[0][21]);
                        }

                        if (dt.Rows[0][22].ToString() != "")
                        {
                            pago.VlrDctoPago = Convert.ToDouble(dt.Rows[0][22]);
                        }

                        if (dt.Rows[0][23].ToString() != "")
                        {
                            pago.VlrIva = Convert.ToDouble(dt.Rows[0][23]);
                        }
                        if (dt.Rows[0][24].ToString() != "")
                        {
                            pago.FormaPago = Convert.ToString(dt.Rows[0][24]);
                        }

                        if (dt.Rows[0][25].ToString() != "")
                        {
                            pago.NROREF = Convert.ToString(dt.Rows[0][25]);
                        }

                        if (dt.Rows[0][26].ToString() != "")
                        {
                            pago.RESPUESTA = Convert.ToString(dt.Rows[0][26]);
                        }

                        //datos de la empresa
                        if (dt.Rows[0][27].ToString() != "")
                        {
                            pago.NitEmpresa = dt.Rows[0][27].ToString();
                        }

                        if (dt.Rows[0][28].ToString() != "")
                        {
                            pago.Empresa = dt.Rows[0][28].ToString();
                        }

                        if (dt.Rows[0][29].ToString() != "")
                        {
                            pago.TelefonoEmpresa = dt.Rows[0][29].ToString();
                        }

                        if (dt.Rows[0][30].ToString() != "")
                        {
                            pago.DireccionEmpresa = dt.Rows[0][30].ToString();
                        }

                        if (dt.Rows[0][31].ToString() != "")
                        {
                            pago.CiudadEmpresa = dt.Rows[0][31].ToString();
                        }

                        if (pago != null)
                        { 
                            db = new Conection();

                            try
                            {


                                using (FbConnection conn2 = new FbConnection(this._cadenaconexion))
                                {
                                    conn2.Open();
                                    using (FbCommand cmd2 = conn2.CreateCommand())
                                    {

                                        cmd2.CommandText = "WS_CONSULTARCIUDADDEPT";
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Parameters.Add("@CEDULA", FbDbType.VarChar).Value = pago.Cedula;
                                        //cmd.ExecuteScalar();


                                        FbDataAdapter adapter2 = new FbDataAdapter();
                                        adapter2.SelectCommand = cmd2;
                                        DataTable dt2 = new DataTable();
                                        adapter2.Fill(dt2);
                                        if (dt2.Rows.Count > 0) { 
                                            pago.Departamento = Convert.ToString(dt2.Rows[0][0]);
                                            pago.Departamento = Convert.ToString(dt2.Rows[0][1]);
                                        }

                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error Consultando Datos Pago Mun y Depto" + ex.Message);
                            }


                        }

                        }
                    conn.Close();
                    return pago;
                }
                
            }
            catch (Exception ex)
            {
                return new ConsulPagoDTO();
            }
            finally
            {
                db.FbConectionClose();
            }
        }


        public CuadreCajaDTO GrabarCuadreCaja(CuadreCajaRequest cuadrecaja)
        {
            Conection db = new Conection();
            CuadreCajaDTO cuadre = new CuadreCajaDTO();
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
                        cmd.CommandText = "WS_CUADRECAJA_IND";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DATO", FbDbType.VarChar).Value = cuadrecaja.Dato;
                        cmd.Parameters.Add("@DFECHA", FbDbType.Date).Value = cuadrecaja.Fecha;
                        //cmd.ExecuteScalar();
                        //return 1;

                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);



                        cuadre = dt.AsEnumerable().Select(m => new CuadreCajaDTO()
                        {
                            CANTIDADANULADOS = m.Field<int>("CANTIDADANULADOS"),
                            FECHA = m.Field<DateTime>("FECHA"),
                            CANTIDADNOVEDADES = m.Field<int>("CANTIDADNOVEDADES"),
                            CANTIDADPAGOS = m.Field<int>("CANTIDADPAGOS"),
                            VALORPAGOS = m.Field<Double>("VALORPAGOS"),
                        }).FirstOrDefault();

                        
                    }
                    conn.Close();
                }


                return cuadre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Cuadrando Caja ");
            }
            finally
            {
                db.FbConectionClose();
            }

        }

        public List<TblTipoPago> TiposdePagos()
        {
            List<TblTipoPago> lstTipoPagos = new List<TblTipoPago>();

            Conection db = new Conection();

            try
            {
                string sql = "select idtipopago, nombretp from TBLTIPOPAGO";

                db.FbConeccion(_cadenaconexion);
                db.FbConectionOpen();
                db.ComenzarTransaccion();
                db.CreateComando(sql);
                FbDataReader datos = db.EjecutarConsulta();

                while (datos.Read())
                {
                    TblTipoPago tpago = new TblTipoPago();
                    try
                    {

                        tpago.IdTipoPago = datos.GetInt32(0);
                        tpago.NombreTipoPago = datos.GetString(1);
                        
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Consultando Contrato por NroCedula" + ex.Message);
                    }

                    lstTipoPagos.Add(tpago);
                }


                return lstTipoPagos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Tipos de Pagos" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<TblFacturasPagos> FacturasdePagos(string idContrato)
        {
            List<TblFacturasPagos> lstTipoPagos = new List<TblFacturasPagos>();

            Conection db = new Conection();

            try
            {

                DataTable table = new DataTable();
                //db.FbConeccion(_cadenaconexion);
                //if (db.FbConectionOpen() == true)
                //{
                using (FbConnection conn = new FbConnection(this._cadenaconexion))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_ConsultarFacturasPago";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCONTRATO", FbDbType.VarChar).Value = idContrato;

                        FbDataAdapter adapter = new FbDataAdapter();
                        adapter.SelectCommand = cmd;
                        FbDataReader datos = cmd.ExecuteReader();


                        while (datos.Read())
                        {
                            TblFacturasPagos tpago = new TblFacturasPagos();


                            try
                            {

                                tpago.Factura = datos.GetString(0);
                                tpago.Valor = datos.GetFloat(1);

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error Consultando las facturas" + ex.Message);
                            }

                            lstTipoPagos.Add(tpago);


                        }
                    }
                    conn.Close();
                }
                return lstTipoPagos;

               
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Tipos de Pagos" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }
        }

      
        public TblFuneraria Funeraria()
        {

            TblFuneraria lstFuneraria = new TblFuneraria();

            Conection db = new Conection();

            try
            {
               DataTable table = new DataTable();
                //db.FbConeccion(_cadenaconexion);
                //if (db.FbConectionOpen() == true)
                //{
                    using (FbConnection conn = new FbConnection(this._cadenaconexion))
                    {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "WS_CONSULTARFUNERARIA";
                            cmd.CommandType = CommandType.StoredProcedure;

                            FbDataAdapter adapter = new FbDataAdapter();
                            adapter.SelectCommand = cmd;
                            FbDataReader datos = cmd.ExecuteReader();


                            while (datos.Read())
                            {

                                try
                                {
                                    lstFuneraria.NIT = datos.GetString(0);
                                    lstFuneraria.NOMBRE = datos.GetString(1);
                                    lstFuneraria.TELEFONOS = datos.GetString(2);
                                    lstFuneraria.DIRECCION = datos.GetString(3);
                                    lstFuneraria.RESOLUCION = datos.GetString(5);

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error Consultando Contrato por NroCedula " + _cadenaconexion + " " + ex.Message);
                                }


                            }
                        }
                    conn.Close();
                }
                    return lstFuneraria;
                //}
                //else
                //{
                //    throw new Exception("Error Abriendo Conexion con la Bd " + _cadenaconexion);
                //}
                

            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando Datos Funeraria" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

            
        }

        public NovedadResultDTO GrabarNovedad(NoveltyRequest novedad)
        {
            Conection db = new Conection();
            NovedadResultDTO Respuesta = new NovedadResultDTO();

            if (novedad.FECHAPROGRAMADA.Year == 1)
                novedad.FECHAPROGRAMADA = DateTime.Now;

            try
            {
                db.FbConeccion(this._cadenaconexionWeb);
                db.FbConectionOpen();
                using (FbConnection conn = new FbConnection(this._cadenaconexionWeb))
                {
                    conn.Open();
                    using (FbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "WS_INSERTANOVEDAD_IND";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CONTRATO", FbDbType.VarChar).Value = novedad.Contrato;
                        cmd.Parameters.Add("@CODNOVEDAD", FbDbType.Integer).Value = novedad.Novedad;
                        cmd.Parameters.Add("@DIAPOS", FbDbType.Integer).Value = novedad.DIAPOST;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = novedad.USUARIO;
                        cmd.Parameters.Add("@IDCOBRADOR", FbDbType.VarChar).Value = novedad.IDCOBRADOR;
                        cmd.Parameters.Add("@MODULO", FbDbType.VarChar).Value = "INDIVIDUAL";//novedad.MODULO;
                        cmd.Parameters.Add("@TRANSAC", FbDbType.Integer).Value = novedad.TRANSAC;
                        
                        cmd.Parameters.Add("@FECHAPROGRAMADA", FbDbType.VarChar).Value = novedad.FECHAPROGRAMADA.ToString("yyyy/MM/dd");
                        cmd.Parameters.Add("@POSX", FbDbType.VarChar).Value = novedad.POSX;
                        cmd.Parameters.Add("@POSY", FbDbType.VarChar).Value = novedad.POSY;
                        cmd.Parameters.Add("@OBSERVACIONES", FbDbType.VarChar).Value = novedad.Observaciones;
                        cmd.Parameters.Add("@IDALTERNA", FbDbType.Integer).Value = 1;
                        /*int result = (int)*/
                       // cmd.ExecuteScalar();

                        FbDataReader adapter = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(adapter);
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            Respuesta.IdContrato = dt.Rows[0][0].ToString();
                        }
                        if (dt.Rows[0][1].ToString() != "")
                        {
                            Respuesta.Respuesta = dt.Rows[0][1].ToString();
                        }
                        // return 1;
                        //Console.WriteLine(result);
                    }
                    conn.Close();
                }
                return Respuesta;
                //db.EjecutarComandoProcedure();
                //db.ConfirmarTransaccion();

            }
            catch (Exception ex)
            {
                //db.CancelarTransaccion();
                //throw new Exception("Error Creando pago " + ex.Message);
                throw new Exception("Error Creando novedad " + ex.Message);

            }
            finally
            {
                db.FbConectionClose();
            }
        }

        public List<string> ConsultaPagos(string NroContrato)
        {

            List<string> lstPagos = new List<string>();

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
                        cmd.CommandText = "WS_CONSULTARPAGOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroContrato;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string pagos = string.Empty;
                            try
                            {
                                pagos += "Nro Documento: " + datos.GetString(1);
                                pagos += " - Nro Recibo: " + datos.GetString(0);
                                pagos += " - Fecha Pago:  " + (datos.GetDateTime(2)).ToString("dd/MMM/yyyy");
                                pagos += " - Valor Pagado: " + datos.GetString(3);
                                pagos += " - Descuento: " + datos.GetString(4);
                                pagos += " - Pago Desde: " + (datos.GetDateTime(5)).ToString("dd/MMM/yyyy");
                                pagos += " - Pago Hasta: " + (datos.GetDateTime(6)).ToString("dd/MMM/yyyy");
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando pagos por contrato" + ex.Message);
                            }

                            lstPagos.Add(pagos);
                        }

                    }
                    conn.Close();
                }

                return lstPagos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando pagos por Contrato" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }
        
        public List<string> ConsultaNovedad(string NroContrato)
        {

            List<string> lstNovedades = new List<string>();

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
                        cmd.CommandText = "WS_CONSULTARNOVEDADES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroContrato;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string novedades = string.Empty;
                            try
                            {

                                novedades += " Nro Novedad: " + datos.GetString(0);
                                novedades += " - Fecha:  " + (datos.GetDateTime(1)).ToString("dd/MMM/yyyy");
                                novedades += " - Motivo: " + datos.GetString(2);
                                novedades += " - Observaciones: " + datos.GetString(3);
                                novedades += " - Usuario: " + datos.GetString(4);
                               
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando novedades por contrato" + ex.Message);
                            }

                            lstNovedades.Add(novedades);
                        }

                    }
                    conn.Close();
                }

                return lstNovedades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando novedades por Contrato" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }

        public List<string> ConsultarAdicionales(string NroContrato)
        {

            List<string> lstNovedades = new List<string>();

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
                        cmd.CommandText = "WS_CONSULTARSERVADICIONALES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idcontrato", FbDbType.VarChar).Value = NroContrato;


                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string novedades = string.Empty;
                            try
                            {

                              
                                novedades += " Fecha:  " + (datos.GetDateTime(0)).ToString("dd/MMM/yyyy");
                                novedades += " - Servicio adicional: " + datos.GetString(1);
                                novedades += " -  Valor: " + datos.GetString(2);
                                novedades += " - Documento: " + datos.GetString(3);
                                novedades += " - Nombre: " + datos.GetString(4);
                                novedades += " - Nota: " + datos.GetString(5);

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando servicios adicionales por contrato" + ex.Message);
                            }

                            lstNovedades.Add(novedades);
                        }

                    }
                    conn.Close();
                }

                return lstNovedades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Consultando servicios adicionales por Contrato" + ex.Message);
            }
            finally
            {
                db.FbConectionClose();
            }

        }


        public List<string> ConsultaLstPagos(CuadreCajaRequest consulta)
        {
            Conection db = new Conection();
            CuadreCajaDTO cuadre = new CuadreCajaDTO();
            List<string> lstPagos = new List<string>();

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
                        cmd.CommandText = "SW_LISTADOPAGOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = consulta.Dato;
                        cmd.Parameters.Add("@FECHA", FbDbType.Date).Value = consulta.Fecha;

                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string pagos = string.Empty;
                            try
                            {
                                pagos += "Persona: " + datos.GetString(1);
                                pagos += " - Nro Documento: " + datos.GetString(0);
                                pagos += " - Fecha Pago:  " + (datos.GetDateTime(2)).ToString("dd/MMM/yyyy");
                                pagos += " - Nro Recibo: " + datos.GetString(3);
                                pagos += " - Descuento: " + datos.GetString(4);
                                pagos += " - Valor Pagado: " + (datos.GetString(5)).ToString();
                                pagos += " - Observaciones: " + (datos.GetString(6)).ToString();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando pagos por contrato" + ex.Message);
                            }

                            lstPagos.Add(pagos);
                        }
                    }
                    conn.Close();
                }


                return lstPagos;
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

        public List<string> ConsultaLstNovedades(CuadreCajaRequest consulta)
        {
            Conection db = new Conection();
            CuadreCajaDTO cuadre = new CuadreCajaDTO();
            List<string> lstNovedades = new List<string>();

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
                        cmd.CommandText = "SW_LISTADONOVEDADES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = consulta.Dato;
                        cmd.Parameters.Add("@FECHA", FbDbType.Date).Value = consulta.Fecha;

                        FbDataReader datos = cmd.ExecuteReader();

                        while (datos.Read())
                        {
                            string novedades = string.Empty;
                            try
                            {
                                novedades += "Nro Novedad: " + datos.GetString(0);
                                novedades += " - Fecha Novedad:  " + (datos.GetDateTime(1)).ToString("dd/MMM/yyyy");
                                novedades += " - Motivo: " + datos.GetString(2);
                                novedades += " - Observaciones: " + (datos.GetString(3)).ToString();
                                novedades += " - Usuario: " + (datos.GetString(4)).ToString();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("error consultando novedades por contrato" + ex.Message);
                            }

                            lstNovedades.Add(novedades);
                        }
                    }
                    conn.Close();
                }


                return lstNovedades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Sacando Lista Novedades ");
            }
            finally
            {
                db.FbConectionClose();
            }
        }

    }
}
