using SistemasBR.SerializadorTx2.Atributos;
using SistemasBR.SerializadorTx2.Configuracao;
using SistemasBR.SerializadorTx2.Resolvedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SistemasBR.SerializadorTx2
{
    public class SerializadorTx2
    {
        public static string Serializar(object objeto) =>
            Serializar(objeto, ConfiguracoesAtuais.DevolverConfiguracoesAtuais());

        public static string Serializar(object objeto, ComportamentoFlags comportamentoExpecifico)
        {
            var comportamentoAtual = ConfiguracoesAtuais.DevolverConfiguracoesAtuais();

            try
            {
                if (objeto == null) throw new ArgumentNullException(nameof(objeto));

                var tipo = objeto.GetType();

                var resposta = "";

                var cabecalho = ResolvedorCabecalho.PegarValorCabecalho(tipo);

                var propriedades = tipo.GetProperties();

                foreach (var propriedade in propriedades)
                {
                    var atributosProriedade = propriedade.CustomAttributes
                                                  as IList<CustomAttributeData> ?? propriedade.CustomAttributes.ToList();

                    if (atributosProriedade.All(a => a.AttributeType != typeof(Tx2CampoAttribute)))
                        continue;

                    var atributoTx2Propriedade = atributosProriedade
                        .First(a => a.AttributeType == typeof(Tx2CampoAttribute));

                    var argumentosConstrutorPropriedade = atributoTx2Propriedade.ConstructorArguments;

                    var nomeTx2 = argumentosConstrutorPropriedade[0].Value.ToString();

                    if (string.IsNullOrWhiteSpace(nomeTx2))
                        throw new ArgumentNullException(nameof(nomeTx2),
                            $"O nome correspondente da propriedade \"{propriedade.Name}\" não foi preenchido.");

                    var valorProriedade = propriedade.GetValue(objeto).ToString();

                    var preenchimentoObrigatorio = Convert.ToBoolean(argumentosConstrutorPropriedade[1].Value.ToString());

                    if (preenchimentoObrigatorio && string.IsNullOrWhiteSpace(valorProriedade))
                        throw new ArgumentNullException(nameof(propriedade),
                            $"A propriedade \"{propriedade.Name}\" tem preenchimento obrigatório.");

                    if (argumentosConstrutorPropriedade.Count >= 3)
                    {
                        var quantidadeLimite = Convert.ToInt32(argumentosConstrutorPropriedade[2].Value.ToString());

                        if (valorProriedade.Length > quantidadeLimite)
                            throw new ArgumentOutOfRangeException(nameof(propriedade), valorProriedade,
                                $"Quantidade de caracteres da propriedade \"{propriedade.Name}\" maior do que o permitido que é {quantidadeLimite}.");
                    }

                    resposta += $"{nomeTx2}={valorProriedade}\n";
                }

                return $"INCLUIR{cabecalho}\n{resposta}SALVAR{cabecalho}";
            }
            finally
            {
                ConfiguracoesAtuais.AtualizarConfiguracoesGerais(comportamentoAtual);
            }
        }
    }
}
