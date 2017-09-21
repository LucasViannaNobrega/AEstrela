using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrelaCaminho
{
    public class Vertice
    {
        public Vertice(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public int linha { get; set; }
        public int coluna { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is Vertice)
            {
                var outro = (Vertice)obj;

                return (linha == outro.linha && coluna == outro.coluna); 
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (linha.ToString() + coluna.ToString()).GetHashCode();
        }
    }
}
