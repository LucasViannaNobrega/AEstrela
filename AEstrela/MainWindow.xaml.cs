using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AEstrelaCaminho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int numeroLinhas = 50;
        private const int numeroColunas = 50;
        private ETipoVertice tipoClick = ETipoVertice.Origem;

        private ETipoVertice[,] tabuleiro = new ETipoVertice[numeroLinhas,numeroColunas];
        private Label[,] labels = new Label[numeroLinhas, numeroColunas];

        private Vertice origem = null;
        private Vertice meta = null;

        public MainWindow()
        {
            InitializeComponent();

            mostrarTabuleiro();
            
        }

        private void mostrarTabuleiro()
        {
            for (int i = 0; i < numeroLinhas; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                conteinerPrincipal.Children.Add(sp);

                for (int j = 0; j < numeroColunas ; j++)
                {
                    Label lbl = new Label();
                    lbl.BorderBrush = Brushes.Black;
                    lbl.BorderThickness = new Thickness(1);
                    lbl.Background = Brushes.Silver;
                    lbl.Name = "lbl" + i.ToString("00") + "_" + j.ToString("00");
                    lbl.MouseDown += Lbl_MouseDown;
                    labels[i, j] = lbl;

                    sp.Children.Add(lbl);
                }
            }
        }

        private void Lbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = sender as Label;

            if (lbl != null)
            {
                string[] indices = lbl.Name.Substring(3).Split('_');

                int i = Convert.ToInt32(indices[0]);
                int j = Convert.ToInt32(indices[1]);

                if (tabuleiro[i, j] == ETipoVertice.Vazio)
                {
                    tabuleiro[i, j] = tipoClick;
                    if (tipoClick == ETipoVertice.Origem)
                    {
                        origem = new Vertice(i, j);
                        labels[i, j].Background = Brushes.Blue;
                        tipoClick = ETipoVertice.Meta;
                        lblMsg.Content = "Clique na meta";
                    }
                    else if (tipoClick == ETipoVertice.Meta)
                    {
                        meta = new Vertice(i, j);
                        labels[i, j].Background = Brushes.Red;
                        tipoClick = ETipoVertice.Obstaculo;
                        lblMsg.Content = "Clique nos obstáculos";
                        btnProcessar.Visibility = Visibility.Visible;
                    }
                    else if (tipoClick == ETipoVertice.Obstaculo)
                    {
                        labels[i, j].Background = Brushes.Black;

                    }
                }

            }
        }

        private void btnProcessar_Click(object sender, RoutedEventArgs e)
        {
            AEstrela algoritmo = new AEstrela();

            List<Vertice> caminho;
            if(algoritmo.Executar(tabuleiro, numeroLinhas, numeroColunas, origem, meta, new Heuristica(),out caminho))
                mostrarCaminho(caminho);
            else
                MessageBox.Show("caminho não encontrado!");


            btnLimpar.Visibility = Visibility.Hidden;
            btnProcessar.IsEnabled = true;
   

        }

        private void mostrarCaminho(List<Vertice> caminho)
        {
           foreach(var v in caminho)
            {
                if (tabuleiro[v.linha, v.coluna] == ETipoVertice.Vazio)
                    labels[v.linha, v.coluna].Background = Brushes.Green;
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < numeroLinhas; i++)
                for (int j = 0; j < numeroColunas; j++)
                    if(tabuleiro[i, j] == ETipoVertice.Vazio)
                        labels[i,j].Background = Brushes.Silver;

            btnLimpar.Visibility = Visibility.Hidden;
            btnProcessar.IsEnabled = true;
        }
    }
}
