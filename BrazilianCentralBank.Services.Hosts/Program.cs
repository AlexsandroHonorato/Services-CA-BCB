using System;
using System.Collections.Generic;
using System.Threading;
using BrazilianCentralBank.Application;
using BrazilianCentralBank.Domain.Entities;

namespace BrazilianCentralBank.Services.Hosts {
    class Program {
        static void Main(string[] args) {
            try {
                Console.WriteLine("====================================================================");
                Console.WriteLine("Iniciando conexão com Banco Central");

                BrazilianCentralBankService banckServices = new BrazilianCentralBankService();
                List<Currency.CurrencyAttributes> currencyAttributes = banckServices.SelectCurrencyType();


                if (currencyAttributes.Count == 0 ) 
                        BrazilianCentralBankService.GetCurrencyType();

                Console.WriteLine("Informações dos tipos das moedas inseridas com sucesso.");

                while (true) {
                    Console.WriteLine("====================================================================");
                    Console.WriteLine("Iniciando conexão com Banco Central");
                    BrazilianCentralBankService.GetCurrencyQuotation(currencyAttributes);

                    Console.WriteLine("Informações das Contações diarias inseridas com sucesso.");
                    Thread.Sleep(10000);
                }

            } catch (Exception error) {

                throw error;
            }
        }
    }
}
