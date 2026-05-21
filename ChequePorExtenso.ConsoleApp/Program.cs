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
        Console.ReadLine();
    }
    static string ConverterExtenso(decimal numero)
    {
        decimal parteInteira = Math.Truncate(numero);
        decimal reais = parteInteira;
        int centavos = Convert.ToInt32((numero - parteInteira) * 100);

        string resultado = "";

        decimal mil = 1_000;
        decimal milhao = 1_000_000;
        decimal bilhao = 1_000_000_000;
        decimal trilhao = 1_000_000_000_000;
        decimal quadrilhao = 1_000_000_000_000_000;

        if (reais / quadrilhao >= 1)
        {
            decimal quantidade = Math.Truncate(reais / quadrilhao);
            resultado += $"{ConverterCentena((int)quantidade)} {(quantidade > 1 ? "quadrilhões" : "quadrilhão")} ";
            reais %= quadrilhao;
        }

        if (reais / trilhao >= 1)
        {
            decimal quantidade = Math.Truncate(reais / trilhao);
            resultado += $"{ConverterCentena((int)quantidade)} {(quantidade > 1 ? "trilhões" : "trilhão")} ";
            reais %= trilhao;
        }

        if (reais / bilhao >= 1)
        {
            decimal quantidade = Math.Truncate(reais / bilhao);
            resultado += $"{ConverterCentena((int)quantidade)} {(quantidade > 1 ? "bilhões" : "bilhão")} ";
            reais %= bilhao;
        }

        if (reais / milhao >= 1)
        {
            decimal quantidade = Math.Truncate(reais / milhao);
            resultado += $"{ConverterCentena((int)quantidade)} {(quantidade > 1 ? "milhões" : "milhão")} ";
            reais %= milhao;
        }

        if (reais / mil >= 1)
        {
            decimal quantidade = Math.Truncate(reais / mil);
            string textoMilhares = quantidade == 1 ? "mil" : $"{ConverterCentena((int)quantidade)} mil";
            resultado += textoMilhares + " ";
            reais %= mil;
        }

        if (reais > 0)
        {
            bool terminaEmCentenaRedonda = reais % 100 == 0;
            string conjuncao = (!string.IsNullOrEmpty(resultado) && terminaEmCentenaRedonda) ? "e " : string.Empty;
            resultado += $"{conjuncao}{ConverterCentena((int)reais)} ";
        }

        if (centavos > 0)
        {
            bool temParteEmReais = parteInteira > 0;
            bool reaisNoPlural = parteInteira > 1;

            string sufixoReais = temParteEmReais ? (reaisNoPlural ? "reais e " : "real e ") : string.Empty;

            string sufixoCentavos = centavos > 1 ? "centavos" : "centavo";

            string sufixoMoeda = temParteEmReais ? string.Empty : " de real";

            resultado += $"{sufixoReais}{ConverterDezena(centavos)} {sufixoCentavos}{sufixoMoeda}";
        }
        else
            resultado += parteInteira > 1 ? "reais" : "real";

        return resultado.Trim();
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

    static string ConverterCentena(int numero)
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
        { 3, "tres" },
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
}
