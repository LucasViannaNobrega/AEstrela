using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrelaCaminho
{
    public class Heuristica
    {
        public void CalcularCustos(Rastreamento atual, Vertice meta, double valor)
        {
            if (atual.Anterior == null)
                atual.CustoG = valor;
            else
                atual.CustoG = atual.Anterior.CustoG + valor;

            double a = atual.verticeAtual.linha - meta.linha;
            double b = atual.verticeAtual.coluna - meta.coluna;

            atual.CustoH = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
    }
}
