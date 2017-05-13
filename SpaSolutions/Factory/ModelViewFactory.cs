using System.Collections.Generic;
using SpaSolutions.ViewModel;

namespace SpaSolutions.Factory
{
    public sealed class ModelViewFactory<T> where T :  ViewModelBase, new()
    {
        static Dictionary<string, T> _instances = new Dictionary<string, T>();
        static object _lock = new object();

        private ModelViewFactory()
        {

        }

        public static T GetView(string name)
        {
            lock (_lock)
            {
                if (!_instances.ContainsKey(name)) _instances.Add(name, new T());
            }
            return _instances[name];
        }
    }
}
