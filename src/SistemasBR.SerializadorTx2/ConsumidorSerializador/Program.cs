using SistemasBR.SerializadorTx2;
using SistemasBR.SerializadorTx2.Configuracao;
using System;

namespace ConsumidorSerializador
{
    public static class Program
    {
        private static void Main()
        {
            var obj = new Mock
            {
                Teste = string.Empty,
                Teste2 = 2,
                Nao = "Não é pra ir",
                Essevai = "123"
            };

            var objSemCabecalho = new MockSemCabecalho
            {
                Teste = string.Empty,
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
                ComportamentoFlags.SerializarPropriedadesSemAtributo |
                ComportamentoFlags.NaoAdicionarCabecalhoRodapeQuandoVazio);

            Console.WriteLine(SerializadorTx2.Serializar(obj));
            Console.WriteLine(SerializadorTx2.Serializar(objSemCabecalho));
            Console.ReadLine();
        }
    }
}
