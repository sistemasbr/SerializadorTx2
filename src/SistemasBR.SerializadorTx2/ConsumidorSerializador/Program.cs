using SistemasBR.SerializadorTx2;
using SistemasBR.SerializadorTx2.Configuracao;
using System;

namespace ConsumidorSerializador
{
    internal class Program
    {
        private static void Main()
        {
            var obj = new Mock
            {
                Teste = "",
                Teste2 = 2,
                Nao = "Não é pra ir",
                Essevai = "123"
            };

            SerializadorTx2Configuracao.ConfigurarComportamento(
                ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido |
                ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias |
                ComportamentoFlags.NaoSerializarCamposNulosNaoObrigatorios |
                ComportamentoFlags.NomeDaClasseNoCabecalhoNaoInformadoOuVazio |
                ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio |
                ComportamentoFlags.SerializarPropriedadesSemAtributo);

            Console.WriteLine(SerializadorTx2.Serializar(obj));
            Console.ReadLine();
        }
    }
}
