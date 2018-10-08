namespace SistemasBR.SerializadorTx2.Configuracao
{
    internal static class ConfiguracoesAtuais
    {
        internal static bool NomeDaClasseNoCabecalhoNaoInformadoOuVazio { get; private set; }
        internal static bool SerializarPropriedadesSemAtributo { get; private set; }
        internal static bool NomeDaPropriedadeQuandoNomeCampoVazio { get; private set; }
        internal static bool NaoDispararExceptionPropriedadesObrigatoriasVazias { get; private set; }
        internal static bool NaoDispararExceptionPropriedadesMaioresPermitido { get; private set; }

        internal static void AtualizarConfiguracoesGerais(ComportamentoFlags configuracoes)
        {
            NomeDaClasseNoCabecalhoNaoInformadoOuVazio =
                configuracoes.HasFlag(ComportamentoFlags.NomeDaClasseNoCabecalhoNaoInformadoOuVazio);

            SerializarPropriedadesSemAtributo =
                configuracoes.HasFlag(ComportamentoFlags.SerializarPropriedadesSemAtributo);

            NomeDaPropriedadeQuandoNomeCampoVazio =
                configuracoes.HasFlag(ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio);

            NaoDispararExceptionPropriedadesObrigatoriasVazias =
                configuracoes.HasFlag(ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias);

            NaoDispararExceptionPropriedadesMaioresPermitido =
                configuracoes.HasFlag(ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido);
        }
    }
}
