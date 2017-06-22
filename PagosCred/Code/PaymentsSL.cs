using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class PaymentsSL
    {
        private String customerID;
        private String documentID;
        /*private String queryGet = String.Format(@"SELECT TOP 1 PIVOT4.MontoFinanciado, PIVOT4.MontoActual, PIVOT4.FechaUltimoPago, PIVOT4.MontoUltimoPago, PIVOT4.PKCliente, PIVOT4.[Plazo(dias)], 
                                                PIVOT4.FechaFactura, PIVOT4.FechaMaximoVencimiento, PIVOT4.CelularTitular, PIVOT4.TelefonoCasa, PIVOT4.TelefonoEmpleo, PIVOT4.Nextel, PIVOT4.LargaDistanciaCelular,
                                                PIVOT4.LargaDistanciaLocal, PIVOT4.DomicilioCasa, PIVOT4.DomicilioEmpleo, PIVOT4.CorreoElectronico, PIVOT4.NombreTitular, PIVOT4.NombreAval, 
                                                PIVOT4.CelularAval, PIVOT4.TelefonoCasaAval, PIVOT4.TelefonoEmpleoAval, PIVOT4.NextelAval, PIVOT4.LargaDistanciaCelularAval,
                                                PIVOT4.LargaDistanciaLocalAval, PIVOT4.Casa 'DomicilioCasaAval', PIVOT4.Empleo 'DomicilioEmpleoAval', PIVOT4.NumReferencia1, PIVOT4.NumReferencia2,
                                                PIVOT4.UnidadEquipoFinanciado, PIVOT4.DiasVencidosDocumento, PIVOT4.TipoCredito, PIVOT4.Scoring, PIVOT4.DocumentosVencidos, PIVOT4.PromesaPago, PIVOT4.EstatusCobro, PIVOT4.DocumentoCurso,
                                                PIVOT4.HistorialMora, PIVOT4.EnCola, PIVOT4.UltimoStatus
                                                FROM
                                                (      
                                                       SELECT PIVOT3.MontoFinanciado, PIVOT3.MontoActual, PIVOT3.FechaUltimoPago, PIVOT3.MontoUltimoPago, PIVOT3.PKCliente, PIVOT3.[Plazo(dias)], 
                                                       PIVOT3.FechaFactura, PIVOT3.FechaMaximoVencimiento, PIVOT3.CelularTitular, PIVOT3.TelefonoCasa, PIVOT3.TelefonoEmpleo, PIVOT3.Nextel, PIVOT3.LargaDistanciaCelular,
                                                       PIVOT3.LargaDistanciaLocal, PIVOT3.Casa 'DomicilioCasa', PIVOT3.Empleo 'DomicilioEmpleo', PIVOT3.CorreoElectronico, PIVOT3.NombreTitular, PIVOT3.NombreAval, 
                                                       PIVOT3.Celular 'CelularAval', PIVOT3.Casa 'TelefonoCasaAval', PIVOT3.Empleo 'TelefonoEmpleoAval', PIVOT3.Nextel 'NextelAval', PIVOT3.[Larga distancia celular] 'LargaDistanciaCelularAval',
                                                       PIVOT3.[Larga distancia local] 'LargaDistanciaLocalAval', PIVOT3.DomicilioAval, PIVOT3.TipoDomicilioAval, PIVOT3.NumReferencia1, PIVOT3.NumReferencia2,
                                                       PIVOT3.UnidadEquipoFinanciado, PIVOT3.DiasVencidosDocumento, PIVOT3.TipoCredito, PIVOT3.Scoring, PIVOT3.DocumentosVencidos, PIVOT3.PromesaPago, PIVOT3.EstatusCobro, PIVOT3.DocumentoCurso,
                                                       PIVOT3.HistorialMora, PIVOT3.EnCola, PIVOT3.UltimoStatus
                                                       FROM
                                                       (      
                                                             SELECT PIVOT2.MontoFinanciado, PIVOT2.MontoActual, PIVOT2.FechaUltimoPago, PIVOT2.MontoUltimoPago, PIVOT2.PKCliente, PIVOT2.[Plazo(dias)], 
                                                             PIVOT2.FechaFactura, PIVOT2.FechaMaximoVencimiento, PIVOT2.CelularTitular, PIVOT2.TelefonoCasa, PIVOT2.TelefonoEmpleo, PIVOT2.NextelTitular, PIVOT2.LargaDistanciaCelular,
                                                             PIVOT2.LargaDistanciaLocal, PIVOT2.Casa 'DomicilioCasa', PIVOT2.Empleo 'DomicilioEmpleo', PIVOT2.CorreoElectronico, PIVOT2.NombreTitular, PIVOT2.NombreAval, 
                                                             PIVOT2.TelefonoAval, PIVOT2.TipoTelefonoAval, PIVOT2.DomicilioAval, PIVOT2.TipoDomicilioAval, PIVOT2.NumReferencia1, PIVOT2.NumReferencia2, PIVOT2.UnidadEquipoFinanciado, 
                                                             PIVOT2.DiasVencidosDocumento,PIVOT2.TipoCredito, PIVOT2.Scoring, PIVOT2.DocumentosVencidos, PIVOT2.PromesaPago, PIVOT2.EstatusCobro, PIVOT2.DocumentoCurso,
                                                             PIVOT2.HistorialMora, PIVOT2.EnCola, PIVOT2.UltimoStatus
                                                             FROM
                                                             (
                                                                    SELECT PIVOT1.MontoFinanciado, PIVOT1.MontoActual, PIVOT1.FechaUltimoPago, PIVOT1.MontoUltimoPago, PIVOT1.PKCliente, PIVOT1.[Plazo(dias)], PIVOT1.FechaFactura, 
                                                                    PIVOT1.FechaMaximoVencimiento, PIVOT1.Celular 'CelularTitular', PIVOT1.Casa 'TelefonoCasa', PIVOT1.Empleo 'TelefonoEmpleo', PIVOT1.Nextel 'NextelTitular', PIVOT1.[Larga distancia celular] 'LargaDistanciaCelular',
                                                                    PIVOT1.[Larga distancia local] 'LargaDistanciaLocal', PIVOT1.Domicilio, PIVOT1.TipoDomicilio, PIVOT1.CorreoElectronico, PIVOT1.NombreTitular, PIVOT1.NombreAval, PIVOT1.TelefonoAval,
                                                                    PIVOT1.TipoTelefonoAval, PIVOT1.DomicilioAval, PIVOT1.TipoDomicilioAval, PIVOT1.NumReferencia1, PIVOT1.NumReferencia2, PIVOT1.UnidadEquipoFinanciado, PIVOT1.DiasVencidosDocumento,
                                                                    PIVOT1.TipoCredito, PIVOT1.Scoring, PIVOT1.DocumentosVencidos, PIVOT1.PromesaPago, PIVOT1.EstatusCobro, PIVOT1.DocumentoCurso, PIVOT1.HistorialMora, PIVOT1.EnCola, PIVOT1.UltimoStatus
                                                                    FROM 
                                                                    (
                                                                         SELECT AD1.MontoActual, AD1.DocumentosVencidos, AT.FechaUltimoPago, AT.MontoUltimoPago, C.CustId 'PKCliente', TRM.DueIntrv 'Plazo(dias)',
                                                                           AD2.FechaFactura, AD2.FechaMaximoVencimiento, CP.Phone 'Telefono', CP.PhoneType 'TipoTelefono', CA.Address 'Domicilio', CA.AddressType 'TipoDomicilio', 
                                                                           XS.Email 'CorreoElectronico', XS.RazonSocial 'NombreTitular', PG.GuaranteeName 'NombreAval', PGP.Phone 'TelefonoAval', PGP.PhoneType 'TipoTelefonoAval', 
                                                                           PGA.Address 'DomicilioAval', PGA.AddressType 'TipoDomicilioAval', PO.ReferenciaPagoBBV 'NumReferencia1', PO.ReferenciaPagoHSBC 'NumReferencia2',
                                                                           PO.MontoOriginalCredito 'MontoFinanciado', CONCAT(PO.MarcaVersionVehiculo, ' ', PO.Modelo) 'UnidadEquipoFinanciado', AD2.DiasVencidosDocumento,
                                                                           PO.TipoCredito 'TipoCredito', PO.Scoring, X2.PromesaPago, X2.StatusCallName 'EstatusCobro',
                                                                           AD2.DocumentoCurso, XS.User6 'HistorialMora', C.User4 'EnCola', X2.StatusCallAbbreviation 'UltimoStatus'
                                                                           FROM Customer C
                                                                           INNER JOIN xSOAddress XS ON C.CustId = XS.CustId
                                                                                                                  -- CUANTO DEBE Y CUANTOS DOCUMENTOS DEBE 
                                                                           INNER JOIN (SELECT SUM(AD.DocBal)'MontoActual', AD.CustId, COUNT(*) 'DocumentosVencidos'
                                                                                               FROM ARDoc AD
                                                                                               WHERE AD.DocBal > 0 AND GETDATE() > AD.DueDate
                                                                                                                           AND AD.DocType  IN ('IN','DM','FI','NC','AD') 
                                                                                               GROUP BY AD.CustId) AD1 ON AD1.CustId = C.CustId
                                                                                                                  -- FECHA DE ULTIMO PAGO Y CUANTO PAGO
                                                                           INNER JOIN (SELECT AT.AdjgDocDate 'FechaUltimoPago', AT.CustId, SUM(AT.AdjAmt) 'MontoUltimoPago'
                                                                                               FROM ARAdjust AT INNER JOIN (SELECT MAX(AT.AdjgDocDate) 'Fecha', AT.CustId
                                                                                                                                             FROM ARAdjust AT
                                                                                                                                                                                                       WHERE AT.AdjgDocType <> 'CM'--DIFERENTE A NOTA DE CREDITO
                                                                                                                                             GROUP BY AT.CustId) W ON W.Fecha = AT.AdjgDocDate AND AT.CustId = W.CustId
                                                                                               GROUP BY AT.AdjgDocDate, AT.CustId) AT ON AT.CustId = C.CustId
                                                                                                                  -- PRIMER FACTURA VENCIDA, CUANTOS DIAS HAN PASADO, NUMERO DE MENSUALIDAD DE FACTURA 
                                                                           INNER JOIN (SELECT AD.DueDate 'FechaMaximoVencimiento', MIN(AD.DocDate) 'FechaFactura',  DATEDIFF(DAY, AD.DueDate, GETDATE()) 'DiasVencidosDocumento', 
                                                                                               AD.CustId, MIN(AD.InstallNbr) 'DocumentoCurso'
                                                                                               FROM ARDoc AD INNER JOIN (SELECT MIN(AD.DueDate) 'Fecha', AD.CustId 
                                                                                                                                      FROM ARDoc AD
                                                                                                                                      WHERE AD.DocBal > 0 AND GETDATE() > AD.DueDate
                                                                                                                                                                                            AND AD.DocType  IN ('IN','DM','FI','NC','AD') 
                                                                                                                                      GROUP BY AD.CustId) Z ON Z.CustId = AD.CustId AND Z.Fecha = AD.DueDate
                                                                                               GROUP BY AD.CustId, AD.DueDate) AD2 ON AD2.CustId = C.CustId
                                                                           LEFT JOIN (SELECT PR.PaymentPromise 'PromesaPago', PSC.StatusCallName, PR.FKCusId, X.MaxxDate, PSC.StatusCallAbbreviation
                                                                                               FROM PaymentsRecord PR
                                                                                               INNER JOIN
                                                                                                     (SELECT PR.FKCusId, MAX(PR.FinalDate) AS MaxxDate 
                                                                                                      FROM PaymentsRecord PR INNER JOIN PaymentsStatusCall PSC ON PSC.PKStatusCall = PR.FKStatusCall
                                                                                                     GROUP BY PR.FKCusId) X ON PR.FKCusId = X.FKCusId AND PR.FinalDate = X.MaxxDate
                                                                                               INNER JOIN PaymentsStatusCall PSC ON PSC.PKStatusCall= PR.FKStatusCall) X2 ON X2.FKCusId = C.CustId
                                                                           INNER JOIN Terms TRM ON C.Terms = TRM.TermsId
                                                                           LEFT JOIN PaymentsCustomerPhone CP on CP.FKCustomerSL = C.CustId
                                                                           LEFT JOIN PaymentsCustomerAddress CA on CA.FKCustomerSL = C.CustId
                                                                           LEFT JOIN PaymentsGuarantee PG ON PG.FKCustomerSL = C.CustId
                                                                           LEFT JOIN PaymentsGuaranteePhone PGP ON PG.PKGuarantee = PGP.FKGuarantee
                                                                           LEFT JOIN PaymentsGuaranteeAddress PGA ON PG.PKGuarantee = PGA.FKGuarantee
                                                                           LEFT JOIN PaymentsCustomer CM ON CM.PKCustomerSL = C.CustId --CHANGE TO INNER
                                                                           LEFT JOIN PaymentsOportunity PO ON CM.PKCustomer = PO.FKCliente --CHANGE TO INNER
                                                                           WHERE C.User3 != 1
                                                                    ) SRC
                                                                    PIVOT
                                                                    (
                                                                    MAX(Telefono)
                                                                    FOR TipoTelefono IN ([Celular], [Casa], [Empleo], [Nextel], [Larga distancia celular], [Larga distancia local]) 
                                                                    ) AS PIVOT1
                                                             ) SRC2
                                                             PIVOT
                                                             (
                                                             MAX(Domicilio)
                                                             FOR TipoDomicilio IN ([Casa], [Empleo])
                                                             ) AS PIVOT2
                                                       )SRC3
                                                       PIVOT
                                                       (
                                                       MAX(TelefonoAval)
                                                       FOR TipoTelefonoAval IN ([Celular], [Casa], [Empleo], [Nextel], [Larga distancia celular], [Larga distancia local])
                                                       ) AS PIVOT3
                                                )SRC4
                                                PIVOT
                                                (
                                                MAX(DomicilioAval)
                                                FOR TipoDomicilioAval IN ([Casa], [Empleo])
                                                ) AS PIVOT4 WHERE 1=1 ");
                                                */
        private String queryGet = String.Format(@"SELECT TOP 1 * FROM xvr_Payments WHERE 1=1 ");

        public string CustomerID
        {
            get
            {
                return customerID;
            }

            set
            {
                customerID = value;
            }
        }

        public string DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
            }
        }

        public PaymentsSL()
        {
            /*
            DataTable aux = GetFirstPayment();
            if(aux.Rows.Count > 0)
            {
                DocumentID = aux.Rows[0]["PKFactura"].ToString().Trim();
                CustomerID = aux.Rows[0]["PKCliente"].ToString().Trim();
            }
            */
        }

        public PaymentsSL(String customerID, String documentID)
        {
            this.CustomerID = customerID;
            this.DocumentID = documentID;
        }

        public PaymentsSL(String customerID)
        {
            this.CustomerID = customerID;
        }

        public DataTable GetContracts(String custId)
        {
            try
            {
                String query = String.Format(@"SELECT DISTINCT User5 as 'Contrato'
                                                FROM ARDoc
                                                WHERE CustId = '{0}' AND User5 != ' '", custId);

                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFirstPaymentByUser(String userID)
        {
            try
            {
                DataBaseSettings db = new DataBaseSettings();
                String finalQuery = queryGet + QueueOperations.GetQueueConditionsByUsers(userID);

                //if (finalQuery.Contains("ORDER BY"))
                //{
                //    finalQuery += String.Format(", EnCola");
                //}
                //else
                //{
                //    finalQuery += String.Format("ORDER BY EnCola");
                //}

                return db.GetDataTable(finalQuery);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFirstPayment(String CustomerID)
        {
            try
            {
                String query = queryGet + String.Format(@"AND PKCliente = '{0}'", CustomerID);
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFirstPaymentByName(String customerName)
        {
            try
            {
                String query = queryGet + String.Format(@"AND NombreTitular = '{0}'", customerName);
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetPaymentBusy()
        {
            try
            {
                String query = String.Format("UPDATE Customer SET User3 = 1, User4 = User4 + 1 WHERE CustId = '{0}'", this.customerID);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetPaymentFree()
        {
            try
            {
                String query = String.Format("UPDATE Customer SET User3 = 0 WHERE CustId = '{0}'", this.customerID);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public Balance GetBalances(DateTime date)
        {
            try
            {
                //date = date.AddHours(23);
                //date = date.AddMinutes(59);
                String query = String.Format(@"SELECT CASE WHEN W.Interes IS NULL THEN (SELECT SUM(PA.Intereses + PA.Iva) AS 'Interes'
										FROM PaymentsOportunity PO
										INNER JOIN PaymentsCustomer PC ON PO.FKCliente = PC.PKCustomer
										INNER JOIN PaymentsAmortization PA ON PO.PKOportunity = PA.FKOportunity
										WHERE PC.PKCustomerSL = '{1}') 
									ELSE W.Interes END 'Interes',
		                                                Z.DiasVencimiento,
		                                                Z.DocBal,
		                                                Z.SaldoVigente,
		                                                Z.DueDate,
		                                                Z.CustId
                                                FROM (SELECT /*CASE WHEN XAM.Interes IS NULL THEN 0 ELSE XAM.Interes END 'Interes', */
		                                                'DiasVencimiento' = 
			                                                CASE
			                                                WHEN  DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') > 0 AND DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') <=30 THEN '30'
			                                                WHEN  DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') > 30 AND DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') <=60 THEN '60'
			                                                WHEN  DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') > 60 AND DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') <=90 THEN '90'
			                                                WHEN  DATEDIFF(DAY, AD.DueDate, '2017 -04-18 00:00:00') > 90 THEN '+90'
                                                            ELSE '0'
			                                                END				
			                                                , CASE WHEN AD.DocType IN ('IN','DM','FI','NC','AD') THEN 1 ELSE -1 END * AD.DocBal AS DocBal, 
			                                                CASE WHEN SV.SaldoVigente IS NULL THEN 0 ELSE SV.SaldoVigente END 'SaldoVigente', 
			                                                AD.DueDate,
			                                                AD.CustId
	                                                FROM xAMContAutDet XAM
	                                                RIGHT JOIN ARDOC AD ON XAM.Custid = AD.CustId AND XAM.Contrato = AD.USER5  AND XAM.Num = AD.InstallNbr
	                                                LEFT JOIN (SELECT SUM(AD.DocBal) 'SaldoVigente', AD.CustId, AD.User5 --Documentos no vencidos
				                                                FROM ARDoc AD 
				                                                WHERE AD.CustId = '{1}' AND '2017 -04-18 00:00:00' <= AD.DueDate AND AD.DocBal != 0 AND AD.DocType  IN ('IN','DM','FI','NC','AD')
				                                                GROUP BY AD.CustId, AD.User5) SV ON SV.CustId = AD.CustId AND SV.User5 = AD.User5
	                                                WHERE AD.DocBal != 0 AND /* '2017 -04-18 00:00:00' > AD.DueDate AND */ AD.CustId = '{1}') Z

                                                LEFT JOIN (SELECT SUM(xa.Interes+ xa.IvaInteres) as 'Interes', xa.Custid
															 FROM ARDoc AD
															 INNER JOIN xAMContAutDet XA ON AD.CustId = XA.Custid AND AD.User5 = XA.Contrato and ad.user6 = xa.anexo AND AD.InstallNbr = XA.Num
															 WHERE AD.CustId = '{1}' AND '2017 -04-18 00:00:00' <= AD.DueDate AND AD.DocBal > 0 --and Anexo = 'b'
															 GROUP BY xa.Custid) 
                                                W ON Z.CustId = W.CustId --ON
                                                GROUP BY W.Interes, Z.DiasVencimiento, Z.DocBal, Z.SaldoVigente, Z.DueDate, Z.CustId", date.ToString("yyyy -MM-dd HH:mm:ss"), this.CustomerID);
                DataBaseSettings db = new DataBaseSettings();
                Balance bl = new Balance(db.GetDataTable(query), date);
                return bl;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}