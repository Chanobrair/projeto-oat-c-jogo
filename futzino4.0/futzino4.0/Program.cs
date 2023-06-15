using System;
using System.Collections.Generic;

namespace futzino
{
    class Program
    {
        static void Main(string[] args)
        {
            Jogo jogo = new Jogo();
            jogo.Iniciar();
        }
    }

    interface ICarta
    {
        int ObterPontuacao();
        string ObterNome();
    }

    class GolCarta : ICarta
    {
        public int ObterPontuacao()
        {              
            return 3;
        }

        public string ObterNome()
        {
            return "Gol";
        }
    }

    class PenaltiCarta : ICarta
    {
        public int ObterPontuacao()
        {
            return 2;
        }

        public string ObterNome()
        {
            return "Pênalti";
        }
    }

    class FaltaCarta : ICarta
    {
        public int ObterPontuacao()
        {
            return 1;
        }

        public string ObterNome()
        {
            return "Falta";
        }
    }

    class CartaoAmareloCarta : ICarta
    {
        public int ObterPontuacao()
        {
            return 1;
        }

        public string ObterNome()
        {
            return "Cartão Amarelo";
        }
    }

    class CartaoVermelhoCarta : ICarta
    {
        public int ObterPontuacao()
        {
            return 0;
        }

        public string ObterNome()
        {
            return "Cartão Vermelho";
        }
    }

    class EnergiaCarta : ICarta
    {
        public int ObterPontuacao()
        {
            return 2;
        }

        public string ObterNome()
        {
            return "Energia";
        }
    }

    class Jogador
    {
        public string Nome { get; set; }
        public int Gols { get; set; }
        public int Pontuacao { get; set; }
        public int Energias { get; set; }
    }

    class Jogo
    {
        private Jogador jogador1;
        private Jogador jogador2;
        private bool jogador1Vez;

       public void Multiplayer()
        {
            Console.WriteLine("Bem-vindo ao futzino!");

            Console.WriteLine("Digite o nome do Jogador 1:");
            string nomeJogador1 = Console.ReadLine();
            jogador1 = new Jogador { Nome = nomeJogador1, Energias = 10 };

            Console.WriteLine("Digite o nome do Jogador 2:");
            string nomeJogador2 = Console.ReadLine();
            jogador2 = new Jogador { Nome = nomeJogador2, Energias = 10 };

            jogador1Vez = SortearJogadorInicial();

            Console.WriteLine("Começando o jogo...");

            while (true)
            {
                if (jogador1Vez)
                {
                    Console.WriteLine($"Vez de {jogador1.Nome}");
                    Jogar(jogador1, jogador2);
                }
                else
                {
                    Console.WriteLine($"Vez de {jogador2.Nome}");
                    Jogar(jogador2, jogador1);
                }
                if (jogador1.Energias == 0 && jogador2.Energias == 0)
                {
                    FinalizarPartida();
                    break;
                }

            }
        }

        public void SinglePlayer()
        {
            Console.WriteLine("Bem-vindo ao futzino!");

            Console.WriteLine("Digite o nome do Jogador 1:");
            string nomeJogador1 = Console.ReadLine();
            jogador1 = new Jogador { Nome = nomeJogador1, Energias = 10 };

            Console.WriteLine("Computador:");
            string nomeJogador2 = "Futzino";
            jogador2 = new Jogador { Nome = nomeJogador2, Energias = 10 };

            jogador1Vez = SortearJogadorInicial();

            Console.WriteLine("Começando o jogo...");

            while (true)
            {
                if (jogador1Vez)
                {
                    Console.WriteLine($"Vez de {jogador1.Nome}");
                    Jogar(jogador1, jogador2);
                }
                else
                {
                    Console.WriteLine($"Vez de {jogador2.Nome}");
                    Jogar(jogador2, jogador1);
                }
                if (jogador1.Energias == 0 && jogador2.Energias == 0)
                {
                    FinalizarPartida();
                    break;
                }

            }
        }

        public void Iniciar()
        {
            int Jogo;
            Console.WriteLine("1 - MultiPlayer");
            Console.WriteLine("2 - SinglePlayer");
            Jogo = int.Parse(Console.ReadLine());
            if (Jogo == 1)
            {
                Multiplayer();               
            }
            else if (Jogo == 2)
            {
                SinglePlayer();
            }
            while (true)
            {
                Console.WriteLine("Digite -1 para sair ou 0 para nova partida:");
                string opcao = Console.ReadLine();
                if (opcao == "-1")
                    break;
                else if (opcao == "0")
                    ResetarPartida();
            }
        }

        private bool SortearJogadorInicial()
        {
            Random random = new Random();
            return random.Next(2) == 0;
        }

