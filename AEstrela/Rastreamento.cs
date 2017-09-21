using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrelaCaminho
{
    public class Rastreamento
    {
        public Rastreamento(Vertice atual, Rastreamento anterior)
        {
            this.verticeAtual = atual;
            this.anterior = anterior;
        }
        public Vertice verticeAtual { get; set; }

        public Rastreamento Anterior
        {
            get
            {
                return anterior;
            }

            set
            {
                anterior = value;
            }
        }

        public double CustoG { get; set; }

        public double CustoH { get; set; }

        public double CustoTotal
        {
            get { return CustoG + CustoH; }
        }

        private Rastreamento anterior = null;
    }
}
