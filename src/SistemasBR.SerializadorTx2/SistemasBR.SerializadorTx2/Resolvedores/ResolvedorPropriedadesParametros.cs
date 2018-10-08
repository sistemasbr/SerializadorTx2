using System.Collections.Generic;
using System.Reflection;

namespace SistemasBR.SerializadorTx2.Resolvedores
{
    internal class ResolvedorPropriedadesParametros
    {
        public object Objeto { get; }

        public PropertyInfo Propriedade { get; }

        public IList<CustomAttributeTypedArgument> ArgumentosConstrutorPropriedade { get; }

        public ResolvedorPropriedadesParametros(
            object objeto,
            PropertyInfo propriedade)
        {
            Objeto = objeto;
            Propriedade = propriedade;
        }

        public ResolvedorPropriedadesParametros(
            IList<CustomAttributeTypedArgument> argumentosConstrutorPropriedade,
            PropertyInfo propriedade)
        {
            ArgumentosConstrutorPropriedade =
                argumentosConstrutorPropriedade ?? new List<CustomAttributeTypedArgument>(0);
            Propriedade = propriedade;
        }

        public ResolvedorPropriedadesParametros(
            object objeto,
            IList<CustomAttributeTypedArgument> argumentosConstrutorPropriedade,
            PropertyInfo propriedade)
        {
            Objeto = objeto;
            ArgumentosConstrutorPropriedade =
                argumentosConstrutorPropriedade ?? new List<CustomAttributeTypedArgument>(0);
            Propriedade = propriedade;
        }
    }
}
