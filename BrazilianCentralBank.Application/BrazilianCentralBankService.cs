
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using BrazilianCentralBank.Domain.Entities;
using BrazilianCentralBank.Infrastructure.Data.Repository;
using Newtonsoft.Json;

namespace BrazilianCentralBank.Application {
    public class BrazilianCentralBankService {

        public static void GetCurrencyType() {
            try {
            
                string strURLCurrency = ConfigurationManager.AppSettings["URLMoedas"];
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURLCurrency);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

                string result = sr.ReadToEnd();
                Currency jsonConvert = JsonConvert.DeserializeObject<Currency>(result);

                BrazilianCentralBankRepository repository = new BrazilianCentralBankRepository();
                repository.InsertCurrencyTipe(jsonConvert);


            } catch (Exception error) {

                throw error;
            }
        }

        public  List<Currency.CurrencyAttributes> SelectCurrencyType() {
            try {

                return BrazilianCentralBankRepository.SelectCurrencyTipe();

            } catch (Exception error) {

                throw error;
            }
        
        }

        public static void GetCurrencyQuotation(List<Currency.CurrencyAttributes> currencyAttributes) {
            try {

             

                foreach (var itemCurrency in currencyAttributes) {

                    string strURLCurrency = ConfigurationManager.AppSettings["URLCotacaoDiaria"];
                    strURLCurrency += $"?@moeda=%27{itemCurrency.Simbolo}%27&@dataInicial=%27{DateTime.Now.ToString("MM-dd-yyyy")}" +
                                      $"%27&@dataFinalCotacao=%27{DateTime.Now.ToString("MM-dd-yyyy")}%27&$top=100&$format=json&$" +
                                      $"select=paridadeCompra,paridadeVenda,cotacaoCompra,cotacaoVenda,dataHoraCotacao,tipoBoletim";
               
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURLCurrency);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

                    string result = sr.ReadToEnd();
                    CurrencyQuotation jsonConvert = JsonConvert.DeserializeObject<CurrencyQuotation>(result);

                    BrazilianCentralBankRepository repository = new BrazilianCentralBankRepository();                   
                    repository.InsertCurrencyQuotation(jsonConvert, itemCurrency.Simbolo);

                   

                }

            } catch (Exception error) {

                throw error;
            }
        }
    }
}
