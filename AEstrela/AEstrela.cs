using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrelaCaminho
{
    public class AEstrela
    {
        public bool Executar(ETipoVertice[,] espacoBusca, int numeroLinhas, int numeroColunas, Vertice origem, Vertice meta, Heuristica heuristica, out List<Vertice> caminho)
        {
            FilaOrdenada abertos = new FilaOrdenada();
            Dictionary<Vertice, Rastreamento> fechados = new Dictionary<Vertice, Rastreamento>();

            if (origem == null || meta == null || heuristica == null)
            {
                caminho = null;
                return false;
            }

            Rastreamento atual = new Rastreamento(origem, null);
            heuristica.CalcularCustos(atual, meta, 0);

            abertos.Adicionar(atual);

            while(atual != null)
            {
                if(atual.verticeAtual.Equals(meta))
                {
                    caminho = retornarCaminho(atual);
                    return true;
                }
                else
                {
                    fechados.Add(atual.verticeAtual, atual);
                    abertos.Remover();

                    for (int i = atual.verticeAtual.linha - 1; i <= atual.verticeAtual.linha + 1; i++)
                        if (i >= 0 && i < numeroLinhas)
                            for (int j = atual.verticeAtual.coluna; j <= atual.verticeAtual.coluna + 1; j++)
                                if (j >= 0 && j < numeroColunas)
                                    if (atual.verticeAtual.linha != i || atual.verticeAtual.coluna != j)
                                        if (espacoBusca[i, j] != ETipoVertice.Obstaculo)
                                            adicionarVertice(abertos, fechados, atual, meta, heuristica, i, j);

                    atual = abertos.Primeiro;
                }
            }
            caminho = null;
            return false;
        }

        private void adicionarVertice(FilaOrdenada abertos, Dictionary<Vertice, Rastreamento> fechados, Rastreamento atual, Vertice meta, Heuristica heuristica, int i, int j)
        {
            Vertice v = new Vertice(i, j);
            if (!fechados.ContainsKey(v))
            {
                Rastreamento r = new Rastreamento(v, atual);

                if (i != atual.verticeAtual.linha && j != atual.verticeAtual.coluna)
                    heuristica.CalcularCustos(r, meta, 14.14);
                else
                    heuristica.CalcularCustos(r, meta, 10);

                if (!abertos.ContemVertice(v))
                    abertos.Adicionar(r);
                else
                {
                    Rastreamento aux = abertos.Get(v);

                    if(aux.CustoTotal > r.CustoTotal)
                    {
                        abertos.Remover(aux);

                        aux.Anterior = r.Anterior;
                        aux.CustoG = r.CustoG;
                        aux.CustoH = r.CustoH;

                        abertos.Adicionar(aux);
                    }
                }


            }
        }

        private List<Vertice> retornarCaminho(Rastreamento atual)
        {
            List<Vertice> lista = new List<Vertice>();
            Rastreamento aux = atual;

            while(aux != null)
            {
                lista.Add(aux.verticeAtual);

                aux = aux.Anterior;
            }
            

            return lista;
        }
    }
}
