namespace ChequePorExtenso.ConsoleApp;

static class Program
{

    static void Main(string[] args)
    {
        decimal n1 = 0.05m;
        decimal n2 = 2.25m;
        decimal n3 = 7.00m;
        decimal n4 = 37.00m;
        decimal n5 = 637.00m;
        decimal n6 = 1637.00m;
        decimal n7 = 15415.16m;
        decimal n8 = 61637.00m;
        decimal n9 = 961637.00m;
        decimal n10 = 1852700.00m;
        decimal n11 = 5961637.00m;
        decimal n12 = 25961637.00m;
        decimal n13 = 425961637.00m;
        decimal n14 = 8425961637.00m;
        decimal n15 = 0.00m;
        decimal n16 = 0.01m;
        decimal n17 = 1.00m;
        decimal n18 = 1.01m;
        decimal n19 = 10.00m;
        decimal n20 = 11.00m;
        decimal n21 = 19.00m;
        decimal n22 = 20.00m;
        decimal n23 = 21.00m;
        decimal n24 = 100.00m;
        decimal n25 = 101.00m;
        decimal n26 = 110.00m;
        decimal n27 = 115.00m;
        decimal n28 = 999.99m;
        decimal n29 = 1000.00m;
        decimal n30 = 1001.00m;
        decimal n31 = 1100.00m;
        decimal n32 = 1000000.00m;
        decimal n33 = 1000001.00m;

        Console.WriteLine(ConverterExtenso(n1));
        Console.WriteLine(ConverterExtenso(n2));
        Console.WriteLine(ConverterExtenso(n3));
        Console.WriteLine(ConverterExtenso(n4));
        Console.WriteLine(ConverterExtenso(n5));
        Console.WriteLine(ConverterExtenso(n6));
        Console.WriteLine(ConverterExtenso(n7));
        Console.WriteLine(ConverterExtenso(n8));
        Console.WriteLine(ConverterExtenso(n9));
        Console.WriteLine(ConverterExtenso(n10));
        Console.WriteLine(ConverterExtenso(n11));
        Console.WriteLine(ConverterExtenso(n12));
        Console.WriteLine(ConverterExtenso(n13));
        Console.WriteLine(ConverterExtenso(n14));
        Console.WriteLine(ConverterExtenso(n15));
        Console.WriteLine(ConverterExtenso(n16));
        Console.WriteLine(ConverterExtenso(n17));
        Console.WriteLine(ConverterExtenso(n18));
        Console.WriteLine(ConverterExtenso(n19));
        Console.WriteLine(ConverterExtenso(n20));
        Console.WriteLine(ConverterExtenso(n21));
        Console.WriteLine(ConverterExtenso(n22));
        Console.WriteLine(ConverterExtenso(n23));
        Console.WriteLine(ConverterExtenso(n24));
        Console.WriteLine(ConverterExtenso(n25));
        Console.WriteLine(ConverterExtenso(n26));
        Console.WriteLine(ConverterExtenso(n27));
        Console.WriteLine(ConverterExtenso(n28));
        Console.WriteLine(ConverterExtenso(n29));
        Console.WriteLine(ConverterExtenso(n30));
        Console.WriteLine(ConverterExtenso(n31));
        Console.WriteLine(ConverterExtenso(n32));
        Console.WriteLine(ConverterExtenso(n33));
        Console.ReadLine();
    }
    static string ConverterExtenso(decimal numero)
    {
        if (numero == 0.00m)
            return "pobre";

        decimal parteInteira = Math.Truncate(numero);
        decimal reais = parteInteira;
        int centavos = Convert.ToInt32((numero - parteInteira) * 100);

        List<string> resultado = [];

        foreach ((decimal escala, NomeEscala palavra) in escalas)
        {
            decimal quantidade = Math.Truncate(reais / escala);

            if (quantidade < 1)
                continue;

            if (escala != 1_000)
            {
                resultado.Add($"{ConverterGrupo((int)quantidade)} {(quantidade > 1 ? palavra.Plural : palavra.Singular)}");
            }
            else
            {
                string textoMilhares = quantidade == 1 ? palavra.Singular : $"{ConverterGrupo((int)quantidade)} {palavra.Plural}";
                resultado.Add(textoMilhares);
            }

            reais %= escala;
        }

        if (reais > 0)
        {
            bool terminaEmCentenaRedonda = reais % 100 == 0;
            string conjuncao = (resultado.Count > 0 && terminaEmCentenaRedonda) ? "e " : string.Empty;
            resultado.Add($"{conjuncao}{ConverterGrupo((int)reais)}");
        }

        if (centavos > 0)
        {
            bool temParteEmReais = parteInteira > 0;
            string realPalavra = parteInteira > 1 ? "reais e " : "real e ";

            string sufixoReais = temParteEmReais ? realPalavra : string.Empty;

            string sufixoCentavos = centavos > 1 ? "centavos" : "centavo";

            string sufixoMoeda = temParteEmReais ? string.Empty : " de real";

            resultado.Add($"{sufixoReais}{ConverterDezena(centavos)} {sufixoCentavos}{sufixoMoeda}");
        }
        else
            resultado.Add(parteInteira > 1 ? "reais" : "real");

        return string.Join(" ", resultado);
    }

