using SistemasBR.SerializadorTx2.Atributos;
using SistemasBR.SerializadorTx2.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SistemasBR.SerializadorTx2.Resolvedores
{
    internal partial class Resolvedor
    {
        internal class Propriedades
        {
            internal static string RetornaValorPropriedades(object objeto, Type tipo)
            {
                var corpoTx2 = "";
                var propriedades = tipo.GetProperties();

                foreach (var propriedade in propriedades)
                {
                    var atributosProriedade = propriedade.CustomAttributes
                                                  as IList<CustomAttributeData> ??
                                              propriedade.CustomAttributes.ToList();

                    if (!ConfiguracoesAtuais.SerializarPropriedadesSemAtributo
                        && atributosProriedade.All(a => a.AttributeType != typeof(Tx2CampoAttribute)))
                        continue;

                    var atributoTx2Propriedade = atributosProriedade
                        .First(a => a.AttributeType == typeof(Tx2CampoAttribute));

                    corpoTx2 += CriaLinhaTx2(atributoTx2Propriedade,
                        new ResolvedorPropriedadesParametros(objeto, propriedade));
                }
                return corpoTx2;
            }

            private static string CriaLinhaTx2(
                CustomAttributeData atributoTx2Propriedade,
                ResolvedorPropriedadesParametros parametrosResolvedorPropriedades)
            {
                var argumentosConstrutorPropriedade = atributoTx2Propriedade.ConstructorArguments;

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
                var nomeTx2 = parametrosResolvedorPropriedades.ArgumentosConstrutorPropriedade[0].Value.ToString();

                if (!string.IsNullOrWhiteSpace(nomeTx2)) return nomeTx2;

                if (!ConfiguracoesAtuais.NomeDaPropriedadeQuandoNomeCampoVazio)
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

                var preenchimentoObrigatorio =
                    Convert
                        .ToBoolean(parametrosResolvedorPropriedades
                            .ArgumentosConstrutorPropriedade[1]
                            .Value
                            .ToString());

                if (!preenchimentoObrigatorio || !string.IsNullOrWhiteSpace(valorPropriedade)) return valorPropriedade;

                if (!ConfiguracoesAtuais.NaoDispararExceptionPropriedadesObrigatoriasVazias)
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
                !ConfiguracoesAtuais.NaoDispararExceptionPropriedadesMaioresPermitido;
        }
    }
}
