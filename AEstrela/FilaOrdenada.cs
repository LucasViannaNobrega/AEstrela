using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrelaCaminho
{
    public class FilaOrdenada
    {
        private LinkedList<Rastreamento> lista = new LinkedList<Rastreamento>();

        public Rastreamento Primeiro
        {
            get { return lista.First != null ? lista.First.Value : null; }
        }

        public void Adicionar(Rastreamento atual)
        {
            LinkedListNode<Rastreamento> noPosterior = lista.First;

            while(noPosterior != null)
            {
                if (noPosterior.Value.CustoTotal > atual.CustoTotal)
                    break;

                noPosterior = noPosterior.Next;
            }

            if (noPosterior == null)
                lista.AddLast(atual);
            else
                lista.AddBefore(noPosterior, atual);
        }

        public bool ContemVertice(Vertice v)
        {
            LinkedListNode<Rastreamento> no = lista.First;

            while(no != null)
            {
                if (no.Value.verticeAtual.Equals(v))
                    return true;
       
                no = no.Next;
            }
            return false;
        }

        public void Remover()
        {
            lista.RemoveFirst();
        }

        public void Remover(Rastreamento atual)
        {
            lista.Remove(atual);
        }

        public Rastreamento Get(Vertice v)
        {
            LinkedListNode<Rastreamento> no = lista.First;

            while(no != null)
            {
                if (no.Value.verticeAtual.Equals(v))
                    return no.Value;

                no = no.Next;
            }

            return null;
        }
    }
}
