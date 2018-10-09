namespace SistemasBR.SerializadorTx2.Configuracao
{
    public static class SerializadorTx2Configuracao
    {
        public static void ConfigurarComportamento(ComportamentoFlags comportamentos) =>
            ComportamentoAtual.AtualizarComportamentoGeral(comportamentos);
    }
}
