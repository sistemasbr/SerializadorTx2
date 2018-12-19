using System;

namespace SistemasBR.SerializadorTx2.Configuracao
{
    [Flags]
    public enum ComportamentoFlags
    {
        NomeDaClasseNoCabecalhoNaoInformadoOuVazio = 1,
        SerializarPropriedadesSemAtributo = 2,
        NomeDaPropriedadeQuandoNomeCampoVazio = 4,
        NaoDispararExceptionPropriedadesObrigatoriasVazias = 8,
        NaoDispararExceptionPropriedadesMaioresPermitido = 16,
        NaoSerializarCamposNulosNaoObrigatorios = 32
    }
}