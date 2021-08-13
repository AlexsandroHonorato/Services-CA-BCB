
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

using BrazilianCentralBank.Domain.Entities;

namespace BrazilianCentralBank.Infrastructure.Data.Repository {
    public class BrazilianCentralBankRepository {

        public void  InsertCurrencyTipe(Currency currency) {
            try {

                foreach (var itemCurrency in currency.value) {

                    StringBuilder _query = new StringBuilder(@"insert into Currency ([Simbolo], [NomeFormatado], [TipoMoeda]) 
                                                               values (@simbolo, @nomeFormatado, @tipoMoeda)");

                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStrings"]))
                    using (SqlCommand command = new SqlCommand(_query.ToString(), sqlConnection)) {
                        sqlConnection.Open();

                        command.Parameters.AddWithValue("@simbolo", itemCurrency.Simbolo);
                        command.Parameters.AddWithValue("@nomeFormatado", itemCurrency.NomeFormatado);
                        command.Parameters.AddWithValue("@tipoMoeda", itemCurrency.TipoMoeda);

                        var retuno = command.ExecuteNonQuery();

                        sqlConnection.Close();
                    }
                }                        

            } catch (Exception error) {

                throw error;
            }

        }

        public static List<Currency.CurrencyAttributes> SelectCurrencyTipe() {
            try {

                List<Currency.CurrencyAttributes> currenciesList = new List<Currency.CurrencyAttributes>();

                StringBuilder _query = new StringBuilder(@" Select 
                                                            convert(nvarchar(50),C.Id)  as Id,
                                                            C.Simbolo				    as Simbolo,
                                                            C.NomeFormatado			    as NomeFormatado,
                                                            C.TipoMoeda				    as TipoMoeda
                                                            from Currency C ");

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStrings"]))
                using (SqlCommand command = new SqlCommand(_query.ToString(), sqlConnection)) {
                    sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            currenciesList.Add(new Currency.CurrencyAttributes() {
                                Id = reader.IsDBNull(0) ? null : reader.GetString(0),
                                Simbolo = reader.IsDBNull(1) ? null : reader.GetString(1),
                                NomeFormatado = reader.IsDBNull(2) ? null : reader.GetString(2),
                                TipoMoeda = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }

                    sqlConnection.Close();
                }

                return currenciesList;

            } catch (Exception error) {

                throw error;
            }

        }

        public void InsertCurrencyQuotation(CurrencyQuotation currencyQuotation, String pSimbolo) {
            try {

                foreach (var itemCurrency in currencyQuotation.value) {

                    StringBuilder _query = new StringBuilder(@"insert into CurrencyQuotation ([Simbolo], [ParidadeCompra], [ParidadeVenda], [CotacaoCompra], [CotacaoVenda], [DataHoraCotacao], [TipoBoletim]) 
                                                                values (@simbolo, @paridadeCompra, @paridadeVenda, @cotacaoCompra, @cotacaoVenda, @dataHoraCotacao, @tipoBoletim)");

                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStrings"]))
                    using (SqlCommand command = new SqlCommand(_query.ToString(), sqlConnection)) {
                        sqlConnection.Open();

                        command.Parameters.AddWithValue("@simbolo", pSimbolo);
                        command.Parameters.AddWithValue("@paridadeCompra", itemCurrency.ParidadeCompra);
                        command.Parameters.AddWithValue("@paridadeVenda", itemCurrency.ParidadeVenda);
                        command.Parameters.AddWithValue("@cotacaoCompra", itemCurrency.CotacaoCompra);
                        command.Parameters.AddWithValue("@cotacaoVenda", itemCurrency.CotacaoVenda);
                        command.Parameters.AddWithValue("@dataHoraCotacao", itemCurrency.DataHoraCotacao);
                        command.Parameters.AddWithValue("@tipoBoletim", itemCurrency.TipoBoletim);

                        var retuno = command.ExecuteNonQuery();

                        sqlConnection.Close();
                    }
                }

            } catch (Exception error) {

                throw error;
            }

        }
    }
}
