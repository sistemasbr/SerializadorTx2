using System;
using System.Runtime;
using SistemasBR.SerializadorTx2;

namespace ConsumidorSerializador
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Mock
            {
                Teste = "aaa",
                Teste2 = 2,
                Nao = "Não é pra ir",
                Essevai = "12"
            };

            Console.WriteLine(SerializadorTx2.Serializar(obj));
            Console.ReadLine();
        }
    }
}
