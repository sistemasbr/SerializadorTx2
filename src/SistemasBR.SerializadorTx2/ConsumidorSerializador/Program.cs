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
                Teste = "Campo1111",
                Teste2 = 2,
                Nao = "Não é pra ir"
            };

            Console.WriteLine(SerializadorTx2.Serializar(obj));
            Console.ReadKey();
        }
    }
}
