using System;
using System.Collections.Generic;

namespace Codachin
{
    internal class OperationManager
    {
        //Represents which operations we support in our CLI system.
        private Dictionary<string, Action<IEnumerable<string>>> operations;

        public OperationManager()
        {
            operations = new Dictionary<string, Action<IEnumerable<string>>>();
            operations.Add("checkout", (arg) => Checkout(arg));
            operations.Add("log", (arg) => Log(arg));

        }

        internal void ExecuteOperation(string op, IEnumerable<string> args)
        {
            operations.GetValueOrDefault(op)(args);
        }
        internal bool HasOperation(string operation)
        {
            try
            {
                return operations[operation] != null;

            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException($"Operation {operation} do not exist.");
            }
        }

        private void Checkout(IEnumerable<string> arg)
        {
            throw new NotImplementedException();
        }

        private void Log(IEnumerable<string> arg)
        {
            throw new NotImplementedException();
        }
    }
}
