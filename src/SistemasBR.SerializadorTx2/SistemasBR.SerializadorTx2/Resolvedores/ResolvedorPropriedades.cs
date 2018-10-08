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
            internal static string RetornaPropriedades(object objeto, Type tipo)
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

                    corpoTx2 += CriaLinhaTx2(objeto, atributoTx2Propriedade, propriedade);
                }
                return corpoTx2;
            }

            private static string CriaLinhaTx2(
                object objeto,
                CustomAttributeData atributoTx2Propriedade,
                PropertyInfo propriedade)
            {
                var argumentosConstrutorPropriedade = atributoTx2Propriedade.ConstructorArguments;

                var nomeTx2 = CapturaNomeTx2(argumentosConstrutorPropriedade, propriedade);

                var valorPropriedade = PreencheValorPropriedade(objeto, propriedade, argumentosConstrutorPropriedade);

                VerificaQuantidadeLimite(argumentosConstrutorPropriedade, valorPropriedade, propriedade);

                return $"{nomeTx2}={valorPropriedade}\n";
            }

            private static string CapturaNomeTx2(
                IList<CustomAttributeTypedArgument> argumentosConstrutorPropriedade,
                MemberInfo propriedade)
            {
                var nomeTx2 = argumentosConstrutorPropriedade[0].Value.ToString();

                if (!string.IsNullOrWhiteSpace(nomeTx2)) return nomeTx2;

                if (!ConfiguracoesAtuais.NomeDaPropriedadeQuandoNomeCampoVazio)
                    throw new ArgumentNullException(nameof(nomeTx2),
                        $"O nome correspondente da propriedade \"{propriedade.Name}\" não foi preenchido.");

                return propriedade.Name;
            }

            private static string PreencheValorPropriedade(
                object objeto,
                PropertyInfo propriedade,
                IList<CustomAttributeTypedArgument> argumentosConstrutorPropriedade)
            {
                var valorPropriedade = propriedade.GetValue(objeto).ToString();

                var preenchimentoObrigatorio =
                    Convert.ToBoolean(argumentosConstrutorPropriedade[1].Value.ToString());

                if (!preenchimentoObrigatorio || !string.IsNullOrWhiteSpace(valorPropriedade)) return valorPropriedade;

                if (!ConfiguracoesAtuais.NaoDispararExceptionPropriedadesObrigatoriasVazias)
                    throw new ArgumentNullException(nameof(propriedade),
                        $"A propriedade \"{propriedade.Name}\" tem preenchimento obrigatório.");

                return "";
            }

            private static void VerificaQuantidadeLimite(
                IList<CustomAttributeTypedArgument> argumentosConstrutorPropriedade,
                string valorPropriedade,
                PropertyInfo propriedade)
            {
                if (argumentosConstrutorPropriedade.Count < 3) return;

                var quantidadeLimite = Convert.ToInt32(argumentosConstrutorPropriedade[2].Value.ToString());

                if (ExceptionLimiteExcedido(valorPropriedade, quantidadeLimite))
                    throw new ArgumentOutOfRangeException(nameof(propriedade), valorPropriedade,
                        $"Quantidade de caracteres da propriedade \"{propriedade.Name}\" maior do que o permitido que é {quantidadeLimite}.");
            }

            private static bool ExceptionLimiteExcedido(string valorPropriedade, int quantidadeLimite) =>
                valorPropriedade.Length > quantidadeLimite &&
                !ConfiguracoesAtuais.NaoDispararExceptionPropriedadesMaioresPermitido;
        }
    }
}
