﻿using System;
using System.Runtime;
using SistemasBR.SerializadorTx2;
using SistemasBR.SerializadorTx2.Configuracao;

namespace ConsumidorSerializador
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Mock
            {
                Teste = "",
                Teste2 = 2,
                Nao = "Não é pra ir",
                Essevai = "123"
            };

            Console.WriteLine(SerializadorTx2.Serializar(obj,
                ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias |
                ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido |
                ComportamentoFlags.NomeDaClasseNoCabecalhoNaoInformadoOuVazio |
                ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio |
                ComportamentoFlags.SerializarPropriedadesSemAtributo));
            Console.ReadKey();
            Console.WriteLine(SerializadorTx2.Serializar(obj));
            Console.ReadLine();
        }
    }
}
