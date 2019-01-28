namespace SistemasBR.SerializadorTx2.Configuracao
{
    internal static class ComportamentoAtual
    {
        internal static bool NomeDaClasseNoCabecalhoQuandoNaoInformadoOuVazio { get; private set; }
        internal static bool SerializarPropriedadesSemAtributo { get; private set; }
        internal static bool NomeDaPropriedadeQuandoNomeCampoVazio { get; private set; }
        internal static bool NaoDispararExceptionPropriedadesObrigatoriasVazias { get; private set; }
        internal static bool NaoDispararExceptionPropriedadesMaioresPermitido { get; private set; }
        internal static bool NaoSerializarCamposNulosNaoObrigatorios { get; private set; }
        internal static bool NaoAdicionarCabecalhoRodapeQuandoVazio { get; private set; }

        internal static void AtualizarComportamentoGeral(ComportamentoFlags configuracoes)
        {
            NomeDaClasseNoCabecalhoQuandoNaoInformadoOuVazio =
                configuracoes.HasFlag(ComportamentoFlags.NomeDaClasseNoCabecalhoNaoInformadoOuVazio);

            SerializarPropriedadesSemAtributo =
                configuracoes.HasFlag(ComportamentoFlags.SerializarPropriedadesSemAtributo);

            NomeDaPropriedadeQuandoNomeCampoVazio =
                configuracoes.HasFlag(ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio);

            NaoDispararExceptionPropriedadesObrigatoriasVazias =
                configuracoes.HasFlag(ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias);

            NaoDispararExceptionPropriedadesMaioresPermitido =
                configuracoes.HasFlag(ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido);

            NaoSerializarCamposNulosNaoObrigatorios =
                configuracoes.HasFlag(ComportamentoFlags.NaoSerializarCamposNulosNaoObrigatorios);

            NaoAdicionarCabecalhoRodapeQuandoVazio =
                configuracoes.HasFlag(ComportamentoFlags.NaoAdicionarCabecalhoRodapeQuandoVazio);
        }

        internal static ComportamentoFlags DevolverComportamentoAtual()
        {
            ComportamentoFlags comportamentoAtual = 0;

            if (NomeDaClasseNoCabecalhoQuandoNaoInformadoOuVazio)
                comportamentoAtual = ComportamentoFlags.NomeDaClasseNoCabecalhoNaoInformadoOuVazio;

            if (SerializarPropriedadesSemAtributo)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.SerializarPropriedadesSemAtributo
                    : comportamentoAtual | ComportamentoFlags.SerializarPropriedadesSemAtributo;

            if (NomeDaPropriedadeQuandoNomeCampoVazio)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio
                    : comportamentoAtual | ComportamentoFlags.NomeDaPropriedadeQuandoNomeCampoVazio;

            if (NaoDispararExceptionPropriedadesObrigatoriasVazias)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias
                    : comportamentoAtual | ComportamentoFlags.NaoDispararExceptionPropriedadesObrigatoriasVazias;

            if (NaoDispararExceptionPropriedadesMaioresPermitido)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido
                    : comportamentoAtual | ComportamentoFlags.NaoDispararExceptionPropriedadesMaioresPermitido;

            if (NaoSerializarCamposNulosNaoObrigatorios)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.NaoSerializarCamposNulosNaoObrigatorios
                    : comportamentoAtual | ComportamentoFlags.NaoSerializarCamposNulosNaoObrigatorios;

            if (NaoAdicionarCabecalhoRodapeQuandoVazio)
                comportamentoAtual = comportamentoAtual == 0
                    ? ComportamentoFlags.NaoAdicionarCabecalhoRodapeQuandoVazio
                    : comportamentoAtual | ComportamentoFlags.NaoAdicionarCabecalhoRodapeQuandoVazio;

            return comportamentoAtual;
        }
    }
}
