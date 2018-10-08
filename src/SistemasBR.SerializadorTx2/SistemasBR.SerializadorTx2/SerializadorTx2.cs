using SistemasBR.SerializadorTx2.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SistemasBR.SerializadorTx2
{
    public class SerializadorTx2
    {
        public static string Serializar(object objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));

            var tipo = objeto.GetType();

            var resposta = "";

            var cabecalho = "";

            var atributosClasse = tipo.CustomAttributes as IList<CustomAttributeData> ?? tipo.CustomAttributes.ToList();

            if (atributosClasse.All(a => a.AttributeType != typeof(Tx2CabecalhoAttribute)))
                throw new CustomAttributeFormatException(
                    $"A classe deve conter o atributo \"{nameof(Tx2CabecalhoAttribute)}\" (Type: {typeof(Tx2CabecalhoAttribute)})");

            var atributoTx2CabecalhoClasse = atributosClasse.First(a => a.AttributeType == typeof(Tx2CabecalhoAttribute));

            foreach (var argumentosConstrutor in atributoTx2CabecalhoClasse.ConstructorArguments)
                cabecalho = argumentosConstrutor.Value.ToString();

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
                    throw new ArgumentNullException(nameof(nomeTx2));

                var valorProriedade = propriedade.GetValue(objeto).ToString();

                var preenchimentoObrigatorio = Convert.ToBoolean(argumentosConstrutorPropriedade[1].Value.ToString());

                if (preenchimentoObrigatorio && string.IsNullOrWhiteSpace(valorProriedade.ToString()))
                    throw new ArgumentNullException(nameof(propriedade));

                if (argumentosConstrutorPropriedade.Count >= 3)
                {
                    var quantidadeLimite = Convert.ToInt32(argumentosConstrutorPropriedade[2].Value.ToString());

                    if (valorProriedade.Length > quantidadeLimite)
                        throw new ArgumentOutOfRangeException(nameof(propriedade), valorProriedade,
                            "Valor maior do que o permitido");
                }

                resposta += $"{nomeTx2}={valorProriedade}\n";
            }

            return $"INCLUIR{cabecalho}\n{resposta}SALVAR{cabecalho}";
        }
    }
}
