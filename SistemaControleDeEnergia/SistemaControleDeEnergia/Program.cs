using System;
class Program
{
    struct Eletro
    {
        public string nome;
        public double potencia;
        public double tempoMedioUso;
    }
    static void addEletro(List<Eletro> lista)
    {
        Eletro novoEletrodomestico = new Eletro();
        Console.Write("Nome do Eletrodoméstico: ");
        novoEletrodomestico.nome = Console.ReadLine();
        Console.Write("Potência (em kW): ");
        novoEletrodomestico.potencia = Convert.ToDouble(Console.ReadLine());
        Console.Write("Tempo Médio Ativo por Dia (em horas): ");
        novoEletrodomestico.tempoMedioUso = Convert.ToDouble(Console.ReadLine());

        lista.Add(novoEletrodomestico);
        Console.WriteLine("Eletrodoméstico adicionado com sucesso!");
    }

    static void listarEletros(List<Eletro> lista)
    {
        Console.WriteLine("***Lista de Eletrodomésticos***");
        foreach (Eletro eletro in lista)
        {
            Console.WriteLine($"Nome: {eletro.nome}");
            Console.WriteLine($"Potência: {eletro.potencia} kW");
            Console.WriteLine($"Tempo Médio Ativo por Dia: {eletro.tempoMedioUso} horas");
            Console.WriteLine();
        }
    }

    static void buscarPorConsumo(List<Eletro> lista, double consumo)
    {
        foreach(Eletro eletro in lista)
        {
            if (consumo < eletro.potencia)
            {
                Console.WriteLine($"***Eletrodomésticos com potência maior que {consumo}:***");
                Console.WriteLine($"Nome: {eletro.nome}");
                Console.WriteLine($"Potência: {eletro.potencia} kW");
                Console.WriteLine($"Tempo Médio Ativo por Dia: {eletro.tempoMedioUso} horas");
                Console.WriteLine();
            }
        }
    }


    static void buscarPorNome(List<Eletro> vetorEletros, string nomeBusca)
    {
        foreach(Eletro eletro in vetorEletros)
        {
            if (eletro.nome.ToUpper().Equals(nomeBusca.ToUpper()))
            {
                Console.WriteLine("***Dados do Eletrodoméstico***");
                Console.WriteLine($"Nome: {eletro.nome}");
                Console.WriteLine($"Potência: {eletro.potencia} kW");
                Console.WriteLine($"Tempo Médio Ativo por Dia: {eletro.tempoMedioUso} horas");
                Console.WriteLine();
            }
        }


    }
    static void calcularCustoEletro(List<Eletro> vetorEletros)
    {
        double somaDia = 0,somaDiaKw = 0, somaMesKw = 0, somaMes = 0, valorKw;
        Console.Write("Valor do Kw em R$:");
        valorKw = Convert.ToDouble(Console.ReadLine());
        foreach (Eletro eletro in vetorEletros)
        {
            somaDia += (eletro.potencia * eletro.tempoMedioUso * valorKw);
            somaDiaKw += (eletro.potencia * eletro.tempoMedioUso);
            somaMes += (eletro.potencia * eletro.tempoMedioUso * valorKw) * 30;
            somaMesKw += (eletro.potencia * eletro.tempoMedioUso)*30;
        }
        Console.WriteLine("***Consumo diário/mensal da casa***");
        Console.WriteLine($"Consumo diário em Kw: {somaDiaKw}");
        Console.WriteLine($"Consumo diário em R$: R${somaDia}");
        Console.WriteLine($"Consumo Mensal em Kw: R${somaMesKw}");
        Console.WriteLine($"Consumo Mensal em R$: R${somaMes}");
        Console.WriteLine();
    }

    static void salvarDados(List<Eletro> lista, string nomeArquivo)
    {
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (var eletro in lista)
            {
                writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempoMedioUso}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void carregarDados(List<Eletro> lista, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');
                Eletro eletro = new Eletro
                {
                    nome = campos[0],
                    potencia = double.Parse(campos[1]),
                    tempoMedioUso = double.Parse(campos[2])
                };
                lista.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static int menu()
    {
        int op;
        Console.WriteLine("*** Sistema de Controle de Energia C# ***");
        Console.WriteLine("1-Cadastrar");
        Console.WriteLine("2-Listar");
        Console.WriteLine("3-Buscar pelo nome");
        Console.WriteLine("4-Buscar por consumo");
        Console.WriteLine("5-Calcular custo");
        Console.WriteLine("0-Sair");
        Console.Write("Escolha uma opção:");
        op = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        return op;
    }// fim funcao menu

    static void buscaNome()
    {

    }
    static void Main()
    {
        List<Eletro> vetorEletros = new List<Eletro>();
        carregarDados(vetorEletros, "dadosEletro.txt");
        int op = 0;
        do
        {
            op = menu();
            switch (op)
            {
                case 1:
                    addEletro(vetorEletros);
                    break;
                case 2:
                    listarEletros(vetorEletros);
                    break;
                case 3:
                    Console.WriteLine("Digite o nome do eletrodoméstico que deseja buscar: ");
                    string nomeEletro = Console.ReadLine();
                    buscarPorNome(vetorEletros, nomeEletro);
                    break;
                case 4:
                    Console.WriteLine("Digite o valor em Kw para filtrar eletrodomésticos que consomem mais");
                    double consumo = Convert.ToDouble(Console.ReadLine());
                    buscarPorConsumo(vetorEletros, consumo);
                    break;
                case 5:
                    calcularCustoEletro(vetorEletros);
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    salvarDados(vetorEletros, "dadosEletro.txt");
                    break;
            }// fim switch
            Console.ReadKey(); // pausa
            Console.Clear();

        } while (op != 0);

    }


}