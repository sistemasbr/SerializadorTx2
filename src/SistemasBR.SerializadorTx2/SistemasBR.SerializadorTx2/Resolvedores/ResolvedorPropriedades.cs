using SistemasBR.SerializadorTx2.Atributos;
using SistemasBR.SerializadorTx2.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SistemasBR.SerializadorTx2.Resolvedores
{
    internal partial class Resolvedor
    {
        internal static class Propriedades
        {
            internal static string RetornaValorPropriedades(object objeto, Type tipo)
            {
                var corpoTx2 = new StringBuilder();
                var propriedades = tipo.GetProperties();

                foreach (var propriedade in propriedades)
                {
                    var atributosProriedade = propriedade.CustomAttributes
                                                  as IList<CustomAttributeData> ??
                                              propriedade.CustomAttributes.ToList();

                    if (!ComportamentoAtual.SerializarPropriedadesSemAtributo
                        && atributosProriedade.All(a => a.AttributeType != typeof(Tx2CampoAttribute)))
                        continue;

                    var atributoTx2Propriedade = atributosProriedade
                        .FirstOrDefault(a => a.AttributeType == typeof(Tx2CampoAttribute));

                    corpoTx2.Append(CriaLinhaTx2(atributoTx2Propriedade,
                        new ResolvedorPropriedadesParametros(objeto, propriedade)));
                }
                return corpoTx2.ToString();
            }

            private static string CriaLinhaTx2(
                CustomAttributeData atributoTx2Propriedade,
                ResolvedorPropriedadesParametros parametrosResolvedorPropriedades)
            {
                var argumentosConstrutorPropriedade = atributoTx2Propriedade?.ConstructorArguments;

                var nomeTx2 = CapturaNomeTx2(
                    new ResolvedorPropriedadesParametros(argumentosConstrutorPropriedade,
                        parametrosResolvedorPropriedades.Propriedade));

                var valorPropriedade = PreencheValorPropriedade(
                    new ResolvedorPropriedadesParametros(
                        parametrosResolvedorPropriedades.Objeto,
                        argumentosConstrutorPropriedade,
                        parametrosResolvedorPropriedades.Propriedade));

                VerificaQuantidadeLimite(valorPropriedade,
                    new ResolvedorPropriedadesParametros(argumentosConstrutorPropriedade,
                        parametrosResolvedorPropriedades.Propriedade));

                return $"{nomeTx2}={valorPropriedade}\n";
            }

            private static string CapturaNomeTx2(
                ResolvedorPropriedadesParametros parametrosResolvedorPropriedades)
            {
                if (parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade.Count == 0)
                    return parametrosResolvedorPropriedades.Propriedade.Name;

                var nomeTx2 = parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade[0].Value.ToString();

                if (!string.IsNullOrWhiteSpace(nomeTx2)) return nomeTx2;

                if (!ComportamentoAtual.NomeDaPropriedadeQuandoNomeCampoVazio)
                    throw new ArgumentNullException(nameof(nomeTx2),
                        $"O nome correspondente da propriedade \"{parametrosResolvedorPropriedades.Propriedade.Name}\" não foi preenchido.");

                return parametrosResolvedorPropriedades.Propriedade.Name;
            }

            private static string PreencheValorPropriedade(
                ResolvedorPropriedadesParametros parametrosResolvedorPropriedades)
            {
                var valorPropriedade = parametrosResolvedorPropriedades
                    .Propriedade
                    .GetValue(parametrosResolvedorPropriedades.Objeto).ToString();

                var preenchimentoObrigatorio = false;
                if (parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade.Count > 0)
                    preenchimentoObrigatorio = Convert
                        .ToBoolean(parametrosResolvedorPropriedades
                            .ArgumentosConstrutorPropriedade[1]
                            .Value
                            .ToString());

                if (!preenchimentoObrigatorio || !string.IsNullOrWhiteSpace(valorPropriedade)) return valorPropriedade;

                if (!ComportamentoAtual.NaoDispararExceptionPropriedadesObrigatoriasVazias)
                    throw new ArgumentNullException(nameof(parametrosResolvedorPropriedades.Propriedade),
                        $"A propriedade \"{parametrosResolvedorPropriedades.Propriedade.Name}\" tem preenchimento obrigatório.");

                return "";
            }

            private static void VerificaQuantidadeLimite(
                string valorPropriedade,
                ResolvedorPropriedadesParametros parametrosResolvedorPropriedades)
            {
                if (parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade.Count < 3) return;

                var quantidadeLimite = Convert.ToInt32(
                    parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade[2].Value.ToString());

                if (ExceptionLimiteExcedido(valorPropriedade, quantidadeLimite))
                    throw new ArgumentOutOfRangeException(nameof(parametrosResolvedorPropriedades.Propriedade),
                        valorPropriedade,
                        $"Quantidade de caracteres da propriedade \"{parametrosResolvedorPropriedades.Propriedade.Name}\" maior do que o permitido que é {quantidadeLimite}.");
            }

            private static bool ExceptionLimiteExcedido(string valorPropriedade, int quantidadeLimite) =>
                valorPropriedade.Length > quantidadeLimite &&
                !ComportamentoAtual.NaoDispararExceptionPropriedadesMaioresPermitido;
        }
    }
}