    static string ConverterUnidade(int numero)
    {
        return unidades[numero];
    }

    static string ConverterDezena(int numero)
    {
        if (numero >= 11 && numero <= 19)
            return excecoes[numero];

        string dezena = dezenas[numero / 10];
        string unidade = ConverterUnidade(numero % 10);

        if (string.IsNullOrEmpty(dezena))
            return unidade;

        if (string.IsNullOrEmpty(unidade))
            return dezena;

        return $"{dezena} e {unidade}";
    }

    static string ConverterGrupo(int numero)
    {
        if (numero == 100)
            return excecoes[numero];

        string centena = centenas[numero / 100];
        string dezena = ConverterDezena(numero % 100);

        if (string.IsNullOrEmpty(centena))
            return dezena;

        if (string.IsNullOrEmpty(dezena))
            return centena;

        return $"{centena} e {dezena}";
    }

    static readonly Dictionary<int, string> excecoes = new()
    {
        { 11, "onze" },
        { 12, "doze" },
        { 13, "treze" },
        { 14, "quatorze" },
        { 15, "quinze" },
        { 16, "dezesseis" },
        { 17, "dezessete" },
        { 18, "dezoito" },
        { 19, "dezenove" },
        { 100, "cem" }
    };

    static readonly Dictionary<int, string> unidades = new()
    {
        { 0, string.Empty },
        { 1, "um" },
        { 2, "dois" },
        { 3, "três" },
        { 4, "quatro" },
        { 5, "cinco" },
        { 6, "seis" },
        { 7, "sete" },
        { 8, "oito" },
        { 9, "nove" },
    };

    static readonly Dictionary<int, string> dezenas = new()
    {
        { 0, string.Empty },
        { 1, "dez" },
        { 2, "vinte" },
        { 3, "trinta" },
        { 4, "quarenta" },
        { 5, "cinquenta" },
        { 6, "sessenta" },
        { 7, "setenta" },
        { 8, "oitenta" },
        { 9, "noventa" },
    };

    static readonly Dictionary<int, string> centenas = new()
    {
        { 0, string.Empty },
        { 1, "cento" },
        { 2, "duzentos" },
        { 3, "trezentos" },
        { 4, "quatrocentos" },
        { 5, "quinhentos" },
        { 6, "seiscentos" },
        { 7, "setecentos" },
        { 8, "oitocentos" },
        { 9, "novecentos" },
    };

    static readonly Escala[] escalas =
    [
        new(1_000_000_000_000_000, new NomeEscala("quadrilhão", "quadrilhões")),
        new(1_000_000_000_000, new NomeEscala("trilhão", "trilhões")),
        new(1_000_000_000, new NomeEscala("bilhão", "bilhões")),
        new(1_000_000, new NomeEscala("milhão", "milhões")),
        new(1_000, new NomeEscala("mil", "mil"))
    ];
}
record NomeEscala(string Singular, string Plural);
record Escala(decimal Valor, NomeEscala Palavra);