        private void Jogar(Jogador jogador, Jogador adversario)
        {

            Console.WriteLine($"Energias de {jogador.Nome}: {jogador.Energias}");
            Console.WriteLine($"Gols de {jogador.Nome}: {jogador.Gols}");
            Console.WriteLine($"Pontuação de {jogador.Nome}: {jogador.Pontuacao}");

            Console.WriteLine("Pressione Enter para abrir as cartas...");
            Console.ReadLine();

            List<ICarta> cartas = ObterCartasAleatorias();

            Console.WriteLine("Cartas abertas:");
            foreach (var carta in cartas)
            {
                Console.WriteLine(carta.ObterNome());         
            }
            int pontuacaoRodada = CalcularPontuacaoRodada(cartas);       
            if (pontuacaoRodada == 0)
            {
                Console.WriteLine("Nenhum gol marcado nessa rodada.");
            }
            else
            {
                jogador.Gols += pontuacaoRodada;
                Console.WriteLine($"Gol marcado! {jogador.Nome} ganhou {pontuacaoRodada} ponto(s).");
            }

            if (jogador.Energias > 0)
            {
                Console.WriteLine("Pressione Enter para continuar...");
                Console.ReadLine();
            }

            jogador.Energias--;
            jogador1Vez = !jogador1Vez;
        }

        private List<ICarta> ObterCartasAleatorias()
        {
            ICarta[] todasCartas =
            {
                new GolCarta(),
                new PenaltiCarta(),
                new FaltaCarta(),
                new CartaoAmareloCarta(),
                new CartaoVermelhoCarta(),
                new EnergiaCarta()
            };

            Random random = new Random();
            List<ICarta> cartas = new List<ICarta>();

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(todasCartas.Length);
                cartas.Add(todasCartas[index]);
            }

            return cartas;
        }
        private void Cartas3()
        {

        }
        private int CalcularPontuacaoRodada(List<ICarta> cartas)
        {
            int pontuacao = 0;

            foreach (var carta in cartas)
            {
                pontuacao += carta.ObterPontuacao();
            }

            return pontuacao;
        }

        private void FinalizarPartida()
        {
            Console.WriteLine("Partida encerrada!");

            Console.WriteLine($"Placar: {jogador1.Nome} {jogador1.Gols} x {jogador2.Gols} {jogador2.Nome}");
            Console.WriteLine($"Pontuação final: {jogador1.Nome}: {jogador1.Pontuacao} pontos, {jogador2.Nome}: {jogador2.Pontuacao} pontos");

            if (jogador1.Gols > jogador2.Gols)
            {
                Console.WriteLine($"Parabéns {jogador1.Nome}! Você venceu com {jogador1.Gols} gol(s) e {jogador1.Pontuacao} ponto(s).");
                Console.WriteLine($"O seu adversário {jogador2.Nome} fez {jogador2.Gols} gol(s) e {jogador2.Pontuacao} ponto(s).");
            }
            else if (jogador1.Gols < jogador2.Gols)
            {
                Console.WriteLine($"Parabéns {jogador2.Nome}! Você venceu com {jogador2.Gols} gol(s) e {jogador2.Pontuacao} ponto(s).");
                Console.WriteLine($"O seu adversário {jogador1.Nome} fez {jogador1.Gols} gol(s) e {jogador1.Pontuacao} ponto(s).");
            }
            else
            {
                if (jogador1.Pontuacao > jogador2.Pontuacao)
                {
                    Console.WriteLine($"Parabéns {jogador1.Nome}! A partida empatou, mas você venceu pelo critério de pontuação.");
                    Console.WriteLine($"Você fez {jogador1.Gols} gol(s) e {jogador1.Pontuacao} ponto(s).");
                    Console.WriteLine($"O seu adversário {jogador2.Nome} fez {jogador2.Gols} gol(s) e {jogador2.Pontuacao} ponto(s).");
                }
                else if (jogador1.Pontuacao < jogador2.Pontuacao)
                {
                    Console.WriteLine($"Parabéns {jogador2.Nome}! A partida empatou, mas você venceu pelo critério de pontuação.");
                    Console.WriteLine($"Você fez {jogador2.Gols} gol(s) e {jogador2.Pontuacao} ponto(s).");
                    Console.WriteLine($"O seu adversário {jogador1.Nome} fez {jogador1.Gols} gol(s) e {jogador1.Pontuacao} ponto(s).");
                }
                else
                {
                    Console.WriteLine("A partida terminou em empate!");
                }
            }
        }

        private void ResetarPartida()
        {
            jogador1 = new Jogador { Nome = jogador1.Nome, Energias = 10 };
            jogador2 = new Jogador { Nome = jogador2.Nome, Energias = 10 };
            jogador1Vez = SortearJogadorInicial();

            Console.WriteLine("Nova partida iniciada!");
        }
    }
}
