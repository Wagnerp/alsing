namespace QI4N.Framework
{
    using System;

    using Proxy;

    public class DefaultObjectBuilder<T> : ObjectBuilder<T>
    {
        public T NewInstance()
        {
            var builder = new Proxy.Proxy();
            var instance = builder.NewProxyInstance<T>();
            return instance;
        }

        public T StateFor()
        {
            throw new NotImplementedException();
        }
    }
}