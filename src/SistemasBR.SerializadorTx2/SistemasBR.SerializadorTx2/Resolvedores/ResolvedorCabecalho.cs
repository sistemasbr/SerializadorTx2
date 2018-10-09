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
        internal static class Cabecalho
        {
            internal static string RetornaValorCabecalho(MemberInfo tipo)
            {
                var atributosClasse = tipo.CustomAttributes as IList<CustomAttributeData> ??
                                      tipo.CustomAttributes.ToList();

                if (DeveDispararExceptionPorNaoConterAtributo(atributosClasse))
                    throw new CustomAttributeFormatException(
                        $"A classe deve conter o atributo \"{nameof(Tx2CabecalhoAttribute)}\" (Type: {typeof(Tx2CabecalhoAttribute)})");

                var atributoTx2CabecalhoClasse =
                    atributosClasse.FirstOrDefault(a => a.AttributeType == typeof(Tx2CabecalhoAttribute));

                if (atributoTx2CabecalhoClasse == null)
                    return tipo.Name;

                var argumentosConstrutorCabecalho = atributoTx2CabecalhoClasse.ConstructorArguments;

                var cabecalho = argumentosConstrutorCabecalho[0].Value.ToString();

                if (!string.IsNullOrWhiteSpace(cabecalho)) return cabecalho;

                if (!ComportamentoAtual.NomeDaClasseNoCabecalhoQuandoNaoInformadoOuVazio)
                    throw new ArgumentNullException(nameof(cabecalho), "O valor do cebeçalho não pode ser vazio.");

                cabecalho = tipo.Name;

                return cabecalho;
            }

            private static bool DeveDispararExceptionPorNaoConterAtributo(
                IEnumerable<CustomAttributeData> atributosClasse) =>
                !ComportamentoAtual.NomeDaClasseNoCabecalhoQuandoNaoInformadoOuVazio
                && atributosClasse.All(a => a.AttributeType != typeof(Tx2CabecalhoAttribute));
        }
    }
}
