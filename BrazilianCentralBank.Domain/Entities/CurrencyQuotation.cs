using System;
using System.Collections.Generic;
using System.Text;

namespace BrazilianCentralBank.Domain.Entities {
    public class CurrencyQuotation {

        public CurrencyQuotationAttributes[] value { get; set; }

        public class CurrencyQuotationAttributes {
            public string Simbolo { get; set; }
            public float ParidadeCompra { get; set; }
            public float ParidadeVenda { get; set; }
            public float CotacaoCompra { get; set; }
            public float CotacaoVenda { get; set; }
            public DateTime DataHoraCotacao { get; set; }
            public string TipoBoletim { get; set; }     
        }
      
    }
}
