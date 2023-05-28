using System;
using Nada.NZazu.Contracts;

namespace Nada.NZazu
{
    public interface INZazuWpfFieldFactory
    {
        INZazuWpfField CreateField(FieldDefinition fieldDefinition, int rowIdx = -1);

        T Resolve<T>(Type x = null);
        void Use<T>(T service);
    }
}