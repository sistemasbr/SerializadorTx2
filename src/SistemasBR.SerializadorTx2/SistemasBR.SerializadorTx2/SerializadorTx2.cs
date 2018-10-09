using SistemasBR.SerializadorTx2.Configuracao;
using SistemasBR.SerializadorTx2.Resolvedores;
using System;

namespace SistemasBR.SerializadorTx2
{
    public class SerializadorTx2
    {
        public static string Serializar(object objeto) =>
            Serializar(objeto, ComportamentoAtual.DevolverComportamentoAtual());

        public static string Serializar(object objeto, ComportamentoFlags comportamentoExpecifico)
        {
            var comportamentoAtual = ComportamentoAtual.DevolverComportamentoAtual();

            ComportamentoAtual.AtualizarComportamentoGeral(comportamentoExpecifico);

            try
            {
                if (objeto == null) throw new ArgumentNullException(nameof(objeto));

                var tipo = objeto.GetType();

                var cabecalhoTx2 = Resolvedor.Cabecalho.RetornaValorCabecalho(tipo);

                var corpoTx2 = Resolvedor.Propriedades.RetornaValorPropriedades(objeto, tipo);

                return $"INCLUIR{cabecalhoTx2}\n{corpoTx2}SALVAR{cabecalhoTx2}";
            }
            finally
            {
                ComportamentoAtual.AtualizarComportamentoGeral(comportamentoAtual);
            }
        }
    }
}
